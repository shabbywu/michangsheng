using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200081A RID: 2074
	public class CrateAvatarSeidJsonData6 : IJSONClass
	{
		// Token: 0x06003E7A RID: 15994 RVA: 0x001AB090 File Offset: 0x001A9290
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

		// Token: 0x06003E7B RID: 15995 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040039C5 RID: 14789
		public static int SEIDID = 6;

		// Token: 0x040039C6 RID: 14790
		public static Dictionary<int, CrateAvatarSeidJsonData6> DataDict = new Dictionary<int, CrateAvatarSeidJsonData6>();

		// Token: 0x040039C7 RID: 14791
		public static List<CrateAvatarSeidJsonData6> DataList = new List<CrateAvatarSeidJsonData6>();

		// Token: 0x040039C8 RID: 14792
		public static Action OnInitFinishAction = new Action(CrateAvatarSeidJsonData6.OnInitFinish);

		// Token: 0x040039C9 RID: 14793
		public int id;

		// Token: 0x040039CA RID: 14794
		public int value1;
	}
}
