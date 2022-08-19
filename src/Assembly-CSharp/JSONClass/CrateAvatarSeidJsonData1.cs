using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000809 RID: 2057
	public class CrateAvatarSeidJsonData1 : IJSONClass
	{
		// Token: 0x06003E36 RID: 15926 RVA: 0x001A98AC File Offset: 0x001A7AAC
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.CrateAvatarSeidJsonData[1].list)
			{
				try
				{
					CrateAvatarSeidJsonData1 crateAvatarSeidJsonData = new CrateAvatarSeidJsonData1();
					crateAvatarSeidJsonData.id = jsonobject["id"].I;
					crateAvatarSeidJsonData.value1 = jsonobject["value1"].I;
					if (CrateAvatarSeidJsonData1.DataDict.ContainsKey(crateAvatarSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典CrateAvatarSeidJsonData1.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", crateAvatarSeidJsonData.id));
					}
					else
					{
						CrateAvatarSeidJsonData1.DataDict.Add(crateAvatarSeidJsonData.id, crateAvatarSeidJsonData);
						CrateAvatarSeidJsonData1.DataList.Add(crateAvatarSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典CrateAvatarSeidJsonData1.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (CrateAvatarSeidJsonData1.OnInitFinishAction != null)
			{
				CrateAvatarSeidJsonData1.OnInitFinishAction();
			}
		}

		// Token: 0x06003E37 RID: 15927 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003959 RID: 14681
		public static int SEIDID = 1;

		// Token: 0x0400395A RID: 14682
		public static Dictionary<int, CrateAvatarSeidJsonData1> DataDict = new Dictionary<int, CrateAvatarSeidJsonData1>();

		// Token: 0x0400395B RID: 14683
		public static List<CrateAvatarSeidJsonData1> DataList = new List<CrateAvatarSeidJsonData1>();

		// Token: 0x0400395C RID: 14684
		public static Action OnInitFinishAction = new Action(CrateAvatarSeidJsonData1.OnInitFinish);

		// Token: 0x0400395D RID: 14685
		public int id;

		// Token: 0x0400395E RID: 14686
		public int value1;
	}
}
