using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Shell32;
using Folder = MGFM.Models.FS.Folder;

namespace MGFM.Extensions
{
    public static class FilesExtensions
    {
        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        private static extern bool ShellExecuteEx(ref SHELLEXECUTEINFO lpExecInfo);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        private struct SHELLEXECUTEINFO
        {
            public int cbSize;
            public uint fMask;
            public IntPtr hwnd;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string lpVerb;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string lpFile;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string lpParameters;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string lpDirectory;
            public int nShow;
            public IntPtr hInstApp;
            public IntPtr lpIDList;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string lpClass;
            public IntPtr hkeyClass;
            public uint dwHotKey;
            public IntPtr hIcon;
            public IntPtr hProcess;
        }

        private const int SW_SHOW = 5;
        private const uint SEE_MASK_INVOKEIDLIST = 12;

        public static bool ShowProperties(string fileName)
        {
            var info = new SHELLEXECUTEINFO();
            info.cbSize = Marshal.SizeOf(info);
            info.lpVerb = "properties";
            info.lpFile = fileName;
            info.nShow = SW_SHOW;
            info.fMask = SEE_MASK_INVOKEIDLIST;
            return ShellExecuteEx(ref info);
        }

        public static void Open(string filePath)
        {
            var process = Process.Start("explorer.exe", $"\"{filePath}\"");
        }

        public static void OpenWith(string filePath)
        {
            var args = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "shell32.dll");
            args += ",OpenAs_RunDLL " + filePath;
            Process.Start("rundll32.exe", args);
        }

        public static string GetShortcutTargetFile(string shortcutFilename)
        {
            if (Path.GetExtension(shortcutFilename) != ".lnk")
            {
                throw new InvalidOperationException("The file is not a shortcut. A shortcut must have an .lnk extension.");
            }

            var pathOnly = Path.GetDirectoryName(shortcutFilename);
            var filenameOnly = Path.GetFileName(shortcutFilename);

            var shell = new Shell();
            var folder = shell.NameSpace(pathOnly);
            var folderItem = folder.ParseName(filenameOnly);
            if (folderItem == null) return string.Empty;
            
            var link = (ShellLinkObject) folderItem.GetLink;
            return link.Path;
        }
    }
}
