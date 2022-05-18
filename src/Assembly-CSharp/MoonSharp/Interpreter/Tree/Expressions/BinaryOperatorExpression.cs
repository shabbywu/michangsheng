using System;
using MoonSharp.Interpreter.Execution;
using MoonSharp.Interpreter.Execution.VM;

namespace MoonSharp.Interpreter.Tree.Expressions
{
	// Token: 0x020010BB RID: 4283
	internal class BinaryOperatorExpression : Expression
	{
		// Token: 0x0600676B RID: 26475 RVA: 0x0004726D File Offset: 0x0004546D
		public static object BeginOperatorChain()
		{
			return new BinaryOperatorExpression.LinkedList();
		}

		// Token: 0x0600676C RID: 26476 RVA: 0x00288858 File Offset: 0x00286A58
		public static void AddExpressionToChain(object chain, Expression exp)
		{
			BinaryOperatorExpression.LinkedList list = (BinaryOperatorExpression.LinkedList)chain;
			BinaryOperatorExpression.Node node = new BinaryOperatorExpression.Node
			{
				Expr = exp
			};
			BinaryOperatorExpression.AddNode(list, node);
		}

		// Token: 0x0600676D RID: 26477 RVA: 0x00288880 File Offset: 0x00286A80
		public static void AddOperatorToChain(object chain, Token op)
		{
			BinaryOperatorExpression.LinkedList list = (BinaryOperatorExpression.LinkedList)chain;
			BinaryOperatorExpression.Node node = new BinaryOperatorExpression.Node
			{
				Op = BinaryOperatorExpression.ParseBinaryOperator(op)
			};
			BinaryOperatorExpression.AddNode(list, node);
		}

		// Token: 0x0600676E RID: 26478 RVA: 0x00047274 File Offset: 0x00045474
		public static Expression CommitOperatorChain(object chain, ScriptLoadingContext lcontext)
		{
			return BinaryOperatorExpression.CreateSubTree((BinaryOperatorExpression.LinkedList)chain, lcontext);
		}

		// Token: 0x0600676F RID: 26479 RVA: 0x00047282 File Offset: 0x00045482
		public static Expression CreatePowerExpression(Expression op1, Expression op2, ScriptLoadingContext lcontext)
		{
			return new BinaryOperatorExpression(op1, op2, BinaryOperatorExpression.Operator.Power, lcontext);
		}

		// Token: 0x06006770 RID: 26480 RVA: 0x002888AC File Offset: 0x00286AAC
		private static void AddNode(BinaryOperatorExpression.LinkedList list, BinaryOperatorExpression.Node node)
		{
			list.OperatorMask |= node.Op;
			if (list.Nodes == null)
			{
				list.Last = node;
				list.Nodes = node;
				return;
			}
			list.Last.Next = node;
			node.Prev = list.Last;
			list.Last = node;
		}

