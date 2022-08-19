using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020008A4 RID: 2212
	public class NpcJinHuoData : IJSONClass
	{
		// Token: 0x060040A3 RID: 16547 RVA: 0x001B99D8 File Offset: 0x001B7BD8
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.NpcJinHuoData.list)
			{
				try
				{
					NpcJinHuoData npcJinHuoData = new NpcJinHuoData();
					npcJinHuoData.id = jsonobject["id"].I;
					npcJinHuoData.Type = jsonobject["Type"].ToList();
					npcJinHuoData.quality = jsonobject["quality"].ToList();
					npcJinHuoData.quanzhong = jsonobject["quanzhong"].ToList();
					if (NpcJinHuoData.DataDict.ContainsKey(npcJinHuoData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典NpcJinHuoData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", npcJinHuoData.id));
					}
					else
					{
						NpcJinHuoData.DataDict.Add(npcJinHuoData.id, npcJinHuoData);
						NpcJinHuoData.DataList.Add(npcJinHuoData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典NpcJinHuoData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (NpcJinHuoData.OnInitFinishAction != null)
			{
				NpcJinHuoData.OnInitFinishAction();
			}
		}

		// Token: 0x060040A4 RID: 16548 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003E75 RID: 15989
		public static Dictionary<int, NpcJinHuoData> DataDict = new Dictionary<int, NpcJinHuoData>();

		// Token: 0x04003E76 RID: 15990
		public static List<NpcJinHuoData> DataList = new List<NpcJinHuoData>();

		// Token: 0x04003E77 RID: 15991
		public static Action OnInitFinishAction = new Action(NpcJinHuoData.OnInitFinish);

		// Token: 0x04003E78 RID: 15992
		public int id;

		// Token: 0x04003E79 RID: 15993
		public List<int> Type = new List<int>();

		// Token: 0x04003E7A RID: 15994
		public List<int> quality = new List<int>();

		// Token: 0x04003E7B RID: 15995
		public List<int> quanzhong = new List<int>();
	}
}
