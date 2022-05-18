using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C32 RID: 3122
	public class NpcJinHuoData : IJSONClass
	{
		// Token: 0x06004C31 RID: 19505 RVA: 0x00202A84 File Offset: 0x00200C84
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

		// Token: 0x06004C32 RID: 19506 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040049CE RID: 18894
		public static Dictionary<int, NpcJinHuoData> DataDict = new Dictionary<int, NpcJinHuoData>();

		// Token: 0x040049CF RID: 18895
		public static List<NpcJinHuoData> DataList = new List<NpcJinHuoData>();

		// Token: 0x040049D0 RID: 18896
		public static Action OnInitFinishAction = new Action(NpcJinHuoData.OnInitFinish);

		// Token: 0x040049D1 RID: 18897
		public int id;

		// Token: 0x040049D2 RID: 18898
		public List<int> Type = new List<int>();

		// Token: 0x040049D3 RID: 18899
		public List<int> quality = new List<int>();

		// Token: 0x040049D4 RID: 18900
		public List<int> quanzhong = new List<int>();
	}
}
