using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Calculator
{
    public class Scanner
    {
        static double ScanReal(TextReader reader)
        {
            double n = reader.Read() - '0';
            while (Char.IsDigit((char)reader.Peek()))
                n = 10 * n + reader.Read() - '0';
            if (reader.Peek() == '.' ) {
                reader.Read();
                return ScanFrac(n, 0.1, reader);
            } else
                return n;
        }
        static double ScanFrac(double n, double wt, TextReader reader)
        {
            while (Char.IsDigit((char)reader.Peek()))
            {
                n += wt * (reader.Read() - '0');
                wt /= 10.0;
            }
            return n;
        }
        public IEnumerator<Token> Scan(TextReader reader)
        {
            while (reader.Peek() != -1)
            {
                if (Char.IsWhiteSpace((char)reader.Peek()))
                {
                    reader.Read();
                }
                else if (Char.IsDigit((char)reader.Peek()))
                {
                    yield return Token.FromDouble(ScanReal(reader));
                }
                else
                {
                    char c = (char)reader.Read();
                    switch (c)
                    {
                        case '+': yield return Token.FromKind(Kind.Plus); break;
                        case '-': yield return Token.FromKind(Kind.Minus); break;
                        case '*': yield return Token.FromKind(Kind.Multiply); break;
                        case '/': yield return Token.FromKind(Kind.Divide); break;
                        case '(': yield return Token.FromKind(Kind.LeftParenthesis); break;
                        case ')': yield return Token.FromKind(Kind.RightParenthesis); break;
                        default:
                            throw new ApplicationException("Illegal character: '" + c + "'");
                    }
                }
            }
            yield return Token.FromKind(Kind.EoF);
        }
    }

}
