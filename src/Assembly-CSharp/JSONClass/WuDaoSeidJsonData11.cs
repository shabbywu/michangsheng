using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000984 RID: 2436
	public class WuDaoSeidJsonData11 : IJSONClass
	{
		// Token: 0x06004424 RID: 17444 RVA: 0x001D06D0 File Offset: 0x001CE8D0
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.WuDaoSeidJsonData[11].list)
			{
				try
				{
					WuDaoSeidJsonData11 wuDaoSeidJsonData = new WuDaoSeidJsonData11();
					wuDaoSeidJsonData.skillid = jsonobject["skillid"].I;
					wuDaoSeidJsonData.value1 = jsonobject["value1"].I;
					if (WuDaoSeidJsonData11.DataDict.ContainsKey(wuDaoSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典WuDaoSeidJsonData11.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", wuDaoSeidJsonData.skillid));
					}
					else
					{
						WuDaoSeidJsonData11.DataDict.Add(wuDaoSeidJsonData.skillid, wuDaoSeidJsonData);
						WuDaoSeidJsonData11.DataList.Add(wuDaoSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典WuDaoSeidJsonData11.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (WuDaoSeidJsonData11.OnInitFinishAction != null)
			{
				WuDaoSeidJsonData11.OnInitFinishAction();
			}
		}

		// Token: 0x06004425 RID: 17445 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040045B3 RID: 17843
		public static int SEIDID = 11;

		// Token: 0x040045B4 RID: 17844
		public static Dictionary<int, WuDaoSeidJsonData11> DataDict = new Dictionary<int, WuDaoSeidJsonData11>();

		// Token: 0x040045B5 RID: 17845
		public static List<WuDaoSeidJsonData11> DataList = new List<WuDaoSeidJsonData11>();

		// Token: 0x040045B6 RID: 17846
		public static Action OnInitFinishAction = new Action(WuDaoSeidJsonData11.OnInitFinish);

		// Token: 0x040045B7 RID: 17847
		public int skillid;

		// Token: 0x040045B8 RID: 17848
		public int value1;
	}
}
