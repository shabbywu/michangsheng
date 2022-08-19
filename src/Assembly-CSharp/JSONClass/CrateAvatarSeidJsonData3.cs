using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000817 RID: 2071
	public class CrateAvatarSeidJsonData3 : IJSONClass
	{
		// Token: 0x06003E6E RID: 15982 RVA: 0x001AAC88 File Offset: 0x001A8E88
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.CrateAvatarSeidJsonData[3].list)
			{
				try
				{
					CrateAvatarSeidJsonData3 crateAvatarSeidJsonData = new CrateAvatarSeidJsonData3();
					crateAvatarSeidJsonData.id = jsonobject["id"].I;
					crateAvatarSeidJsonData.value1 = jsonobject["value1"].I;
					if (CrateAvatarSeidJsonData3.DataDict.ContainsKey(crateAvatarSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典CrateAvatarSeidJsonData3.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", crateAvatarSeidJsonData.id));
					}
					else
					{
						CrateAvatarSeidJsonData3.DataDict.Add(crateAvatarSeidJsonData.id, crateAvatarSeidJsonData);
						CrateAvatarSeidJsonData3.DataList.Add(crateAvatarSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典CrateAvatarSeidJsonData3.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (CrateAvatarSeidJsonData3.OnInitFinishAction != null)
			{
				CrateAvatarSeidJsonData3.OnInitFinishAction();
			}
		}

		// Token: 0x06003E6F RID: 15983 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040039B3 RID: 14771
		public static int SEIDID = 3;

		// Token: 0x040039B4 RID: 14772
		public static Dictionary<int, CrateAvatarSeidJsonData3> DataDict = new Dictionary<int, CrateAvatarSeidJsonData3>();

		// Token: 0x040039B5 RID: 14773
		public static List<CrateAvatarSeidJsonData3> DataList = new List<CrateAvatarSeidJsonData3>();

		// Token: 0x040039B6 RID: 14774
		public static Action OnInitFinishAction = new Action(CrateAvatarSeidJsonData3.OnInitFinish);

		// Token: 0x040039B7 RID: 14775
		public int id;

		// Token: 0x040039B8 RID: 14776
		public int value1;
	}
}
