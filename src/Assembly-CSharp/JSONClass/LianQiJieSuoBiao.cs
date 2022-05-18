using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C07 RID: 3079
	public class LianQiJieSuoBiao : IJSONClass
	{
		// Token: 0x06004B85 RID: 19333 RVA: 0x001FE3E0 File Offset: 0x001FC5E0
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

		// Token: 0x06004B86 RID: 19334 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400482A RID: 18474
		public static Dictionary<int, LianQiJieSuoBiao> DataDict = new Dictionary<int, LianQiJieSuoBiao>();

		// Token: 0x0400482B RID: 18475
		public static List<LianQiJieSuoBiao> DataList = new List<LianQiJieSuoBiao>();

		// Token: 0x0400482C RID: 18476
		public static Action OnInitFinishAction = new Action(LianQiJieSuoBiao.OnInitFinish);

		// Token: 0x0400482D RID: 18477
		public int id;

		// Token: 0x0400482E RID: 18478
		public string desc;

		// Token: 0x0400482F RID: 18479
		public List<int> content = new List<int>();
	}
}
