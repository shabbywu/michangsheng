using System;
using MoonSharp.Interpreter.Execution;
using MoonSharp.Interpreter.Execution.VM;

namespace MoonSharp.Interpreter.Tree.Expressions
{
	// Token: 0x02000CE7 RID: 3303
	internal class IndexExpression : Expression, IVariable
	{
		// Token: 0x06005C84 RID: 23684 RVA: 0x002604B2 File Offset: 0x0025E6B2
		public IndexExpression(Expression baseExp, Expression indexExp, ScriptLoadingContext lcontext) : base(lcontext)
		{
			this.m_BaseExp = baseExp;
			this.m_IndexExp = indexExp;
		}

		// Token: 0x06005C85 RID: 23685 RVA: 0x002604C9 File Offset: 0x0025E6C9
		public IndexExpression(Expression baseExp, string name, ScriptLoadingContext lcontext) : base(lcontext)
		{
			this.m_BaseExp = baseExp;
			this.m_Name = name;
		}

		// Token: 0x06005C86 RID: 23686 RVA: 0x002604E0 File Offset: 0x0025E6E0
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

		// Token: 0x06005C87 RID: 23687 RVA: 0x00260564 File Offset: 0x0025E764
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

		// Token: 0x06005C88 RID: 23688 RVA: 0x002605EC File Offset: 0x0025E7EC
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

		// Token: 0x040053B0 RID: 21424
		private Expression m_BaseExp;

		// Token: 0x040053B1 RID: 21425
		private Expression m_IndexExp;

		// Token: 0x040053B2 RID: 21426
		private string m_Name;
	}
}
