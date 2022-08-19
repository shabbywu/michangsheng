using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000875 RID: 2165
	public class LianQiDuoDuanShangHaiBiao : IJSONClass
	{
		// Token: 0x06003FE7 RID: 16359 RVA: 0x001B426C File Offset: 0x001B246C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.LianQiDuoDuanShangHaiBiao.list)
			{
				try
				{
					LianQiDuoDuanShangHaiBiao lianQiDuoDuanShangHaiBiao = new LianQiDuoDuanShangHaiBiao();
					lianQiDuoDuanShangHaiBiao.id = jsonobject["id"].I;
					lianQiDuoDuanShangHaiBiao.seid = jsonobject["seid"].I;
					lianQiDuoDuanShangHaiBiao.value1 = jsonobject["value1"].I;
					lianQiDuoDuanShangHaiBiao.value2 = jsonobject["value2"].I;
					lianQiDuoDuanShangHaiBiao.value3 = jsonobject["value3"].I;
					lianQiDuoDuanShangHaiBiao.cast = jsonobject["cast"].I;
					lianQiDuoDuanShangHaiBiao.desc = jsonobject["desc"].Str;
					if (LianQiDuoDuanShangHaiBiao.DataDict.ContainsKey(lianQiDuoDuanShangHaiBiao.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典LianQiDuoDuanShangHaiBiao.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", lianQiDuoDuanShangHaiBiao.id));
					}
					else
					{
						LianQiDuoDuanShangHaiBiao.DataDict.Add(lianQiDuoDuanShangHaiBiao.id, lianQiDuoDuanShangHaiBiao);
						LianQiDuoDuanShangHaiBiao.DataList.Add(lianQiDuoDuanShangHaiBiao);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典LianQiDuoDuanShangHaiBiao.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (LianQiDuoDuanShangHaiBiao.OnInitFinishAction != null)
			{
				LianQiDuoDuanShangHaiBiao.OnInitFinishAction();
			}
		}

		// Token: 0x06003FE8 RID: 16360 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003C9D RID: 15517
		public static Dictionary<int, LianQiDuoDuanShangHaiBiao> DataDict = new Dictionary<int, LianQiDuoDuanShangHaiBiao>();

		// Token: 0x04003C9E RID: 15518
		public static List<LianQiDuoDuanShangHaiBiao> DataList = new List<LianQiDuoDuanShangHaiBiao>();

		// Token: 0x04003C9F RID: 15519
		public static Action OnInitFinishAction = new Action(LianQiDuoDuanShangHaiBiao.OnInitFinish);

		// Token: 0x04003CA0 RID: 15520
		public int id;

		// Token: 0x04003CA1 RID: 15521
		public int seid;

		// Token: 0x04003CA2 RID: 15522
		public int value1;

		// Token: 0x04003CA3 RID: 15523
		public int value2;

		// Token: 0x04003CA4 RID: 15524
		public int value3;

		// Token: 0x04003CA5 RID: 15525
		public int cast;

		// Token: 0x04003CA6 RID: 15526
		public string desc;
	}
}
