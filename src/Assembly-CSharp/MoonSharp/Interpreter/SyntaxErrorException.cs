using System;
using MoonSharp.Interpreter.Debugging;
using MoonSharp.Interpreter.Tree;

namespace MoonSharp.Interpreter
{
	// Token: 0x0200107F RID: 4223
	[Serializable]
	public class SyntaxErrorException : InterpreterException
	{
		// Token: 0x17000913 RID: 2323
		// (get) Token: 0x06006612 RID: 26130 RVA: 0x0004661E File Offset: 0x0004481E
		// (set) Token: 0x06006613 RID: 26131 RVA: 0x00046626 File Offset: 0x00044826
		internal Token Token { get; private set; }

		// Token: 0x17000914 RID: 2324
		// (get) Token: 0x06006614 RID: 26132 RVA: 0x0004662F File Offset: 0x0004482F
		// (set) Token: 0x06006615 RID: 26133 RVA: 0x00046637 File Offset: 0x00044837
		public bool IsPrematureStreamTermination { get; set; }

		// Token: 0x06006616 RID: 26134 RVA: 0x00046640 File Offset: 0x00044840
		internal SyntaxErrorException(Token t, string format, params object[] args) : base(format, args)
		{
			this.Token = t;
		}

		// Token: 0x06006617 RID: 26135 RVA: 0x00046651 File Offset: 0x00044851
		internal SyntaxErrorException(Token t, string message) : base(message)
		{
			this.Token = t;
		}

		// Token: 0x06006618 RID: 26136 RVA: 0x00046661 File Offset: 0x00044861
		internal SyntaxErrorException(Script script, SourceRef sref, string format, params object[] args) : base(format, args)
		{
			base.DecorateMessage(script, sref, -1);
		}

		// Token: 0x06006619 RID: 26137 RVA: 0x00046675 File Offset: 0x00044875
		internal SyntaxErrorException(Script script, SourceRef sref, string message) : base(message)
		{
			base.DecorateMessage(script, sref, -1);
		}

		// Token: 0x0600661A RID: 26138 RVA: 0x00046687 File Offset: 0x00044887
		private SyntaxErrorException(SyntaxErrorException syntaxErrorException) : base(syntaxErrorException, syntaxErrorException.DecoratedMessage)
		{
			this.Token = syntaxErrorException.Token;
			base.DecoratedMessage = this.Message;
		}

		// Token: 0x0600661B RID: 26139 RVA: 0x000466AE File Offset: 0x000448AE
		internal void DecorateMessage(Script script)
		{
			if (this.Token != null)
			{
				base.DecorateMessage(script, this.Token.GetSourceRef(false), -1);
			}
		}

		// Token: 0x0600661C RID: 26140 RVA: 0x000466CC File Offset: 0x000448CC
		public override void Rethrow()
		{
			if (Script.GlobalOptions.RethrowExceptionNested)
			{
				throw new SyntaxErrorException(this);
			}
		}
	}
}
