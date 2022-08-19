using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000878 RID: 2168
	public class LianQiJieSuanBiao : IJSONClass
	{
		// Token: 0x06003FF3 RID: 16371 RVA: 0x001B4990 File Offset: 0x001B2B90
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

		// Token: 0x06003FF4 RID: 16372 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003CCA RID: 15562
		public static Dictionary<int, LianQiJieSuanBiao> DataDict = new Dictionary<int, LianQiJieSuanBiao>();

		// Token: 0x04003CCB RID: 15563
		public static List<LianQiJieSuanBiao> DataList = new List<LianQiJieSuanBiao>();

		// Token: 0x04003CCC RID: 15564
		public static Action OnInitFinishAction = new Action(LianQiJieSuanBiao.OnInitFinish);

		// Token: 0x04003CCD RID: 15565
		public int id;

		// Token: 0x04003CCE RID: 15566
		public int damage;

		// Token: 0x04003CCF RID: 15567
		public int exp;

		// Token: 0x04003CD0 RID: 15568
		public int time;
	}
}
