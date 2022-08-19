using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000983 RID: 2435
	public class WuDaoSeidJsonData10 : IJSONClass
	{
		// Token: 0x06004420 RID: 17440 RVA: 0x001D0578 File Offset: 0x001CE778
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.WuDaoSeidJsonData[10].list)
			{
				try
				{
					WuDaoSeidJsonData10 wuDaoSeidJsonData = new WuDaoSeidJsonData10();
					wuDaoSeidJsonData.skillid = jsonobject["skillid"].I;
					wuDaoSeidJsonData.value1 = jsonobject["value1"].I;
					if (WuDaoSeidJsonData10.DataDict.ContainsKey(wuDaoSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典WuDaoSeidJsonData10.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", wuDaoSeidJsonData.skillid));
					}
					else
					{
						WuDaoSeidJsonData10.DataDict.Add(wuDaoSeidJsonData.skillid, wuDaoSeidJsonData);
						WuDaoSeidJsonData10.DataList.Add(wuDaoSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典WuDaoSeidJsonData10.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (WuDaoSeidJsonData10.OnInitFinishAction != null)
			{
				WuDaoSeidJsonData10.OnInitFinishAction();
			}
		}

		// Token: 0x06004421 RID: 17441 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040045AD RID: 17837
		public static int SEIDID = 10;

		// Token: 0x040045AE RID: 17838
		public static Dictionary<int, WuDaoSeidJsonData10> DataDict = new Dictionary<int, WuDaoSeidJsonData10>();

		// Token: 0x040045AF RID: 17839
		public static List<WuDaoSeidJsonData10> DataList = new List<WuDaoSeidJsonData10>();

		// Token: 0x040045B0 RID: 17840
		public static Action OnInitFinishAction = new Action(WuDaoSeidJsonData10.OnInitFinish);

		// Token: 0x040045B1 RID: 17841
		public int skillid;

		// Token: 0x040045B2 RID: 17842
		public int value1;
	}
}
