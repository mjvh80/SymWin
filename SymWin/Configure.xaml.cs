using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SymWin
{
   /// <summary>
   /// Interaction logic for Configure.xaml
   /// </summary>
   public partial class Configure : Window
   {
      public Configure()
      {
         InitializeComponent();
      }

      private void OnConfigureSelectionChanged(Object s, RoutedEventArgs e)
      {
         var key = (Key)Enum.Parse(typeof(Key), Key.SelectedValue.ToString());
         var letters = LetterMappings.KeysToSymbols[key];
         Letters.Text = new String(letters);
      }

      private void OnConfigureSave(Object s, RoutedEventArgs e)
      {
         var key = (Key)Enum.Parse(typeof(Key), Key.SelectedValue.ToString());
         var letters = Letters.Text.ToCharArray();

         LetterMappings.UpdateKey(key, letters);
      }
   }
}
