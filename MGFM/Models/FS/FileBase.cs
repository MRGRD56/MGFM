using System.Drawing;
using System.Windows.Media;
using MGFM.Extensions;

namespace MGFM.Models.FS
{
    public abstract class FileBase
    {
        public string Path { get; set; }

        public abstract Icon Icon { get; }

        public ImageSource IconSource => Icon.ToBitmap().ToImageSource();
    }
}
