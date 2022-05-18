using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000D02 RID: 3330
	public class WuDaoSeidJsonData1 : IJSONClass
	{
		// Token: 0x06004F72 RID: 20338 RVA: 0x002154CC File Offset: 0x002136CC
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.WuDaoSeidJsonData[1].list)
			{
				try
				{
					WuDaoSeidJsonData1 wuDaoSeidJsonData = new WuDaoSeidJsonData1();
					wuDaoSeidJsonData.skillid = jsonobject["skillid"].I;
					wuDaoSeidJsonData.target = jsonobject["target"].I;
					wuDaoSeidJsonData.value1 = jsonobject["value1"].ToList();
					wuDaoSeidJsonData.value2 = jsonobject["value2"].ToList();
					if (WuDaoSeidJsonData1.DataDict.ContainsKey(wuDaoSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典WuDaoSeidJsonData1.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", wuDaoSeidJsonData.skillid));
					}
					else
					{
						WuDaoSeidJsonData1.DataDict.Add(wuDaoSeidJsonData.skillid, wuDaoSeidJsonData);
						WuDaoSeidJsonData1.DataList.Add(wuDaoSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典WuDaoSeidJsonData1.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (WuDaoSeidJsonData1.OnInitFinishAction != null)
			{
				WuDaoSeidJsonData1.OnInitFinishAction();
			}
		}

		// Token: 0x06004F73 RID: 20339 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04005099 RID: 20633
		public static int SEIDID = 1;

		// Token: 0x0400509A RID: 20634
		public static Dictionary<int, WuDaoSeidJsonData1> DataDict = new Dictionary<int, WuDaoSeidJsonData1>();

		// Token: 0x0400509B RID: 20635
		public static List<WuDaoSeidJsonData1> DataList = new List<WuDaoSeidJsonData1>();

		// Token: 0x0400509C RID: 20636
		public static Action OnInitFinishAction = new Action(WuDaoSeidJsonData1.OnInitFinish);

		// Token: 0x0400509D RID: 20637
		public int skillid;

		// Token: 0x0400509E RID: 20638
		public int target;

		// Token: 0x0400509F RID: 20639
		public List<int> value1 = new List<int>();

		// Token: 0x040050A0 RID: 20640
		public List<int> value2 = new List<int>();
	}
}
