using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000818 RID: 2072
	public class CrateAvatarSeidJsonData4 : IJSONClass
	{
		// Token: 0x06003E72 RID: 15986 RVA: 0x001AADE0 File Offset: 0x001A8FE0
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.CrateAvatarSeidJsonData[4].list)
			{
				try
				{
					CrateAvatarSeidJsonData4 crateAvatarSeidJsonData = new CrateAvatarSeidJsonData4();
					crateAvatarSeidJsonData.id = jsonobject["id"].I;
					crateAvatarSeidJsonData.value1 = jsonobject["value1"].I;
					if (CrateAvatarSeidJsonData4.DataDict.ContainsKey(crateAvatarSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典CrateAvatarSeidJsonData4.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", crateAvatarSeidJsonData.id));
					}
					else
					{
						CrateAvatarSeidJsonData4.DataDict.Add(crateAvatarSeidJsonData.id, crateAvatarSeidJsonData);
						CrateAvatarSeidJsonData4.DataList.Add(crateAvatarSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典CrateAvatarSeidJsonData4.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (CrateAvatarSeidJsonData4.OnInitFinishAction != null)
			{
				CrateAvatarSeidJsonData4.OnInitFinishAction();
			}
		}

		// Token: 0x06003E73 RID: 15987 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040039B9 RID: 14777
		public static int SEIDID = 4;

		// Token: 0x040039BA RID: 14778
		public static Dictionary<int, CrateAvatarSeidJsonData4> DataDict = new Dictionary<int, CrateAvatarSeidJsonData4>();

		// Token: 0x040039BB RID: 14779
		public static List<CrateAvatarSeidJsonData4> DataList = new List<CrateAvatarSeidJsonData4>();

		// Token: 0x040039BC RID: 14780
		public static Action OnInitFinishAction = new Action(CrateAvatarSeidJsonData4.OnInitFinish);

		// Token: 0x040039BD RID: 14781
		public int id;

		// Token: 0x040039BE RID: 14782
		public int value1;
	}
}
