using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020008A6 RID: 2214
	public class NpcLevelShouYiDate : IJSONClass
	{
		// Token: 0x060040AB RID: 16555 RVA: 0x001B9F40 File Offset: 0x001B8140
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.NpcLevelShouYiDate.list)
			{
				try
				{
					NpcLevelShouYiDate npcLevelShouYiDate = new NpcLevelShouYiDate();
					npcLevelShouYiDate.id = jsonobject["id"].I;
					npcLevelShouYiDate.money = jsonobject["money"].I;
					npcLevelShouYiDate.gongxian = jsonobject["gongxian"].I;
					npcLevelShouYiDate.fabao = jsonobject["fabao"].I;
					npcLevelShouYiDate.wudaoexp = jsonobject["wudaoexp"].I;
					npcLevelShouYiDate.ZengLi = jsonobject["ZengLi"].I;
					npcLevelShouYiDate.jieshapanduan = jsonobject["jieshapanduan"].I;
					npcLevelShouYiDate.siwangjilv = jsonobject["siwangjilv"].I;
					if (NpcLevelShouYiDate.DataDict.ContainsKey(npcLevelShouYiDate.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典NpcLevelShouYiDate.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", npcLevelShouYiDate.id));
					}
					else
					{
						NpcLevelShouYiDate.DataDict.Add(npcLevelShouYiDate.id, npcLevelShouYiDate);
						NpcLevelShouYiDate.DataList.Add(npcLevelShouYiDate);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典NpcLevelShouYiDate.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (NpcLevelShouYiDate.OnInitFinishAction != null)
			{
				NpcLevelShouYiDate.OnInitFinishAction();
			}
		}

		// Token: 0x060040AC RID: 16556 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003E97 RID: 16023
		public static Dictionary<int, NpcLevelShouYiDate> DataDict = new Dictionary<int, NpcLevelShouYiDate>();

		// Token: 0x04003E98 RID: 16024
		public static List<NpcLevelShouYiDate> DataList = new List<NpcLevelShouYiDate>();

		// Token: 0x04003E99 RID: 16025
		public static Action OnInitFinishAction = new Action(NpcLevelShouYiDate.OnInitFinish);

		// Token: 0x04003E9A RID: 16026
		public int id;

		// Token: 0x04003E9B RID: 16027
		public int money;

		// Token: 0x04003E9C RID: 16028
		public int gongxian;

		// Token: 0x04003E9D RID: 16029
		public int fabao;

		// Token: 0x04003E9E RID: 16030
		public int wudaoexp;

		// Token: 0x04003E9F RID: 16031
		public int ZengLi;

		// Token: 0x04003EA0 RID: 16032
		public int jieshapanduan;

		// Token: 0x04003EA1 RID: 16033
		public int siwangjilv;
	}
}
