using Domain;
using Domain.DTOs;
using Microsoft.EntityFrameworkCore;
using NPOI.HPSF;
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
        private readonly object locker = new object();

        public object Process(FileSystem itemInfo)
        {
            var args = itemInfo.args;

            switch (itemInfo.command)
            {
                case "getItems":
                    string pathKeys = args.itemInfo.key;

                    var parentId = ParseKey(pathKeys);
                    var fileItems = GetDirectoryContents(parentId);
                    var hasSubDirectoriesInfo = GetHasSubDirectoriesInfo(fileItems);

                    var clientItemList = new List<Iteminfo>();
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

                    return clientItemList;

                case "createDirectory":
                    Console.WriteLine(args);
                    parentId = ParseKey(args.itemInfo.key);
                    FileItem? parentItem = FileManagementDbContext.FileItems
                        .Include(x => x.Parent)
                        .Include(x => x.ModifiedBy)
                        .FirstOrDefault(x => x.Id == parentId);

                    var newDir = new FileItem()
                    {
                        Key = Guid.NewGuid(),
                        Size = 0,
                        Created = DateTime.Now,
                        Modified = DateTime.Now,
                        Extension = "",
                        File = null,
                        FileItems = new HashSet<FileItem>(),
                        Name = args.name,
                        Type = FileType.Folder,
                        Deals = new HashSet<Deal>(),
                        ModifiedBy = FileManagementDbContext.Users.FirstOrDefault(),
                        Parent = parentItem,
                        Path = null,
                    };
                    FileManagementDbContext.FileItems.Add(newDir);
                    FileManagementDbContext.SaveChanges();
                    break;

            }

            return null;
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
    }
}

