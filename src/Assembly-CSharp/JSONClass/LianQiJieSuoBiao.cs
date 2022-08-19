using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000879 RID: 2169
	public class LianQiJieSuoBiao : IJSONClass
	{
		// Token: 0x06003FF7 RID: 16375 RVA: 0x001B4B08 File Offset: 0x001B2D08
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.LianQiJieSuoBiao.list)
			{
				try
				{
					LianQiJieSuoBiao lianQiJieSuoBiao = new LianQiJieSuoBiao();
					lianQiJieSuoBiao.id = jsonobject["id"].I;
					lianQiJieSuoBiao.desc = jsonobject["desc"].Str;
					lianQiJieSuoBiao.content = jsonobject["content"].ToList();
					if (LianQiJieSuoBiao.DataDict.ContainsKey(lianQiJieSuoBiao.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典LianQiJieSuoBiao.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", lianQiJieSuoBiao.id));
					}
					else
					{
						LianQiJieSuoBiao.DataDict.Add(lianQiJieSuoBiao.id, lianQiJieSuoBiao);
						LianQiJieSuoBiao.DataList.Add(lianQiJieSuoBiao);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典LianQiJieSuoBiao.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (LianQiJieSuoBiao.OnInitFinishAction != null)
			{
				LianQiJieSuoBiao.OnInitFinishAction();
			}
		}

		// Token: 0x06003FF8 RID: 16376 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003CD1 RID: 15569
		public static Dictionary<int, LianQiJieSuoBiao> DataDict = new Dictionary<int, LianQiJieSuoBiao>();

		// Token: 0x04003CD2 RID: 15570
		public static List<LianQiJieSuoBiao> DataList = new List<LianQiJieSuoBiao>();

		// Token: 0x04003CD3 RID: 15571
		public static Action OnInitFinishAction = new Action(LianQiJieSuoBiao.OnInitFinish);

		// Token: 0x04003CD4 RID: 15572
		public int id;

		// Token: 0x04003CD5 RID: 15573
		public string desc;

		// Token: 0x04003CD6 RID: 15574
		public List<int> content = new List<int>();
	}
}
