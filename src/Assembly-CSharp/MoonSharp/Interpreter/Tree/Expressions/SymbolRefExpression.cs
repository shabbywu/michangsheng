using System;
using MoonSharp.Interpreter.Execution;
using MoonSharp.Interpreter.Execution.VM;

namespace MoonSharp.Interpreter.Tree.Expressions
{
	// Token: 0x020010C6 RID: 4294
	internal class SymbolRefExpression : Expression, IVariable
	{
		// Token: 0x060067A4 RID: 26532 RVA: 0x00289B8C File Offset: 0x00287D8C
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

		// Token: 0x060067A5 RID: 26533 RVA: 0x0004740D File Offset: 0x0004560D
		public SymbolRefExpression(ScriptLoadingContext lcontext, SymbolRef refr) : base(lcontext)
		{
			this.m_Ref = refr;
			if (lcontext.IsDynamicExpression)
			{
				throw new DynamicExpressionException("Unsupported symbol reference expression detected.");
			}
		}

		// Token: 0x060067A6 RID: 26534 RVA: 0x00047430 File Offset: 0x00045630
		public override void Compile(ByteCode bc)
		{
			bc.Emit_Load(this.m_Ref);
		}

		// Token: 0x060067A7 RID: 26535 RVA: 0x0004743F File Offset: 0x0004563F
		public void CompileAssignment(ByteCode bc, int stackofs, int tupleidx)
		{
			bc.Emit_Store(this.m_Ref, stackofs, tupleidx);
		}

		// Token: 0x060067A8 RID: 26536 RVA: 0x00047450 File Offset: 0x00045650
		public override DynValue Eval(ScriptExecutionContext context)
		{
			return context.EvaluateSymbolByName(this.m_VarName);
		}

		// Token: 0x060067A9 RID: 26537 RVA: 0x0004745E File Offset: 0x0004565E
		public override SymbolRef FindDynamic(ScriptExecutionContext context)
		{
			return context.FindSymbolByName(this.m_VarName);
		}

		// Token: 0x04005FB8 RID: 24504
		private SymbolRef m_Ref;

		// Token: 0x04005FB9 RID: 24505
		private string m_VarName;
	}
}
