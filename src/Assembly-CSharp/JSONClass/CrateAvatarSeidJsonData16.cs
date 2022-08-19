using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000810 RID: 2064
	public class CrateAvatarSeidJsonData16 : IJSONClass
	{
		// Token: 0x06003E52 RID: 15954 RVA: 0x001AA250 File Offset: 0x001A8450
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.CrateAvatarSeidJsonData[16].list)
			{
				try
				{
					CrateAvatarSeidJsonData16 crateAvatarSeidJsonData = new CrateAvatarSeidJsonData16();
					crateAvatarSeidJsonData.id = jsonobject["id"].I;
					crateAvatarSeidJsonData.value1 = jsonobject["value1"].I;
					if (CrateAvatarSeidJsonData16.DataDict.ContainsKey(crateAvatarSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典CrateAvatarSeidJsonData16.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", crateAvatarSeidJsonData.id));
					}
					else
					{
						CrateAvatarSeidJsonData16.DataDict.Add(crateAvatarSeidJsonData.id, crateAvatarSeidJsonData);
						CrateAvatarSeidJsonData16.DataList.Add(crateAvatarSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典CrateAvatarSeidJsonData16.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (CrateAvatarSeidJsonData16.OnInitFinishAction != null)
			{
				CrateAvatarSeidJsonData16.OnInitFinishAction();
			}
		}

		// Token: 0x06003E53 RID: 15955 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003985 RID: 14725
		public static int SEIDID = 16;

		// Token: 0x04003986 RID: 14726
		public static Dictionary<int, CrateAvatarSeidJsonData16> DataDict = new Dictionary<int, CrateAvatarSeidJsonData16>();

		// Token: 0x04003987 RID: 14727
		public static List<CrateAvatarSeidJsonData16> DataList = new List<CrateAvatarSeidJsonData16>();

		// Token: 0x04003988 RID: 14728
		public static Action OnInitFinishAction = new Action(CrateAvatarSeidJsonData16.OnInitFinish);

		// Token: 0x04003989 RID: 14729
		public int id;

		// Token: 0x0400398A RID: 14730
		public int value1;
	}
}
