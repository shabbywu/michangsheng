using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000989 RID: 2441
	public class WuDaoSeidJsonData8 : IJSONClass
	{
		// Token: 0x06004438 RID: 17464 RVA: 0x001D0D88 File Offset: 0x001CEF88
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.WuDaoSeidJsonData[8].list)
			{
				try
				{
					WuDaoSeidJsonData8 wuDaoSeidJsonData = new WuDaoSeidJsonData8();
					wuDaoSeidJsonData.skillid = jsonobject["skillid"].I;
					wuDaoSeidJsonData.value1 = jsonobject["value1"].I;
					if (WuDaoSeidJsonData8.DataDict.ContainsKey(wuDaoSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典WuDaoSeidJsonData8.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", wuDaoSeidJsonData.skillid));
					}
					else
					{
						WuDaoSeidJsonData8.DataDict.Add(wuDaoSeidJsonData.skillid, wuDaoSeidJsonData);
						WuDaoSeidJsonData8.DataList.Add(wuDaoSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典WuDaoSeidJsonData8.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (WuDaoSeidJsonData8.OnInitFinishAction != null)
			{
				WuDaoSeidJsonData8.OnInitFinishAction();
			}
		}

		// Token: 0x06004439 RID: 17465 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040045D1 RID: 17873
		public static int SEIDID = 8;

		// Token: 0x040045D2 RID: 17874
		public static Dictionary<int, WuDaoSeidJsonData8> DataDict = new Dictionary<int, WuDaoSeidJsonData8>();

		// Token: 0x040045D3 RID: 17875
		public static List<WuDaoSeidJsonData8> DataList = new List<WuDaoSeidJsonData8>();

		// Token: 0x040045D4 RID: 17876
		public static Action OnInitFinishAction = new Action(WuDaoSeidJsonData8.OnInitFinish);

		// Token: 0x040045D5 RID: 17877
		public int skillid;

		// Token: 0x040045D6 RID: 17878
		public int value1;
	}
}
