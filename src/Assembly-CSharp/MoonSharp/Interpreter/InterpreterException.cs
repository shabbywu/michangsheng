using System;
using System.Collections.Generic;
using MoonSharp.Interpreter.Debugging;

namespace MoonSharp.Interpreter
{
	// Token: 0x02000CAE RID: 3246
	[Serializable]
	public class InterpreterException : Exception
	{
		// Token: 0x06005AEF RID: 23279 RVA: 0x0025926C File Offset: 0x0025746C
		protected InterpreterException(Exception ex, string message) : base(message, ex)
		{
		}

		// Token: 0x06005AF0 RID: 23280 RVA: 0x00259276 File Offset: 0x00257476
		protected InterpreterException(Exception ex) : base(ex.Message, ex)
		{
		}

		// Token: 0x06005AF1 RID: 23281 RVA: 0x001402A9 File Offset: 0x0013E4A9
		protected InterpreterException(string message) : base(message)
		{
		}

		// Token: 0x06005AF2 RID: 23282 RVA: 0x00259285 File Offset: 0x00257485
		protected InterpreterException(string format, params object[] args) : base(string.Format(format, args))
		{
		}

		// Token: 0x170006B4 RID: 1716
		// (get) Token: 0x06005AF3 RID: 23283 RVA: 0x00259294 File Offset: 0x00257494
		// (set) Token: 0x06005AF4 RID: 23284 RVA: 0x0025929C File Offset: 0x0025749C
		public int InstructionPtr { get; internal set; }

		// Token: 0x170006B5 RID: 1717
		// (get) Token: 0x06005AF5 RID: 23285 RVA: 0x002592A5 File Offset: 0x002574A5
		// (set) Token: 0x06005AF6 RID: 23286 RVA: 0x002592AD File Offset: 0x002574AD
		public IList<WatchItem> CallStack { get; internal set; }

		// Token: 0x170006B6 RID: 1718
		// (get) Token: 0x06005AF7 RID: 23287 RVA: 0x002592B6 File Offset: 0x002574B6
		// (set) Token: 0x06005AF8 RID: 23288 RVA: 0x002592BE File Offset: 0x002574BE
		public string DecoratedMessage { get; internal set; }

		// Token: 0x170006B7 RID: 1719
		// (get) Token: 0x06005AF9 RID: 23289 RVA: 0x002592C7 File Offset: 0x002574C7
		// (set) Token: 0x06005AFA RID: 23290 RVA: 0x002592CF File Offset: 0x002574CF
		public bool DoNotDecorateMessage { get; set; }

		// Token: 0x06005AFB RID: 23291 RVA: 0x002592D8 File Offset: 0x002574D8
		internal void DecorateMessage(Script script, SourceRef sref, int ip = -1)
		{
			if (string.IsNullOrEmpty(this.DecoratedMessage))
			{
				if (this.DoNotDecorateMessage)
				{
					this.DecoratedMessage = this.Message;
					return;
				}
				if (sref != null)
				{
					this.DecoratedMessage = string.Format("{0}: {1}", sref.FormatLocation(script, false), this.Message);
					return;
				}
				this.DecoratedMessage = string.Format("bytecode:{0}: {1}", ip, this.Message);
			}
		}

		// Token: 0x06005AFC RID: 23292 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void Rethrow()
		{
		}
	}
}
