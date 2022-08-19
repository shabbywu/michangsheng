using System;
using MoonSharp.Interpreter.Tree.Expressions;

namespace MoonSharp.Interpreter
{
	// Token: 0x02000CB1 RID: 3249
	public class DynamicExpression : IScriptPrivateResource
	{
		// Token: 0x06005B2B RID: 23339 RVA: 0x0025998B File Offset: 0x00257B8B
		internal DynamicExpression(Script S, string strExpr, DynamicExprExpression expr)
		{
			this.ExpressionCode = strExpr;
			this.OwnerScript = S;
			this.m_Exp = expr;
		}

		// Token: 0x06005B2C RID: 23340 RVA: 0x002599A8 File Offset: 0x00257BA8
		internal DynamicExpression(Script S, string strExpr, DynValue constant)
		{
			this.ExpressionCode = strExpr;
			this.OwnerScript = S;
			this.m_Constant = constant;
		}

		// Token: 0x06005B2D RID: 23341 RVA: 0x002599C5 File Offset: 0x00257BC5
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

		// Token: 0x06005B2E RID: 23342 RVA: 0x00259A01 File Offset: 0x00257C01
		public SymbolRef FindSymbol(ScriptExecutionContext context)
		{
			this.CheckScriptOwnership(context.GetScript());
			if (this.m_Exp != null)
			{
				return this.m_Exp.FindDynamic(context);
			}
			return null;
		}

		// Token: 0x170006BA RID: 1722
		// (get) Token: 0x06005B2F RID: 23343 RVA: 0x00259A25 File Offset: 0x00257C25
		// (set) Token: 0x06005B30 RID: 23344 RVA: 0x00259A2D File Offset: 0x00257C2D
		public Script OwnerScript { get; private set; }

		// Token: 0x06005B31 RID: 23345 RVA: 0x00259A36 File Offset: 0x00257C36
		public bool IsConstant()
		{
			return this.m_Constant != null;
		}

		// Token: 0x06005B32 RID: 23346 RVA: 0x00259A41 File Offset: 0x00257C41
		public override int GetHashCode()
		{
			return this.ExpressionCode.GetHashCode();
		}

		// Token: 0x06005B33 RID: 23347 RVA: 0x00259A50 File Offset: 0x00257C50
		public override bool Equals(object obj)
		{
			DynamicExpression dynamicExpression = obj as DynamicExpression;
			return dynamicExpression != null && dynamicExpression.ExpressionCode == this.ExpressionCode;
		}

		// Token: 0x040052AD RID: 21165
		private DynamicExprExpression m_Exp;

		// Token: 0x040052AE RID: 21166
		private DynValue m_Constant;

		// Token: 0x040052AF RID: 21167
		public readonly string ExpressionCode;
	}
}
