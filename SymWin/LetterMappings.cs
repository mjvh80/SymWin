/*
 * © Marcus van Houdt 2014
 */

using System;
using System.Collections.Generic;
using System.Windows.Input;
using System.Xml;

namespace SymWin
{
   public static class LetterMappings
   {
      private static readonly Tuple<Char[], Char[]> _sEmpty = Tuple.Create(new Char[0], new Char[0]);

      public static readonly Dictionary<Key, Tuple<Char[], Char[]>> KeysToSymbols = new Dictionary<Key, Tuple<Char[], Char[]>>
      {
         // Initial rough mapping for letters, not too much thought has gone into this yet.
         { Key.A, Tuple.Create(new[] { 'á', 'à', 'ä', 'å', 'α', 'æ' },
                               new[] { 'Á', 'À', 'Ä', 'Å', 'Α', 'Æ' })},

         { Key.B, Tuple.Create(new[] { 'β' },
                               new[] { 'Β' })},

         { Key.C, Tuple.Create(new[] { 'ç', '©' },
                               new[] { 'Ç', '©' })},

         { Key.D, Tuple.Create(new[] { 'δ' },
                               new[] { 'Δ' })},

         { Key.E, Tuple.Create(new[] { 'é', 'è', 'ë', 'ê', 'ε', 'ð', 'η' },
                               new[] { 'É', 'È', 'Ë', 'Ê', 'Ε', 'Ð', 'Η' })},

         { Key.F, Tuple.Create(new[] { 'ƒ' },
                               new[] { 'Ƒ' })},

         { Key.G, Tuple.Create(new[] { 'γ' },
                               new[] { 'Γ' })},

         { Key.H, Tuple.Create(new[] { 'ħ' },
                               new[] { 'Ħ' })},

         { Key.I, Tuple.Create(new[] { 'í', 'ì', 'ï', 'î', 'ι' },
                               new[] { 'Í', 'Ì', 'Ï', 'Î', 'Ι' })},

         { Key.J, _sEmpty},

         { Key.L, Tuple.Create(new[] { 'λ' },
                               new[] { 'Λ' })},

         { Key.M, Tuple.Create(new[] { 'µ' },
                               new[] { 'Μ' })},

         { Key.K, Tuple.Create(new[] { 'κ' },
                               new[] { 'Κ' })},

         { Key.N, Tuple.Create(new[] { 'ñ', 'ν', 'ŋ' },
                               new[] { 'Ñ', 'Ν', 'Ŋ' })},

         { Key.O, Tuple.Create(new[] { 'ó', 'ò', 'ö', 'ô', 'õ', 'ø', 'ω' },
                               new[] { 'Ó', 'Ò', 'Ö', 'Ô', 'Õ', 'Ø', 'Ω' })},

         { Key.P, Tuple.Create(new[] { 'π', '¶' },
                               new[] { 'Π', '¶' })},

         { Key.Q, _sEmpty},

         { Key.R, Tuple.Create(new[] { 'ρ', '®' },
                               new[] { 'Ρ', '®' })},

         { Key.S, Tuple.Create(new[] { 'ß', 'š', 'σ', 'ς' },
                               new[] { 'ẞ', 'Š', 'Σ', 'Σ' })},

         { Key.T, Tuple.Create(new[] { 'θ', 'τ', 'þ', '™' },
                               new[] { 'Θ', 'τ', 'Þ', '™' })},

         { Key.U, Tuple.Create(new[] { 'ú', 'ù', 'ü', 'û' },
                               new[] { 'Ú', 'Ù', 'Ü', 'Û' })},

         { Key.V, _sEmpty},

         { Key.W, Tuple.Create(new[] { 'ẃ', 'ẁ', 'ẅ', 'ŵ' },
                               new[] { 'Ẃ', 'Ẁ', 'Ẅ', 'Ŵ' })},

         { Key.X, Tuple.Create(new[] { 'Ξ', 'χ' },
                               new[] { 'ξ', 'Χ' })},

         { Key.Y, Tuple.Create(new[] { 'ý', 'ỳ', 'ÿ', 'ŷ' },
                               new[] { 'Ý', 'Ỳ', 'Ÿ', 'Ŷ' })},

         { Key.Z, Tuple.Create(new[] { 'ζ' },
                               new[] { 'Ζ' })},

         // Numbers
         { Key.D0, Tuple.Create(new[] { '☺', '☻', '∞', 'ø' }, new[]  { '☺', '☻', '∞', 'Ø' })},
         { Key.D1, Tuple.Create(new[] { '¡', '‼', '¹' }, new[] { '¡', '‼', '¹' })},
         { Key.D2, Tuple.Create(new[] { '²', '½', '√' }, new[] { '²', '½', '√' })},
         { Key.D3, Tuple.Create(new[] { '⅓', '³', '§' }, new[] { '⅓', '³', '§' })},
         { Key.D4, Tuple.Create(new[] { '£', '¥', '$', '€', '¤', '¼' }, new[] { '£', '¥', '$', '€', '¤', '¼' })},
         { Key.D5, Tuple.Create(new[] { '‰', '⅕', '♫', '♪' }, new[] { '‰', '⅕', '♫', '♪' })},
         { Key.D8, Tuple.Create(new[] { '★', '✼', '❀' }, new[] { '★', '✼', '❀' })},

         { Key.D9, Tuple.Create(new[] { '☹' }, new[] { '☹' })},
         { Key.OemCloseBrackets, Tuple.Create(new[] { '☹' }, new[] { '☹' })},

         { Key.Divide, Tuple.Create(new[]  {'÷'}, new[]  {'÷'})},
         { Key.OemQuotes, Tuple.Create(new[] { '«', '»'}, new[] { '«', '»'})},
         { Key.OemComma, Tuple.Create(new[]  { '…', '∙', '●', '≤'}, new[]  { '…', '∙', '●', '≤'})},
         { Key.OemPeriod,  Tuple.Create(new[] { '≥' }, new[] { '≥' })},
         { Key.OemPlus,   Tuple.Create(new[] { '≈', '≠', '±' }, new[] { '≈', '≠', '±' })},

         { Key.Decimal, _sEmpty },
         { Key.Multiply, Tuple.Create(new[] { '×' }, new[] { '×' })},
         { Key.OemOpenBrackets, _sEmpty }, // = Oem4
         { Key.Oem8, _sEmpty },
         { Key.OemQuestion, Tuple.Create(new[] { '¿'}, new[] { '¿' })},
         { Key.OemBackslash, _sEmpty }, // Equals 102
         { Key.OemMinus, _sEmpty },
         { Key.OemPipe, Tuple.Create(new[] { '¦' }, new[] { '¦' }) },
         { Key.OemSemicolon, _sEmpty }, // = Oem1 = Oem102
         { Key.OemTilde, _sEmpty },
         { Key.Subtract, _sEmpty },
      };

