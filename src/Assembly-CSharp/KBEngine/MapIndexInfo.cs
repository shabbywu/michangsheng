using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000C8E RID: 3214
	public class MapIndexInfo
	{
		// Token: 0x0600598D RID: 22925 RVA: 0x002561AC File Offset: 0x002543AC
		public MapIndexInfo(FubenContrl aa)
		{
			this.parent = aa;
		}

		// Token: 0x17000672 RID: 1650
		// (get) Token: 0x0600598E RID: 22926 RVA: 0x002561C6 File Offset: 0x002543C6
		// (set) Token: 0x0600598F RID: 22927 RVA: 0x002561F4 File Offset: 0x002543F4
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

		// Token: 0x17000673 RID: 1651
		// (get) Token: 0x06005990 RID: 22928 RVA: 0x0025627C File Offset: 0x0025447C
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

		// Token: 0x06005991 RID: 22929 RVA: 0x002562DC File Offset: 0x002544DC
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

		// Token: 0x06005992 RID: 22930 RVA: 0x00256394 File Offset: 0x00254594
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

		// Token: 0x17000674 RID: 1652
		// (get) Token: 0x06005993 RID: 22931 RVA: 0x002564D5 File Offset: 0x002546D5
		public DateTime StartTime
		{
			get
			{
				return DateTime.Parse(this.parent.entity.FuBen[this.SenceName]["StartTime"].str);
			}
		}

		// Token: 0x17000675 RID: 1653
		// (get) Token: 0x06005994 RID: 22932 RVA: 0x00256508 File Offset: 0x00254708
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

		// Token: 0x06005995 RID: 22933 RVA: 0x00256595 File Offset: 0x00254795
		public void setStartTime()
		{
			this.parent.entity.FuBen[this.SenceName].SetField("StartTime", Tools.instance.getPlayer().worldTimeMag.nowTime);
		}

		// Token: 0x06005996 RID: 22934 RVA: 0x002565D0 File Offset: 0x002547D0
		public void setFirstIndex(int index)
		{
			this.NowIndex = index;
			this.setStartTime();
		}

		// Token: 0x04005229 RID: 21033
		public string SenceName = "";

		// Token: 0x0400522A RID: 21034
		private FubenContrl parent;
	}
}