		// Token: 0x06006771 RID: 26481 RVA: 0x00288904 File Offset: 0x00286B04
		private static Expression CreateSubTree(BinaryOperatorExpression.LinkedList list, ScriptLoadingContext lcontext)
		{
			BinaryOperatorExpression.Operator operatorMask = list.OperatorMask;
			BinaryOperatorExpression.Node node = list.Nodes;
			if ((operatorMask & BinaryOperatorExpression.Operator.Power) != BinaryOperatorExpression.Operator.NotAnOperator)
			{
				node = BinaryOperatorExpression.PrioritizeRightAssociative(node, lcontext, BinaryOperatorExpression.Operator.Power);
			}
			if ((operatorMask & (BinaryOperatorExpression.Operator.Mul | BinaryOperatorExpression.Operator.Div | BinaryOperatorExpression.Operator.Mod)) != BinaryOperatorExpression.Operator.NotAnOperator)
			{
				node = BinaryOperatorExpression.PrioritizeLeftAssociative(node, lcontext, BinaryOperatorExpression.Operator.Mul | BinaryOperatorExpression.Operator.Div | BinaryOperatorExpression.Operator.Mod);
			}
			if ((operatorMask & (BinaryOperatorExpression.Operator.Add | BinaryOperatorExpression.Operator.Sub)) != BinaryOperatorExpression.Operator.NotAnOperator)
			{
				node = BinaryOperatorExpression.PrioritizeLeftAssociative(node, lcontext, BinaryOperatorExpression.Operator.Add | BinaryOperatorExpression.Operator.Sub);
			}
			if ((operatorMask & BinaryOperatorExpression.Operator.StrConcat) != BinaryOperatorExpression.Operator.NotAnOperator)
			{
				node = BinaryOperatorExpression.PrioritizeRightAssociative(node, lcontext, BinaryOperatorExpression.Operator.StrConcat);
			}
			if ((operatorMask & (BinaryOperatorExpression.Operator.Less | BinaryOperatorExpression.Operator.Greater | BinaryOperatorExpression.Operator.LessOrEqual | BinaryOperatorExpression.Operator.GreaterOrEqual | BinaryOperatorExpression.Operator.NotEqual | BinaryOperatorExpression.Operator.Equal)) != BinaryOperatorExpression.Operator.NotAnOperator)
			{
				node = BinaryOperatorExpression.PrioritizeLeftAssociative(node, lcontext, BinaryOperatorExpression.Operator.Less | BinaryOperatorExpression.Operator.Greater | BinaryOperatorExpression.Operator.LessOrEqual | BinaryOperatorExpression.Operator.GreaterOrEqual | BinaryOperatorExpression.Operator.NotEqual | BinaryOperatorExpression.Operator.Equal);
			}
			if ((operatorMask & BinaryOperatorExpression.Operator.And) != BinaryOperatorExpression.Operator.NotAnOperator)
			{
				node = BinaryOperatorExpression.PrioritizeLeftAssociative(node, lcontext, BinaryOperatorExpression.Operator.And);
			}
			if ((operatorMask & BinaryOperatorExpression.Operator.Or) != BinaryOperatorExpression.Operator.NotAnOperator)
			{
				node = BinaryOperatorExpression.PrioritizeLeftAssociative(node, lcontext, BinaryOperatorExpression.Operator.Or);
			}
			if (node.Next != null || node.Prev != null)
			{
				throw new InternalErrorException("Expression reduction didn't work! - 1");
			}
			if (node.Expr == null)
			{
				throw new InternalErrorException("Expression reduction didn't work! - 2");
			}
			return node.Expr;
		}

		// Token: 0x06006772 RID: 26482 RVA: 0x002889DC File Offset: 0x00286BDC
		private static BinaryOperatorExpression.Node PrioritizeLeftAssociative(BinaryOperatorExpression.Node nodes, ScriptLoadingContext lcontext, BinaryOperatorExpression.Operator operatorsToFind)
		{
			for (BinaryOperatorExpression.Node node = nodes; node != null; node = node.Next)
			{
				BinaryOperatorExpression.Operator op = node.Op;
				if ((op & operatorsToFind) != BinaryOperatorExpression.Operator.NotAnOperator)
				{
					node.Op = BinaryOperatorExpression.Operator.NotAnOperator;
					node.Expr = new BinaryOperatorExpression(node.Prev.Expr, node.Next.Expr, op, lcontext);
					node.Prev = node.Prev.Prev;
					node.Next = node.Next.Next;
					if (node.Next != null)
					{
						node.Next.Prev = node;
					}
					if (node.Prev != null)
					{
						node.Prev.Next = node;
					}
					else
					{
						nodes = node;
					}
				}
			}
			return nodes;
		}

