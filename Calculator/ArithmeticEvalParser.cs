using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator
{
    public class ArithmeticEvalParser
    {
        //Сontext sensitive grammar
        //Expression = MultyplyDivide ExpressionOpt.
        //ExpressionOpt = "+" MultyplyDivide ExpressionOpt | "-" MultyplyDivide ExpressionOpt | Λ.
        //MultyplyDivide = ParenthesisNumber MultyplyDivideOpt.
        //MultyplyDivideOpt = "*" ParenthesisNumber MultyplyDivideOpt | "/" ParenthesisNumber MultyplyDivideOpt | Λ.
        //ParenthesisNumber = Real | "(" Expression ")" .

        public double Parse(IEnumerator<Token> ts)
        {
            ts.MoveNext();
            double result = Expression(ts);
            switch (ts.Current.kind)
            {
                case Kind.EoF: return result;
                default: throw new ApplicationException("Parse error: " + ts.Current);
            }
        }
        double Expression(IEnumerator<Token> ts) 
        { 
            return ExpressionOpt(MultyplyDivide(ts), ts); 
        }
        double ExpressionOpt(double value, IEnumerator<Token> ts)
        {
            switch (ts.Current.kind)
            {
                case Kind.Plus:
                    ts.MoveNext(); 
                    return ExpressionOpt(value + MultyplyDivide(ts), ts);
                case Kind.Minus:
                    ts.MoveNext(); 
                    return ExpressionOpt(value - MultyplyDivide(ts), ts);
                default:
                    return value;
            }
        }
        double MultyplyDivide(IEnumerator<Token> ts) 
        { 
            return MultyplyDivideOpt(ParenthesisNumber(ts), ts); 
        }
        double MultyplyDivideOpt(double inval, IEnumerator<Token> ts)
        {
            switch (ts.Current.kind)
            {
                case Kind.Multiply:
                    ts.MoveNext(); 
                    return MultyplyDivideOpt(inval * ParenthesisNumber(ts), ts);
                case Kind.Divide:
                    ts.MoveNext(); 
                    return MultyplyDivideOpt(inval / ParenthesisNumber(ts), ts);
                default:
                    return inval;
            }
        }
        double ParenthesisNumber(IEnumerator<Token> ts)
        {
            switch (ts.Current.kind)
            {
                case Kind.Number:
                    double nval = ts.Current.nval;
                    ts.MoveNext(); 
                    return nval;
                case Kind.LeftParenthesis:
                    ts.MoveNext();
                    double ev = Expression(ts);
                    if (ts.Current.kind != Kind.RightParenthesis)
                    {
                        throw new ApplicationException("Parse error: expected ’)’");
                    }
                    ts.MoveNext(); 
                    return ev;
                default:
                    throw new ApplicationException("Parse error: expected number or ’(’");
            }
        }
    }
}
