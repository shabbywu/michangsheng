using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C35 RID: 3125
	public class NpcPaiMaiData : IJSONClass
	{
		// Token: 0x06004C3D RID: 19517 RVA: 0x00203138 File Offset: 0x00201338
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.NpcPaiMaiData.list)
			{
				try
				{
					NpcPaiMaiData npcPaiMaiData = new NpcPaiMaiData();
					npcPaiMaiData.id = jsonobject["id"].I;
					npcPaiMaiData.PaiMaiID = jsonobject["PaiMaiID"].I;
					npcPaiMaiData.ItemNum = jsonobject["ItemNum"].I;
					npcPaiMaiData.Type = jsonobject["Type"].ToList();
					npcPaiMaiData.quality = jsonobject["quality"].ToList();
					if (NpcPaiMaiData.DataDict.ContainsKey(npcPaiMaiData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典NpcPaiMaiData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", npcPaiMaiData.id));
					}
					else
					{
						NpcPaiMaiData.DataDict.Add(npcPaiMaiData.id, npcPaiMaiData);
						NpcPaiMaiData.DataList.Add(npcPaiMaiData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典NpcPaiMaiData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (NpcPaiMaiData.OnInitFinishAction != null)
			{
				NpcPaiMaiData.OnInitFinishAction();
			}
		}

		// Token: 0x06004C3E RID: 19518 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040049FB RID: 18939
		public static Dictionary<int, NpcPaiMaiData> DataDict = new Dictionary<int, NpcPaiMaiData>();

		// Token: 0x040049FC RID: 18940
		public static List<NpcPaiMaiData> DataList = new List<NpcPaiMaiData>();

		// Token: 0x040049FD RID: 18941
		public static Action OnInitFinishAction = new Action(NpcPaiMaiData.OnInitFinish);

		// Token: 0x040049FE RID: 18942
		public int id;

		// Token: 0x040049FF RID: 18943
		public int PaiMaiID;

		// Token: 0x04004A00 RID: 18944
		public int ItemNum;

		// Token: 0x04004A01 RID: 18945
		public List<int> Type = new List<int>();

		// Token: 0x04004A02 RID: 18946
		public List<int> quality = new List<int>();
	}
}
