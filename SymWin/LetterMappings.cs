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

           
         { Key.A, Tuple.Create(CharacterConst.ARROWS_CHARS_PART1, CharacterConst.ARROWS_CHARS_PART2)},
         { Key.B, CharacterConst.EMPTY_TUPLE},
         { Key.C, CharacterConst.EMPTY_TUPLE},
         { Key.D, Tuple.Create(CharacterConst.DOUBLE_STRUCK_CHARS, CharacterConst.DOUBLE_STRUCK_CHARS)},
         { Key.E, Tuple.Create(CharacterConst.BASIC_MATH_PART1, CharacterConst.BASIC_MATH_PART2)},
         { Key.F, CharacterConst.EMPTY_TUPLE},
         { Key.G, Tuple.Create(CharacterConst.GREEK_LETTERS_LOWERCASE, CharacterConst.GREEK_LETTERS_UPPERCASE)},
         { Key.H, CharacterConst.EMPTY_TUPLE},
         { Key.I, CharacterConst.EMPTY_TUPLE},
         { Key.J, CharacterConst.EMPTY_TUPLE},
         { Key.L, CharacterConst.EMPTY_TUPLE},
         { Key.M, CharacterConst.EMPTY_TUPLE},
         { Key.K, CharacterConst.EMPTY_TUPLE},
         { Key.N, CharacterConst.EMPTY_TUPLE},
         { Key.O, CharacterConst.EMPTY_TUPLE},
         { Key.P, CharacterConst.EMPTY_TUPLE},
         { Key.Q, Tuple.Create(CharacterConst.RELATIONS_CHARS, CharacterConst.NEGATED_RELATIONS_CHARS)},
         
         { Key.R, Tuple.Create(CharacterConst.ADVANCED_RELATIONAL_OPERATIONS, CharacterConst.ADVANCED_RELATIONAL_OPERATIONS)},
         { Key.S, Tuple.Create(CharacterConst.NEGATED_ARROWS_CHARS, CharacterConst.NEGATED_ARROWS_CHARS)},
         { Key.T, CharacterConst.EMPTY_TUPLE},
         { Key.U, CharacterConst.EMPTY_TUPLE},
         { Key.V, CharacterConst.EMPTY_TUPLE},
         { Key.W, Tuple.Create(CharacterConst.BINARY_OPERATIONS, CharacterConst.BINARY_OPERATIONS)},
         { Key.X, Tuple.Create(CharacterConst.BASIC_N_ARRAY_OPERATORS, CharacterConst.BASIC_N_ARRAY_OPERATORS)},
         { Key.Y, CharacterConst.EMPTY_TUPLE},
         { Key.Z, Tuple.Create(CharacterConst.GEOMETRY_CHARS, CharacterConst.GEOMETRY_CHARS)},

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

         { Key.Decimal, CharacterConst.EMPTY_TUPLE },
         { Key.Multiply, Tuple.Create(new[] { '×' }, new[] { '×' })},
         { Key.OemOpenBrackets, CharacterConst.EMPTY_TUPLE }, // = Oem4
         { Key.Oem8, CharacterConst.EMPTY_TUPLE },
         { Key.OemQuestion, Tuple.Create(new[] { '¿'}, new[] { '¿' })},
         { Key.OemBackslash, CharacterConst.EMPTY_TUPLE }, // Equals 102
         { Key.OemMinus, CharacterConst.EMPTY_TUPLE },
         { Key.OemPipe, Tuple.Create(new[] { '¦' }, new[] { '¦' }) },
         { Key.OemSemicolon, CharacterConst.EMPTY_TUPLE }, // = Oem1 = Oem102
         { Key.OemTilde, CharacterConst.EMPTY_TUPLE },
         { Key.Subtract, CharacterConst.EMPTY_TUPLE },
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
