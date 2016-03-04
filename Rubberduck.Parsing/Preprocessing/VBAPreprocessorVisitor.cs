﻿using Antlr4.Runtime.Misc;
using System;
using System.Globalization;
using System.Linq;

namespace Rubberduck.Parsing.Preprocessing
{
    public sealed class VBAPreprocessorVisitor : VBAConditionalCompilationBaseVisitor<IExpression>
    {
        private readonly SymbolTable<string, IValue> _symbolTable;
        private readonly VBAOptionCompare _optionCompare;

        public VBAPreprocessorVisitor(
            SymbolTable<string, IValue> symbolTable, 
            VBAPredefinedCompilationConstants predefinedConstants,
            VBAOptionCompare optionCompare)
        {
            _symbolTable = symbolTable;
            _symbolTable.Add(VBAPredefinedCompilationConstants.VBA6_NAME, new BoolValue(predefinedConstants.VBA6));
            _symbolTable.Add(VBAPredefinedCompilationConstants.VBA7_NAME, new BoolValue(predefinedConstants.VBA7));
            _symbolTable.Add(VBAPredefinedCompilationConstants.WIN64_NAME, new BoolValue(predefinedConstants.Win64));
            _symbolTable.Add(VBAPredefinedCompilationConstants.WIN32_NAME, new BoolValue(predefinedConstants.Win32));
            _symbolTable.Add(VBAPredefinedCompilationConstants.WIN16_NAME, new BoolValue(predefinedConstants.Win16));
            _symbolTable.Add(VBAPredefinedCompilationConstants.MAC_NAME, new BoolValue(predefinedConstants.Mac));
            _optionCompare = optionCompare;
        }

        public override IExpression VisitCompilationUnit([NotNull] VBAConditionalCompilationParser.CompilationUnitContext context)
        {
            return Visit(context.ccBlock());
        }

        public override IExpression VisitLogicalLine([NotNull] VBAConditionalCompilationParser.LogicalLineContext context)
        {
            return new ConstantExpression(new StringValue(context.GetText()));
        }

        public override IExpression VisitCcBlock([NotNull] VBAConditionalCompilationParser.CcBlockContext context)
        {
            if (context.children == null)
            {
                return new ConstantExpression(EmptyValue.Value);
            }
            return new ConditionalCompilationBlockExpression(context.children.Select(child => Visit(child)).ToList());
        }

        public override IExpression VisitCcConst([NotNull] VBAConditionalCompilationParser.CcConstContext context)
        {
            return new ConditionalCompilationConstantExpression(
                    new ConstantExpression(new StringValue(context.GetText())),
                    new ConstantExpression(new StringValue(context.ccVarLhs().name().IDENTIFIER().GetText())),
                    Visit(context.ccExpression()),
                    _symbolTable);
        }

        public override IExpression VisitCcIfBlock([NotNull] VBAConditionalCompilationParser.CcIfBlockContext context)
        {
            var ifCondCode = new ConstantExpression(new StringValue(context.ccIf().GetText()));
            var ifCond = Visit(context.ccIf().ccExpression());
            var ifBlock = Visit(context.ccBlock());
            var elseIfCodeCondBlocks = context
                .ccElseIfBlock()
                .Select(elseIf =>
                        Tuple.Create<IExpression, IExpression, IExpression>(
                            new ConstantExpression(new StringValue(elseIf.ccElseIf().GetText())),
                            Visit(elseIf.ccElseIf().ccExpression()),
                            Visit(elseIf.ccBlock()))).ToList();

            IExpression elseCondCode = null;
            IExpression elseBlock = null;
            if (context.ccElseBlock() != null)
            {
                elseCondCode = new ConstantExpression(new StringValue(context.ccElseBlock().ccElse().GetText()));
                elseBlock = Visit(context.ccElseBlock().ccBlock());
            }
            IExpression endIf = new ConstantExpression(new StringValue(context.ccEndIf().GetText()));
            return new ConditionalCompilationIfExpression(
                    ifCondCode,
                    ifCond,
                    ifBlock,
                    elseIfCodeCondBlocks,
                    elseCondCode,
                    elseBlock,
                    endIf);
        }

