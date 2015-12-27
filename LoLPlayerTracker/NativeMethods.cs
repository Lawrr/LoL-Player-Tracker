﻿using System;
using System.Runtime.InteropServices;

namespace LoLPlayerTracker {
    public static class NativeMethods {
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetForegroundWindow(IntPtr hWnd);
    }
}
