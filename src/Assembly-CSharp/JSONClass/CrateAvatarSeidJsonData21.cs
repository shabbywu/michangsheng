using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000814 RID: 2068
	public class CrateAvatarSeidJsonData21 : IJSONClass
	{
		// Token: 0x06003E62 RID: 15970 RVA: 0x001AA7C4 File Offset: 0x001A89C4
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.CrateAvatarSeidJsonData[21].list)
			{
				try
				{
					CrateAvatarSeidJsonData21 crateAvatarSeidJsonData = new CrateAvatarSeidJsonData21();
					crateAvatarSeidJsonData.id = jsonobject["id"].I;
					crateAvatarSeidJsonData.value1 = jsonobject["value1"].I;
					if (CrateAvatarSeidJsonData21.DataDict.ContainsKey(crateAvatarSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典CrateAvatarSeidJsonData21.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", crateAvatarSeidJsonData.id));
					}
					else
					{
						CrateAvatarSeidJsonData21.DataDict.Add(crateAvatarSeidJsonData.id, crateAvatarSeidJsonData);
						CrateAvatarSeidJsonData21.DataList.Add(crateAvatarSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典CrateAvatarSeidJsonData21.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (CrateAvatarSeidJsonData21.OnInitFinishAction != null)
			{
				CrateAvatarSeidJsonData21.OnInitFinishAction();
			}
		}

		// Token: 0x06003E63 RID: 15971 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400399D RID: 14749
		public static int SEIDID = 21;

		// Token: 0x0400399E RID: 14750
		public static Dictionary<int, CrateAvatarSeidJsonData21> DataDict = new Dictionary<int, CrateAvatarSeidJsonData21>();

		// Token: 0x0400399F RID: 14751
		public static List<CrateAvatarSeidJsonData21> DataList = new List<CrateAvatarSeidJsonData21>();

		// Token: 0x040039A0 RID: 14752
		public static Action OnInitFinishAction = new Action(CrateAvatarSeidJsonData21.OnInitFinish);

		// Token: 0x040039A1 RID: 14753
		public int id;

		// Token: 0x040039A2 RID: 14754
		public int value1;
	}
}
