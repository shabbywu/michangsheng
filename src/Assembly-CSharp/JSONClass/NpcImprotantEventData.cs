using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020008A2 RID: 2210
	public class NpcImprotantEventData : IJSONClass
	{
		// Token: 0x0600409B RID: 16539 RVA: 0x001B9620 File Offset: 0x001B7820
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.NpcImprotantEventData.list)
			{
				try
				{
					NpcImprotantEventData npcImprotantEventData = new NpcImprotantEventData();
					npcImprotantEventData.id = jsonobject["id"].I;
					npcImprotantEventData.ImportantNPC = jsonobject["ImportantNPC"].I;
					npcImprotantEventData.Time = jsonobject["Time"].Str;
					npcImprotantEventData.fuhao = jsonobject["fuhao"].Str;
					npcImprotantEventData.ShiJianInfo = jsonobject["ShiJianInfo"].Str;
					npcImprotantEventData.EventLv = jsonobject["EventLv"].ToList();
					if (NpcImprotantEventData.DataDict.ContainsKey(npcImprotantEventData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典NpcImprotantEventData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", npcImprotantEventData.id));
					}
					else
					{
						NpcImprotantEventData.DataDict.Add(npcImprotantEventData.id, npcImprotantEventData);
						NpcImprotantEventData.DataList.Add(npcImprotantEventData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典NpcImprotantEventData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (NpcImprotantEventData.OnInitFinishAction != null)
			{
				NpcImprotantEventData.OnInitFinishAction();
			}
		}

		// Token: 0x0600409C RID: 16540 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003E62 RID: 15970
		public static Dictionary<int, NpcImprotantEventData> DataDict = new Dictionary<int, NpcImprotantEventData>();

		// Token: 0x04003E63 RID: 15971
		public static List<NpcImprotantEventData> DataList = new List<NpcImprotantEventData>();

		// Token: 0x04003E64 RID: 15972
		public static Action OnInitFinishAction = new Action(NpcImprotantEventData.OnInitFinish);

		// Token: 0x04003E65 RID: 15973
		public int id;

		// Token: 0x04003E66 RID: 15974
		public int ImportantNPC;

		// Token: 0x04003E67 RID: 15975
		public string Time;

		// Token: 0x04003E68 RID: 15976
		public string fuhao;

		// Token: 0x04003E69 RID: 15977
		public string ShiJianInfo;

		// Token: 0x04003E6A RID: 15978
		public List<int> EventLv = new List<int>();
	}
}
