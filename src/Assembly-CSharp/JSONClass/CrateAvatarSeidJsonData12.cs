using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200080C RID: 2060
	public class CrateAvatarSeidJsonData12 : IJSONClass
	{
		// Token: 0x06003E42 RID: 15938 RVA: 0x001A9CDC File Offset: 0x001A7EDC
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.CrateAvatarSeidJsonData[12].list)
			{
				try
				{
					CrateAvatarSeidJsonData12 crateAvatarSeidJsonData = new CrateAvatarSeidJsonData12();
					crateAvatarSeidJsonData.id = jsonobject["id"].I;
					crateAvatarSeidJsonData.value1 = jsonobject["value1"].I;
					if (CrateAvatarSeidJsonData12.DataDict.ContainsKey(crateAvatarSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典CrateAvatarSeidJsonData12.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", crateAvatarSeidJsonData.id));
					}
					else
					{
						CrateAvatarSeidJsonData12.DataDict.Add(crateAvatarSeidJsonData.id, crateAvatarSeidJsonData);
						CrateAvatarSeidJsonData12.DataList.Add(crateAvatarSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典CrateAvatarSeidJsonData12.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (CrateAvatarSeidJsonData12.OnInitFinishAction != null)
			{
				CrateAvatarSeidJsonData12.OnInitFinishAction();
			}
		}

		// Token: 0x06003E43 RID: 15939 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400396C RID: 14700
		public static int SEIDID = 12;

		// Token: 0x0400396D RID: 14701
		public static Dictionary<int, CrateAvatarSeidJsonData12> DataDict = new Dictionary<int, CrateAvatarSeidJsonData12>();

		// Token: 0x0400396E RID: 14702
		public static List<CrateAvatarSeidJsonData12> DataList = new List<CrateAvatarSeidJsonData12>();

		// Token: 0x0400396F RID: 14703
		public static Action OnInitFinishAction = new Action(CrateAvatarSeidJsonData12.OnInitFinish);

		// Token: 0x04003970 RID: 14704
		public int id;

		// Token: 0x04003971 RID: 14705
		public int value1;
	}
}
