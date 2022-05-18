using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C25 RID: 3109
	public class NpcBiaoBaiTiKuData : IJSONClass
	{
		// Token: 0x06004BFD RID: 19453 RVA: 0x00201318 File Offset: 0x001FF518
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

		// Token: 0x06004BFE RID: 19454 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400493E RID: 18750
		public static Dictionary<int, NpcBiaoBaiTiKuData> DataDict = new Dictionary<int, NpcBiaoBaiTiKuData>();

		// Token: 0x0400493F RID: 18751
		public static List<NpcBiaoBaiTiKuData> DataList = new List<NpcBiaoBaiTiKuData>();

		// Token: 0x04004940 RID: 18752
		public static Action OnInitFinishAction = new Action(NpcBiaoBaiTiKuData.OnInitFinish);

		// Token: 0x04004941 RID: 18753
		public int id;

		// Token: 0x04004942 RID: 18754
		public int Type;

		// Token: 0x04004943 RID: 18755
		public string TiWen;

		// Token: 0x04004944 RID: 18756
		public string optionDesc1;

		// Token: 0x04004945 RID: 18757
		public string optionDesc2;

		// Token: 0x04004946 RID: 18758
		public string optionDesc3;
	}
}
