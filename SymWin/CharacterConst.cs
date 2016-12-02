using System;

namespace SymWin
{
    public class CharacterConst
    {
        public static readonly char[] OPERATION_CHARS = { '⊕', '¬', '÷', '±', '∕', '∘', '∙', '⋅', '∓' };

        public static readonly char[] NEGATED_RELATIONS_CHARS = { '∉', '∌', '⊄', '⊅', '⊈', '⊊', '⊋', '⊉', '≮', '≯', '≰', '≱', '≢', '≁', '≄', '≉', '≇', '≭', '≨', '≩', '⊀', '⊁', '⋠', '⋡', '⋢', '⋣', '⋦', '⋧', '⋨', '⋩', '⋪', '⋫', '⋬', '⋭', '∤', '∦', '⊬', '⊭', '⊮', '⊯', '∄' };

        public static readonly char[] RELATIONS_CHARS = { '∀', '∃', '∈', '∋', '⊂', '⊃', '⊆', '⊇', '<', '>', '≤', '≥', '≡', '∼', '≃', '≈', '≅', '≍', '≪', '≺', '≻', '≼', '≽', '⊏', '⊐', '⊑', '⊒', '∥', '⊥', '⊢', '⊣', '⋈', '×', '∝', '∩', '∪', '⊎', '⊓', '⊔', '∧', '∨' };

        public static readonly char[] GREEK_LETTERS = { 'α', 'β', 'γ', 'δ', 'ε', 'ζ', 'η', 'θ', 'κ', 'λ', 'μ', 'π', 'ρ', 'σ', 'τ', 'υ', 'φ', 'χ', 'ψ', 'ω' };

        public static readonly char[] DOUBLE_STRUCK_CHARS = { 'ℂ', 'ℕ', 'ℙ', 'ℚ', 'ℝ', 'ℤ' };

        public static readonly Tuple<Char[], Char[]> EMPTY= Tuple.Create(new Char[0], new Char[0]);
    }
}
