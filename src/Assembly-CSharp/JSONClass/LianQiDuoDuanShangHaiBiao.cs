using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C03 RID: 3075
	public class LianQiDuoDuanShangHaiBiao : IJSONClass
	{
		// Token: 0x06004B75 RID: 19317 RVA: 0x001FDBE4 File Offset: 0x001FBDE4
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

		// Token: 0x06004B76 RID: 19318 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040047F6 RID: 18422
		public static Dictionary<int, LianQiDuoDuanShangHaiBiao> DataDict = new Dictionary<int, LianQiDuoDuanShangHaiBiao>();

		// Token: 0x040047F7 RID: 18423
		public static List<LianQiDuoDuanShangHaiBiao> DataList = new List<LianQiDuoDuanShangHaiBiao>();

		// Token: 0x040047F8 RID: 18424
		public static Action OnInitFinishAction = new Action(LianQiDuoDuanShangHaiBiao.OnInitFinish);

		// Token: 0x040047F9 RID: 18425
		public int id;

		// Token: 0x040047FA RID: 18426
		public int seid;

		// Token: 0x040047FB RID: 18427
		public int value1;

		// Token: 0x040047FC RID: 18428
		public int value2;

		// Token: 0x040047FD RID: 18429
		public int value3;

		// Token: 0x040047FE RID: 18430
		public int cast;

		// Token: 0x040047FF RID: 18431
		public string desc;
	}
}
