using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000D07 RID: 3335
	public class WuDaoSeidJsonData2 : IJSONClass
	{
		// Token: 0x06004F86 RID: 20358 RVA: 0x00215AC0 File Offset: 0x00213CC0
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

		// Token: 0x06004F87 RID: 20359 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040050B9 RID: 20665
		public static int SEIDID = 2;

		// Token: 0x040050BA RID: 20666
		public static Dictionary<int, WuDaoSeidJsonData2> DataDict = new Dictionary<int, WuDaoSeidJsonData2>();

		// Token: 0x040050BB RID: 20667
		public static List<WuDaoSeidJsonData2> DataList = new List<WuDaoSeidJsonData2>();

		// Token: 0x040050BC RID: 20668
		public static Action OnInitFinishAction = new Action(WuDaoSeidJsonData2.OnInitFinish);

		// Token: 0x040050BD RID: 20669
		public int skillid;

		// Token: 0x040050BE RID: 20670
		public int value1;
	}
}
