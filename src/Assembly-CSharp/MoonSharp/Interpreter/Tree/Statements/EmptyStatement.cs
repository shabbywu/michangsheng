using System;
using MoonSharp.Interpreter.Execution;
using MoonSharp.Interpreter.Execution.VM;

namespace MoonSharp.Interpreter.Tree.Statements
{
	// Token: 0x020010AA RID: 4266
	internal class EmptyStatement : Statement
	{
		// Token: 0x06006723 RID: 26403 RVA: 0x00047002 File Offset: 0x00045202
		public EmptyStatement(ScriptLoadingContext lcontext) : base(lcontext)
		{
		}

		// Token: 0x06006724 RID: 26404 RVA: 0x000042DD File Offset: 0x000024DD
		public override void Compile(ByteCode bc)
		{
		}
	}
}
