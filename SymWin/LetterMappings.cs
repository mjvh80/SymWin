using System;
using System.Collections.Generic;
/*
 * © Marcus van Houdt 2014
 */

using System.Windows.Input;

namespace SymWin
{
   public static class LetterMappings
   {
      public static readonly Dictionary<Key, Char[]> KeysToSymbols = new Dictionary<Key, Char[]>
      {
         // todo: different symbols for caps, e.g. Ð ?

         // Initial rough mapping for letters, not too much thought has gone into this yet.

         { Key.A, new[] { 'ä', 'å', 'à', 'á', 'α', 'æ' }},
         { Key.B, new[] { 'β' }},
         { Key.C, new[] { 'ç', 'γ', '©' }},
         { Key.D, new[] { 'Þ', 'ð', 'δ' }},
         { Key.E, new[] { 'ë', 'è', 'é', 'ê', 'ε', 'η' }},
         { Key.F, new[] { 'ƒ', 'θ' }},
         // { 'g', new[] { '' }}
         // h -> astro h?
         { Key.I, new[] { 'ì', 'í', 'î', 'ï', 'ι' }},
         // j
         { Key.L, new[] { 'λ' }},
         { Key.M, new[] { 'µ' }},
         { Key.K, new[] { 'κ' }},
         { Key.N, new[] { 'ñ', 'ν' }},
         { Key.O, new[] { 'ö', 'ò', 'ó', 'ô', 'õ', 'ø' }},
         { Key.P, new[] { 'π', '¶' }},
         // { 'q', new[] {} },
         { Key.R, new[] { '®', 'ρ' }},
         { Key.S, new[] { 'ß', 'š', 'σ' }},
         { Key.T, new[] { 'τ', '™' }},
         { Key.U, new[] { 'ù', 'ú', 'û', 'ü' }},
         // { 'v', new[] {}},
         { Key.W, new[] { 'ω' }},
         { Key.X, new[] { 'χ', '×' }},
         { Key.Y, new[] { 'ý', 'ÿ'}},
         { Key.Z, new[] { 'ζ' }},

         // Numbers
         { Key.D0, new[]  { '☺', '☻', '∞', 'ø' }},
         { Key.D1, new[] { '¡', '‼', '¹' }},
         { Key.D2, new[] { '²', '½', '√' }},
         { Key.D3, new[] { '³', '§' }},
         { Key.D4, new[] { '£', '¥', '$', '¤' }},
         { Key.D5, new[] { '‰', '♫', '♪' }},
         { Key.D8, new[] { '★', '✼', '❀' }},

         { Key.D9, new[] { '☹' }},
         { Key.OemCloseBrackets, new[] { '☹' }},

         { Key.Divide, new[]  {'÷'}},
         { Key.OemQuotes, new[] { '«', '»'}},
         { Key.OemPeriod, new[]  { '…', '∙', '●', '≤'}},
         { Key.OemComma, new[] { '≥' }},
         { Key.OemPlus, new[] { '≈', '≠' }},

      };

      public static void UpdateKey(Key key, Char[] letters)
      {
         KeysToSymbols[key] = letters;
         KeyToWindowMap[key] = new LetterSelector(key, letters);
      }

      public static Dictionary<Key, LetterSelector> KeyToWindowMap { get; private set; }

      /// <summary>
      /// Constructs a popup window for each of the letter mappings.
      /// </summary>
      public static void InitializeWindows()
      {
         KeyToWindowMap = new Dictionary<Key, LetterSelector>();

         foreach (var kvp in KeysToSymbols)
         {
            KeyToWindowMap.Add(kvp.Key, new LetterSelector(kvp.Key, kvp.Value));
         }
      }
   }
}
