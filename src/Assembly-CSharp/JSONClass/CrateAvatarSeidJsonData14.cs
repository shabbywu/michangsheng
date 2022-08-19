using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200080E RID: 2062
	public class CrateAvatarSeidJsonData14 : IJSONClass
	{
		// Token: 0x06003E4A RID: 15946 RVA: 0x001A9F8C File Offset: 0x001A818C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.CrateAvatarSeidJsonData[14].list)
			{
				try
				{
					CrateAvatarSeidJsonData14 crateAvatarSeidJsonData = new CrateAvatarSeidJsonData14();
					crateAvatarSeidJsonData.id = jsonobject["id"].I;
					crateAvatarSeidJsonData.value1 = jsonobject["value1"].I;
					if (CrateAvatarSeidJsonData14.DataDict.ContainsKey(crateAvatarSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典CrateAvatarSeidJsonData14.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", crateAvatarSeidJsonData.id));
					}
					else
					{
						CrateAvatarSeidJsonData14.DataDict.Add(crateAvatarSeidJsonData.id, crateAvatarSeidJsonData);
						CrateAvatarSeidJsonData14.DataList.Add(crateAvatarSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典CrateAvatarSeidJsonData14.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (CrateAvatarSeidJsonData14.OnInitFinishAction != null)
			{
				CrateAvatarSeidJsonData14.OnInitFinishAction();
			}
		}

		// Token: 0x06003E4B RID: 15947 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003978 RID: 14712
		public static int SEIDID = 14;

		// Token: 0x04003979 RID: 14713
		public static Dictionary<int, CrateAvatarSeidJsonData14> DataDict = new Dictionary<int, CrateAvatarSeidJsonData14>();

		// Token: 0x0400397A RID: 14714
		public static List<CrateAvatarSeidJsonData14> DataList = new List<CrateAvatarSeidJsonData14>();

		// Token: 0x0400397B RID: 14715
		public static Action OnInitFinishAction = new Action(CrateAvatarSeidJsonData14.OnInitFinish);

		// Token: 0x0400397C RID: 14716
		public int id;

		// Token: 0x0400397D RID: 14717
		public int value1;
	}
}
