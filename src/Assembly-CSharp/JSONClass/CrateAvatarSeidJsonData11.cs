using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200080B RID: 2059
	public class CrateAvatarSeidJsonData11 : IJSONClass
	{
		// Token: 0x06003E3E RID: 15934 RVA: 0x001A9B70 File Offset: 0x001A7D70
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.CrateAvatarSeidJsonData[11].list)
			{
				try
				{
					CrateAvatarSeidJsonData11 crateAvatarSeidJsonData = new CrateAvatarSeidJsonData11();
					crateAvatarSeidJsonData.id = jsonobject["id"].I;
					crateAvatarSeidJsonData.value1 = jsonobject["value1"].I;
					crateAvatarSeidJsonData.value2 = jsonobject["value2"].I;
					if (CrateAvatarSeidJsonData11.DataDict.ContainsKey(crateAvatarSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典CrateAvatarSeidJsonData11.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", crateAvatarSeidJsonData.id));
					}
					else
					{
						CrateAvatarSeidJsonData11.DataDict.Add(crateAvatarSeidJsonData.id, crateAvatarSeidJsonData);
						CrateAvatarSeidJsonData11.DataList.Add(crateAvatarSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典CrateAvatarSeidJsonData11.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (CrateAvatarSeidJsonData11.OnInitFinishAction != null)
			{
				CrateAvatarSeidJsonData11.OnInitFinishAction();
			}
		}

		// Token: 0x06003E3F RID: 15935 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003965 RID: 14693
		public static int SEIDID = 11;

		// Token: 0x04003966 RID: 14694
		public static Dictionary<int, CrateAvatarSeidJsonData11> DataDict = new Dictionary<int, CrateAvatarSeidJsonData11>();

		// Token: 0x04003967 RID: 14695
		public static List<CrateAvatarSeidJsonData11> DataList = new List<CrateAvatarSeidJsonData11>();

		// Token: 0x04003968 RID: 14696
		public static Action OnInitFinishAction = new Action(CrateAvatarSeidJsonData11.OnInitFinish);

		// Token: 0x04003969 RID: 14697
		public int id;

		// Token: 0x0400396A RID: 14698
		public int value1;

		// Token: 0x0400396B RID: 14699
		public int value2;
	}
}
