using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Input;
using System.Diagnostics;
using System.Windows;

namespace SymWin.Keyboard
{
   /// <summary>
   /// A class that manages a global low level keyboard hook
   /// </summary>
   public static class Listener
   {
      static Listener()
      {
         HookedKeys = new List<Key>();
         Hook = IntPtr.Zero;
      }

      #region PInvoke Structures
      
      public delegate Int32 keyboardHookProc(Int32 code, Int32 wParam, ref keyboardHookStruct lParam);

      public struct keyboardHookStruct
      {
         public Int32 vkCode;
         public Int32 scanCode;
         public Int32 flags;
         public Int32 time;
         public Int32 dwExtraInfo;
      }

      private const Int32 WH_KEYBOARD_LL = 13;
      private const Int32 WM_KEYDOWN = 0x100;
      private const Int32 WM_KEYUP = 0x101;
      private const Int32 WM_SYSKEYDOWN = 0x104;
      private const Int32 WM_SYSKEYUP = 0x105;

      #endregion

      #region Public declarations

      public static List<Key> HookedKeys { get; set; }
      internal static IntPtr Hook { get; set; }

      public static event KeyHookEventHandler KeyDown;
      public static event KeyHookEventHandler KeyUp;

      public static Boolean ModifierLeftAlt { get; set; }
      public static Boolean ModifierRightAlt { get; set; }
      public static Boolean ModifierLeftCtrl { get; set; }
      public static Boolean ModifierRightCtrl { get; set; }
      public static Boolean ModifierLeftShift { get; set; }
      public static Boolean ModifierRightShift { get; set; }
      public static Boolean ModifierLeftWin { get; set; }
      public static Boolean ModifierRightWin { get; set; }
      public static Boolean ModifierCapsLock { get; set; }
      public static Boolean ModifierFn { get; set; }

      public class KeyHookEventArgs
      {
         public static readonly KeyHookEventArgs None = new KeyHookEventArgs()
         {
            Key = Key.None

            // Other fields left to their default: false
         };

         public KeyHookEventArgs()
         {
         }
         public Key Key { get; set; }
         public Boolean ModifierLeftAlt { get; set; }
         public Boolean ModifierRightAlt { get; set; }
         public Boolean ModifierLeftCtrl { get; set; }
         public Boolean ModifierRightCtrl { get; set; }
         public Boolean ModifierLeftShift { get; set; }
         public Boolean ModifierRightShift { get; set; }
         public Boolean ModifierLeftWin { get; set; }
         public Boolean ModifierRightWin { get; set; }
         public Boolean ModifierCapsLock { get; set; }
         public Boolean ModifierFn { get; set; }

         public Boolean ModifierAnyAlt { get { return ModifierLeftAlt || ModifierRightAlt; } set { ModifierLeftAlt = value; } }
         public Boolean ModifierAnyCtrl { get { return ModifierLeftCtrl || ModifierRightCtrl; } set { ModifierLeftCtrl = value; } }
         public Boolean ModifierAnyShift { get { return ModifierLeftShift || ModifierRightShift; } set { ModifierLeftShift = value; } }
         public Boolean ModifierAnyWin { get { return ModifierLeftWin || ModifierRightWin; } set { ModifierLeftWin = value; } }

         public Boolean ModifierAnyNative { get { return (((ModifierAnyAlt) || (ModifierAnyWin)) || ((ModifierAnyCtrl) || (ModifierAnyShift))); } }
         public Boolean ModifierAny { get { return ((ModifierAny) || (ModifierFn)); } }
      }

      public delegate Boolean KeyHookEventHandler(KeyHookEventArgs e);
      #endregion

      #region Public Methods

      internal static void SetModifiers(Key key, Boolean IsPressed, Int32 VKey)
      {
         switch (key)
         {
            case Key.LeftAlt:
               ModifierLeftAlt = IsPressed;
               break;
            case Key.RightAlt:
               ModifierRightAlt = IsPressed;
               break;
            case Key.RightCtrl:
               ModifierRightCtrl = IsPressed;
               break;
            case Key.LeftCtrl:
               ModifierLeftCtrl = IsPressed;
               break;
            case Key.LeftShift:
               ModifierLeftShift = IsPressed;
               break;
            case Key.RightShift:
               ModifierRightShift = IsPressed;
               break;
            case Key.CapsLock:
               ModifierCapsLock = IsPressed;
               break;
            case Key.LWin:
               ModifierLeftWin = IsPressed;
               break;
            case Key.RWin:
               ModifierRightWin = IsPressed;
               break;
         }
         //if (!AppleKeyboardHID2.Registered)
         //   AppleKeyboardHID2.Start();
      }
      internal static KeyHookEventArgs CreateEventArgs(Key key)
      {
         return new KeyHookEventArgs()
         {
            Key = key,
            ModifierCapsLock = ModifierCapsLock,
            ModifierLeftAlt = ModifierLeftAlt,
            ModifierRightAlt = ModifierRightAlt,
            ModifierLeftCtrl = ModifierLeftCtrl,
            ModifierRightCtrl = ModifierRightCtrl,
            ModifierLeftShift = ModifierLeftShift,
            ModifierRightShift = ModifierRightShift,
            ModifierLeftWin = ModifierLeftWin,
            ModifierRightWin = ModifierRightWin,
            ModifierFn = ModifierFn
         };
      }

      public static keyboardHookProc HookProcessor { get; set; }

      public static void Register()
      {
         HookProcessor = Hook_Callback;
         IntPtr hInstance = GetModuleHandle(Process.GetCurrentProcess().MainModule.ModuleName);
         Hook = SetWindowsHookEx(WH_KEYBOARD_LL, HookProcessor, hInstance, 0);
      }

      public static void UnRegister()
      {
         UnhookWindowsHookEx(Hook);
      }

      public static Boolean TriggerKeyDown(KeyHookEventArgs args)
      {
         if (KeyDown != null)
            return KeyDown(args);
         else return false;
      }

      public static Boolean TriggerKeyUp(KeyHookEventArgs args)
      {
         if (KeyUp != null)
            return KeyUp(args);
         else return false;
      }

      public static Int32 Hook_Callback(Int32 code, Int32 wParam, ref keyboardHookStruct lParam)
      {
         if (code >= 0)
         {
            Key key = (Key)System.Windows.Input.KeyInterop.KeyFromVirtualKey(lParam.vkCode);
            Boolean IsPressed = (wParam == WM_KEYDOWN || wParam == WM_SYSKEYDOWN);

            SetModifiers(key, IsPressed, lParam.vkCode);
            if (HookedKeys.Contains(key))
            {
               KeyHookEventArgs kea = CreateEventArgs(key);

               Boolean Handled = false;
               if (IsPressed)
                  Handled = TriggerKeyDown(kea);
               else
                  Handled = TriggerKeyUp(kea);

               if (Handled)
                  return 1;
            }
         }
         return CallNextHookEx(Hook, code, wParam, ref lParam);
      }
      #endregion

      #region DLL imports
      [DllImport("user32.dll")]
      static extern IntPtr SetWindowsHookEx(Int32 idHook, keyboardHookProc callback, IntPtr hInstance, UInt32 threadId);

      [DllImport("user32.dll")]
      static extern Boolean UnhookWindowsHookEx(IntPtr hInstance);

      [DllImport("user32.dll")]
      static extern Int32 CallNextHookEx(IntPtr idHook, Int32 nCode, Int32 wParam, ref keyboardHookStruct lParam);

      [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
      public static extern IntPtr GetModuleHandle(String lpModuleName);

      #endregion
   }
}
