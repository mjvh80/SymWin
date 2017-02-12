﻿/*
 * © Marcus van Houdt 2014
 */

using System;
using System.Runtime.InteropServices;

namespace SymWin.Keyboard
{
   [StructLayout(LayoutKind.Sequential)]
   internal struct Point
   {
      public Int32 X;
      public Int32 Y;
   }

   [StructLayout(LayoutKind.Sequential)]
   internal struct RECT
   {
      public Int32 Left, Top, Right, Bottom;
   }

   [StructLayout(LayoutKind.Sequential)]
   internal struct GUITHREADINFO
   {
      public UInt32 cbSize;
      public UInt32 flags;
      public IntPtr hwndActive;
      public IntPtr hwndFocus;
      public IntPtr hwndCapture;
      public IntPtr hwndMenuOwner;
      public IntPtr hwndMoveSize;
      public IntPtr hwndCaret;
      public RECT rcCaret;
   }

   internal static class Caret
   {
      #region Interop

      [DllImport("user32.dll")]
      private static extern Boolean GetCaretPos(out Point point);

      [DllImport("user32.dll")]
      private static extern Boolean CreateCaret(IntPtr window, IntPtr bitmap, Int32 width, Int32 height);

      [DllImport("user32.dll")]
      private static extern Boolean DestroyCaret();

      [DllImport("user32.dll")]
      private static extern Boolean SetCaretPos(Int32 x, Int32 y);

      [DllImport("user32.dll")]
      private static extern Boolean ShowCaret(IntPtr window);

      [DllImport("user32.dll")]
      static extern Boolean ClientToScreen(IntPtr hWnd, ref Point lpPoint);

      [DllImport("user32.dll")]
      private static extern Boolean GetGUIThreadInfo(UInt32 idThread, out GUITHREADINFO lpgui);

      [DllImport("user32.dll")]
      static extern uint GetWindowThreadProcessId(IntPtr hWnd, IntPtr ProcessId);

      [DllImport("kernel32.dll")]
      static extern uint GetCurrentThreadId();

      [DllImport("user32.dll")]
      static extern bool AttachThreadInput(uint idAttach, uint idAttachTo, bool fAttach);

      [DllImport("user32.dll")]
      static extern IntPtr GetFocus();

      #endregion

      // Based on http://www.codeproject.com/Articles/34520/Getting-Caret-Position-Inside-Any-Application.
      public static Point GetPosition(IntPtr window)
      {
         GUITHREADINFO info = new GUITHREADINFO();
         info.cbSize = (UInt32)Marshal.SizeOf(info);
         GetGUIThreadInfo(0, out info);

         Point caretPos;
         caretPos.X = info.rcCaret.Left;
         caretPos.Y = info.rcCaret.Bottom;

         ClientToScreen(info.hwndCaret, ref caretPos);

         return caretPos;
      }
   }
}
