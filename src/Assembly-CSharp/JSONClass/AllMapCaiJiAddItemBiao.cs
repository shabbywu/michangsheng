using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000748 RID: 1864
	public class AllMapCaiJiAddItemBiao : IJSONClass
	{
		// Token: 0x06003B34 RID: 15156 RVA: 0x001975EC File Offset: 0x001957EC
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.AllMapCaiJiAddItemBiao.list)
			{
				try
				{
					AllMapCaiJiAddItemBiao allMapCaiJiAddItemBiao = new AllMapCaiJiAddItemBiao();
					allMapCaiJiAddItemBiao.ID = jsonobject["ID"].I;
					allMapCaiJiAddItemBiao.percent = jsonobject["percent"].I;
					allMapCaiJiAddItemBiao.time = jsonobject["time"].I;
					if (AllMapCaiJiAddItemBiao.DataDict.ContainsKey(allMapCaiJiAddItemBiao.ID))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典AllMapCaiJiAddItemBiao.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", allMapCaiJiAddItemBiao.ID));
					}
					else
					{
						AllMapCaiJiAddItemBiao.DataDict.Add(allMapCaiJiAddItemBiao.ID, allMapCaiJiAddItemBiao);
						AllMapCaiJiAddItemBiao.DataList.Add(allMapCaiJiAddItemBiao);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典AllMapCaiJiAddItemBiao.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (AllMapCaiJiAddItemBiao.OnInitFinishAction != null)
			{
				AllMapCaiJiAddItemBiao.OnInitFinishAction();
			}
		}

		// Token: 0x06003B35 RID: 15157 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040033B9 RID: 13241
		public static Dictionary<int, AllMapCaiJiAddItemBiao> DataDict = new Dictionary<int, AllMapCaiJiAddItemBiao>();

		// Token: 0x040033BA RID: 13242
		public static List<AllMapCaiJiAddItemBiao> DataList = new List<AllMapCaiJiAddItemBiao>();

		// Token: 0x040033BB RID: 13243
		public static Action OnInitFinishAction = new Action(AllMapCaiJiAddItemBiao.OnInitFinish);

		// Token: 0x040033BC RID: 13244
		public int ID;

		// Token: 0x040033BD RID: 13245
		public int percent;

		// Token: 0x040033BE RID: 13246
		public int time;
	}
}
