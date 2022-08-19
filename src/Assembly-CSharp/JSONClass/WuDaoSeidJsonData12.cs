using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000985 RID: 2437
	public class WuDaoSeidJsonData12 : IJSONClass
	{
		// Token: 0x06004428 RID: 17448 RVA: 0x001D0828 File Offset: 0x001CEA28
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.WuDaoSeidJsonData[12].list)
			{
				try
				{
					WuDaoSeidJsonData12 wuDaoSeidJsonData = new WuDaoSeidJsonData12();
					wuDaoSeidJsonData.skillid = jsonobject["skillid"].I;
					wuDaoSeidJsonData.value1 = jsonobject["value1"].I;
					if (WuDaoSeidJsonData12.DataDict.ContainsKey(wuDaoSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典WuDaoSeidJsonData12.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", wuDaoSeidJsonData.skillid));
					}
					else
					{
						WuDaoSeidJsonData12.DataDict.Add(wuDaoSeidJsonData.skillid, wuDaoSeidJsonData);
						WuDaoSeidJsonData12.DataList.Add(wuDaoSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典WuDaoSeidJsonData12.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (WuDaoSeidJsonData12.OnInitFinishAction != null)
			{
				WuDaoSeidJsonData12.OnInitFinishAction();
			}
		}

		// Token: 0x06004429 RID: 17449 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040045B9 RID: 17849
		public static int SEIDID = 12;

		// Token: 0x040045BA RID: 17850
		public static Dictionary<int, WuDaoSeidJsonData12> DataDict = new Dictionary<int, WuDaoSeidJsonData12>();

		// Token: 0x040045BB RID: 17851
		public static List<WuDaoSeidJsonData12> DataList = new List<WuDaoSeidJsonData12>();

		// Token: 0x040045BC RID: 17852
		public static Action OnInitFinishAction = new Action(WuDaoSeidJsonData12.OnInitFinish);

		// Token: 0x040045BD RID: 17853
		public int skillid;

		// Token: 0x040045BE RID: 17854
		public int value1;
	}
}
