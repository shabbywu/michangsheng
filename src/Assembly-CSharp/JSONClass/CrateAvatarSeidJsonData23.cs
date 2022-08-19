using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000816 RID: 2070
	public class CrateAvatarSeidJsonData23 : IJSONClass
	{
		// Token: 0x06003E6A RID: 15978 RVA: 0x001AAB00 File Offset: 0x001A8D00
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.CrateAvatarSeidJsonData[23].list)
			{
				try
				{
					CrateAvatarSeidJsonData23 crateAvatarSeidJsonData = new CrateAvatarSeidJsonData23();
					crateAvatarSeidJsonData.id = jsonobject["id"].I;
					crateAvatarSeidJsonData.value1 = jsonobject["value1"].ToList();
					crateAvatarSeidJsonData.value2 = jsonobject["value2"].ToList();
					if (CrateAvatarSeidJsonData23.DataDict.ContainsKey(crateAvatarSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典CrateAvatarSeidJsonData23.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", crateAvatarSeidJsonData.id));
					}
					else
					{
						CrateAvatarSeidJsonData23.DataDict.Add(crateAvatarSeidJsonData.id, crateAvatarSeidJsonData);
						CrateAvatarSeidJsonData23.DataList.Add(crateAvatarSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典CrateAvatarSeidJsonData23.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (CrateAvatarSeidJsonData23.OnInitFinishAction != null)
			{
				CrateAvatarSeidJsonData23.OnInitFinishAction();
			}
		}

		// Token: 0x06003E6B RID: 15979 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040039AC RID: 14764
		public static int SEIDID = 23;

		// Token: 0x040039AD RID: 14765
		public static Dictionary<int, CrateAvatarSeidJsonData23> DataDict = new Dictionary<int, CrateAvatarSeidJsonData23>();

		// Token: 0x040039AE RID: 14766
		public static List<CrateAvatarSeidJsonData23> DataList = new List<CrateAvatarSeidJsonData23>();

		// Token: 0x040039AF RID: 14767
		public static Action OnInitFinishAction = new Action(CrateAvatarSeidJsonData23.OnInitFinish);

		// Token: 0x040039B0 RID: 14768
		public int id;

		// Token: 0x040039B1 RID: 14769
		public List<int> value1 = new List<int>();

		// Token: 0x040039B2 RID: 14770
		public List<int> value2 = new List<int>();
	}
}