		// Token: 0x06006773 RID: 26483 RVA: 0x00288A84 File Offset: 0x00286C84
		private static BinaryOperatorExpression.Node PrioritizeRightAssociative(BinaryOperatorExpression.Node nodes, ScriptLoadingContext lcontext, BinaryOperatorExpression.Operator operatorsToFind)
		{
			BinaryOperatorExpression.Node node = nodes;
			while (node.Next != null)
			{
				node = node.Next;
			}
			for (BinaryOperatorExpression.Node node2 = node; node2 != null; node2 = node2.Prev)
			{
				BinaryOperatorExpression.Operator op = node2.Op;
				if ((op & operatorsToFind) != BinaryOperatorExpression.Operator.NotAnOperator)
				{
					node2.Op = BinaryOperatorExpression.Operator.NotAnOperator;
					node2.Expr = new BinaryOperatorExpression(node2.Prev.Expr, node2.Next.Expr, op, lcontext);
					node2.Prev = node2.Prev.Prev;
					node2.Next = node2.Next.Next;
					if (node2.Next != null)
					{
						node2.Next.Prev = node2;
					}
					if (node2.Prev != null)
					{
						node2.Prev.Next = node2;
					}
					else
					{
						nodes = node2;
					}
				}
			}
			return nodes;
		}

		// Token: 0x06006774 RID: 26484 RVA: 0x00288B40 File Offset: 0x00286D40
		private static BinaryOperatorExpression.Operator ParseBinaryOperator(Token token)
		{
			TokenType type = token.Type;
			if (type != TokenType.And)
			{
				switch (type)
				{
				case TokenType.Or:
					return BinaryOperatorExpression.Operator.Or;
				case TokenType.Repeat:
				case TokenType.Return:
				case TokenType.Then:
				case TokenType.True:
				case TokenType.Until:
				case TokenType.While:
				case TokenType.Op_Assignment:
					break;
				case TokenType.Op_Equal:
					return BinaryOperatorExpression.Operator.Equal;
				case TokenType.Op_LessThan:
					return BinaryOperatorExpression.Operator.Less;
				case TokenType.Op_LessThanEqual:
					return BinaryOperatorExpression.Operator.LessOrEqual;
				case TokenType.Op_GreaterThanEqual:
					return BinaryOperatorExpression.Operator.GreaterOrEqual;
				case TokenType.Op_GreaterThan:
					return BinaryOperatorExpression.Operator.Greater;
				case TokenType.Op_NotEqual:
					return BinaryOperatorExpression.Operator.NotEqual;
				case TokenType.Op_Concat:
					return BinaryOperatorExpression.Operator.StrConcat;
				default:
					switch (type)
					{
					case TokenType.Op_Pwr:
						return BinaryOperatorExpression.Operator.Power;
					case TokenType.Op_Mod:
						return BinaryOperatorExpression.Operator.Mod;
					case TokenType.Op_Div:
						return BinaryOperatorExpression.Operator.Div;
					case TokenType.Op_Mul:
						return BinaryOperatorExpression.Operator.Mul;
					case TokenType.Op_MinusOrSub:
						return BinaryOperatorExpression.Operator.Sub;
					case TokenType.Op_Add:
						return BinaryOperatorExpression.Operator.Add;
					}
					break;
				}
				throw new InternalErrorException("Unexpected binary operator '{0}'", new object[]
				{
					token.Text
				});
			}
			return BinaryOperatorExpression.Operator.And;
		}

		// Token: 0x06006775 RID: 26485 RVA: 0x00047291 File Offset: 0x00045491
		private BinaryOperatorExpression(Expression exp1, Expression exp2, BinaryOperatorExpression.Operator op, ScriptLoadingContext lcontext) : base(lcontext)
		{
			this.m_Exp1 = exp1;
			this.m_Exp2 = exp2;
			this.m_Operator = op;
		}

		// Token: 0x06006776 RID: 26486 RVA: 0x000472B0 File Offset: 0x000454B0
		private static bool ShouldInvertBoolean(BinaryOperatorExpression.Operator op)
		{
			return op == BinaryOperatorExpression.Operator.NotEqual || op == BinaryOperatorExpression.Operator.GreaterOrEqual || op == BinaryOperatorExpression.Operator.Greater;
		}