        private IExpression Visit(VBAConditionalCompilationParser.NameContext context)
        {
            return new NameExpression(
                new ConstantExpression(new StringValue(context.IDENTIFIER().GetText())),
                _symbolTable);
        }

        private IExpression VisitUnaryMinus(VBAConditionalCompilationParser.CcExpressionContext context)
        {
            return new UnaryMinusExpression(Visit(context.ccExpression()[0]));
        }

        private IExpression VisitUnaryNot(VBAConditionalCompilationParser.CcExpressionContext context)
        {
            return new UnaryNotExpression(Visit(context.ccExpression()[0]));
        }

        private IExpression VisitPlus(VBAConditionalCompilationParser.CcExpressionContext context)
        {
            return new BinaryPlusExpression(Visit(context.ccExpression()[0]), Visit(context.ccExpression()[1]));
        }

        private IExpression VisitMinus(VBAConditionalCompilationParser.CcExpressionContext context)
        {
            return new BinaryMinusExpression(Visit(context.ccExpression()[0]), Visit(context.ccExpression()[1]));
        }

        private IExpression Visit(VBAConditionalCompilationParser.CcExpressionContext context)
        {
            if (context.literal() != null)
            {
                return Visit(context.literal());
            }
            else if (context.name() != null)
            {
                return Visit(context.name());
            }
            else if (context.L_PAREN() != null)
            {
                return Visit(context.ccExpression()[0]);
            }
            else if (context.MINUS() != null && context.ccExpression().Count == 1)
            {
                return VisitUnaryMinus(context);
            }
            else if (context.NOT() != null)
            {
                return VisitUnaryNot(context);
            }
            else if (context.PLUS() != null)
            {
                return VisitPlus(context);
            }
            else if (context.MINUS() != null && context.ccExpression().Count == 2)
            {
                return VisitMinus(context);
            }
            else if (context.MULT() != null)
            {
                return VisitMult(context);
            }
            else if (context.DIV() != null)
            {
                return VisitDiv(context);
            }
            else if (context.INTDIV() != null)
            {
                return VisitIntDiv(context);
            }
            else if (context.MOD() != null)
            {
                return VisitMod(context);
            }
            else if (context.POW() != null)
            {
                return VisitPow(context);
            }
            else if (context.AMPERSAND() != null)
            {
                return VisitConcat(context);
            }
            else if (context.EQ() != null)
            {
                return VisitEq(context);
            }
            else if (context.NEQ() != null)
            {
                return VisitNeq(context);
            }
            else if (context.LT() != null)
            {
                return VisitLt(context);
            }
            else if (context.GT() != null)
            {
                return VisitGt(context);
            }
            else if (context.LEQ() != null)
            {
                return VisitLeq(context);
            }
            else if (context.GEQ() != null)
            {
                return VisitGeq(context);
            }
            else if (context.AND() != null)
            {
                return VisitAnd(context);
            }
            else if (context.OR() != null)
            {
                return VisitOr(context);
            }
            else if (context.XOR() != null)
            {
                return VisitXor(context);
            }
            else if (context.EQV() != null)
            {
                return VisitEqv(context);
            }
            else if (context.IMP() != null)
            {
                return VisitImp(context);
            }
            else if (context.IS() != null)
            {
                return VisitIs(context);
            }
            else if (context.LIKE() != null)
            {
                return VisitLike(context);
            }
            else
            {
                return VisitLibraryFunction(context);
            }
        }

        private IExpression VisitLibraryFunction(VBAConditionalCompilationParser.CcExpressionContext context)
        {
            var intrinsicFunction = context.intrinsicFunction();
            var functionName = intrinsicFunction.intrinsicFunctionName().GetText();
            var argument = Visit(intrinsicFunction.ccExpression());
            return VBAEnvironment.CreateLibraryFunction(functionName, argument);
        }

