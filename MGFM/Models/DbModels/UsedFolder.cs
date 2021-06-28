using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MGFM.Models.FS;

namespace MGFM.Models.DbModels
{
    public class UsedFolder
    {
        [Key]
        public string Path { get; set; }

        public Folder Folder => new(Path);
    }
}
