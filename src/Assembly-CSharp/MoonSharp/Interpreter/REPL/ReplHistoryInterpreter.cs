using System;

namespace MoonSharp.Interpreter.REPL
{
	// Token: 0x02000CF1 RID: 3313
	public class ReplHistoryInterpreter : ReplInterpreter
	{
		// Token: 0x06005CB2 RID: 23730 RVA: 0x002617FB File Offset: 0x0025F9FB
		public ReplHistoryInterpreter(Script script, int historySize) : base(script)
		{
			this.m_History = new string[historySize];
		}

		// Token: 0x06005CB3 RID: 23731 RVA: 0x0026181E File Offset: 0x0025FA1E
		public override DynValue Evaluate(string input)
		{
			this.m_Navi = -1;
			this.m_Last = (this.m_Last + 1) % this.m_History.Length;
			this.m_History[this.m_Last] = input;
			return base.Evaluate(input);
		}

		// Token: 0x06005CB4 RID: 23732 RVA: 0x00261854 File Offset: 0x0025FA54
		public string HistoryPrev()
		{
			if (this.m_Navi == -1)
			{
				this.m_Navi = this.m_Last;
			}
			else
			{
				this.m_Navi = (this.m_Navi - 1 + this.m_History.Length) % this.m_History.Length;
			}
			if (this.m_Navi >= 0)
			{
				return this.m_History[this.m_Navi];
			}
			return null;
		}

		// Token: 0x06005CB5 RID: 23733 RVA: 0x002618B0 File Offset: 0x0025FAB0
		public string HistoryNext()
		{
			if (this.m_Navi == -1)
			{
				return null;
			}
			this.m_Navi = (this.m_Navi + 1) % this.m_History.Length;
			if (this.m_Navi >= 0)
			{
				return this.m_History[this.m_Navi];
			}
			return null;
		}

		// Token: 0x040053BC RID: 21436
		private string[] m_History;

		// Token: 0x040053BD RID: 21437
		private int m_Last = -1;

		// Token: 0x040053BE RID: 21438
		private int m_Navi = -1;
	}
}
