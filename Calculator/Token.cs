using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator
{
    public struct Token
    {
        public readonly Kind kind;
        public readonly double nval;
        private Token(Kind k) { kind = k; nval = 0; }
        private Token(double n) { kind = Kind.Number; nval = n; }
        public override string ToString()
        {
            if (kind == Kind.Number) return "Number(" + nval + ")";
            else return kind.ToString();
        }
        static public Token FromKind(Kind k) { return new Token(k); }
        static public Token FromDouble(double d) { return new Token(d); }
    }
}
