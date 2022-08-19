using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200080F RID: 2063
	public class CrateAvatarSeidJsonData15 : IJSONClass
	{
		// Token: 0x06003E4E RID: 15950 RVA: 0x001AA0E4 File Offset: 0x001A82E4
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.CrateAvatarSeidJsonData[15].list)
			{
				try
				{
					CrateAvatarSeidJsonData15 crateAvatarSeidJsonData = new CrateAvatarSeidJsonData15();
					crateAvatarSeidJsonData.id = jsonobject["id"].I;
					crateAvatarSeidJsonData.value1 = jsonobject["value1"].I;
					crateAvatarSeidJsonData.value2 = jsonobject["value2"].I;
					if (CrateAvatarSeidJsonData15.DataDict.ContainsKey(crateAvatarSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典CrateAvatarSeidJsonData15.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", crateAvatarSeidJsonData.id));
					}
					else
					{
						CrateAvatarSeidJsonData15.DataDict.Add(crateAvatarSeidJsonData.id, crateAvatarSeidJsonData);
						CrateAvatarSeidJsonData15.DataList.Add(crateAvatarSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典CrateAvatarSeidJsonData15.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (CrateAvatarSeidJsonData15.OnInitFinishAction != null)
			{
				CrateAvatarSeidJsonData15.OnInitFinishAction();
			}
		}

		// Token: 0x06003E4F RID: 15951 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400397E RID: 14718
		public static int SEIDID = 15;

		// Token: 0x0400397F RID: 14719
		public static Dictionary<int, CrateAvatarSeidJsonData15> DataDict = new Dictionary<int, CrateAvatarSeidJsonData15>();

		// Token: 0x04003980 RID: 14720
		public static List<CrateAvatarSeidJsonData15> DataList = new List<CrateAvatarSeidJsonData15>();

		// Token: 0x04003981 RID: 14721
		public static Action OnInitFinishAction = new Action(CrateAvatarSeidJsonData15.OnInitFinish);

		// Token: 0x04003982 RID: 14722
		public int id;

		// Token: 0x04003983 RID: 14723
		public int value1;

		// Token: 0x04003984 RID: 14724
		public int value2;
	}
}
