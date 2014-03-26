using System;
using System.Collections.Generic;
/*
 * © Marcus van Houdt 2014
 */

using System.Windows.Input;

namespace SymWin
{
   internal static class LetterMappings
   {
      public static readonly Dictionary<Char, Char[]> LetterToSymbols = new Dictionary<Char, Char[]>
      {
         // ξοςυφψ

         // Initial rough mapping for letters, not too much thought has gone into this yet.

         { 'a', new[] { 'ä', 'å', 'à', 'á', 'α', 'æ' }},
         { 'b', new[] { 'β' }},
         { 'c', new[] { 'ç', 'γ' }},
         { 'd', new[] { 'Þ', 'ð', 'δ' }},
         { 'e', new[] { 'è', 'é', 'ê', 'ë', 'ε', 'η' }},
         { 'f', new[] { 'ƒ', 'θ' }},
         // { 'g', new[] { '' }}
         // h -> astro h?
         { 'i', new[] { 'ì', 'í', 'î', 'ï', 'ι' }},
         // j
         { 'l', new[] { 'λ' }},
         { 'm', new[] { 'µ' }},
         { 'k', new[] { 'κ' }},
         { 'n', new[] { 'ν' }},
         { 'o', new[] { 'ò', 'ó', 'ô', 'õ', 'ö', 'ø' }},
         { 'p', new[] { 'π' }},
         // { 'q', new[] {} },
         { 'r', new[] { 'ρ' }},
         { 's', new[] { 'ß', 'š', 'σ' }},
         { 't', new[] { 'τ' }},
         { 'u', new[] { 'ù', 'ú', 'û', 'ü' }},
         // { 'v', new[] {}},
         { 'w', new[] { 'ω' }},
         { 'x', new[] { 'χ' }},
         { 'y', new[] { 'ý', 'ÿ'}},
         { 'z', new[] { 'ζ' }}
      };

      public static Key LetterToKey(Char letter)
      {
         // obviously wont work for anything other than alphabet chars etc
         return (Key)((Int32)Key.A + ((Int32)letter - (Int32)'a'));
      }

      public static Char KeyToLetter(Key key)
      {
         return (Char)('a' + (key - Key.A));
      }

      public static Dictionary<Char, LetterSelector> LettersToWindow { get; private set; }

      /// <summary>
      /// Constructs a popup window for each of the letter mappings.
      /// </summary>
      public static void InitializeWindows()
      {
         LettersToWindow = new Dictionary<Char, LetterSelector>();

         foreach (var kvp in LetterToSymbols)
         {
            LettersToWindow.Add(kvp.Key, new LetterSelector(kvp.Value));
         }
      }
   }
}
