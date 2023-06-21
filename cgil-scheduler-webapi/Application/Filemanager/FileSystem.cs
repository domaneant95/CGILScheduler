using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Filemanager
{
    public class FileSystem
    {
        public string command { get; set; }
        public Args args { get; set; }
    }

    public class Args
    {
        public string name { get; set; }
        public Iteminfo itemInfo { get; set; }
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
