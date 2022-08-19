using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200083D RID: 2109
	public class FavorabilityAvatarInfoJsonData : IJSONClass
	{
		// Token: 0x06003F06 RID: 16134 RVA: 0x001AE964 File Offset: 0x001ACB64
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.FavorabilityAvatarInfoJsonData.list)
			{
				try
				{
					FavorabilityAvatarInfoJsonData favorabilityAvatarInfoJsonData = new FavorabilityAvatarInfoJsonData();
					favorabilityAvatarInfoJsonData.id = jsonobject["id"].I;
					favorabilityAvatarInfoJsonData.AvatarID = jsonobject["AvatarID"].ToList();
					if (FavorabilityAvatarInfoJsonData.DataDict.ContainsKey(favorabilityAvatarInfoJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典FavorabilityAvatarInfoJsonData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", favorabilityAvatarInfoJsonData.id));
					}
					else
					{
						FavorabilityAvatarInfoJsonData.DataDict.Add(favorabilityAvatarInfoJsonData.id, favorabilityAvatarInfoJsonData);
						FavorabilityAvatarInfoJsonData.DataList.Add(favorabilityAvatarInfoJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典FavorabilityAvatarInfoJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (FavorabilityAvatarInfoJsonData.OnInitFinishAction != null)
			{
				FavorabilityAvatarInfoJsonData.OnInitFinishAction();
			}
		}

		// Token: 0x06003F07 RID: 16135 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003AE1 RID: 15073
		public static Dictionary<int, FavorabilityAvatarInfoJsonData> DataDict = new Dictionary<int, FavorabilityAvatarInfoJsonData>();

		// Token: 0x04003AE2 RID: 15074
		public static List<FavorabilityAvatarInfoJsonData> DataList = new List<FavorabilityAvatarInfoJsonData>();

		// Token: 0x04003AE3 RID: 15075
		public static Action OnInitFinishAction = new Action(FavorabilityAvatarInfoJsonData.OnInitFinish);

		// Token: 0x04003AE4 RID: 15076
		public int id;

		// Token: 0x04003AE5 RID: 15077
		public List<int> AvatarID = new List<int>();
	}
}
