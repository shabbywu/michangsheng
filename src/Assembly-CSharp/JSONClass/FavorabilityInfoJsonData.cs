using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000BCF RID: 3023
	public class FavorabilityInfoJsonData : IJSONClass
	{
		// Token: 0x06004AA4 RID: 19108 RVA: 0x001F9364 File Offset: 0x001F7564
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

		// Token: 0x06004AA5 RID: 19109 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004654 RID: 18004
		public static Dictionary<int, FavorabilityInfoJsonData> DataDict = new Dictionary<int, FavorabilityInfoJsonData>();

		// Token: 0x04004655 RID: 18005
		public static List<FavorabilityInfoJsonData> DataList = new List<FavorabilityInfoJsonData>();

		// Token: 0x04004656 RID: 18006
		public static Action OnInitFinishAction = new Action(FavorabilityInfoJsonData.OnInitFinish);

		// Token: 0x04004657 RID: 18007
		public int id;

		// Token: 0x04004658 RID: 18008
		public int AvatarID;

		// Token: 0x04004659 RID: 18009
		public int JinDu;

		// Token: 0x0400465A RID: 18010
		public int ItemID;

		// Token: 0x0400465B RID: 18011
		public int HaoGanDu;

		// Token: 0x0400465C RID: 18012
		public int Time;

		// Token: 0x0400465D RID: 18013
		public int AvatarLevel;

		// Token: 0x0400465E RID: 18014
		public string yes;

		// Token: 0x0400465F RID: 18015
		public string no;
	}
}
