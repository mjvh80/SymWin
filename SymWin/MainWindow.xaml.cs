/*
 * © Marcus van Houdt 2014
 */

using SymWin.Keyboard;
using System;
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
         var item = (MenuItem)sender;
         var enable = item.Header.Equals("Enable");

         if (enable)
            item.Header = "Disable";
         else
            item.Header = "Enable";

         Handler.Enable(enable);
      }

      private void OnTaskTrayQuit(Object sender, RoutedEventArgs e) 
      {
         Environment.Exit(0);
      }

      private static Configure _sConfigureWindow;

      private void OnConfigureClick(Object sender, RoutedEventArgs e)
      {
         (_sConfigureWindow ?? (_sConfigureWindow = new Configure())).Show();
      }

      private void OnDelConfigureClick(Object sender, RoutedEventArgs e)
      {
         LetterMappings.DeleteBindings();
      }
   }
}
