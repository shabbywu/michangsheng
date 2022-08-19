using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200081C RID: 2076
	public class CrateAvatarSeidJsonData8 : IJSONClass
	{
		// Token: 0x06003E82 RID: 16002 RVA: 0x001AB340 File Offset: 0x001A9540
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.CrateAvatarSeidJsonData[8].list)
			{
				try
				{
					CrateAvatarSeidJsonData8 crateAvatarSeidJsonData = new CrateAvatarSeidJsonData8();
					crateAvatarSeidJsonData.id = jsonobject["id"].I;
					crateAvatarSeidJsonData.value1 = jsonobject["value1"].I;
					if (CrateAvatarSeidJsonData8.DataDict.ContainsKey(crateAvatarSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典CrateAvatarSeidJsonData8.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", crateAvatarSeidJsonData.id));
					}
					else
					{
						CrateAvatarSeidJsonData8.DataDict.Add(crateAvatarSeidJsonData.id, crateAvatarSeidJsonData);
						CrateAvatarSeidJsonData8.DataList.Add(crateAvatarSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典CrateAvatarSeidJsonData8.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (CrateAvatarSeidJsonData8.OnInitFinishAction != null)
			{
				CrateAvatarSeidJsonData8.OnInitFinishAction();
			}
		}

		// Token: 0x06003E83 RID: 16003 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040039D1 RID: 14801
		public static int SEIDID = 8;

		// Token: 0x040039D2 RID: 14802
		public static Dictionary<int, CrateAvatarSeidJsonData8> DataDict = new Dictionary<int, CrateAvatarSeidJsonData8>();

		// Token: 0x040039D3 RID: 14803
		public static List<CrateAvatarSeidJsonData8> DataList = new List<CrateAvatarSeidJsonData8>();

		// Token: 0x040039D4 RID: 14804
		public static Action OnInitFinishAction = new Action(CrateAvatarSeidJsonData8.OnInitFinish);

		// Token: 0x040039D5 RID: 14805
		public int id;

		// Token: 0x040039D6 RID: 14806
		public int value1;
	}
}
