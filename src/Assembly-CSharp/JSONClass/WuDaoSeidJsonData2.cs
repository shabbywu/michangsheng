using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000987 RID: 2439
	public class WuDaoSeidJsonData2 : IJSONClass
	{
		// Token: 0x06004430 RID: 17456 RVA: 0x001D0AD8 File Offset: 0x001CECD8
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.WuDaoSeidJsonData[2].list)
			{
				try
				{
					WuDaoSeidJsonData2 wuDaoSeidJsonData = new WuDaoSeidJsonData2();
					wuDaoSeidJsonData.skillid = jsonobject["skillid"].I;
					wuDaoSeidJsonData.value1 = jsonobject["value1"].I;
					if (WuDaoSeidJsonData2.DataDict.ContainsKey(wuDaoSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典WuDaoSeidJsonData2.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", wuDaoSeidJsonData.skillid));
					}
					else
					{
						WuDaoSeidJsonData2.DataDict.Add(wuDaoSeidJsonData.skillid, wuDaoSeidJsonData);
						WuDaoSeidJsonData2.DataList.Add(wuDaoSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典WuDaoSeidJsonData2.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (WuDaoSeidJsonData2.OnInitFinishAction != null)
			{
				WuDaoSeidJsonData2.OnInitFinishAction();
			}
		}

		// Token: 0x06004431 RID: 17457 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040045C5 RID: 17861
		public static int SEIDID = 2;

		// Token: 0x040045C6 RID: 17862
		public static Dictionary<int, WuDaoSeidJsonData2> DataDict = new Dictionary<int, WuDaoSeidJsonData2>();

		// Token: 0x040045C7 RID: 17863
		public static List<WuDaoSeidJsonData2> DataList = new List<WuDaoSeidJsonData2>();

		// Token: 0x040045C8 RID: 17864
		public static Action OnInitFinishAction = new Action(WuDaoSeidJsonData2.OnInitFinish);

		// Token: 0x040045C9 RID: 17865
		public int skillid;

		// Token: 0x040045CA RID: 17866
		public int value1;
	}
}
