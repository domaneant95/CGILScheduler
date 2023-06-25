using Application.Activities;
using Domain;
using Domain.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NPOI.HPSF;
using NPOI.POIFS.Properties;
using Org.BouncyCastle.Math.EC.Rfc7748;
using Persistence;
using System.Linq;

namespace Application.Filemanager
{
    public class DbFileProvider
    {
        public DbFileProvider(DataContext fileManagementDbContext)
        {
            FileManagementDbContext = fileManagementDbContext;
        }

        DataContext FileManagementDbContext { get; }
        private SemaphoreSlim SemaphoreSlim { get; set; } = new SemaphoreSlim(1);

        public object Process(FileSystem itemInfo)
        {
            var objRes = new object();
            try
            {
                SemaphoreSlim.Wait();

                var clientItemList = new List<Iteminfo>();
                var args = itemInfo.args;

                switch (itemInfo.command)
                {
                    case "getItems":
                        {
                            string pathKeys = args.itemInfo.key;

                            var parentId = ParseKey(pathKeys);
                            var fileItems = GetDirectoryContents(parentId);
                            var hasSubDirectoriesInfo = GetHasSubDirectoriesInfo(fileItems);

                            foreach (var item in fileItems)
                            {
                                var clientItem = new Iteminfo
                                {
                                    key = item.Id.ToString(),
                                    name = item.Name,
                                    isDirectory = item.Type == FileType.Folder,
                                    dateModified = item.Modified
                                };

                                if (item.Type == FileType.Folder)
                                {
                                    clientItem.hasSubdirectories = hasSubDirectoriesInfo.ContainsKey(item.Id) && hasSubDirectoriesInfo[item.Id];
                                }

                                clientItem.modifiedBy = item.ModifiedBy.Email;
                                clientItem.created = item.Created;
                                clientItemList.Add(clientItem);
                            }

                            objRes = clientItemList;
                            break;
                        }

                    case "createDirectory":
                        {
                            Console.WriteLine(args);
                            CreateDirectory(itemInfo);
                            #region comments
                            //var parentId = ParseKey(args.itemInfo.key);
                            //FileItem? parentItem = FileManagementDbContext.FileItems
                            //    .Include(x => x.Parent)
                            //    .Include(x => x.ModifiedBy)
                            //    .FirstOrDefault(x => x.Id == parentId);

                            //var newDir = new FileItem()
                            //{
                            //    Key = Guid.NewGuid(),
                            //    Size = 0,
                            //    Created = DateTime.Now,
                            //    Modified = DateTime.Now,
                            //    Extension = "",
                            //    File = null,
                            //    FileItems = new HashSet<FileItem>(),
                            //    Name = args.name,
                            //    Type = FileType.Folder,
                            //    Deals = new HashSet<Deal>(),
                            //    ModifiedBy = FileManagementDbContext.Users.FirstOrDefault(),
                            //    Parent = parentItem,
                            //    Path = null,
                            //};

                            //objRes = new Iteminfo()
                            //{
                            //    key = newDir.Id.ToString(),
                            //    name = newDir.Name,
                            //    isDirectory = newDir.Type == FileType.Folder,
                            //    dateModified = newDir.Modified,
                            //    hasSubdirectories = newDir.Parent != null,
                            //    modifiedBy = newDir.ModifiedBy.Email,
                            //    created = newDir.Created
                            //};

                            //FileManagementDbContext.FileItems.Add(newDir);
                            //FileManagementDbContext.SaveChanges();
                            #endregion
                            break;
                        }

                    case "uploadFileChunk":
                        {
                            Console.WriteLine(args);
                            var parentId = ParseKey(args.itemInfo.key);
                            FileItem? parentItem = FileManagementDbContext.FileItems
                                .Include(x => x.Parent)
                                .Include(x => x.ModifiedBy)
                                .FirstOrDefault(x => x.Id == parentId);

                            var newFile = new FileItem()
                            {
                                Key = Guid.NewGuid(),
                                Size = args.fileData.size,
                                Created = DateTime.Now,
                                Modified = DateTime.Now,
                                Extension = Path.GetExtension(args.fileData.name),
                                File = args.fileData.file,
                                FileItems = new HashSet<FileItem>(),
                                Name = args.fileData.name,
                                Type = FileType.File,
                                Deals = new HashSet<Deal>(),
                                ModifiedBy = FileManagementDbContext.Users.FirstOrDefault(),
                                Parent = parentItem,
                                Path = null,
                            };

                            objRes = new Iteminfo()
                            {
                                key = newFile.Id.ToString(),
                                name = newFile.Name,
                                isDirectory = newFile.Type == FileType.Folder,
                                dateModified = newFile.Modified,
                                hasSubdirectories = newFile.Parent != null,
                                modifiedBy = newFile.ModifiedBy.Email,
                                created = newFile.Created
                            };

                            FileManagementDbContext.FileItems.Add(newFile);
                            FileManagementDbContext.SaveChanges();

                            break;
                        }

                    case "deleteItem":
                        {
                            DeleteItem(itemInfo);
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            SemaphoreSlim.Release();
            return objRes;
        }

        private IDictionary<int, bool> GetHasSubDirectoriesInfo(IEnumerable<FileItem> fileItems)
        {
            var keys = fileItems.Select(i => i.Id).ToArray();
            if (keys.Count() == 0)
            {
                return new Dictionary<int, bool>();
            }

            return FileManagementDbContext.FileItems
                .Include(x => x.Parent)
                .Include(x => x.FileItems)
                .Where(item => item.Type == FileType.Folder)
                .Where(i => keys.Contains(i.Id))
                .ToDictionary(group => group.Id, group => group.FileItems.Any());
        }

        private IEnumerable<FileItem> GetDirectoryContents(int parentKey)
        {
            if (parentKey == 0)
            {
                return FileManagementDbContext.FileItems
                        .Include(x => x.Parent)
                        .Include(x => x.ModifiedBy)
                        .OrderByDescending(item => item.Type == FileType.Folder)
                        .ThenBy(item => item.Name)
                        .Where(x => x.Parent == null);
            }

            return FileManagementDbContext.FileItems
                .Include(x => x.Parent)
                .Include(x => x.ModifiedBy)
                .OrderByDescending(item => item.Type == FileType.Folder)
                .ThenBy(item => item.Name)
                .Where(items => items.Parent.Id == parentKey);
        }

        int ParseKey(string key)
        {
            return string.IsNullOrEmpty(key) ? 0 : int.Parse(key);
        }

        public void CreateDirectory(FileSystem options)
        {
            var parentDirectory = options.args;
            if (!IsFileItemExists(parentDirectory))
                return;

            string parentPath = "";
            var parentId = ParseKey(parentDirectory.itemInfo.key);
            FileItem? parentItem = FileManagementDbContext.FileItems
                .Include(x => x.Parent)
                .Include(x => x.ModifiedBy)
                .FirstOrDefault(x => x.Id == parentId);

            if(parentItem != null)
            {
                parentPath = parentItem.Name;
            }

            var directory = new FileItem
            {
                Key = Guid.NewGuid(),
                Name = options.args.name,
                Modified = DateTime.Now,
                Created = DateTime.Now,
                Type = FileType.Folder,
                Parent = parentItem,
                ModifiedBy = FileManagementDbContext.Users.FirstOrDefault(),
                File = null,
                Extension = "",
                FileItems = new HashSet<FileItem>(),
                Deals = new HashSet<Deal>(),
                Path = Path.Combine(parentPath, options.args.name)
            };

            FileManagementDbContext.FileItems.Add(directory);
            FileManagementDbContext.SaveChanges();
        }

        public void DeleteItem(FileSystem options)
        {
            var item = options.args;

            if (!IsFileItemExists(item))
                return;

            var fileItem = GetFileItem(ParseKey(item.itemInfo.key));
            FileManagementDbContext.FileItems.Remove(fileItem);
            
            if (fileItem.Type == FileType.Folder)
                RemoveDirectoryContentRecursive(fileItem.Id);

            FileManagementDbContext.SaveChanges();
        }

        void RemoveDirectoryContentRecursive(int parenDirectoryKey)
        {
            var itemsToRemove = FileManagementDbContext
                .FileItems
                .Include(x => x.Parent)
                .Where(item => item.Parent.Id == parenDirectoryKey);

            foreach (var item in itemsToRemove)
            {
                FileManagementDbContext.FileItems.Remove(item);
            }

            foreach (var item in itemsToRemove)
            {
                if (!(item.Type == FileType.Folder)) continue;
                RemoveDirectoryContentRecursive(item.Id);
            }
        }

        private FileItem GetFileItem(int itemId)
        {
            return FileManagementDbContext.FileItems.FirstOrDefault(i => i.Id == itemId);
        }

        bool IsFileItemExists(Args args)
        {
            var pathKeys = args.itemInfo.pathKeys.Select(key => ParseKey(key.ToString())).ToArray();
            var entries = FileManagementDbContext.FileItems.Include(x => x.Parent).Where(item => pathKeys.Contains(item.Id)).ToList();
            var foundEntries = entries.Select(item => new { Id = item.Id, ParentId = item.Parent != null ? item.Parent.Id : 0, Name = item.Name, IsDirectory = item.Type == FileType.Folder });

            var pathNames = args.itemInfo.path.Replace("/","\\").Split(new[] { Path.DirectorySeparatorChar }, StringSplitOptions.RemoveEmptyEntries);

            var isDirectoryExists = true;
            for (var i = 0; i < pathKeys.Length && isDirectoryExists; i++)
            {
                var entry = foundEntries.FirstOrDefault(e => e.Id == pathKeys[i]);
                isDirectoryExists = entry != null && entry.Name == pathNames[i] &&
                                    (i == 0 && entry.ParentId == 0 || entry.ParentId == pathKeys[i - 1]);
                if (isDirectoryExists && i < pathKeys.Length - 1)
                    isDirectoryExists = entry.IsDirectory;
            }
            return isDirectoryExists;
        }

        //private string BuildName(int key, string name)
        //{
        //    //get fileItem by key
        //    var fileItem = GetFileItem(key);
        //    if(fileItem == null)
        //        return name;

        //    List<string> pathNames = new List<string>();
        //    while(fileItem.Parent != null) 
        //    {

        //    }
        //}
        //void ThrowItemNotFoundException(FileSystemItemInfo item)
        //{
        //    var itemType = item.IsDirectory ? "Directory" : "File";
        //    var errorCode = item.IsDirectory ? FileSystemErrorCode.DirectoryNotFound : FileSystemErrorCode.FileNotFound;
        //    string message = $"{itemType} '{item.Path}' not found.";
        //    throw new FileSystemException(errorCode, message);
        //}

        //void ThrowNoAccessException()
        //{
        //    string message = "Access denied. The operation cannot be completed.";
        //    throw new FileSystemException(FileSystemErrorCode.NoAccess, message);
        //}
    }
}

