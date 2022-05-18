using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000D03 RID: 3331
	public class WuDaoSeidJsonData10 : IJSONClass
	{
		// Token: 0x06004F76 RID: 20342 RVA: 0x00215620 File Offset: 0x00213820
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

		// Token: 0x06004F77 RID: 20343 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040050A1 RID: 20641
		public static int SEIDID = 10;

		// Token: 0x040050A2 RID: 20642
		public static Dictionary<int, WuDaoSeidJsonData10> DataDict = new Dictionary<int, WuDaoSeidJsonData10>();

		// Token: 0x040050A3 RID: 20643
		public static List<WuDaoSeidJsonData10> DataList = new List<WuDaoSeidJsonData10>();

		// Token: 0x040050A4 RID: 20644
		public static Action OnInitFinishAction = new Action(WuDaoSeidJsonData10.OnInitFinish);

		// Token: 0x040050A5 RID: 20645
		public int skillid;

		// Token: 0x040050A6 RID: 20646
		public int value1;
	}
}
