using System;
using MoonSharp.Interpreter.Execution;
using MoonSharp.Interpreter.Execution.VM;

namespace MoonSharp.Interpreter.Tree.Expressions
{
	// Token: 0x02000CE9 RID: 3305
	internal class SymbolRefExpression : Expression, IVariable
	{
		// Token: 0x06005C8E RID: 23694 RVA: 0x00260764 File Offset: 0x0025E964
		public SymbolRefExpression(Token T, ScriptLoadingContext lcontext) : base(lcontext)
		{
			this.m_VarName = T.Text;
			if (T.Type == TokenType.VarArgs)
			{
				this.m_Ref = lcontext.Scope.Find("...");
				if (!lcontext.Scope.CurrentFunctionHasVarArgs())
				{
					throw new SyntaxErrorException(T, "cannot use '...' outside a vararg function");
				}
				if (lcontext.IsDynamicExpression)
				{
					throw new DynamicExpressionException("cannot use '...' in a dynamic expression.");
				}
			}
			else if (!lcontext.IsDynamicExpression)
			{
				this.m_Ref = lcontext.Scope.Find(this.m_VarName);
			}
			lcontext.Lexer.Next();
		}

		// Token: 0x06005C8F RID: 23695 RVA: 0x002607FA File Offset: 0x0025E9FA
		public SymbolRefExpression(ScriptLoadingContext lcontext, SymbolRef refr) : base(lcontext)
		{
			this.m_Ref = refr;
			if (lcontext.IsDynamicExpression)
			{
				throw new DynamicExpressionException("Unsupported symbol reference expression detected.");
			}
		}

		// Token: 0x06005C90 RID: 23696 RVA: 0x0026081D File Offset: 0x0025EA1D
		public override void Compile(ByteCode bc)
		{
			bc.Emit_Load(this.m_Ref);
		}

		// Token: 0x06005C91 RID: 23697 RVA: 0x0026082C File Offset: 0x0025EA2C
		public void CompileAssignment(ByteCode bc, int stackofs, int tupleidx)
		{
			bc.Emit_Store(this.m_Ref, stackofs, tupleidx);
		}

		// Token: 0x06005C92 RID: 23698 RVA: 0x0026083D File Offset: 0x0025EA3D
		public override DynValue Eval(ScriptExecutionContext context)
		{
			return context.EvaluateSymbolByName(this.m_VarName);
		}

		// Token: 0x06005C93 RID: 23699 RVA: 0x0026084B File Offset: 0x0025EA4B
		public override SymbolRef FindDynamic(ScriptExecutionContext context)
		{
			return context.FindSymbolByName(this.m_VarName);
		}

		// Token: 0x040053B4 RID: 21428
		private SymbolRef m_Ref;

		// Token: 0x040053B5 RID: 21429
		private string m_VarName;
	}
}
