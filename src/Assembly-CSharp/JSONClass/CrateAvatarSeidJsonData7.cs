using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200081B RID: 2075
	public class CrateAvatarSeidJsonData7 : IJSONClass
	{
		// Token: 0x06003E7E RID: 15998 RVA: 0x001AB1E8 File Offset: 0x001A93E8
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.CrateAvatarSeidJsonData[7].list)
			{
				try
				{
					CrateAvatarSeidJsonData7 crateAvatarSeidJsonData = new CrateAvatarSeidJsonData7();
					crateAvatarSeidJsonData.id = jsonobject["id"].I;
					crateAvatarSeidJsonData.value1 = jsonobject["value1"].I;
					if (CrateAvatarSeidJsonData7.DataDict.ContainsKey(crateAvatarSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典CrateAvatarSeidJsonData7.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", crateAvatarSeidJsonData.id));
					}
					else
					{
						CrateAvatarSeidJsonData7.DataDict.Add(crateAvatarSeidJsonData.id, crateAvatarSeidJsonData);
						CrateAvatarSeidJsonData7.DataList.Add(crateAvatarSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典CrateAvatarSeidJsonData7.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (CrateAvatarSeidJsonData7.OnInitFinishAction != null)
			{
				CrateAvatarSeidJsonData7.OnInitFinishAction();
			}
		}

		// Token: 0x06003E7F RID: 15999 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040039CB RID: 14795
		public static int SEIDID = 7;

		// Token: 0x040039CC RID: 14796
		public static Dictionary<int, CrateAvatarSeidJsonData7> DataDict = new Dictionary<int, CrateAvatarSeidJsonData7>();

		// Token: 0x040039CD RID: 14797
		public static List<CrateAvatarSeidJsonData7> DataList = new List<CrateAvatarSeidJsonData7>();

		// Token: 0x040039CE RID: 14798
		public static Action OnInitFinishAction = new Action(CrateAvatarSeidJsonData7.OnInitFinish);

		// Token: 0x040039CF RID: 14799
		public int id;

		// Token: 0x040039D0 RID: 14800
		public int value1;
	}
}
