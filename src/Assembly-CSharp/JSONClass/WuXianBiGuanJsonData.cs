using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200098E RID: 2446
	public class WuXianBiGuanJsonData : IJSONClass
	{
		// Token: 0x0600444C RID: 17484 RVA: 0x001D14EC File Offset: 0x001CF6EC
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.WuXianBiGuanJsonData.list)
			{
				try
				{
					WuXianBiGuanJsonData wuXianBiGuanJsonData = new WuXianBiGuanJsonData();
					wuXianBiGuanJsonData.id = jsonobject["id"].I;
					wuXianBiGuanJsonData.SceneName = jsonobject["SceneName"].Str;
					if (WuXianBiGuanJsonData.DataDict.ContainsKey(wuXianBiGuanJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典WuXianBiGuanJsonData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", wuXianBiGuanJsonData.id));
					}
					else
					{
						WuXianBiGuanJsonData.DataDict.Add(wuXianBiGuanJsonData.id, wuXianBiGuanJsonData);
						WuXianBiGuanJsonData.DataList.Add(wuXianBiGuanJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典WuXianBiGuanJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (WuXianBiGuanJsonData.OnInitFinishAction != null)
			{
				WuXianBiGuanJsonData.OnInitFinishAction();
			}
		}

		// Token: 0x0600444D RID: 17485 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040045F2 RID: 17906
		public static Dictionary<int, WuXianBiGuanJsonData> DataDict = new Dictionary<int, WuXianBiGuanJsonData>();

		// Token: 0x040045F3 RID: 17907
		public static List<WuXianBiGuanJsonData> DataList = new List<WuXianBiGuanJsonData>();

		// Token: 0x040045F4 RID: 17908
		public static Action OnInitFinishAction = new Action(WuXianBiGuanJsonData.OnInitFinish);

		// Token: 0x040045F5 RID: 17909
		public int id;

		// Token: 0x040045F6 RID: 17910
		public string SceneName;
	}
}
