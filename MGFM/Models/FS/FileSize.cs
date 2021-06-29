using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGFM.Models.FS
{
    public class FileSize
    {
        public long Value { get; }

        public FileSizeUnit Unit { get; }


        private static readonly Dictionary<FileSizeUnit, (long Bytes, long MinBytes, string UnitName)> Units = new()
        {
            { FileSizeUnit.Byte, (1, 0, "Byte") },
            { FileSizeUnit.KB, (1024, 1024 - 400, "KB") },
            { FileSizeUnit.MB, ((long) Math.Pow(1024, 2), (long) Math.Pow(1024, 2) - 400, "MB") },
            { FileSizeUnit.GB, ((long) Math.Pow(1024, 3), (long) Math.Pow(1024, 3) - 400, "GB") },
            { FileSizeUnit.TB, ((long) Math.Pow(1024, 4), (long) Math.Pow(1024, 4) - 400, "TB") }
        };

        public FileSize(long fileSizeInBytes)
        {
            foreach (var (key, (bytes, minBytes, _)) in Units)
            {
                if (fileSizeInBytes < minBytes) continue;

                Value = Convert.ToInt64(Math.Round((double) fileSizeInBytes / bytes, 2));
                Unit = key;
                break;
            }
        }
    }

    public enum FileSizeUnit
    {
        Byte,
        KB,
        MB,
        GB,
        TB
    }
}
