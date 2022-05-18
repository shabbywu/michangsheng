using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000BD2 RID: 3026
	public class FuBenInfoJsonData : IJSONClass
	{
		// Token: 0x06004AB0 RID: 19120 RVA: 0x001F9864 File Offset: 0x001F7A64
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

		// Token: 0x06004AB1 RID: 19121 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004673 RID: 18035
		public static Dictionary<string, FuBenInfoJsonData> DataDict = new Dictionary<string, FuBenInfoJsonData>();

		// Token: 0x04004674 RID: 18036
		public static List<FuBenInfoJsonData> DataList = new List<FuBenInfoJsonData>();

		// Token: 0x04004675 RID: 18037
		public static Action OnInitFinishAction = new Action(FuBenInfoJsonData.OnInitFinish);

		// Token: 0x04004676 RID: 18038
		public int TimeY;

		// Token: 0x04004677 RID: 18039
		public int TimeM;

		// Token: 0x04004678 RID: 18040
		public int TimeD;

		// Token: 0x04004679 RID: 18041
		public int CanDie;

		// Token: 0x0400467A RID: 18042
		public string Name;
	}
}
