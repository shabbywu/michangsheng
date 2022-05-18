using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000D0A RID: 3338
	public class WuDaoZhiData : IJSONClass
	{
		// Token: 0x06004F92 RID: 20370 RVA: 0x00215E38 File Offset: 0x00214038
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

		// Token: 0x06004F93 RID: 20371 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040050CB RID: 20683
		public static Dictionary<int, WuDaoZhiData> DataDict = new Dictionary<int, WuDaoZhiData>();

		// Token: 0x040050CC RID: 20684
		public static List<WuDaoZhiData> DataList = new List<WuDaoZhiData>();

		// Token: 0x040050CD RID: 20685
		public static Action OnInitFinishAction = new Action(WuDaoZhiData.OnInitFinish);

		// Token: 0x040050CE RID: 20686
		public int id;

		// Token: 0x040050CF RID: 20687
		public int LevelUpExp;

		// Token: 0x040050D0 RID: 20688
		public int LevelUpNum;
	}
}
