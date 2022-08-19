using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000841 RID: 2113
	public class FuBenInfoJsonData : IJSONClass
	{
		// Token: 0x06003F16 RID: 16150 RVA: 0x001AF050 File Offset: 0x001AD250
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.FuBenInfoJsonData.list)
			{
				try
				{
					FuBenInfoJsonData fuBenInfoJsonData = new FuBenInfoJsonData();
					fuBenInfoJsonData.TimeY = jsonobject["TimeY"].I;
					fuBenInfoJsonData.TimeM = jsonobject["TimeM"].I;
					fuBenInfoJsonData.TimeD = jsonobject["TimeD"].I;
					fuBenInfoJsonData.CanDie = jsonobject["CanDie"].I;
					fuBenInfoJsonData.Name = jsonobject["Name"].Str;
					if (FuBenInfoJsonData.DataDict.ContainsKey(fuBenInfoJsonData.Name))
					{
						PreloadManager.LogException("!!!错误!!!向字典FuBenInfoJsonData.DataDict添加数据时出现重复的键，Key:" + fuBenInfoJsonData.Name + "，已跳过，请检查配表");
					}
					else
					{
						FuBenInfoJsonData.DataDict.Add(fuBenInfoJsonData.Name, fuBenInfoJsonData);
						FuBenInfoJsonData.DataList.Add(fuBenInfoJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典FuBenInfoJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (FuBenInfoJsonData.OnInitFinishAction != null)
			{
				FuBenInfoJsonData.OnInitFinishAction();
			}
		}

		// Token: 0x06003F17 RID: 16151 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003B05 RID: 15109
		public static Dictionary<string, FuBenInfoJsonData> DataDict = new Dictionary<string, FuBenInfoJsonData>();

		// Token: 0x04003B06 RID: 15110
		public static List<FuBenInfoJsonData> DataList = new List<FuBenInfoJsonData>();

		// Token: 0x04003B07 RID: 15111
		public static Action OnInitFinishAction = new Action(FuBenInfoJsonData.OnInitFinish);

		// Token: 0x04003B08 RID: 15112
		public int TimeY;

		// Token: 0x04003B09 RID: 15113
		public int TimeM;

		// Token: 0x04003B0A RID: 15114
		public int TimeD;

		// Token: 0x04003B0B RID: 15115
		public int CanDie;

		// Token: 0x04003B0C RID: 15116
		public string Name;
	}
}
