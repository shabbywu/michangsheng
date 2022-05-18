using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C30 RID: 3120
	public class NpcImprotantEventData : IJSONClass
	{
		// Token: 0x06004C29 RID: 19497 RVA: 0x00202744 File Offset: 0x00200944
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

		// Token: 0x06004C2A RID: 19498 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040049BB RID: 18875
		public static Dictionary<int, NpcImprotantEventData> DataDict = new Dictionary<int, NpcImprotantEventData>();

		// Token: 0x040049BC RID: 18876
		public static List<NpcImprotantEventData> DataList = new List<NpcImprotantEventData>();

		// Token: 0x040049BD RID: 18877
		public static Action OnInitFinishAction = new Action(NpcImprotantEventData.OnInitFinish);

		// Token: 0x040049BE RID: 18878
		public int id;

		// Token: 0x040049BF RID: 18879
		public int ImportantNPC;

		// Token: 0x040049C0 RID: 18880
		public string Time;

		// Token: 0x040049C1 RID: 18881
		public string fuhao;

		// Token: 0x040049C2 RID: 18882
		public string ShiJianInfo;

		// Token: 0x040049C3 RID: 18883
		public List<int> EventLv = new List<int>();
	}
}
