using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200074A RID: 1866
	public class AllMapCaiJiMiaoShuBiao : IJSONClass
	{
		// Token: 0x06003B3C RID: 15164 RVA: 0x00197958 File Offset: 0x00195B58
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.AllMapCaiJiMiaoShuBiao.list)
			{
				try
				{
					AllMapCaiJiMiaoShuBiao allMapCaiJiMiaoShuBiao = new AllMapCaiJiMiaoShuBiao();
					allMapCaiJiMiaoShuBiao.ID = jsonobject["ID"].I;
					allMapCaiJiMiaoShuBiao.desc1 = jsonobject["desc1"].Str;
					allMapCaiJiMiaoShuBiao.desc2 = jsonobject["desc2"].Str;
					allMapCaiJiMiaoShuBiao.desc3 = jsonobject["desc3"].Str;
					allMapCaiJiMiaoShuBiao.desc4 = jsonobject["desc4"].Str;
					allMapCaiJiMiaoShuBiao.desc5 = jsonobject["desc5"].Str;
					allMapCaiJiMiaoShuBiao.desc6 = jsonobject["desc6"].Str;
					allMapCaiJiMiaoShuBiao.desc7 = jsonobject["desc7"].Str;
					if (AllMapCaiJiMiaoShuBiao.DataDict.ContainsKey(allMapCaiJiMiaoShuBiao.ID))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典AllMapCaiJiMiaoShuBiao.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", allMapCaiJiMiaoShuBiao.ID));
					}
					else
					{
						AllMapCaiJiMiaoShuBiao.DataDict.Add(allMapCaiJiMiaoShuBiao.ID, allMapCaiJiMiaoShuBiao);
						AllMapCaiJiMiaoShuBiao.DataList.Add(allMapCaiJiMiaoShuBiao);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典AllMapCaiJiMiaoShuBiao.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (AllMapCaiJiMiaoShuBiao.OnInitFinishAction != null)
			{
				AllMapCaiJiMiaoShuBiao.OnInitFinishAction();
			}
		}

		// Token: 0x06003B3D RID: 15165 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040033CA RID: 13258
		public static Dictionary<int, AllMapCaiJiMiaoShuBiao> DataDict = new Dictionary<int, AllMapCaiJiMiaoShuBiao>();

		// Token: 0x040033CB RID: 13259
		public static List<AllMapCaiJiMiaoShuBiao> DataList = new List<AllMapCaiJiMiaoShuBiao>();

		// Token: 0x040033CC RID: 13260
		public static Action OnInitFinishAction = new Action(AllMapCaiJiMiaoShuBiao.OnInitFinish);

		// Token: 0x040033CD RID: 13261
		public int ID;

		// Token: 0x040033CE RID: 13262
		public string desc1;

		// Token: 0x040033CF RID: 13263
		public string desc2;

		// Token: 0x040033D0 RID: 13264
		public string desc3;

		// Token: 0x040033D1 RID: 13265
		public string desc4;

		// Token: 0x040033D2 RID: 13266
		public string desc5;

		// Token: 0x040033D3 RID: 13267
		public string desc6;

		// Token: 0x040033D4 RID: 13268
		public string desc7;
	}
}
