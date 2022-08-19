using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace script.NpcAction
{
	// Token: 0x020009F1 RID: 2545
	public class NpcActionMag : MonoBehaviour
	{
		// Token: 0x06004690 RID: 18064 RVA: 0x001DD87C File Offset: 0x001DBA7C
		private void Awake()
		{
			this.Pool = new GroupPool();
			NpcActionMag.Inst = this;
			Object.DontDestroyOnLoad(NpcActionMag.Inst.gameObject);
		}

		// Token: 0x06004691 RID: 18065 RVA: 0x001DD8A0 File Offset: 0x001DBAA0
		public void GroupAction(int times, bool isCanChanger = true)
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
			this.NeedTimes = times;
			this.BeforeAction();
		}

		// Token: 0x06004692 RID: 18066 RVA: 0x001DDA48 File Offset: 0x001DBC48
		private void BeforeAction()
		{
			Loom.RunAsync(delegate
			{
				try
				{
					NpcJieSuanManager.inst.PaiMaiAction();
					NpcJieSuanManager.inst.LunDaoAction();
					NpcJieSuanManager.inst.npcTeShu.NextJieSha();
					NpcJieSuanManager.inst.npcMap.RestartMap();
				}
				catch (Exception ex)
				{
					Debug.LogError("BeforeAction出错");
					Debug.LogError(ex);
					throw;
				}
				Loom.QueueOnMainThread(delegate(object obj)
				{
					this.StartAction();
				}, null);
			});
		}

		// Token: 0x06004693 RID: 18067 RVA: 0x001DDA5C File Offset: 0x001DBC5C
		private void AfterAction()
		{
			Loom.RunAsync(delegate
			{
				Loom.QueueOnMainThread(delegate(object obj)
				{
					this.CompleteGroupNum = 0;
					this.NeedTimes--;
					NpcJieSuanManager.inst.JieSuanTimes++;
					NpcJieSuanManager.inst.JieSuanTime = DateTime.Parse(NpcJieSuanManager.inst.JieSuanTime).AddMonths(1).ToString(CultureInfo.CurrentCulture);
					if (this.NeedTimes == 0)
					{
						this.ActionCompleteCallBack();
						return;
					}
					this.BeforeAction();
				}, null);
			});
		}

		// Token: 0x06004694 RID: 18068 RVA: 0x001DDA70 File Offset: 0x001DBC70
		private void StartAction()
		{
			foreach (NpcDataGroup npcDataGroup in this.GroupList)
			{
				npcDataGroup.Start(false);
			}
		}

		// Token: 0x06004695 RID: 18069 RVA: 0x001DDAC4 File Offset: 0x001DBCC4
		public void ActionCompleteCallBack()
		{
			Loom.RunAsync(delegate
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
			});
		}

		// Token: 0x06004696 RID: 18070 RVA: 0x001DDAD8 File Offset: 0x001DBCD8
		public void SendMessage()
		{
			foreach (NpcDataGroup npcDataGroup in this.GroupList)
			{
				foreach (int num in npcDataGroup.NpcDict.Keys)
				{
				}
			}
		}

		// Token: 0x06004697 RID: 18071 RVA: 0x001DDB64 File Offset: 0x001DBD64
		public void GroupCompleteCallBack()
		{
			this.CompleteGroupNum++;
			if (this.CompleteGroupNum == this.GroupNum)
			{
				this.AfterAction();
			}
		}

		// Token: 0x040047F3 RID: 18419
		public static NpcActionMag Inst;

		// Token: 0x040047F4 RID: 18420
		public bool IsNoJieSuan;

		// Token: 0x040047F5 RID: 18421
		public int GroupNum;

		// Token: 0x040047F6 RID: 18422
		public int CompleteGroupNum;

		// Token: 0x040047F7 RID: 18423
		public List<NpcDataGroup> GroupList;

		// Token: 0x040047F8 RID: 18424
		public GroupPool Pool;

		// Token: 0x040047F9 RID: 18425
		public int NeedTimes;

		// Token: 0x040047FA RID: 18426
		public bool IsFree;
	}
}
