using System;
using MoonSharp.Interpreter.Execution;
using MoonSharp.Interpreter.Execution.VM;

namespace MoonSharp.Interpreter.Tree.Statements
{
	// Token: 0x020010A8 RID: 4264
	internal class ChunkStatement : Statement, IClosureBuilder
	{
		// Token: 0x0600671E RID: 26398 RVA: 0x0028708C File Offset: 0x0028528C
		public ChunkStatement(ScriptLoadingContext lcontext) : base(lcontext)
		{
			lcontext.Scope.PushFunction(this, true);
			this.m_Env = lcontext.Scope.DefineLocal("_ENV");
			this.m_VarArgs = lcontext.Scope.DefineLocal("...");
			this.m_Block = new CompositeStatement(lcontext);
			if (lcontext.Lexer.Current.Type != TokenType.Eof)
			{
				throw new SyntaxErrorException(lcontext.Lexer.Current, "<eof> expected near '{0}'", new object[]
				{
					lcontext.Lexer.Current.Text
				});
			}
			this.m_StackFrame = lcontext.Scope.PopFunction();
		}

		// Token: 0x0600671F RID: 26399 RVA: 0x00287138 File Offset: 0x00285338
		public override void Compile(ByteCode bc)
		{
			Instruction instruction = bc.Emit_Meta("<chunk-root>", OpCodeMetadataType.ChunkEntrypoint, null);
			int jumpPointForLastInstruction = bc.GetJumpPointForLastInstruction();
			bc.Emit_BeginFn(this.m_StackFrame);
			bc.Emit_Args(new SymbolRef[]
			{
				this.m_VarArgs
			});
			bc.Emit_Load(SymbolRef.Upvalue("_ENV", 0));
			bc.Emit_Store(this.m_Env, 0, 0);
			bc.Emit_Pop(1);
			this.m_Block.Compile(bc);
			bc.Emit_Ret(0);
			instruction.NumVal = bc.GetJumpPointForLastInstruction() - jumpPointForLastInstruction;
		}

		// Token: 0x06006720 RID: 26400 RVA: 0x0000B171 File Offset: 0x00009371
		public SymbolRef CreateUpvalue(BuildTimeScope scope, SymbolRef symbol)
		{
			return null;
		}

		// Token: 0x04005F38 RID: 24376
		private Statement m_Block;

		// Token: 0x04005F39 RID: 24377
		private RuntimeScopeFrame m_StackFrame;

		// Token: 0x04005F3A RID: 24378
		private SymbolRef m_Env;

		// Token: 0x04005F3B RID: 24379
		private SymbolRef m_VarArgs;
	}
}