		// Token: 0x06006777 RID: 26487 RVA: 0x00288C1C File Offset: 0x00286E1C
		private static OpCode OperatorToOpCode(BinaryOperatorExpression.Operator op)
		{
			if (op <= BinaryOperatorExpression.Operator.Equal)
			{
				if (op <= BinaryOperatorExpression.Operator.LessOrEqual)
				{
					if (op != BinaryOperatorExpression.Operator.Less)
					{
						if (op != BinaryOperatorExpression.Operator.Greater && op != BinaryOperatorExpression.Operator.LessOrEqual)
						{
							goto IL_9F;
						}
						return OpCode.LessEq;
					}
				}
				else if (op != BinaryOperatorExpression.Operator.GreaterOrEqual)
				{
					if (op != BinaryOperatorExpression.Operator.NotEqual && op != BinaryOperatorExpression.Operator.Equal)
					{
						goto IL_9F;
					}
					return OpCode.Eq;
				}
				return OpCode.Less;
			}
			if (op <= BinaryOperatorExpression.Operator.Sub)
			{
				if (op == BinaryOperatorExpression.Operator.StrConcat)
				{
					return OpCode.Concat;
				}
				if (op == BinaryOperatorExpression.Operator.Add)
				{
					return OpCode.Add;
				}
				if (op == BinaryOperatorExpression.Operator.Sub)
				{
					return OpCode.Sub;
				}
			}
			else if (op <= BinaryOperatorExpression.Operator.Div)
			{
				if (op == BinaryOperatorExpression.Operator.Mul)
				{
					return OpCode.Mul;
				}
				if (op == BinaryOperatorExpression.Operator.Div)
				{
					return OpCode.Div;
				}
			}
			else
			{
				if (op == BinaryOperatorExpression.Operator.Mod)
				{
					return OpCode.Mod;
				}
				if (op == BinaryOperatorExpression.Operator.Power)
				{
					return OpCode.Power;
				}
			}
			IL_9F:
			throw new InternalErrorException("Unsupported operator {0}", new object[]
			{
				op
			});
		}

		// Token: 0x06006778 RID: 26488 RVA: 0x00288CE4 File Offset: 0x00286EE4
		public override void Compile(ByteCode bc)
		{
			this.m_Exp1.Compile(bc);
			if (this.m_Operator == BinaryOperatorExpression.Operator.Or)
			{
				Instruction instruction = bc.Emit_Jump(OpCode.JtOrPop, -1, 0);
				this.m_Exp2.Compile(bc);
				instruction.NumVal = bc.GetJumpPointForNextInstruction();
				return;
			}
			if (this.m_Operator == BinaryOperatorExpression.Operator.And)
			{
				Instruction instruction2 = bc.Emit_Jump(OpCode.JfOrPop, -1, 0);
				this.m_Exp2.Compile(bc);
				instruction2.NumVal = bc.GetJumpPointForNextInstruction();
				return;
			}
			if (this.m_Exp2 != null)
			{
				this.m_Exp2.Compile(bc);
			}
			bc.Emit_Operator(BinaryOperatorExpression.OperatorToOpCode(this.m_Operator));
			if (BinaryOperatorExpression.ShouldInvertBoolean(this.m_Operator))
			{
				bc.Emit_Operator(OpCode.Not);
			}
		}

		// Token: 0x06006779 RID: 26489 RVA: 0x00288D90 File Offset: 0x00286F90
		public override DynValue Eval(ScriptExecutionContext context)
		{
			DynValue dynValue = this.m_Exp1.Eval(context).ToScalar();
			if (this.m_Operator == BinaryOperatorExpression.Operator.Or)
			{
				if (dynValue.CastToBool())
				{
					return dynValue;
				}
				return this.m_Exp2.Eval(context).ToScalar();
			}
			else if (this.m_Operator == BinaryOperatorExpression.Operator.And)
			{
				if (!dynValue.CastToBool())
				{
					return dynValue;
				}
				return this.m_Exp2.Eval(context).ToScalar();
			}
			else
			{
				DynValue dynValue2 = this.m_Exp2.Eval(context).ToScalar();
				if ((this.m_Operator & (BinaryOperatorExpression.Operator.Less | BinaryOperatorExpression.Operator.Greater | BinaryOperatorExpression.Operator.LessOrEqual | BinaryOperatorExpression.Operator.GreaterOrEqual | BinaryOperatorExpression.Operator.NotEqual | BinaryOperatorExpression.Operator.Equal)) != BinaryOperatorExpression.Operator.NotAnOperator)
				{
					return DynValue.NewBoolean(this.EvalComparison(dynValue, dynValue2, this.m_Operator));
				}
				if (this.m_Operator != BinaryOperatorExpression.Operator.StrConcat)
				{
					return DynValue.NewNumber(this.EvalArithmetic(dynValue, dynValue2));
				}
				string text = dynValue.CastToString();
				string text2 = dynValue2.CastToString();
				if (text == null || text2 == null)
				{
					throw new DynamicExpressionException("Attempt to perform concatenation on non-strings.");
				}
				return DynValue.NewString(text + text2);
			}
		}

