using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Filemanager
{
    public class FileSystem
    {
        public string command { get; set; } = string.Empty;
        public Args? args { get; set; }
    }

    public class Args
    {
        public string name { get; set; }
        public Iteminfo itemInfo { get; set; }
        public FileData? fileData { get; set; }
        public UploadInfo? uploadInfo { get; set; }
    }

    public class FileData
    {
        public long lastModified { get; set; }
        public DateTime lastModifiedDate { get; set; }
        public string name { get; set; } = string.Empty;
        public long size { get; set; }
        public string type { get; set; } = string.Empty;
        public byte[] file { get; set; }
    }

    public class UploadInfo
    {
        public long bytesUploaded { get; set; }
        public int chunkCount { get; set; }
        public int chunkIndex { get; set; }
        public int fileIndex { get; set; }
        public ChunkBlob chunkBlob { get; set; }
        public object customData { get; set; }
    }

    public class ChunkBlob
    {
        public int size { get; set; }
        public string type { get; set; } = string.Empty;
    }

    public class Iteminfo
    {
        public string name { get; set; }
        public object[] pathInfo { get; set; }
        public string parentPath { get; set; }
        public string relativeName { get; set; }
        public string key { get; set; } = "";
        public string path { get; set; }
        public object[] pathKeys { get; set; }
        public bool isDirectory { get; set; }
        public long size { get; set; }
        public DateTime dateModified { get; set; }
        public string thumbnail { get; set; }
        public string tooltipText { get; set; }
        public bool hasSubdirectories { get; set; } = false;
        public string modifiedBy { get; set; } = "";
        public DateTime created { get; set; } = DateTime.Now;
    }
}
