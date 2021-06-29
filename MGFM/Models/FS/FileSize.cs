using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MGFM.Properties;

namespace MGFM.Models.FS
{
    public class FileSize : IComparable, IComparable<FileSize>
    {
        public long SizeInBytes { get; }

        public long Value { get; }

        public FileSizeUnit Unit { get; }


        private readonly bool _isNullSize;

        private static readonly Dictionary<FileSizeUnit, (long Bytes, long MinBytes, string UnitName)> Units = new()
        {
            { FileSizeUnit.Byte, (1, 0, Resources.FileSize_Byte) },
            { FileSizeUnit.KB, (1024, 1024 - 400, Resources.FileSize_KB) },
            { FileSizeUnit.MB, ((long) Math.Pow(1024, 2), (long) Math.Pow(1024, 2) - 400, Resources.FileSize_MB) },
            { FileSizeUnit.GB, ((long) Math.Pow(1024, 3), (long) Math.Pow(1024, 3) - 400, Resources.FileSize_GB) },
            { FileSizeUnit.TB, ((long) Math.Pow(1024, 4), (long) Math.Pow(1024, 4) - 400, Resources.FileSize_TB) }
        };

        public FileSize()
        {
            _isNullSize = true;
            SizeInBytes = 0;
        }

        public FileSize(long sizeInBytes)
        {
            SizeInBytes = sizeInBytes;
            foreach (var (key, (bytes, minBytes, _)) in Units.Reverse())
            {
                if (sizeInBytes < minBytes) continue;

                Value = Convert.ToInt64(Math.Round((double) sizeInBytes / bytes));
                Unit = key;
                break;
            }
        }

        public int CompareTo(FileSize other)
        {
            return (SizeInBytes - other.SizeInBytes) switch
            {
                > 0 => 1,
                < 0 => -1,
                0 => 0
            };
        }

        public int CompareTo(object obj) => CompareTo((FileSize) obj);

        public override string ToString() => _isNullSize ? "" : $"{Value} {Units[Unit].UnitName}";
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
