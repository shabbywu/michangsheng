using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000BB0 RID: 2992
	public class CrateAvatarSeidJsonData6 : IJSONClass
	{
		// Token: 0x06004A28 RID: 18984 RVA: 0x001F6588 File Offset: 0x001F4788
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.CrateAvatarSeidJsonData[6].list)
			{
				try
				{
					CrateAvatarSeidJsonData6 crateAvatarSeidJsonData = new CrateAvatarSeidJsonData6();
					crateAvatarSeidJsonData.id = jsonobject["id"].I;
					crateAvatarSeidJsonData.value1 = jsonobject["value1"].I;
					if (CrateAvatarSeidJsonData6.DataDict.ContainsKey(crateAvatarSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典CrateAvatarSeidJsonData6.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", crateAvatarSeidJsonData.id));
					}
					else
					{
						CrateAvatarSeidJsonData6.DataDict.Add(crateAvatarSeidJsonData.id, crateAvatarSeidJsonData);
						CrateAvatarSeidJsonData6.DataList.Add(crateAvatarSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典CrateAvatarSeidJsonData6.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (CrateAvatarSeidJsonData6.OnInitFinishAction != null)
			{
				CrateAvatarSeidJsonData6.OnInitFinishAction();
			}
		}

		// Token: 0x06004A29 RID: 18985 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400454D RID: 17741
		public static int SEIDID = 6;

		// Token: 0x0400454E RID: 17742
		public static Dictionary<int, CrateAvatarSeidJsonData6> DataDict = new Dictionary<int, CrateAvatarSeidJsonData6>();

		// Token: 0x0400454F RID: 17743
		public static List<CrateAvatarSeidJsonData6> DataList = new List<CrateAvatarSeidJsonData6>();

		// Token: 0x04004550 RID: 17744
		public static Action OnInitFinishAction = new Action(CrateAvatarSeidJsonData6.OnInitFinish);

		// Token: 0x04004551 RID: 17745
		public int id;

		// Token: 0x04004552 RID: 17746
		public int value1;
	}
}
