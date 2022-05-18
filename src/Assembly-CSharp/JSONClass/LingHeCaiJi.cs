using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C10 RID: 3088
	public class LingHeCaiJi : IJSONClass
	{
		// Token: 0x06004BA9 RID: 19369 RVA: 0x001FF138 File Offset: 0x001FD338
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

		// Token: 0x06004BAA RID: 19370 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004873 RID: 18547
		public static Dictionary<int, LingHeCaiJi> DataDict = new Dictionary<int, LingHeCaiJi>();

		// Token: 0x04004874 RID: 18548
		public static List<LingHeCaiJi> DataList = new List<LingHeCaiJi>();

		// Token: 0x04004875 RID: 18549
		public static Action OnInitFinishAction = new Action(LingHeCaiJi.OnInitFinish);

		// Token: 0x04004876 RID: 18550
		public int MapIndex;

		// Token: 0x04004877 RID: 18551
		public int ShouYiLv;

		// Token: 0x04004878 RID: 18552
		public int LingHe;

		// Token: 0x04004879 RID: 18553
		public int ShengShiLimit;
	}
}
