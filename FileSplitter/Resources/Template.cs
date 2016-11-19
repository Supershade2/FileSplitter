using System;
using System.Linq;
using System.Reflection;
using System.IO;
using System.Runtime.InteropServices;
using System.Collections.Generic;

class Joiner
{
	static void Main()
	{
        string filetobuild = "Z";
		var handle = GetConsoleWindow();
		ShowWindow(handle,0);
		var assembly = Assembly.GetExecutingAssembly();
		IEnumerable<string> files = (from item in assembly.GetManifestResourceNames() where(item.Contains(".split"))select item);
            using (FileStream filetocreate = File.Create(Directory.GetCurrentDirectory() + @"\\"+filetobuild))
            {
                foreach (string i in files)
                {
                    Stream filepart = assembly.GetManifestResourceStream(i);
                    int cbyte = filepart.ReadByte();
                    while (cbyte != -1)
                    {
                        filetocreate.WriteByte((byte)cbyte);
                        cbyte = filepart.ReadByte();
                    }
                    filepart.Dispose();
                }
                filetocreate.Flush();
                filetocreate.Dispose();
            }
		}
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();
        [DllImport("user32.dll")]
        static extern IntPtr ShowWindow(IntPtr hWnd, int nCmdShow);
}