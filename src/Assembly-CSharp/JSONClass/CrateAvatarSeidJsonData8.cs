using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000BB2 RID: 2994
	public class CrateAvatarSeidJsonData8 : IJSONClass
	{
		// Token: 0x06004A30 RID: 18992 RVA: 0x001F67D8 File Offset: 0x001F49D8
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

		// Token: 0x06004A31 RID: 18993 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004559 RID: 17753
		public static int SEIDID = 8;

		// Token: 0x0400455A RID: 17754
		public static Dictionary<int, CrateAvatarSeidJsonData8> DataDict = new Dictionary<int, CrateAvatarSeidJsonData8>();

		// Token: 0x0400455B RID: 17755
		public static List<CrateAvatarSeidJsonData8> DataList = new List<CrateAvatarSeidJsonData8>();

		// Token: 0x0400455C RID: 17756
		public static Action OnInitFinishAction = new Action(CrateAvatarSeidJsonData8.OnInitFinish);

		// Token: 0x0400455D RID: 17757
		public int id;

		// Token: 0x0400455E RID: 17758
		public int value1;
	}
}
