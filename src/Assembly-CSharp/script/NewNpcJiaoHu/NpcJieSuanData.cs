using System;
using System.Collections.Generic;

namespace script.NewNpcJiaoHu
{
	// Token: 0x020009F7 RID: 2551
	[Serializable]
	public class NpcJieSuanData
	{
		// Token: 0x060046A9 RID: 18089 RVA: 0x001DDEF4 File Offset: 0x001DC0F4
		public void SaveData()
		{
			this.allBigMapNpcList = NpcJieSuanManager.inst.allBigMapNpcList;
			this.lunDaoNpcList = NpcJieSuanManager.inst.lunDaoNpcList;
			this.JieShaNpcList = NpcJieSuanManager.inst.JieShaNpcList;
			this.lateEmailList = NpcJieSuanManager.inst.lateEmailList;
			this.lateEmailDict = NpcJieSuanManager.inst.lateEmailDict;
			this.afterDeathList = NpcJieSuanManager.inst.afterDeathList;
		}

		// Token: 0x060046AA RID: 18090 RVA: 0x001DDF64 File Offset: 0x001DC164
		public void Init()
		{
			NpcJieSuanManager.inst.allBigMapNpcList = this.allBigMapNpcList;
			NpcJieSuanManager.inst.lunDaoNpcList = this.lunDaoNpcList;
			NpcJieSuanManager.inst.JieShaNpcList = this.JieShaNpcList;
			NpcJieSuanManager.inst.lateEmailList = this.lateEmailList;
			NpcJieSuanManager.inst.lateEmailDict = this.lateEmailDict;
			NpcJieSuanManager.inst.afterDeathList = this.afterDeathList;
		}

		// Token: 0x04004806 RID: 18438
		public List<int> allBigMapNpcList = new List<int>();

		// Token: 0x04004807 RID: 18439
		public List<int> JieShaNpcList = new List<int>();

		// Token: 0x04004808 RID: 18440
		public List<EmailData> lateEmailList = new List<EmailData>();

		// Token: 0x04004809 RID: 18441
		public Dictionary<int, EmailData> lateEmailDict = new Dictionary<int, EmailData>();

		// Token: 0x0400480A RID: 18442
		public List<int> lunDaoNpcList = new List<int>();

		// Token: 0x0400480B RID: 18443
		public List<List<int>> afterDeathList = new List<List<int>>();
	}
}
