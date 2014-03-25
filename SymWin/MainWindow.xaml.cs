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

         // Register keys.
         Listener.HookedKeys.Add(Key.A);
         Listener.HookedKeys.Add(Key.CapsLock);
         Listener.Register();

         Selector = new LetterSelector();
       //  Selector.Show();

         Listener.KeyDown += new Listener.KeyHookEventHandler(e => Handler.HandleKeyPress(true, e));
         Listener.KeyUp += new Listener.KeyHookEventHandler(e => Handler.HandleKeyPress(false, e));
      }

      private void Button_Click(object sender, RoutedEventArgs e)
      {
         Selector.SelectNext();
      }
   }
}
