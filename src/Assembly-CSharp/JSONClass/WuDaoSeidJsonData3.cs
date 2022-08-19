using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000988 RID: 2440
	public class WuDaoSeidJsonData3 : IJSONClass
	{
		// Token: 0x06004434 RID: 17460 RVA: 0x001D0C30 File Offset: 0x001CEE30
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.WuDaoSeidJsonData[3].list)
			{
				try
				{
					WuDaoSeidJsonData3 wuDaoSeidJsonData = new WuDaoSeidJsonData3();
					wuDaoSeidJsonData.skillid = jsonobject["skillid"].I;
					wuDaoSeidJsonData.value1 = jsonobject["value1"].I;
					if (WuDaoSeidJsonData3.DataDict.ContainsKey(wuDaoSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典WuDaoSeidJsonData3.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", wuDaoSeidJsonData.skillid));
					}
					else
					{
						WuDaoSeidJsonData3.DataDict.Add(wuDaoSeidJsonData.skillid, wuDaoSeidJsonData);
						WuDaoSeidJsonData3.DataList.Add(wuDaoSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典WuDaoSeidJsonData3.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (WuDaoSeidJsonData3.OnInitFinishAction != null)
			{
				WuDaoSeidJsonData3.OnInitFinishAction();
			}
		}

		// Token: 0x06004435 RID: 17461 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040045CB RID: 17867
		public static int SEIDID = 3;

		// Token: 0x040045CC RID: 17868
		public static Dictionary<int, WuDaoSeidJsonData3> DataDict = new Dictionary<int, WuDaoSeidJsonData3>();

		// Token: 0x040045CD RID: 17869
		public static List<WuDaoSeidJsonData3> DataList = new List<WuDaoSeidJsonData3>();

		// Token: 0x040045CE RID: 17870
		public static Action OnInitFinishAction = new Action(WuDaoSeidJsonData3.OnInitFinish);

		// Token: 0x040045CF RID: 17871
		public int skillid;

		// Token: 0x040045D0 RID: 17872
		public int value1;
	}
}
