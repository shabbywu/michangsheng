using System;
using System.Collections.Generic;
using MoonSharp.Interpreter.Execution;
using MoonSharp.Interpreter.Execution.VM;

namespace MoonSharp.Interpreter.Tree.Statements
{
	// Token: 0x02000CD3 RID: 3283
	internal class CompositeStatement : Statement
	{
		// Token: 0x06005C18 RID: 23576 RVA: 0x0025D9EC File Offset: 0x0025BBEC
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

		// Token: 0x06005C19 RID: 23577 RVA: 0x0025DA58 File Offset: 0x0025BC58
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

		// Token: 0x04005359 RID: 21337
		private List<Statement> m_Statements = new List<Statement>();
	}
}
