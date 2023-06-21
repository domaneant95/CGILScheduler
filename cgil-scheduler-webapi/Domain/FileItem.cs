using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public enum FileType:int
    {
        File=0,
        Folder=1
    }

    public class FileItem
    {
        public FileItem()
        {
            Deals = new HashSet<Deal>();    
            FileItems = new HashSet<FileItem>();
        }

        public int Id { get; set; }
        public Guid Key { get; set; }
        public byte[] File { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string Extension { get; set; }
        public long Size { get; set; }  //[byte]
        public FileType Type { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public AppUser ModifiedBy { get; set; }
        public FileItem? Parent { get; set; }
        public ICollection<FileItem>? FileItems { get; set; }
        public ICollection<Deal> Deals { get; set; }
        public bool Nothing { get; set; }
    }
}
