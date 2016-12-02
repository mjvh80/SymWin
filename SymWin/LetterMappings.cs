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
        public static readonly Dictionary<Key, Tuple<Char[], Char[]>> KeysToSymbols = new Dictionary<Key, Tuple<Char[], Char[]>>
      {
         { Key.A, Tuple.Create(new[] { '≠','≈','≝','≡','≤','≥','≪','≫' }, new[] {'≠','≈','≝','≡','≤','≥','≪','≫' })},
         { Key.B, CharacterConst.EMPTY},
         { Key.C, CharacterConst.EMPTY},
         { Key.D, CharacterConst.EMPTY},
         { Key.E, CharacterConst.EMPTY},
         { Key.F, Tuple.Create(new[] { 'ƒ', 'θ' }, new[] { 'Ƒ', 'Θ' })},
         { Key.G, Tuple.Create(CharacterConst.GREEK_LETTERS,CharacterConst.GREEK_LETTERS) },
         { Key.H, CharacterConst.EMPTY},
         { Key.I, CharacterConst.EMPTY},
         { Key.J, CharacterConst.EMPTY},
         { Key.L, CharacterConst.EMPTY},
         { Key.M, CharacterConst.EMPTY},
         { Key.K, CharacterConst.EMPTY},
         { Key.N, CharacterConst.EMPTY},
         { Key.O, Tuple.Create(CharacterConst.OPERATION_CHARS, CharacterConst.OPERATION_CHARS)},
         { Key.P, Tuple.Create(new[] { 'π', '¶' }, new[] { 'Π', '¶' })},
         { Key.Q, Tuple.Create(CharacterConst.RELATIONS_CHARS, CharacterConst.NEGATED_RELATIONS_CHARS)},

         { Key.R, CharacterConst.EMPTY},
         { Key.S, Tuple.Create(CharacterConst.DOUBLE_STRUCK_CHARS,CharacterConst.DOUBLE_STRUCK_CHARS)},
         { Key.T, CharacterConst.EMPTY},
         { Key.U, CharacterConst.EMPTY},
         { Key.V, CharacterConst.EMPTY},
         { Key.W, CharacterConst.EMPTY},
         { Key.X, CharacterConst.EMPTY},
         { Key.Y, CharacterConst.EMPTY},
         { Key.Z, CharacterConst.EMPTY},

         // Numbers
         { Key.D0, Tuple.Create(new[]  { '☺', '☻', '∞', 'ø' }, new[]  { '☺', '☻', '∞', 'Ø' })},
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

         { Key.Decimal, CharacterConst.EMPTY },
         { Key.Multiply, Tuple.Create(new[] { '×' }, new[] { '×' })},
         { Key.OemOpenBrackets, CharacterConst.EMPTY }, // = Oem4
         { Key.Oem8, CharacterConst.EMPTY },
         { Key.OemQuestion, Tuple.Create(new[] { '¿'}, new[] { '¿' })},
         { Key.OemBackslash, CharacterConst.EMPTY }, // Equals 102
         { Key.OemMinus, CharacterConst.EMPTY },
         { Key.OemPipe, Tuple.Create(new[] { '¦' }, new[] { '¦' }) },
         { Key.OemSemicolon, CharacterConst.EMPTY }, // = Oem1 = Oem102
         { Key.OemTilde, CharacterConst.EMPTY },
         { Key.Subtract, CharacterConst.EMPTY },
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
