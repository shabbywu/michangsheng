using System;
using System.Collections.Generic;
using MoonSharp.Interpreter.Debugging;

namespace MoonSharp.Interpreter
{
	// Token: 0x0200107D RID: 4221
	[Serializable]
	public class InterpreterException : Exception
	{
		// Token: 0x060065E1 RID: 26081 RVA: 0x000462C3 File Offset: 0x000444C3
		protected InterpreterException(Exception ex, string message) : base(message, ex)
		{
		}

		// Token: 0x060065E2 RID: 26082 RVA: 0x000462CD File Offset: 0x000444CD
		protected InterpreterException(Exception ex) : base(ex.Message, ex)
		{
		}

		// Token: 0x060065E3 RID: 26083 RVA: 0x00024864 File Offset: 0x00022A64
		protected InterpreterException(string message) : base(message)
		{
		}

		// Token: 0x060065E4 RID: 26084 RVA: 0x000462DC File Offset: 0x000444DC
		protected InterpreterException(string format, params object[] args) : base(string.Format(format, args))
		{
		}

		// Token: 0x1700090F RID: 2319
		// (get) Token: 0x060065E5 RID: 26085 RVA: 0x000462EB File Offset: 0x000444EB
		// (set) Token: 0x060065E6 RID: 26086 RVA: 0x000462F3 File Offset: 0x000444F3
		public int InstructionPtr { get; internal set; }

		// Token: 0x17000910 RID: 2320
		// (get) Token: 0x060065E7 RID: 26087 RVA: 0x000462FC File Offset: 0x000444FC
		// (set) Token: 0x060065E8 RID: 26088 RVA: 0x00046304 File Offset: 0x00044504
		public IList<WatchItem> CallStack { get; internal set; }

		// Token: 0x17000911 RID: 2321
		// (get) Token: 0x060065E9 RID: 26089 RVA: 0x0004630D File Offset: 0x0004450D
		// (set) Token: 0x060065EA RID: 26090 RVA: 0x00046315 File Offset: 0x00044515
		public string DecoratedMessage { get; internal set; }

		// Token: 0x17000912 RID: 2322
		// (get) Token: 0x060065EB RID: 26091 RVA: 0x0004631E File Offset: 0x0004451E
		// (set) Token: 0x060065EC RID: 26092 RVA: 0x00046326 File Offset: 0x00044526
		public bool DoNotDecorateMessage { get; set; }

		// Token: 0x060065ED RID: 26093 RVA: 0x002836A0 File Offset: 0x002818A0
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

		// Token: 0x060065EE RID: 26094 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void Rethrow()
		{
		}
	}
}
