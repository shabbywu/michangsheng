using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020008A3 RID: 2211
	public class NpcImprotantPanDingData : IJSONClass
	{
		// Token: 0x0600409F RID: 16543 RVA: 0x001B97F0 File Offset: 0x001B79F0
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.NpcImprotantPanDingData.list)
			{
				try
				{
					NpcImprotantPanDingData npcImprotantPanDingData = new NpcImprotantPanDingData();
					npcImprotantPanDingData.id = jsonobject["id"].I;
					npcImprotantPanDingData.NPC = jsonobject["NPC"].I;
					npcImprotantPanDingData.XingWei = jsonobject["XingWei"].I;
					npcImprotantPanDingData.fuhao = jsonobject["fuhao"].Str;
					npcImprotantPanDingData.StartTime = jsonobject["StartTime"].Str;
					npcImprotantPanDingData.EndTime = jsonobject["EndTime"].Str;
					npcImprotantPanDingData.EventValue = jsonobject["EventValue"].ToList();
					if (NpcImprotantPanDingData.DataDict.ContainsKey(npcImprotantPanDingData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典NpcImprotantPanDingData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", npcImprotantPanDingData.id));
					}
					else
					{
						NpcImprotantPanDingData.DataDict.Add(npcImprotantPanDingData.id, npcImprotantPanDingData);
						NpcImprotantPanDingData.DataList.Add(npcImprotantPanDingData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典NpcImprotantPanDingData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (NpcImprotantPanDingData.OnInitFinishAction != null)
			{
				NpcImprotantPanDingData.OnInitFinishAction();
			}
		}

		// Token: 0x060040A0 RID: 16544 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003E6B RID: 15979
		public static Dictionary<int, NpcImprotantPanDingData> DataDict = new Dictionary<int, NpcImprotantPanDingData>();

		// Token: 0x04003E6C RID: 15980
		public static List<NpcImprotantPanDingData> DataList = new List<NpcImprotantPanDingData>();

		// Token: 0x04003E6D RID: 15981
		public static Action OnInitFinishAction = new Action(NpcImprotantPanDingData.OnInitFinish);

		// Token: 0x04003E6E RID: 15982
		public int id;

		// Token: 0x04003E6F RID: 15983
		public int NPC;

		// Token: 0x04003E70 RID: 15984
		public int XingWei;

		// Token: 0x04003E71 RID: 15985
		public string fuhao;

		// Token: 0x04003E72 RID: 15986
		public string StartTime;

		// Token: 0x04003E73 RID: 15987
		public string EndTime;

		// Token: 0x04003E74 RID: 15988
		public List<int> EventValue = new List<int>();
	}
}
