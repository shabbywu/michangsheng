using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000C83 RID: 3203
	public class BuffLoopData
	{
		// Token: 0x0600596A RID: 22890 RVA: 0x000027FC File Offset: 0x000009FC
		public BuffLoopData()
		{
		}

		// Token: 0x0600596B RID: 22891 RVA: 0x00255AB2 File Offset: 0x00253CB2
		public BuffLoopData(int TargetLoopTime, List<int> seid)
		{
			this.TargetLoopTime = TargetLoopTime;
			this.SetSeid(seid);
		}

		// Token: 0x0600596C RID: 22892 RVA: 0x00255AC8 File Offset: 0x00253CC8
		public void SetSeid(List<int> seid)
		{
			this.seid = new List<int>();
			for (int i = 0; i < seid.Count; i++)
			{
				this.seid.Add(seid[i]);
			}
		}

		// Token: 0x0600596D RID: 22893 RVA: 0x00255B04 File Offset: 0x00253D04
		public override string ToString()
		{
			string text = string.Format("TargetLoopTime:{0},seid:", this.TargetLoopTime);
			foreach (int num in this.seid)
			{
				text += string.Format("{0} ", num);
			}
			return text;
		}

		// Token: 0x04005212 RID: 21010
		public List<int> seid;

		// Token: 0x04005213 RID: 21011
		public int TargetLoopTime;
	}
}
