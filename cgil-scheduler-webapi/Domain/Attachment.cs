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

    public class Attachment
    {
        public Attachment()
        {
            Deals = new HashSet<Deal>();    
        }

        public int Id { get; set; } 
        public byte[] File { get; set; }
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public long FileSize { get; set; }  //[byte]
        public FileType FileType { get; set; }
        public ICollection<Deal> Deals { get; set; }
    }
}
