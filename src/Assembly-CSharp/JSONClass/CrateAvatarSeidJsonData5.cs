using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000819 RID: 2073
	public class CrateAvatarSeidJsonData5 : IJSONClass
	{
		// Token: 0x06003E76 RID: 15990 RVA: 0x001AAF38 File Offset: 0x001A9138
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.CrateAvatarSeidJsonData[5].list)
			{
				try
				{
					CrateAvatarSeidJsonData5 crateAvatarSeidJsonData = new CrateAvatarSeidJsonData5();
					crateAvatarSeidJsonData.id = jsonobject["id"].I;
					crateAvatarSeidJsonData.value1 = jsonobject["value1"].I;
					if (CrateAvatarSeidJsonData5.DataDict.ContainsKey(crateAvatarSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典CrateAvatarSeidJsonData5.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", crateAvatarSeidJsonData.id));
					}
					else
					{
						CrateAvatarSeidJsonData5.DataDict.Add(crateAvatarSeidJsonData.id, crateAvatarSeidJsonData);
						CrateAvatarSeidJsonData5.DataList.Add(crateAvatarSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典CrateAvatarSeidJsonData5.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (CrateAvatarSeidJsonData5.OnInitFinishAction != null)
			{
				CrateAvatarSeidJsonData5.OnInitFinishAction();
			}
		}

		// Token: 0x06003E77 RID: 15991 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040039BF RID: 14783
		public static int SEIDID = 5;

		// Token: 0x040039C0 RID: 14784
		public static Dictionary<int, CrateAvatarSeidJsonData5> DataDict = new Dictionary<int, CrateAvatarSeidJsonData5>();

		// Token: 0x040039C1 RID: 14785
		public static List<CrateAvatarSeidJsonData5> DataList = new List<CrateAvatarSeidJsonData5>();

		// Token: 0x040039C2 RID: 14786
		public static Action OnInitFinishAction = new Action(CrateAvatarSeidJsonData5.OnInitFinish);

		// Token: 0x040039C3 RID: 14787
		public int id;

		// Token: 0x040039C4 RID: 14788
		public int value1;
	}
}
