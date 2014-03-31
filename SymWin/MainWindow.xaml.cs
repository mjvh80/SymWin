/*
 * © Marcus van Houdt 2014
 */

using SymWin.Keyboard;
using System;
using System.Linq;
using System.Windows;
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

         Handler.ValidateCAPSLOCKState();

         LetterMappings.InitializeWindows();

         // Register keys.
         foreach (var letter in LetterMappings.LetterToSymbols.Keys)
            LowLevelListener.HookedKeys.Add(LetterMappings.LetterToKey(letter));

         // Hook left, right arrow keys to move the selector.
         LowLevelListener.HookedKeys.Add(Key.Left);
         LowLevelListener.HookedKeys.Add(Key.Right);

         // Hook our "hot key".
         LowLevelListener.HookedKeys.Add(Key.CapsLock);
         LowLevelListener.HookedKeys.Add(Key.LeftShift);
         LowLevelListener.HookedKeys.Add(Key.RightShift);
         LowLevelListener.Register();

         Selector = new LetterSelector(Key.A, LetterMappings.LetterToSymbols['a']);

         LowLevelListener.KeyDown += new LowLevelListener.KeyHookEventHandler(e => Handler.HandleKeyPress(true, e));
         LowLevelListener.KeyUp += new LowLevelListener.KeyHookEventHandler(e => Handler.HandleKeyPress(false, e));
      }

      private void Button_Click(object sender, RoutedEventArgs e)
      {
         Selector.Show();
         Selector.SelectNext();
      }
   }
}
