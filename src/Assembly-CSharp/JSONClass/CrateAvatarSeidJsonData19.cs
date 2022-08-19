using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000811 RID: 2065
	public class CrateAvatarSeidJsonData19 : IJSONClass
	{
		// Token: 0x06003E56 RID: 15958 RVA: 0x001AA3A8 File Offset: 0x001A85A8
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.CrateAvatarSeidJsonData[19].list)
			{
				try
				{
					CrateAvatarSeidJsonData19 crateAvatarSeidJsonData = new CrateAvatarSeidJsonData19();
					crateAvatarSeidJsonData.id = jsonobject["id"].I;
					crateAvatarSeidJsonData.value1 = jsonobject["value1"].I;
					if (CrateAvatarSeidJsonData19.DataDict.ContainsKey(crateAvatarSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典CrateAvatarSeidJsonData19.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", crateAvatarSeidJsonData.id));
					}
					else
					{
						CrateAvatarSeidJsonData19.DataDict.Add(crateAvatarSeidJsonData.id, crateAvatarSeidJsonData);
						CrateAvatarSeidJsonData19.DataList.Add(crateAvatarSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典CrateAvatarSeidJsonData19.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (CrateAvatarSeidJsonData19.OnInitFinishAction != null)
			{
				CrateAvatarSeidJsonData19.OnInitFinishAction();
			}
		}

		// Token: 0x06003E57 RID: 15959 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400398B RID: 14731
		public static int SEIDID = 19;

		// Token: 0x0400398C RID: 14732
		public static Dictionary<int, CrateAvatarSeidJsonData19> DataDict = new Dictionary<int, CrateAvatarSeidJsonData19>();

		// Token: 0x0400398D RID: 14733
		public static List<CrateAvatarSeidJsonData19> DataList = new List<CrateAvatarSeidJsonData19>();

		// Token: 0x0400398E RID: 14734
		public static Action OnInitFinishAction = new Action(CrateAvatarSeidJsonData19.OnInitFinish);

		// Token: 0x0400398F RID: 14735
		public int id;

		// Token: 0x04003990 RID: 14736
		public int value1;
	}
}