		// Token: 0x0600677A RID: 26490 RVA: 0x00288E74 File Offset: 0x00287074
		private double EvalArithmetic(DynValue v1, DynValue v2)
		{
			double? num = v1.CastToNumber();
			double? num2 = v2.CastToNumber();
			if (num == null || num2 == null)
			{
				throw new DynamicExpressionException("Attempt to perform arithmetic on non-numbers.");
			}
			double value = num.Value;
			double value2 = num2.Value;
			BinaryOperatorExpression.Operator @operator = this.m_Operator;
			if (@operator <= BinaryOperatorExpression.Operator.Sub)
			{
				if (@operator == BinaryOperatorExpression.Operator.Add)
				{
					return value + value2;
				}
				if (@operator == BinaryOperatorExpression.Operator.Sub)
				{
					return value - value2;
				}
			}
			else
			{
				if (@operator == BinaryOperatorExpression.Operator.Mul)
				{
					return value * value2;
				}
				if (@operator == BinaryOperatorExpression.Operator.Div)
				{
					return value / value2;
				}
				if (@operator == BinaryOperatorExpression.Operator.Mod)
				{
					double num3 = Math.IEEERemainder(value, value2);
					if (num3 < 0.0)
					{
						num3 += value2;
					}
					return num3;
				}
			}
			throw new DynamicExpressionException("Unsupported operator {0}", new object[]
			{
				this.m_Operator
			});
		}

		// Token: 0x0600677B RID: 26491 RVA: 0x00288F4C File Offset: 0x0028714C
		private bool EvalComparison(DynValue l, DynValue r, BinaryOperatorExpression.Operator op)
		{
			if (op <= BinaryOperatorExpression.Operator.LessOrEqual)
			{
				if (op != BinaryOperatorExpression.Operator.Less)
				{
					if (op == BinaryOperatorExpression.Operator.Greater)
					{
						return !this.EvalComparison(l, r, BinaryOperatorExpression.Operator.LessOrEqual);
					}
					if (op == BinaryOperatorExpression.Operator.LessOrEqual)
					{
						if (l.Type == DataType.Number && r.Type == DataType.Number)
						{
							return l.Number <= r.Number;
						}
						if (l.Type == DataType.String && r.Type == DataType.String)
						{
							return l.String.CompareTo(r.String) <= 0;
						}
						throw new DynamicExpressionException("Attempt to compare non-numbers, non-strings.");
					}
				}
				else
				{
					if (l.Type == DataType.Number && r.Type == DataType.Number)
					{
						return l.Number < r.Number;
					}
					if (l.Type == DataType.String && r.Type == DataType.String)
					{
						return l.String.CompareTo(r.String) < 0;
					}
					throw new DynamicExpressionException("Attempt to compare non-numbers, non-strings.");
				}
			}
			else
			{
				if (op == BinaryOperatorExpression.Operator.GreaterOrEqual)
				{
					return !this.EvalComparison(l, r, BinaryOperatorExpression.Operator.Less);
				}
				if (op == BinaryOperatorExpression.Operator.NotEqual)
				{
					return !this.EvalComparison(l, r, BinaryOperatorExpression.Operator.Equal);
				}
				if (op == BinaryOperatorExpression.Operator.Equal)
				{
					if (r == l)
					{
						return true;
					}
					if (r.Type != l.Type)
					{
						return (l.Type == DataType.Nil && r.Type == DataType.Void) || (l.Type == DataType.Void && r.Type == DataType.Nil);
					}
					return r.Equals(l);
				}
			}
			throw new DynamicExpressionException("Unsupported operator {0}", new object[]
			{
				op
			});
		}

