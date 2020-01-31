using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace MyControl

{
    class RecMemory
    {
        public static void ClearMemory()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            bool flag = Environment.OSVersion.Platform == PlatformID.Win32NT;
            if (flag)
            {
                RecMemory.SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }
        [DllImport("kernel32.dll")]
        public static extern int SetProcessWorkingSetSize(IntPtr process, int minSize, int maxSize);
    }
}
