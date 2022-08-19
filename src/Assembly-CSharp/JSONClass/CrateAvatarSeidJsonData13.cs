using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200080D RID: 2061
	public class CrateAvatarSeidJsonData13 : IJSONClass
	{
		// Token: 0x06003E46 RID: 15942 RVA: 0x001A9E34 File Offset: 0x001A8034
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.CrateAvatarSeidJsonData[13].list)
			{
				try
				{
					CrateAvatarSeidJsonData13 crateAvatarSeidJsonData = new CrateAvatarSeidJsonData13();
					crateAvatarSeidJsonData.id = jsonobject["id"].I;
					crateAvatarSeidJsonData.value1 = jsonobject["value1"].I;
					if (CrateAvatarSeidJsonData13.DataDict.ContainsKey(crateAvatarSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典CrateAvatarSeidJsonData13.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", crateAvatarSeidJsonData.id));
					}
					else
					{
						CrateAvatarSeidJsonData13.DataDict.Add(crateAvatarSeidJsonData.id, crateAvatarSeidJsonData);
						CrateAvatarSeidJsonData13.DataList.Add(crateAvatarSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典CrateAvatarSeidJsonData13.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (CrateAvatarSeidJsonData13.OnInitFinishAction != null)
			{
				CrateAvatarSeidJsonData13.OnInitFinishAction();
			}
		}

		// Token: 0x06003E47 RID: 15943 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003972 RID: 14706
		public static int SEIDID = 13;

		// Token: 0x04003973 RID: 14707
		public static Dictionary<int, CrateAvatarSeidJsonData13> DataDict = new Dictionary<int, CrateAvatarSeidJsonData13>();

		// Token: 0x04003974 RID: 14708
		public static List<CrateAvatarSeidJsonData13> DataList = new List<CrateAvatarSeidJsonData13>();

		// Token: 0x04003975 RID: 14709
		public static Action OnInitFinishAction = new Action(CrateAvatarSeidJsonData13.OnInitFinish);

		// Token: 0x04003976 RID: 14710
		public int id;

		// Token: 0x04003977 RID: 14711
		public int value1;
	}
}
