using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderBrowserDialog
{
    public class Infor
    {
        public string FileType { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public string IconPath
        {
            get
            {
                if (FileType == "Folder")
                {
                    return "/images/pngwing.png";
                }
                else if (FileType == "File")
                {
                    return "/images/file.png";
                }
                return string.Empty;
            }
        }
    }
}
