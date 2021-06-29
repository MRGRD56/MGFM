using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGFM.Models.FS.Additional
{
    public class ComparableFile : IComparable, IComparable<ComparableFile>
    {
        public FileBase File { get; }

        public ComparableFile(FileBase file)
        {
            File = file;
        }
        
        public int CompareTo(object obj) => CompareTo((ComparableFile) obj);

        public int CompareTo(ComparableFile other)
        {
            return File switch
            {
                FS.Folder when other.File is File => -1,
                FS.File when other.File is Folder => 1,
                _ => string.Compare(File.ShortName, other.File.ShortName, StringComparison.OrdinalIgnoreCase)
            };
        }
    }
}
