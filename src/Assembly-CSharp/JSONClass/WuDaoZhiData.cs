using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200098A RID: 2442
	public class WuDaoZhiData : IJSONClass
	{
		// Token: 0x0600443C RID: 17468 RVA: 0x001D0EE0 File Offset: 0x001CF0E0
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.WuDaoZhiData.list)
			{
				try
				{
					WuDaoZhiData wuDaoZhiData = new WuDaoZhiData();
					wuDaoZhiData.id = jsonobject["id"].I;
					wuDaoZhiData.LevelUpExp = jsonobject["LevelUpExp"].I;
					wuDaoZhiData.LevelUpNum = jsonobject["LevelUpNum"].I;
					if (WuDaoZhiData.DataDict.ContainsKey(wuDaoZhiData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典WuDaoZhiData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", wuDaoZhiData.id));
					}
					else
					{
						WuDaoZhiData.DataDict.Add(wuDaoZhiData.id, wuDaoZhiData);
						WuDaoZhiData.DataList.Add(wuDaoZhiData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典WuDaoZhiData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (WuDaoZhiData.OnInitFinishAction != null)
			{
				WuDaoZhiData.OnInitFinishAction();
			}
		}

		// Token: 0x0600443D RID: 17469 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040045D7 RID: 17879
		public static Dictionary<int, WuDaoZhiData> DataDict = new Dictionary<int, WuDaoZhiData>();

		// Token: 0x040045D8 RID: 17880
		public static List<WuDaoZhiData> DataList = new List<WuDaoZhiData>();

		// Token: 0x040045D9 RID: 17881
		public static Action OnInitFinishAction = new Action(WuDaoZhiData.OnInitFinish);

		// Token: 0x040045DA RID: 17882
		public int id;

		// Token: 0x040045DB RID: 17883
		public int LevelUpExp;

		// Token: 0x040045DC RID: 17884
		public int LevelUpNum;
	}
}
