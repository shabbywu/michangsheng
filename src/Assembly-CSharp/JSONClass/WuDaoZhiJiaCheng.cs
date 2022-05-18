using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000D0B RID: 3339
	public class WuDaoZhiJiaCheng : IJSONClass
	{
		// Token: 0x06004F96 RID: 20374 RVA: 0x00215F74 File Offset: 0x00214174
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

		// Token: 0x06004F97 RID: 20375 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040050D1 RID: 20689
		public static Dictionary<int, WuDaoZhiJiaCheng> DataDict = new Dictionary<int, WuDaoZhiJiaCheng>();

		// Token: 0x040050D2 RID: 20690
		public static List<WuDaoZhiJiaCheng> DataList = new List<WuDaoZhiJiaCheng>();

		// Token: 0x040050D3 RID: 20691
		public static Action OnInitFinishAction = new Action(WuDaoZhiJiaCheng.OnInitFinish);

		// Token: 0x040050D4 RID: 20692
		public int id;

		// Token: 0x040050D5 RID: 20693
		public int JiaCheng;
	}
}
