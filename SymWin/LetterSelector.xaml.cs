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
   /// Interaction logic for LetterSelector.xaml
   /// </summary>
   public partial class LetterSelector : Window
   {
      private readonly TextBox[] _mTextBoxes;
      private Tuple<Char[], Char[]> _mLetters;

      public readonly Boolean IsEmpty;

      public LetterSelector(Key key, Tuple<Char[], Char[]> letters)
      {
         if (letters == null) throw new ArgumentNullException("letters");
         if (letters.Item1 == null || letters.Item2 == null || letters.Item1.Length == 0 || letters.Item1.Length != letters.Item2.Length)
            throw new ArgumentException("Invalid letter definition, unequal lower and upper case sequence length or length 0.");

         InitializeComponent();

         var letterTemplate = Utils.CloneWPFObject(this.LetterPanel.Children.Cast<TextBox>().First());
         var width = 0.0;

         // Remove sample children.
         this.LetterPanel.Children.Clear();

         _mTextBoxes = new TextBox[letters.Item1.Length];

         // Add letters in order of appearance.
         for (var i = 0; i < letters.Item1.Length; i++)
         {
            var letter = letters.Item1[i];
            var newLetter = Utils.CloneWPFObject(letterTemplate);

            // Todo: it seems our "clone" is not cloning events, so let's hook it here.
            newLetter.PreviewMouseUp += OnMouseUp;

            // Adjust border thickness. It'd be nice if we can (?) do this in xaml using style ala css pseudo selectors
            var borderThick = newLetter.BorderThickness;
            borderThick.Left = borderThick.Right = 1;
            if (i == 0)
            {
               borderThick.Right = letters.Item1.Length > 1 ? 1 : 2;
               borderThick.Left = 2;
            }
            else if (i == letters.Item1.Length - 1)
            {
               borderThick.Left = letters.Item1.Length > 1 ? 1 : 2;
               borderThick.Right = 2;
            }
            newLetter.BorderThickness = borderThick;

            newLetter.Text = letter.ToString();

            this.LetterPanel.Children.Add(newLetter);

            _mTextBoxes[i] = newLetter;
            width += newLetter.Width;
         }

         this._mLetters = letters;

         // Restrict window size to panel width.
         this.Width = width;
         this.Height = letterTemplate.Height;
         this.Key = key;

         this.Loaded += (_, __) => SelectNext();
      }

      public readonly Key Key;

      public Char SelectedLetter
      {
         get
         {
            return _mTextBoxes[_mActiveIndex].Text[0];
         }
      }

      private Boolean _mIsLowerCase = true;

      public void ToUpper()
      {
         if (!_mIsLowerCase) return;

         for (var i = 0; i < _mTextBoxes.Length; i++)
            _mTextBoxes[i].Text = _mLetters.Item2[i].ToString();

         _mIsLowerCase = false;
      }

      public void ToLower()
      {
         if (_mIsLowerCase) return;

         for (var i = 0; i < _mTextBoxes.Length; i++)
            _mTextBoxes[i].Text = _mLetters.Item1[i].ToString();

         _mIsLowerCase = true;
      }

      private Int32 _mActiveIndex = -1;

      public void SelectNext()
      {
         var count = _mTextBoxes.Length;

         _mActiveIndex = (_mActiveIndex + 1) % count;

         _mTextBoxes[_mActiveIndex].Focus();
      }

      public void SelectPrevious()
      {
         var count = _mTextBoxes.Length;

         _mActiveIndex = (count + _mActiveIndex - 1) % count;

         _mTextBoxes[_mActiveIndex].Focus();
      }

      private void TextBox_TextChanged(Object sender, TextChangedEventArgs e)
      {
      }

#if  false

      public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
      {
         if (depObj != null)
         {
            for (Int32 i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
               DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
               if (child != null && child is T)
               {
                  yield return (T)child;
               }

               foreach (T childOfChild in FindVisualChildren<T>(child))
               {
                  yield return childOfChild;
               }
            }
         }
      }

#endif

      private void OnWindowDeactivated(Object sender, EventArgs e)
      {
         this.Visibility = System.Windows.Visibility.Hidden;
      }

      private void OnMouseUp(Object sender, MouseButtonEventArgs e)
      {
         var textBox = e.Source as TextBox;
         if (textBox == null) return;
         _mActiveIndex = Array.IndexOf(_mTextBoxes, textBox);
         textBox.Focus();
         Handler.HandleMouseUp();
      }
   }
}
