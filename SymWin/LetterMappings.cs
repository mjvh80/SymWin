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
         { Key.A, Tuple.Create(new[] { '≠','≈','≝','≡','≤','≥','≪','≫' }, new[] {'≠','≈','≝','≡','≤','≥','≪','≫' })},
         { Key.B, _sEmpty},
         { Key.C, _sEmpty},
         { Key.D, _sEmpty},
         { Key.E, _sEmpty},
         { Key.F, Tuple.Create(new[] { 'ƒ', 'θ' }, new[] { 'Ƒ', 'Θ' })},
         { Key.G, Tuple.Create(new [] { 'α','β','γ','δ', 'ε','ζ','η','θ', 'κ', 'λ','μ','π','ρ','σ','τ','υ', 'φ', 'χ', 'ψ', 'ω' },
                                   new[] {  'α','β','γ','δ', 'ε','ζ','η','θ', 'κ', 'λ','μ','π','ρ','σ','τ','υ', 'φ', 'χ', 'ψ', 'ω'  })},
         { Key.H, _sEmpty},
         { Key.I, _sEmpty},
         { Key.J, _sEmpty},
         { Key.L, _sEmpty},
         { Key.M, _sEmpty},
         { Key.K, _sEmpty},
         { Key.N, _sEmpty},
         { Key.O, Tuple.Create(new[] { '∈','∉','⊂','⊄','⊆','⊈','⊃','∪','∩','∖','∅' },
                               new[] { '¬','∨','∧','⊕','↑','∀','∃','⇒','→','⇔','↔'})},
         { Key.P, Tuple.Create(new[] { 'π', '¶' }, new[] { 'Π', '¶' })},
         { Key.Q, Tuple.Create(new[] { '−','±','·','×','÷','²','³','√','∛','′','″','‴','∏','∑' }, new[] { '−','±','·','×','÷','²','³','√','∛','′','″','‴','∏','∑' })},
         { Key.R, _sEmpty},
         { Key.S, Tuple.Create(new[] { 'ℂ','ℕ','ℙ','ℚ','ℝ','ℤ' }, new[] { 'ℂ','ℕ','ℙ','ℚ','ℝ','ℤ' })},
         { Key.T, _sEmpty},
         { Key.U, _sEmpty},
         { Key.V, _sEmpty},
         { Key.W, _sEmpty},
         { Key.X, _sEmpty},
         { Key.Y, _sEmpty},
         { Key.Z, _sEmpty},

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
