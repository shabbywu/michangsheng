using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02001046 RID: 4166
	public class BuffLoopData
	{
		// Token: 0x06006427 RID: 25639 RVA: 0x0000403D File Offset: 0x0000223D
		public BuffLoopData()
		{
		}

		// Token: 0x06006428 RID: 25640 RVA: 0x00044E75 File Offset: 0x00043075
		public BuffLoopData(int TargetLoopTime, List<int> seid)
		{
			this.TargetLoopTime = TargetLoopTime;
			this.SetSeid(seid);
		}

		// Token: 0x06006429 RID: 25641 RVA: 0x00280DDC File Offset: 0x0027EFDC
		public void SetSeid(List<int> seid)
		{
			this.seid = new List<int>();
			for (int i = 0; i < seid.Count; i++)
			{
				this.seid.Add(seid[i]);
			}
		}

		// Token: 0x0600642A RID: 25642 RVA: 0x00280E18 File Offset: 0x0027F018
		public override string ToString()
		{
			string text = string.Format("TargetLoopTime:{0},seid:", this.TargetLoopTime);
			foreach (int num in this.seid)
			{
				text += string.Format("{0} ", num);
			}
			return text;
		}

		// Token: 0x04005DBE RID: 23998
		public List<int> seid;

		// Token: 0x04005DBF RID: 23999
		public int TargetLoopTime;
	}
}
