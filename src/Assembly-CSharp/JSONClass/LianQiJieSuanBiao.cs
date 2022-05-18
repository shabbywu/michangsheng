using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C06 RID: 3078
	public class LianQiJieSuanBiao : IJSONClass
	{
		// Token: 0x06004B81 RID: 19329 RVA: 0x001FE290 File Offset: 0x001FC490
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.LianQiJieSuanBiao.list)
			{
				try
				{
					LianQiJieSuanBiao lianQiJieSuanBiao = new LianQiJieSuanBiao();
					lianQiJieSuanBiao.id = jsonobject["id"].I;
					lianQiJieSuanBiao.damage = jsonobject["damage"].I;
					lianQiJieSuanBiao.exp = jsonobject["exp"].I;
					lianQiJieSuanBiao.time = jsonobject["time"].I;
					if (LianQiJieSuanBiao.DataDict.ContainsKey(lianQiJieSuanBiao.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典LianQiJieSuanBiao.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", lianQiJieSuanBiao.id));
					}
					else
					{
						LianQiJieSuanBiao.DataDict.Add(lianQiJieSuanBiao.id, lianQiJieSuanBiao);
						LianQiJieSuanBiao.DataList.Add(lianQiJieSuanBiao);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典LianQiJieSuanBiao.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (LianQiJieSuanBiao.OnInitFinishAction != null)
			{
				LianQiJieSuanBiao.OnInitFinishAction();
			}
		}

		// Token: 0x06004B82 RID: 19330 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004823 RID: 18467
		public static Dictionary<int, LianQiJieSuanBiao> DataDict = new Dictionary<int, LianQiJieSuanBiao>();

		// Token: 0x04004824 RID: 18468
		public static List<LianQiJieSuanBiao> DataList = new List<LianQiJieSuanBiao>();

		// Token: 0x04004825 RID: 18469
		public static Action OnInitFinishAction = new Action(LianQiJieSuanBiao.OnInitFinish);

		// Token: 0x04004826 RID: 18470
		public int id;

		// Token: 0x04004827 RID: 18471
		public int damage;

		// Token: 0x04004828 RID: 18472
		public int exp;

		// Token: 0x04004829 RID: 18473
		public int time;
	}
}
