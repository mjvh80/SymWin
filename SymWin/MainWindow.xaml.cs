/*
 * © Marcus van Houdt 2014
 */

using SymWin.Keyboard;
using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SymWin
{
   /// <summary>
   /// Interaction logic for MainWindow.xaml
   /// </summary>
   public partial class MainWindow : Window
   {
      public static LetterSelector Selector;

      public MainWindow()
      {
         var args = Environment.GetCommandLineArgs();

         var showWindow = false;
         
         // Parse command line args.
         foreach (var arg in args.Skip(1))
         {
            switch (arg)
            {
               case "--window":
                  showWindow = true;
                  break;

               default:
                  Console.Error.WriteLine("Unknown parameter: " + arg);
                  Environment.Exit(1); return;
            }
         }

         if (!showWindow)
         {
            this.Visibility = System.Windows.Visibility.Hidden;
            this.ShowInTaskbar = false;
         }

         InitializeComponent();

         // Hook keyboard events.
         Handler.ValidateCAPSLOCKState();

         LetterMappings.InitializeWindowsAndBindings();

         // Register keys.
         foreach (var letter in LetterMappings.KeysToSymbols.Keys)
            LowLevelListener.HookedKeys.Add(letter);

         // Hook left, right arrow keys to move the selector.
         LowLevelListener.HookedKeys.Add(Key.Left);
         LowLevelListener.HookedKeys.Add(Key.Right);

         // Hook our "hot key".
         LowLevelListener.HookedKeys.Add(Key.CapsLock);
         LowLevelListener.HookedKeys.Add(Key.LeftShift);
         LowLevelListener.HookedKeys.Add(Key.RightShift);
         LowLevelListener.Register();

         LowLevelListener.KeyDown += new LowLevelListener.KeyHookEventHandler(e => Handler.HandleKeyPress(true, e));
         LowLevelListener.KeyUp += new LowLevelListener.KeyHookEventHandler(e => Handler.HandleKeyPress(false, e));

         try
         {
            // Keep the app responsive even if system is busy.
            // I found that AboveNormal does not keep the app responsive enough if the system is particularly busy,
            // although even at High there can be a noticeable (but generally acceptable) lag for activation.
            Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;
         }
         catch { }
      }

      private void Button_Click(object sender, RoutedEventArgs e)
      {
         if (Selector == null)
            Selector = new LetterSelector(Key.A, LetterMappings.KeysToSymbols[Key.A]);
         Selector.Show();
         Selector.SelectNext();
      }

      private void OnTaskTrayDisable(Object sender, RoutedEventArgs e) 
      {
         var enable = EnableMenuItem.Header.Equals("Enable");

         if (enable)
            EnableMenuItem.Header = "Disable";
         else
            EnableMenuItem.Header = "Enable";

         // For reasons not understood at the moment, capslock state is not detected whenever the taskicon click
         // is being handled. In this case we'll always update caps state.
         Handler.Enable(enable);

         // Show a balloon tip only if directly clicked on icon.
         if (sender is Hardcodet.Wpf.TaskbarNotification.TaskbarIcon)
         {
            TrayIcon.ShowBalloonTip(enable ? "Enabled" : "Disabled", "SymWin has been " + (enable ? "enabled" : "disabled") + ".", Hardcodet.Wpf.TaskbarNotification.BalloonIcon.Info);
         }
      }

      private void OnTaskTrayQuit(Object sender, RoutedEventArgs e) 
      {
         Environment.Exit(0);
      }

      private static Configure _sConfigureWindow;

      private void OnConfigureClick(Object sender, RoutedEventArgs e)
      {
         var window = (_sConfigureWindow ?? (_sConfigureWindow = new Configure()));
         window.Show();
         window.Activate();
      }

      private void OnDelConfigureClick(Object sender, RoutedEventArgs e)
      {
         LetterMappings.DeleteBindings();
      }
   }
}
