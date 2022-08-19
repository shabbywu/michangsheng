using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000982 RID: 2434
	public class WuDaoSeidJsonData1 : IJSONClass
	{
		// Token: 0x0600441C RID: 17436 RVA: 0x001D03D8 File Offset: 0x001CE5D8
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

		// Token: 0x0600441D RID: 17437 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040045A5 RID: 17829
		public static int SEIDID = 1;

		// Token: 0x040045A6 RID: 17830
		public static Dictionary<int, WuDaoSeidJsonData1> DataDict = new Dictionary<int, WuDaoSeidJsonData1>();

		// Token: 0x040045A7 RID: 17831
		public static List<WuDaoSeidJsonData1> DataList = new List<WuDaoSeidJsonData1>();

		// Token: 0x040045A8 RID: 17832
		public static Action OnInitFinishAction = new Action(WuDaoSeidJsonData1.OnInitFinish);

		// Token: 0x040045A9 RID: 17833
		public int skillid;

		// Token: 0x040045AA RID: 17834
		public int target;

		// Token: 0x040045AB RID: 17835
		public List<int> value1 = new List<int>();

		// Token: 0x040045AC RID: 17836
		public List<int> value2 = new List<int>();
	}
}
