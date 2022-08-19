using System;
using MoonSharp.Interpreter.Execution;
using MoonSharp.Interpreter.Execution.VM;

namespace MoonSharp.Interpreter.Tree.Statements
{
	// Token: 0x02000CD2 RID: 3282
	internal class ChunkStatement : Statement, IClosureBuilder
	{
		// Token: 0x06005C15 RID: 23573 RVA: 0x0025D8B0 File Offset: 0x0025BAB0
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

		// Token: 0x06005C16 RID: 23574 RVA: 0x0025D95C File Offset: 0x0025BB5C
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

		// Token: 0x06005C17 RID: 23575 RVA: 0x000306E7 File Offset: 0x0002E8E7
		public SymbolRef CreateUpvalue(BuildTimeScope scope, SymbolRef symbol)
		{
			return null;
		}

		// Token: 0x04005355 RID: 21333
		private Statement m_Block;

		// Token: 0x04005356 RID: 21334
		private RuntimeScopeFrame m_StackFrame;

		// Token: 0x04005357 RID: 21335
		private SymbolRef m_Env;

		// Token: 0x04005358 RID: 21336
		private SymbolRef m_VarArgs;
	}
}
