using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02001051 RID: 4177
	public class MapIndexInfo
	{
		// Token: 0x06006449 RID: 25673 RVA: 0x00044F8A File Offset: 0x0004318A
		public MapIndexInfo(FubenContrl aa)
		{
			this.parent = aa;
		}

		// Token: 0x170008C5 RID: 2245
		// (get) Token: 0x0600644A RID: 25674 RVA: 0x00044FA4 File Offset: 0x000431A4
		// (set) Token: 0x0600644B RID: 25675 RVA: 0x00281384 File Offset: 0x0027F584
		public int NowIndex
		{
			get
			{
				return (int)this.parent.entity.FuBen[this.SenceName]["NowIndex"].n;
			}
			set
			{
				if (!this.parent.entity.FuBen[this.SenceName].HasField("NowIndex"))
				{
					this.parent.entity.FuBen[this.SenceName].SetField("NowIndex", -1);
				}
				this.addExploredNode(value);
				this.parent.entity.FuBen[this.SenceName].SetField("NowIndex", value);
			}
		}

		// Token: 0x170008C6 RID: 2246
		// (get) Token: 0x0600644C RID: 25676 RVA: 0x0028140C File Offset: 0x0027F60C
		public List<int> ExploredNode
		{
			get
			{
				List<int> TempList = new List<int>();
				this.parent.entity.FuBen[this.SenceName]["ExploredNode"].list.ForEach(delegate(JSONObject aa)
				{
					TempList.Add((int)aa.n);
				});
				return TempList;
			}
		}

		// Token: 0x0600644D RID: 25677 RVA: 0x0028146C File Offset: 0x0027F66C
		public void addExploredNode(int index)
		{
			if (!this.parent.entity.FuBen[this.SenceName].HasField("ExploredNode"))
			{
				this.parent.entity.FuBen[this.SenceName].SetField("ExploredNode", new JSONObject(JSONObject.Type.ARRAY));
			}
			if (!this.parent.entity.FuBen[this.SenceName]["ExploredNode"].HasItem(index))
			{
				this.parent.entity.FuBen[this.SenceName]["ExploredNode"].Add(index);
			}
		}

		// Token: 0x0600644E RID: 25678 RVA: 0x00281524 File Offset: 0x0027F724
		public void AddNodeRoad(int index, int ToIndex)
		{
			if (!this.parent.entity.FuBen[this.SenceName].HasField("RoadNode"))
			{
				this.parent.entity.FuBen[this.SenceName].SetField("RoadNode", new JSONObject(JSONObject.Type.OBJECT));
			}
			if (!this.parent.entity.FuBen[this.SenceName]["RoadNode"].HasField(string.Concat(index)))
			{
				this.parent.entity.FuBen[this.SenceName]["RoadNode"].SetField(string.Concat(index), new JSONObject(JSONObject.Type.ARRAY));
			}
			if (!this.parent.entity.FuBen[this.SenceName]["RoadNode"][index.ToString()].HasItem(ToIndex))
			{
				this.parent.entity.FuBen[this.SenceName]["RoadNode"][index.ToString()].Add(ToIndex);
			}
		}

		// Token: 0x170008C7 RID: 2247
		// (get) Token: 0x0600644F RID: 25679 RVA: 0x00044FD1 File Offset: 0x000431D1
		public DateTime StartTime
		{
			get
			{
				return DateTime.Parse(this.parent.entity.FuBen[this.SenceName]["StartTime"].str);
			}
		}

		// Token: 0x170008C8 RID: 2248
		// (get) Token: 0x06006450 RID: 25680 RVA: 0x00281668 File Offset: 0x0027F868
		public int ResidueTimeDay
		{
			get
			{
				DateTime startTime = this.StartTime;
				DateTime nowTime = Tools.instance.getPlayer().worldTimeMag.getNowTime();
				JSONObject jsonobject = jsonData.instance.FuBenInfoJsonData[this.SenceName];
				return (int)jsonobject["TimeY"].n * 12 * 30 + (int)jsonobject["TimeM"].n * 30 + (int)jsonobject["TimeD"].n - (nowTime - startTime).Days;
			}
		}

		// Token: 0x06006451 RID: 25681 RVA: 0x00045002 File Offset: 0x00043202
		public void setStartTime()
		{
			this.parent.entity.FuBen[this.SenceName].SetField("StartTime", Tools.instance.getPlayer().worldTimeMag.nowTime);
		}

		// Token: 0x06006452 RID: 25682 RVA: 0x0004503D File Offset: 0x0004323D
		public void setFirstIndex(int index)
		{
			this.NowIndex = index;
			this.setStartTime();
		}

		// Token: 0x04005DD5 RID: 24021
		public string SenceName = "";

		// Token: 0x04005DD6 RID: 24022
		private FubenContrl parent;
	}
}