        private IExpression VisitLike(VBAConditionalCompilationParser.CcExpressionContext context)
        {
            var expr = Visit(context.ccExpression()[0]);
            var pattern = Visit(context.ccExpression()[1]);
            return new LikeExpression(expr, pattern);
        }

        private IExpression VisitIs(VBAConditionalCompilationParser.CcExpressionContext context)
        {
            var left = Visit(context.ccExpression()[0]);
            var right = Visit(context.ccExpression()[1]);
            return new IsExpression(left, right);
        }

        private IExpression VisitImp(VBAConditionalCompilationParser.CcExpressionContext context)
        {
            var left = Visit(context.ccExpression()[0]);
            var right = Visit(context.ccExpression()[1]);
            return new LogicalImpExpression(left, right);
        }

        private IExpression VisitEqv(VBAConditionalCompilationParser.CcExpressionContext context)
        {
            var left = Visit(context.ccExpression()[0]);
            var right = Visit(context.ccExpression()[1]);
            return new LogicalEqvExpression(left, right);
        }

        private IExpression VisitXor(VBAConditionalCompilationParser.CcExpressionContext context)
        {
            var left = Visit(context.ccExpression()[0]);
            var right = Visit(context.ccExpression()[1]);
            return new LogicalXorExpression(left, right);
        }

        private IExpression VisitOr(VBAConditionalCompilationParser.CcExpressionContext context)
        {
            var left = Visit(context.ccExpression()[0]);
            var right = Visit(context.ccExpression()[1]);
            return new LogicalOrExpression(left, right);
        }

        private IExpression VisitAnd(VBAConditionalCompilationParser.CcExpressionContext context)
        {
            var left = Visit(context.ccExpression()[0]);
            var right = Visit(context.ccExpression()[1]);
            return new LogicalAndExpression(left, right);
        }

        private IExpression VisitGeq(VBAConditionalCompilationParser.CcExpressionContext context)
        {
            var left = Visit(context.ccExpression()[0]);
            var right = Visit(context.ccExpression()[1]);
            return new LogicalGreaterOrEqualsExpression(left, right, _optionCompare);
        }

        private IExpression VisitLeq(VBAConditionalCompilationParser.CcExpressionContext context)
        {
            var left = Visit(context.ccExpression()[0]);
            var right = Visit(context.ccExpression()[1]);
            return new LogicalLessOrEqualsExpression(left, right, _optionCompare);
        }

        private IExpression VisitGt(VBAConditionalCompilationParser.CcExpressionContext context)
        {
            var left = Visit(context.ccExpression()[0]);
            var right = Visit(context.ccExpression()[1]);
            return new LogicalGreaterThanExpression(left, right, _optionCompare);
        }

        private IExpression VisitLt(VBAConditionalCompilationParser.CcExpressionContext context)
        {
            var left = Visit(context.ccExpression()[0]);
            var right = Visit(context.ccExpression()[1]);
            return new LogicalLessThanExpression(left, right, _optionCompare);
        }

        private IExpression VisitNeq(VBAConditionalCompilationParser.CcExpressionContext context)
        {
            var left = Visit(context.ccExpression()[0]);
            var right = Visit(context.ccExpression()[1]);
            return new LogicalNotEqualsExpression(left, right, _optionCompare);
        }

        private IExpression VisitEq(VBAConditionalCompilationParser.CcExpressionContext context)
        {
            var left = Visit(context.ccExpression()[0]);
            var right = Visit(context.ccExpression()[1]);
            return new LogicalEqualsExpression(left, right, _optionCompare);
        }

        private IExpression VisitConcat(VBAConditionalCompilationParser.CcExpressionContext context)
        {
            var left = Visit(context.ccExpression()[0]);
            var right = Visit(context.ccExpression()[1]);
            return new ConcatExpression(left, right);
        }

        private IExpression VisitPow(VBAConditionalCompilationParser.CcExpressionContext context)
        {
            var left = Visit(context.ccExpression()[0]);
            var right = Visit(context.ccExpression()[1]);
            return new PowExpression(left, right);
        }

