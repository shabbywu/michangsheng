using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000BCE RID: 3022
	public class FavorabilityAvatarInfoJsonData : IJSONClass
	{
		// Token: 0x06004AA0 RID: 19104 RVA: 0x001F9240 File Offset: 0x001F7440
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

		// Token: 0x06004AA1 RID: 19105 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400464F RID: 17999
		public static Dictionary<int, FavorabilityAvatarInfoJsonData> DataDict = new Dictionary<int, FavorabilityAvatarInfoJsonData>();

		// Token: 0x04004650 RID: 18000
		public static List<FavorabilityAvatarInfoJsonData> DataList = new List<FavorabilityAvatarInfoJsonData>();

		// Token: 0x04004651 RID: 18001
		public static Action OnInitFinishAction = new Action(FavorabilityAvatarInfoJsonData.OnInitFinish);

		// Token: 0x04004652 RID: 18002
		public int id;

		// Token: 0x04004653 RID: 18003
		public List<int> AvatarID = new List<int>();
	}
}
