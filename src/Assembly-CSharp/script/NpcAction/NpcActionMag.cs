using System;
using System.Collections.Generic;
using UnityEngine;

namespace script.NpcAction
{
	// Token: 0x02000ABE RID: 2750
	public class NpcActionMag : MonoBehaviour
	{
		// Token: 0x06004648 RID: 17992 RVA: 0x001DF660 File Offset: 0x001DD860
		private void Awake()
		{
			if (NpcActionMag.Inst != null)
			{
				Object.Destroy(NpcActionMag.Inst.gameObject);
			}
			this.Pool = new GroupPool();
			NpcActionMag.Inst = this;
			Object.DontDestroyOnLoad(NpcActionMag.Inst.gameObject);
			jsonData.instance.init("Effect/json/d_avatar.py.datas", out jsonData.instance.AvatarJsonData);
		}

		// Token: 0x06004649 RID: 17993 RVA: 0x001DF6C4 File Offset: 0x001DD8C4
		public void NpcAction(int times, bool isCanChanger = true)
		{
			this.GroupNum = 0;
			this.CompleteGroupNum = 0;
			this.GroupList = new List<NpcDataGroup>();
			List<NpcData> list = new List<NpcData>();
			if (this.IsNoJieSuan)
			{
				this.IsNoJieSuan = false;
				return;
			}
			DateTime.Parse(this.JieSuanTime).AddMonths(times);
			foreach (string s in jsonData.instance.AvatarJsonData.keys)
			{
				int num = int.Parse(s);
				if (num >= 20000)
				{
					NpcData npcData = new NpcData(num);
					if (npcData.IsInit)
					{
						list.Add(npcData);
					}
				}
			}
			if (list.Count == 0)
			{
				return;
			}
			int num2 = 1;
			if (list.Count >= Loom.maxThreads)
			{
				num2 = list.Count / Loom.maxThreads;
			}
			int num3 = 1;
			int num4 = 0;
			this.GroupList.Add(this.Pool.GetGroup());
			foreach (NpcData npcData2 in list)
			{
				if (num3 > num2 && this.GroupList.Count <= Loom.maxThreads)
				{
					num4++;
					num3 = 1;
					this.GroupList.Add(this.Pool.GetGroup());
				}
				this.GroupList[num4].NpcDict.Add(npcData2.NpcId, npcData2);
				num3++;
			}
			this.GroupNum = this.GroupList.Count;
			if (this.GroupNum > 0)
			{
				this.IsFree = false;
			}
			foreach (NpcDataGroup npcDataGroup in this.GroupList)
			{
				npcDataGroup.GroupAction(times);
			}
		}

		// Token: 0x0600464A RID: 17994 RVA: 0x001DF8B8 File Offset: 0x001DDAB8
		public void GroupCompleteCallBack()
		{
			this.CompleteGroupNum++;
			if (this.CompleteGroupNum == this.GroupNum)
			{
				try
				{
					foreach (NpcDataGroup npcDataGroup in this.GroupList)
					{
						foreach (int key in npcDataGroup.NpcDict.Keys)
						{
							npcDataGroup.NpcDict[key].BackWriter();
						}
						this.Pool.BackGroup(npcDataGroup);
					}
				}
				catch (Exception ex)
				{
					Debug.LogError("完成小组行动回调出错");
					Debug.LogError(ex);
				}
				finally
				{
					this.IsFree = true;
				}
			}
		}

		// Token: 0x04003E62 RID: 15970
		public static NpcActionMag Inst;

		// Token: 0x04003E63 RID: 15971
		public bool isUpDateNpcList;

		// Token: 0x04003E64 RID: 15972
		public bool isCanJieSuan = true;

		// Token: 0x04003E65 RID: 15973
		public bool JieSuanAnimation;

		// Token: 0x04003E66 RID: 15974
		public int JieSuanTimes;

		// Token: 0x04003E67 RID: 15975
		public string JieSuanTime = "0001-1-1";

		// Token: 0x04003E68 RID: 15976
		public bool IsNoJieSuan;

		// Token: 0x04003E69 RID: 15977
		public int GroupNum;

		// Token: 0x04003E6A RID: 15978
		public int CompleteGroupNum;

		// Token: 0x04003E6B RID: 15979
		public List<NpcDataGroup> GroupList;

		// Token: 0x04003E6C RID: 15980
		public GroupPool Pool;

		// Token: 0x04003E6D RID: 15981
		public bool IsFree;
	}
}