      public static void UpdateKey(Key key, Char[] lowerCase, Char[] upperCase)
      {
         if (lowerCase.Length != upperCase.Length) throw new ArgumentException("lower and upper case letter arrays must be of equal length");
         var pair = Tuple.Create(lowerCase, upperCase);
         KeysToSymbols[key] = pair;

         if (lowerCase.Length > 0)
            KeyToWindowMap[key] = new LetterSelector(key, pair);

         _UpdateCustomKeyBindings();
      }

      public static Dictionary<Key, LetterSelector> KeyToWindowMap { get; private set; }

      /// <summary>
      /// Constructs a popup window for each of the letter mappings.
      /// </summary>
      public static void InitializeWindowsAndBindings()
      {
         // Let's check if we got custom definitions for key bindings.
         _LoadCustomKeyBindings();

         KeyToWindowMap = new Dictionary<Key, LetterSelector>();

         foreach (var kvp in KeysToSymbols)
         {
            if (kvp.Value.Item1.Length > 0)
               KeyToWindowMap.Add(kvp.Key, new LetterSelector(kvp.Key, kvp.Value)); ;
         }
      }

      private static void _LoadCustomKeyBindings()
      {
         var bindingsStr = Properties.Settings.Default.KeyBindings;
         if (String.IsNullOrEmpty(bindingsStr)) return;

         var bindings = new XmlDocument();
         bindings.LoadXml(bindingsStr);

         try
         {
            foreach (XmlElement binding in bindings.SelectNodes("/bindings/binding"))
            {
               var key = (Key)Enum.Parse(typeof(Key), binding.GetAttribute("key"));
               var lcase = binding.GetAttribute("lower-case").ToCharArray();
               var ucase = binding.GetAttribute("upper-case").ToCharArray();

               if (lcase.Length != ucase.Length || lcase.Length == 0)
                  continue;

               if (KeysToSymbols.ContainsKey(key))
                  KeysToSymbols[key] = Tuple.Create(lcase, ucase);
            }
         }
         catch
         {
            return;
         }
      }

      private static void _UpdateCustomKeyBindings()
      {
         var doc = new XmlDocument();
         doc.LoadXml("<bindings />");

         foreach (var kvp in KeysToSymbols)
         {
            var binding = doc.CreateElement("binding");
            binding.SetAttribute("key", kvp.Key.ToString());
            binding.SetAttribute("lower-case", new String(kvp.Value.Item1));
            binding.SetAttribute("upper-case", new String(kvp.Value.Item2));
            doc.DocumentElement.AppendChild(binding);
         }

         Properties.Settings.Default.KeyBindings = doc.OuterXml;
         Properties.Settings.Default.Save();
      }

      public static void DeleteBindings()
      {
         Properties.Settings.Default.KeyBindings = "";
         Properties.Settings.Default.Save();
      }
   }
}
