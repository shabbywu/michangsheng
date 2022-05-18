using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000D0E RID: 3342
	public class WuXianBiGuanJsonData : IJSONClass
	{
		// Token: 0x06004FA2 RID: 20386 RVA: 0x0021637C File Offset: 0x0021457C
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

		// Token: 0x06004FA3 RID: 20387 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040050E6 RID: 20710
		public static Dictionary<int, WuXianBiGuanJsonData> DataDict = new Dictionary<int, WuXianBiGuanJsonData>();

		// Token: 0x040050E7 RID: 20711
		public static List<WuXianBiGuanJsonData> DataList = new List<WuXianBiGuanJsonData>();

		// Token: 0x040050E8 RID: 20712
		public static Action OnInitFinishAction = new Action(WuXianBiGuanJsonData.OnInitFinish);

		// Token: 0x040050E9 RID: 20713
		public int id;

		// Token: 0x040050EA RID: 20714
		public string SceneName;
	}
}
