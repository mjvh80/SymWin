/*
 * © Marcus van Houdt 2014
 */

using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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

      private void _Error(String msg)
      {
         Message.Text = msg;
         Message.Style = (Style)this.Resources["MessageError"];
      }

      private void _Ok(String msg)
      {
         Message.Text = msg;
         Message.Style = (Style)this.Resources["MessageOk"];
      }

      private void _Status(String msg)
      {
         Message.Text = msg;
         Message.Style = (Style)this.Resources["MessageStatus"];
      }

      private void OnClosing(Object s, CancelEventArgs e)
      {
         // Hide the window instead, and cancel closing.
         this.Visibility = System.Windows.Visibility.Hidden;
         e.Cancel = true;
      }

      private void OnConfigureAutoComplete(Object s, KeyEventArgs e)
      {
         ((ComboBox)s).SelectedItem = e.Key;
      }

      private void OnConfigureSelectionChanged(Object s, RoutedEventArgs e)
      {
         var key = (Key)Enum.Parse(typeof(Key), Key.SelectedValue.ToString());
         var letters = LetterMappings.KeysToSymbols[key];

         LowerCaseLetters.Style = UpperCaseLetters.Style = (Style)this.Resources["Selected"];

         LowerCaseLetters.Text = new String(letters.Item1);
         UpperCaseLetters.Text = new String(letters.Item2);
      }

      private void OnConfigureSave(Object s, RoutedEventArgs e)
      {
         var key = (Key)Enum.Parse(typeof(Key), Key.SelectedValue.ToString());

         var lowerCase = LowerCaseLetters.Text.ToCharArray();
         var upperCase = UpperCaseLetters.Text.ToCharArray();

         if (lowerCase.Length != upperCase.Length)
         {
            _Error("Lower and upper case should contain an equal amount of symbols.");
            return;
         }

         LetterMappings.UpdateKey(key, lowerCase, upperCase);
         _Ok("Changes for " + key + " saved!");
      }
   }
}
