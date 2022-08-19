using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200083E RID: 2110
	public class FavorabilityInfoJsonData : IJSONClass
	{
		// Token: 0x06003F0A RID: 16138 RVA: 0x001AEAC4 File Offset: 0x001ACCC4
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.FavorabilityInfoJsonData.list)
			{
				try
				{
					FavorabilityInfoJsonData favorabilityInfoJsonData = new FavorabilityInfoJsonData();
					favorabilityInfoJsonData.id = jsonobject["id"].I;
					favorabilityInfoJsonData.AvatarID = jsonobject["AvatarID"].I;
					favorabilityInfoJsonData.JinDu = jsonobject["JinDu"].I;
					favorabilityInfoJsonData.ItemID = jsonobject["ItemID"].I;
					favorabilityInfoJsonData.HaoGanDu = jsonobject["HaoGanDu"].I;
					favorabilityInfoJsonData.Time = jsonobject["Time"].I;
					favorabilityInfoJsonData.AvatarLevel = jsonobject["AvatarLevel"].I;
					favorabilityInfoJsonData.yes = jsonobject["yes"].Str;
					favorabilityInfoJsonData.no = jsonobject["no"].Str;
					if (FavorabilityInfoJsonData.DataDict.ContainsKey(favorabilityInfoJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典FavorabilityInfoJsonData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", favorabilityInfoJsonData.id));
					}
					else
					{
						FavorabilityInfoJsonData.DataDict.Add(favorabilityInfoJsonData.id, favorabilityInfoJsonData);
						FavorabilityInfoJsonData.DataList.Add(favorabilityInfoJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典FavorabilityInfoJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (FavorabilityInfoJsonData.OnInitFinishAction != null)
			{
				FavorabilityInfoJsonData.OnInitFinishAction();
			}
		}

		// Token: 0x06003F0B RID: 16139 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003AE6 RID: 15078
		public static Dictionary<int, FavorabilityInfoJsonData> DataDict = new Dictionary<int, FavorabilityInfoJsonData>();

		// Token: 0x04003AE7 RID: 15079
		public static List<FavorabilityInfoJsonData> DataList = new List<FavorabilityInfoJsonData>();

		// Token: 0x04003AE8 RID: 15080
		public static Action OnInitFinishAction = new Action(FavorabilityInfoJsonData.OnInitFinish);

		// Token: 0x04003AE9 RID: 15081
		public int id;

		// Token: 0x04003AEA RID: 15082
		public int AvatarID;

		// Token: 0x04003AEB RID: 15083
		public int JinDu;

		// Token: 0x04003AEC RID: 15084
		public int ItemID;

		// Token: 0x04003AED RID: 15085
		public int HaoGanDu;

		// Token: 0x04003AEE RID: 15086
		public int Time;

		// Token: 0x04003AEF RID: 15087
		public int AvatarLevel;

		// Token: 0x04003AF0 RID: 15088
		public string yes;

		// Token: 0x04003AF1 RID: 15089
		public string no;
	}
}
