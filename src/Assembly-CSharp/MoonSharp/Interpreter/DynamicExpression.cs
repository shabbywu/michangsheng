using System;
using MoonSharp.Interpreter.Tree.Expressions;

namespace MoonSharp.Interpreter
{
	// Token: 0x02001080 RID: 4224
	public class DynamicExpression : IScriptPrivateResource
	{
		// Token: 0x0600661D RID: 26141 RVA: 0x000466E1 File Offset: 0x000448E1
		internal DynamicExpression(Script S, string strExpr, DynamicExprExpression expr)
		{
			this.ExpressionCode = strExpr;
			this.OwnerScript = S;
			this.m_Exp = expr;
		}

		// Token: 0x0600661E RID: 26142 RVA: 0x000466FE File Offset: 0x000448FE
		internal DynamicExpression(Script S, string strExpr, DynValue constant)
		{
			this.ExpressionCode = strExpr;
			this.OwnerScript = S;
			this.m_Constant = constant;
		}

		// Token: 0x0600661F RID: 26143 RVA: 0x0004671B File Offset: 0x0004491B
		public DynValue Evaluate(ScriptExecutionContext context = null)
		{
			context = (context ?? this.OwnerScript.CreateDynamicExecutionContext(null));
			this.CheckScriptOwnership(context.GetScript());
			if (this.m_Constant != null)
			{
				return this.m_Constant;
			}
			return this.m_Exp.Eval(context);
		}

		// Token: 0x06006620 RID: 26144 RVA: 0x00046757 File Offset: 0x00044957
		public SymbolRef FindSymbol(ScriptExecutionContext context)
		{
			this.CheckScriptOwnership(context.GetScript());
			if (this.m_Exp != null)
			{
				return this.m_Exp.FindDynamic(context);
			}
			return null;
		}

		// Token: 0x17000915 RID: 2325
		// (get) Token: 0x06006621 RID: 26145 RVA: 0x0004677B File Offset: 0x0004497B
		// (set) Token: 0x06006622 RID: 26146 RVA: 0x00046783 File Offset: 0x00044983
		public Script OwnerScript { get; private set; }

		// Token: 0x06006623 RID: 26147 RVA: 0x0004678C File Offset: 0x0004498C
		public bool IsConstant()
		{
			return this.m_Constant != null;
		}

		// Token: 0x06006624 RID: 26148 RVA: 0x00046797 File Offset: 0x00044997
		public override int GetHashCode()
		{
			return this.ExpressionCode.GetHashCode();
		}

		// Token: 0x06006625 RID: 26149 RVA: 0x002839A4 File Offset: 0x00281BA4
		public override bool Equals(object obj)
		{
			DynamicExpression dynamicExpression = obj as DynamicExpression;
			return dynamicExpression != null && dynamicExpression.ExpressionCode == this.ExpressionCode;
		}

		// Token: 0x04005E82 RID: 24194
		private DynamicExprExpression m_Exp;

		// Token: 0x04005E83 RID: 24195
		private DynValue m_Constant;

		// Token: 0x04005E84 RID: 24196
		public readonly string ExpressionCode;
	}
}
