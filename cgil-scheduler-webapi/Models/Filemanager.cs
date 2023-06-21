
using System.Collections.Concurrent;

namespace Models.Filemanager
{
    public class CustomFile
    {
        public string value { get; set; }
        public string id { get; set; }
        public long size { get; set; }
        public double date { get; set; }
        public string type { get; set; }
        public ConcurrentBag<CustomFile> data { get; set; } = new ConcurrentBag<CustomFile>();
        public FilemanagerError filemanagerError { get; set; }

        public override string ToString()
        {
            return $"Value: {value}\n" +
                $"Id: {id}\n" +
                $"Size: {size}\n" +
                $"Date: {date}\n" +
                $"Type: {type}\n" +
                $"Child-count: {data.Count}\n";
        }
    }

    public class FilemanagerError
    {
        public bool invalid { get; set; }
        public string err { get; set; }
        public string id { get; set; }

        public FilemanagerError(string id, bool invalid, string err)
        {
            this.id = id;
            this.invalid = invalid;
            this.err = err;
        }
    }
    public class FileStats
    {
        public Stats stats { get; set; }
        public Features features { get; set; }
    }

    public class Stats
    {
        public long free { get; set; }
        public long total { get; set; }
        public long used { get; set; }
    }

    public class Features
    {
        public Preview preview { get; set; }
        public Meta meta { get; set; }
    }

    public class Preview
    {
        public bool code { get; set; }
        public bool document { get; set; }
        public bool image { get; set; }
    }

    public class Meta
    {
        public bool audio { get; set; }
        public bool image { get; set; }
    }

    public class CopyFeature
    {
        public string id { get; set; }
        public string to { get; set; }
    }

    public class RenameFeature
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class MakeFeature
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class EditTextFeature
    {
        public string id { get; set; }
        public string content { get; set; }
    }

    public class FormDeleteData
    {
        public string id { get; set; }
    }

    public class MakeRemoteFile
    {
        public string id { get; set; }
        public string name { get; set; }
        public string or { get; set; }
        public string ar { get; set; }
        public string ph { get; set; }
    }

    public interface IFileManager
    {
        void InitFileManager();
        bool TryCheckConnection();
        (long, long) TotalDirsAndFilesCount();
        bool ValidatePath(string dir = null, bool verbose = false);
        bool TrySetRootDir(string dir);
        string GetRootDir();
        CustomFile[] GetFiles(string id, string searchText = null);
        FileStats GetDriveStats(string drive);
        CustomFile EditOrder(string id, string order, string article, string phase);
        CustomFile UploadFile(string id, string fileName, byte[] buffer, object extraContent = null);
        bool Delete(string id);
        CustomFile MakeDir(string id, string name);
        CustomFile WriteContentToFile(string id, string content);
        CustomFile MakeFile(string id, string name);
        FilemanagerError Rename(string id, string name);
        string ReadText(string id);
        byte[] GetThumbnail(string id, int width, int height);
        byte[] ReadBytes(string id);
        CustomFile Move(string id, string to);
        CustomFile Copy(string id, string to);
        byte[] GetIcons(string size, string type, string name);
        (long, long) DriveStats(string driveName);
        ConcurrentBag<CustomFile> GetDirectoryTree(string id = null);
        long GetDirectoryCount(string id);
        CustomFile GetDirs(CustomFile bag, string path, bool dynamicLoading = false, int depth = 0);
        string TranslatePathToRemote(string rootPath, string currentDirPath, out string id);
        string TranslatePathToLocal(string rootPath, string id);
        string TranslatePathToLocal(string id);
        CustomFile CreateCustomFile(string rootPath, string currentDirPath, string type, DateTime lastWrite, long size);
        CustomFile SaveImage(string dataUrl);
    }


    public class FileManager
    {
    }
}
