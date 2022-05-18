using System;
using System.Collections.Generic;
using MoonSharp.Interpreter.Execution;
using MoonSharp.Interpreter.Execution.VM;

namespace MoonSharp.Interpreter.Tree.Statements
{
	// Token: 0x020010A9 RID: 4265
	internal class CompositeStatement : Statement
	{
		// Token: 0x06006721 RID: 26401 RVA: 0x002871C8 File Offset: 0x002853C8
		public CompositeStatement(ScriptLoadingContext lcontext) : base(lcontext)
		{
			while (!lcontext.Lexer.Current.IsEndOfBlock())
			{
				bool flag;
				Statement item = Statement.CreateStatement(lcontext, out flag);
				this.m_Statements.Add(item);
				if (flag)
				{
					break;
				}
			}
			while (lcontext.Lexer.Current.Type == TokenType.SemiColon)
			{
				lcontext.Lexer.Next();
			}
		}

		// Token: 0x06006722 RID: 26402 RVA: 0x00287234 File Offset: 0x00285434
		public override void Compile(ByteCode bc)
		{
			if (this.m_Statements != null)
			{
				foreach (Statement statement in this.m_Statements)
				{
					statement.Compile(bc);
				}
			}
		}

		// Token: 0x04005F3C RID: 24380
		private List<Statement> m_Statements = new List<Statement>();
	}
}
