using System;

namespace MoonSharp.Interpreter.REPL
{
	// Token: 0x020010CE RID: 4302
	public class ReplHistoryInterpreter : ReplInterpreter
	{
		// Token: 0x060067C8 RID: 26568 RVA: 0x0004755E File Offset: 0x0004575E
		public ReplHistoryInterpreter(Script script, int historySize) : base(script)
		{
			this.m_History = new string[historySize];
		}

		// Token: 0x060067C9 RID: 26569 RVA: 0x00047581 File Offset: 0x00045781
		public override DynValue Evaluate(string input)
		{
			this.m_Navi = -1;
			this.m_Last = (this.m_Last + 1) % this.m_History.Length;
			this.m_History[this.m_Last] = input;
			return base.Evaluate(input);
		}

		// Token: 0x060067CA RID: 26570 RVA: 0x0028AAD0 File Offset: 0x00288CD0
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

		// Token: 0x060067CB RID: 26571 RVA: 0x000475B6 File Offset: 0x000457B6
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

		// Token: 0x04005FC0 RID: 24512
		private string[] m_History;

		// Token: 0x04005FC1 RID: 24513
		private int m_Last = -1;

		// Token: 0x04005FC2 RID: 24514
		private int m_Navi = -1;
	}
}
