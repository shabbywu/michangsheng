using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200098B RID: 2443
	public class WuDaoZhiJiaCheng : IJSONClass
	{
		// Token: 0x06004440 RID: 17472 RVA: 0x001D1044 File Offset: 0x001CF244
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.WuDaoZhiJiaCheng.list)
			{
				try
				{
					WuDaoZhiJiaCheng wuDaoZhiJiaCheng = new WuDaoZhiJiaCheng();
					wuDaoZhiJiaCheng.id = jsonobject["id"].I;
					wuDaoZhiJiaCheng.JiaCheng = jsonobject["JiaCheng"].I;
					if (WuDaoZhiJiaCheng.DataDict.ContainsKey(wuDaoZhiJiaCheng.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典WuDaoZhiJiaCheng.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", wuDaoZhiJiaCheng.id));
					}
					else
					{
						WuDaoZhiJiaCheng.DataDict.Add(wuDaoZhiJiaCheng.id, wuDaoZhiJiaCheng);
						WuDaoZhiJiaCheng.DataList.Add(wuDaoZhiJiaCheng);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典WuDaoZhiJiaCheng.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (WuDaoZhiJiaCheng.OnInitFinishAction != null)
			{
				WuDaoZhiJiaCheng.OnInitFinishAction();
			}
		}

		// Token: 0x06004441 RID: 17473 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040045DD RID: 17885
		public static Dictionary<int, WuDaoZhiJiaCheng> DataDict = new Dictionary<int, WuDaoZhiJiaCheng>();

		// Token: 0x040045DE RID: 17886
		public static List<WuDaoZhiJiaCheng> DataList = new List<WuDaoZhiJiaCheng>();

		// Token: 0x040045DF RID: 17887
		public static Action OnInitFinishAction = new Action(WuDaoZhiJiaCheng.OnInitFinish);

		// Token: 0x040045E0 RID: 17888
		public int id;

		// Token: 0x040045E1 RID: 17889
		public int JiaCheng;
	}
}