		// Token: 0x04005F7F RID: 24447
		private const BinaryOperatorExpression.Operator POWER = BinaryOperatorExpression.Operator.Power;

		// Token: 0x04005F80 RID: 24448
		private const BinaryOperatorExpression.Operator MUL_DIV_MOD = BinaryOperatorExpression.Operator.Mul | BinaryOperatorExpression.Operator.Div | BinaryOperatorExpression.Operator.Mod;

		// Token: 0x04005F81 RID: 24449
		private const BinaryOperatorExpression.Operator ADD_SUB = BinaryOperatorExpression.Operator.Add | BinaryOperatorExpression.Operator.Sub;

		// Token: 0x04005F82 RID: 24450
		private const BinaryOperatorExpression.Operator STRCAT = BinaryOperatorExpression.Operator.StrConcat;

		// Token: 0x04005F83 RID: 24451
		private const BinaryOperatorExpression.Operator COMPARES = BinaryOperatorExpression.Operator.Less | BinaryOperatorExpression.Operator.Greater | BinaryOperatorExpression.Operator.LessOrEqual | BinaryOperatorExpression.Operator.GreaterOrEqual | BinaryOperatorExpression.Operator.NotEqual | BinaryOperatorExpression.Operator.Equal;

		// Token: 0x04005F84 RID: 24452
		private const BinaryOperatorExpression.Operator LOGIC_AND = BinaryOperatorExpression.Operator.And;

		// Token: 0x04005F85 RID: 24453
		private const BinaryOperatorExpression.Operator LOGIC_OR = BinaryOperatorExpression.Operator.Or;

		// Token: 0x04005F86 RID: 24454
		private Expression m_Exp1;

		// Token: 0x04005F87 RID: 24455
		private Expression m_Exp2;

		// Token: 0x04005F88 RID: 24456
		private BinaryOperatorExpression.Operator m_Operator;

		// Token: 0x020010BC RID: 4284
		[Flags]
		private enum Operator
		{
			// Token: 0x04005F8A RID: 24458
			NotAnOperator = 0,
			// Token: 0x04005F8B RID: 24459
			Or = 1,
			// Token: 0x04005F8C RID: 24460
			And = 2,
			// Token: 0x04005F8D RID: 24461
			Less = 4,
			// Token: 0x04005F8E RID: 24462
			Greater = 8,
			// Token: 0x04005F8F RID: 24463
			LessOrEqual = 16,
			// Token: 0x04005F90 RID: 24464
			GreaterOrEqual = 32,
			// Token: 0x04005F91 RID: 24465
			NotEqual = 64,
			// Token: 0x04005F92 RID: 24466
			Equal = 128,
			// Token: 0x04005F93 RID: 24467
			StrConcat = 256,
			// Token: 0x04005F94 RID: 24468
			Add = 512,
			// Token: 0x04005F95 RID: 24469
			Sub = 1024,
			// Token: 0x04005F96 RID: 24470
			Mul = 4096,
			// Token: 0x04005F97 RID: 24471
			Div = 8192,
			// Token: 0x04005F98 RID: 24472
			Mod = 16384,
			// Token: 0x04005F99 RID: 24473
			Power = 32768
		}

		// Token: 0x020010BD RID: 4285
		private class Node
		{
			// Token: 0x04005F9A RID: 24474
			public Expression Expr;

			// Token: 0x04005F9B RID: 24475
			public BinaryOperatorExpression.Operator Op;

			// Token: 0x04005F9C RID: 24476
			public BinaryOperatorExpression.Node Prev;

			// Token: 0x04005F9D RID: 24477
			public BinaryOperatorExpression.Node Next;
		}

		// Token: 0x020010BE RID: 4286
		private class LinkedList
		{
			// Token: 0x04005F9E RID: 24478
			public BinaryOperatorExpression.Node Nodes;

			// Token: 0x04005F9F RID: 24479
			public BinaryOperatorExpression.Node Last;

			// Token: 0x04005FA0 RID: 24480
			public BinaryOperatorExpression.Operator OperatorMask;
		}
	}
}
