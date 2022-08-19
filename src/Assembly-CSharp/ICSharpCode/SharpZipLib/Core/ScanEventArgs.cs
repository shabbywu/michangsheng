using System;

namespace ICSharpCode.SharpZipLib.Core
{
	// Token: 0x02000578 RID: 1400
	public class ScanEventArgs : EventArgs
	{
		// Token: 0x06002E25 RID: 11813 RVA: 0x0015114F File Offset: 0x0014F34F
		public ScanEventArgs(string name)
		{
			this.name_ = name;
		}

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06002E26 RID: 11814 RVA: 0x00151165 File Offset: 0x0014F365
		public string Name
		{
			get
			{
				return this.name_;
			}
		}

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06002E27 RID: 11815 RVA: 0x0015116D File Offset: 0x0014F36D
		// (set) Token: 0x06002E28 RID: 11816 RVA: 0x00151175 File Offset: 0x0014F375
		public bool ContinueRunning
		{
			get
			{
				return this.continueRunning_;
			}
			set
			{
				this.continueRunning_ = value;
			}
		}

		// Token: 0x040028CE RID: 10446
		private string name_;

		// Token: 0x040028CF RID: 10447
		private bool continueRunning_ = true;
	}
}
