using System;
using MoonSharp.Interpreter.Execution;
using MoonSharp.Interpreter.Execution.VM;

namespace MoonSharp.Interpreter.Tree.Expressions
{
	// Token: 0x020010C4 RID: 4292
	internal class IndexExpression : Expression, IVariable
	{
		// Token: 0x0600679A RID: 26522 RVA: 0x000473B8 File Offset: 0x000455B8
		public IndexExpression(Expression baseExp, Expression indexExp, ScriptLoadingContext lcontext) : base(lcontext)
		{
			this.m_BaseExp = baseExp;
			this.m_IndexExp = indexExp;
		}

		// Token: 0x0600679B RID: 26523 RVA: 0x000473CF File Offset: 0x000455CF
		public IndexExpression(Expression baseExp, string name, ScriptLoadingContext lcontext) : base(lcontext)
		{
			this.m_BaseExp = baseExp;
			this.m_Name = name;
		}

		// Token: 0x0600679C RID: 26524 RVA: 0x00289930 File Offset: 0x00287B30
		public override void Compile(ByteCode bc)
		{
			this.m_BaseExp.Compile(bc);
			if (this.m_Name != null)
			{
				bc.Emit_Index(DynValue.NewString(this.m_Name), true, false);
				return;
			}
			if (this.m_IndexExp is LiteralExpression)
			{
				LiteralExpression literalExpression = (LiteralExpression)this.m_IndexExp;
				bc.Emit_Index(literalExpression.Value, false, false);
				return;
			}
			this.m_IndexExp.Compile(bc);
			bc.Emit_Index(null, false, this.m_IndexExp is ExprListExpression);
		}

		// Token: 0x0600679D RID: 26525 RVA: 0x002899B4 File Offset: 0x00287BB4
		public void CompileAssignment(ByteCode bc, int stackofs, int tupleidx)
		{
			this.m_BaseExp.Compile(bc);
			if (this.m_Name != null)
			{
				bc.Emit_IndexSet(stackofs, tupleidx, DynValue.NewString(this.m_Name), true, false);
				return;
			}
			if (this.m_IndexExp is LiteralExpression)
			{
				LiteralExpression literalExpression = (LiteralExpression)this.m_IndexExp;
				bc.Emit_IndexSet(stackofs, tupleidx, literalExpression.Value, false, false);
				return;
			}
			this.m_IndexExp.Compile(bc);
			bc.Emit_IndexSet(stackofs, tupleidx, null, false, this.m_IndexExp is ExprListExpression);
		}

		// Token: 0x0600679E RID: 26526 RVA: 0x00289A3C File Offset: 0x00287C3C
		public override DynValue Eval(ScriptExecutionContext context)
		{
			DynValue dynValue = this.m_BaseExp.Eval(context).ToScalar();
			DynValue dynValue2 = (this.m_IndexExp != null) ? this.m_IndexExp.Eval(context).ToScalar() : DynValue.NewString(this.m_Name);
			if (dynValue.Type != DataType.Table)
			{
				throw new DynamicExpressionException("Attempt to index non-table.");
			}
			if (dynValue2.IsNilOrNan())
			{
				throw new DynamicExpressionException("Attempt to index with nil or nan key.");
			}
			return dynValue.Table.Get(dynValue2) ?? DynValue.Nil;
		}

		// Token: 0x04005FB4 RID: 24500
		private Expression m_BaseExp;

		// Token: 0x04005FB5 RID: 24501
		private Expression m_IndexExp;

		// Token: 0x04005FB6 RID: 24502
		private string m_Name;
	}
}
