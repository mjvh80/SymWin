using SymWin.Keyboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
         InitializeComponent();

         LetterMappings.InitializeWindows();

         // Register keys.
         foreach (var letter in LetterMappings.LetterToSymbols.Keys)
            Listener.HookedKeys.Add(LetterMappings.LetterToKey(letter));

         // Hook left, right arrow keys to move the selector.
         Listener.HookedKeys.Add(Key.Left);
         Listener.HookedKeys.Add(Key.Right);

         // Hook our "hot key".
         Listener.HookedKeys.Add(Key.CapsLock);
         Listener.Register();

         Selector = new LetterSelector(LetterMappings.LetterToSymbols['a']); // 'a', 'b', 'c', 'd');
       //  Selector.Show();

         Listener.KeyDown += new Listener.KeyHookEventHandler(e => Handler.HandleKeyPress(true, e));
         Listener.KeyUp += new Listener.KeyHookEventHandler(e => Handler.HandleKeyPress(false, e));
      }

      private void Button_Click(object sender, RoutedEventArgs e)
      {
         Selector.Show();
         Selector.SelectNext();
      }
   }
}
