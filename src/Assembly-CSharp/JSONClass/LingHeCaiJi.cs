using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000882 RID: 2178
	public class LingHeCaiJi : IJSONClass
	{
		// Token: 0x0600401B RID: 16411 RVA: 0x001B59D8 File Offset: 0x001B3BD8
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.LingHeCaiJi.list)
			{
				try
				{
					LingHeCaiJi lingHeCaiJi = new LingHeCaiJi();
					lingHeCaiJi.MapIndex = jsonobject["MapIndex"].I;
					lingHeCaiJi.ShouYiLv = jsonobject["ShouYiLv"].I;
					lingHeCaiJi.LingHe = jsonobject["LingHe"].I;
					lingHeCaiJi.ShengShiLimit = jsonobject["ShengShiLimit"].I;
					if (LingHeCaiJi.DataDict.ContainsKey(lingHeCaiJi.MapIndex))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典LingHeCaiJi.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", lingHeCaiJi.MapIndex));
					}
					else
					{
						LingHeCaiJi.DataDict.Add(lingHeCaiJi.MapIndex, lingHeCaiJi);
						LingHeCaiJi.DataList.Add(lingHeCaiJi);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典LingHeCaiJi.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (LingHeCaiJi.OnInitFinishAction != null)
			{
				LingHeCaiJi.OnInitFinishAction();
			}
		}

		// Token: 0x0600401C RID: 16412 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003D1A RID: 15642
		public static Dictionary<int, LingHeCaiJi> DataDict = new Dictionary<int, LingHeCaiJi>();

		// Token: 0x04003D1B RID: 15643
		public static List<LingHeCaiJi> DataList = new List<LingHeCaiJi>();

		// Token: 0x04003D1C RID: 15644
		public static Action OnInitFinishAction = new Action(LingHeCaiJi.OnInitFinish);

		// Token: 0x04003D1D RID: 15645
		public int MapIndex;

		// Token: 0x04003D1E RID: 15646
		public int ShouYiLv;

		// Token: 0x04003D1F RID: 15647
		public int LingHe;

		// Token: 0x04003D20 RID: 15648
		public int ShengShiLimit;
	}
}
