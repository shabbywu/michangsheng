using System;
using MoonSharp.Interpreter.Debugging;
using MoonSharp.Interpreter.Tree;

namespace MoonSharp.Interpreter
{
	// Token: 0x02000CB0 RID: 3248
	[Serializable]
	public class SyntaxErrorException : InterpreterException
	{
		// Token: 0x170006B8 RID: 1720
		// (get) Token: 0x06005B20 RID: 23328 RVA: 0x002598C8 File Offset: 0x00257AC8
		// (set) Token: 0x06005B21 RID: 23329 RVA: 0x002598D0 File Offset: 0x00257AD0
		internal Token Token { get; private set; }

		// Token: 0x170006B9 RID: 1721
		// (get) Token: 0x06005B22 RID: 23330 RVA: 0x002598D9 File Offset: 0x00257AD9
		// (set) Token: 0x06005B23 RID: 23331 RVA: 0x002598E1 File Offset: 0x00257AE1
		public bool IsPrematureStreamTermination { get; set; }

		// Token: 0x06005B24 RID: 23332 RVA: 0x002598EA File Offset: 0x00257AEA
		internal SyntaxErrorException(Token t, string format, params object[] args) : base(format, args)
		{
			this.Token = t;
		}

		// Token: 0x06005B25 RID: 23333 RVA: 0x002598FB File Offset: 0x00257AFB
		internal SyntaxErrorException(Token t, string message) : base(message)
		{
			this.Token = t;
		}

		// Token: 0x06005B26 RID: 23334 RVA: 0x0025990B File Offset: 0x00257B0B
		internal SyntaxErrorException(Script script, SourceRef sref, string format, params object[] args) : base(format, args)
		{
			base.DecorateMessage(script, sref, -1);
		}

		// Token: 0x06005B27 RID: 23335 RVA: 0x0025991F File Offset: 0x00257B1F
		internal SyntaxErrorException(Script script, SourceRef sref, string message) : base(message)
		{
			base.DecorateMessage(script, sref, -1);
		}

		// Token: 0x06005B28 RID: 23336 RVA: 0x00259931 File Offset: 0x00257B31
		private SyntaxErrorException(SyntaxErrorException syntaxErrorException) : base(syntaxErrorException, syntaxErrorException.DecoratedMessage)
		{
			this.Token = syntaxErrorException.Token;
			base.DecoratedMessage = this.Message;
		}

		// Token: 0x06005B29 RID: 23337 RVA: 0x00259958 File Offset: 0x00257B58
		internal void DecorateMessage(Script script)
		{
			if (this.Token != null)
			{
				base.DecorateMessage(script, this.Token.GetSourceRef(false), -1);
			}
		}

		// Token: 0x06005B2A RID: 23338 RVA: 0x00259976 File Offset: 0x00257B76
		public override void Rethrow()
		{
			if (Script.GlobalOptions.RethrowExceptionNested)
			{
				throw new SyntaxErrorException(this);
			}
		}
	}
}