        private IExpression VisitMod(VBAConditionalCompilationParser.CcExpressionContext context)
        {
            var left = Visit(context.ccExpression()[0]);
            var right = Visit(context.ccExpression()[1]);
            return new ModExpression(left, right);
        }

        private IExpression VisitIntDiv(VBAConditionalCompilationParser.CcExpressionContext context)
        {
            var left = Visit(context.ccExpression()[0]);
            var right = Visit(context.ccExpression()[1]);
            return new BinaryIntDivExpression(left, right);
        }

        private IExpression VisitMult(VBAConditionalCompilationParser.CcExpressionContext context)
        {
            var left = Visit(context.ccExpression()[0]);
            var right = Visit(context.ccExpression()[1]);
            return new BinaryMultiplicationExpression(left, right);
        }

        private IExpression VisitDiv(VBAConditionalCompilationParser.CcExpressionContext context)
        {
            var left = Visit(context.ccExpression()[0]);
            var right = Visit(context.ccExpression()[1]);
            return new BinaryDivisionExpression(left, right);
        }

        private IExpression Visit(VBAConditionalCompilationParser.LiteralContext context)
        {
            if (context.HEXLITERAL() != null)
            {
                return VisitHexLiteral(context);
            }
            else if (context.OCTLITERAL() != null)
            {
                return VisitOctLiteral(context);
            }
            else if (context.DATELITERAL() != null)
            {
                return VisitDateLiteral(context);
            }
            else if (context.DOUBLELITERAL() != null)
            {
                return VisitDoubleLiteral(context);
            }
            else if (context.INTEGERLITERAL() != null)
            {
                return VisitIntegerLiteral(context);
            }
            else if (context.SHORTLITERAL() != null)
            {
                return VisitShortLiteral(context);
            }
            else if (context.STRINGLITERAL() != null)
            {
                return VisitStringLiteral(context);
            }
            else if (context.TRUE() != null)
            {
                return new ConstantExpression(new BoolValue(true));
            }
            else if (context.FALSE() != null)
            {
                return new ConstantExpression(new BoolValue(false));
            }
            else if (context.NOTHING() != null || context.NULL() != null)
            {
                return new ConstantExpression(null);
            }
            else if (context.EMPTY() != null)
            {
                return new ConstantExpression(EmptyValue.Value);
            }
            throw new Exception(string.Format("Unexpected literal encountered: {0}", context.GetText()));
        }

        private IExpression VisitStringLiteral(VBAConditionalCompilationParser.LiteralContext context)
        {
            return new StringLiteralExpression(new ConstantExpression(new StringValue(context.STRINGLITERAL().GetText())));
        }

        private IExpression VisitShortLiteral(VBAConditionalCompilationParser.LiteralContext context)
        {
            return new NumberLiteralExpression(new ConstantExpression(new StringValue(context.SHORTLITERAL().GetText())));
        }

        private IExpression VisitIntegerLiteral(VBAConditionalCompilationParser.LiteralContext context)
        {
            return new NumberLiteralExpression(new ConstantExpression(new StringValue(context.INTEGERLITERAL().GetText())));
        }

        private IExpression VisitDoubleLiteral(VBAConditionalCompilationParser.LiteralContext context)
        {
            return new NumberLiteralExpression(new ConstantExpression(new StringValue(context.DOUBLELITERAL().GetText())));
        }

        private IExpression VisitDateLiteral(VBAConditionalCompilationParser.LiteralContext context)
        {
            return new DateLiteralExpression(new ConstantExpression(new StringValue(context.DATELITERAL().GetText())));
        }

        private IExpression VisitOctLiteral(VBAConditionalCompilationParser.LiteralContext context)
        {
            return new OctNumberLiteralExpression(new ConstantExpression(new StringValue(context.OCTLITERAL().GetText())));
        }

        private IExpression VisitHexLiteral(VBAConditionalCompilationParser.LiteralContext context)
        {
            return new HexNumberLiteralExpression(new ConstantExpression(new StringValue(context.HEXLITERAL().GetText())));
        }
    }
}
