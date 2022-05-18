using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000D09 RID: 3337
	public class WuDaoSeidJsonData8 : IJSONClass
	{
		// Token: 0x06004F8E RID: 20366 RVA: 0x00215D10 File Offset: 0x00213F10
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

		// Token: 0x06004F8F RID: 20367 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040050C5 RID: 20677
		public static int SEIDID = 8;

		// Token: 0x040050C6 RID: 20678
		public static Dictionary<int, WuDaoSeidJsonData8> DataDict = new Dictionary<int, WuDaoSeidJsonData8>();

		// Token: 0x040050C7 RID: 20679
		public static List<WuDaoSeidJsonData8> DataList = new List<WuDaoSeidJsonData8>();

		// Token: 0x040050C8 RID: 20680
		public static Action OnInitFinishAction = new Action(WuDaoSeidJsonData8.OnInitFinish);

		// Token: 0x040050C9 RID: 20681
		public int skillid;

		// Token: 0x040050CA RID: 20682
		public int value1;
	}
}
