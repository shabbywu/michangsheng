using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000897 RID: 2199
	public class NpcBiaoBaiTiKuData : IJSONClass
	{
		// Token: 0x0600406F RID: 16495 RVA: 0x001B7FCC File Offset: 0x001B61CC
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.NpcBiaoBaiTiKuData.list)
			{
				try
				{
					NpcBiaoBaiTiKuData npcBiaoBaiTiKuData = new NpcBiaoBaiTiKuData();
					npcBiaoBaiTiKuData.id = jsonobject["id"].I;
					npcBiaoBaiTiKuData.Type = jsonobject["Type"].I;
					npcBiaoBaiTiKuData.TiWen = jsonobject["TiWen"].Str;
					npcBiaoBaiTiKuData.optionDesc1 = jsonobject["optionDesc1"].Str;
					npcBiaoBaiTiKuData.optionDesc2 = jsonobject["optionDesc2"].Str;
					npcBiaoBaiTiKuData.optionDesc3 = jsonobject["optionDesc3"].Str;
					if (NpcBiaoBaiTiKuData.DataDict.ContainsKey(npcBiaoBaiTiKuData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典NpcBiaoBaiTiKuData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", npcBiaoBaiTiKuData.id));
					}
					else
					{
						NpcBiaoBaiTiKuData.DataDict.Add(npcBiaoBaiTiKuData.id, npcBiaoBaiTiKuData);
						NpcBiaoBaiTiKuData.DataList.Add(npcBiaoBaiTiKuData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典NpcBiaoBaiTiKuData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (NpcBiaoBaiTiKuData.OnInitFinishAction != null)
			{
				NpcBiaoBaiTiKuData.OnInitFinishAction();
			}
		}

		// Token: 0x06004070 RID: 16496 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003DE5 RID: 15845
		public static Dictionary<int, NpcBiaoBaiTiKuData> DataDict = new Dictionary<int, NpcBiaoBaiTiKuData>();

		// Token: 0x04003DE6 RID: 15846
		public static List<NpcBiaoBaiTiKuData> DataList = new List<NpcBiaoBaiTiKuData>();

		// Token: 0x04003DE7 RID: 15847
		public static Action OnInitFinishAction = new Action(NpcBiaoBaiTiKuData.OnInitFinish);

		// Token: 0x04003DE8 RID: 15848
		public int id;

		// Token: 0x04003DE9 RID: 15849
		public int Type;

		// Token: 0x04003DEA RID: 15850
		public string TiWen;

		// Token: 0x04003DEB RID: 15851
		public string optionDesc1;

		// Token: 0x04003DEC RID: 15852
		public string optionDesc2;

		// Token: 0x04003DED RID: 15853
		public string optionDesc3;
	}
}
