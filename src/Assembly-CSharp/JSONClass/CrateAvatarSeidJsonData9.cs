using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200081D RID: 2077
	public class CrateAvatarSeidJsonData9 : IJSONClass
	{
		// Token: 0x06003E86 RID: 16006 RVA: 0x001AB498 File Offset: 0x001A9698
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.CrateAvatarSeidJsonData[9].list)
			{
				try
				{
					CrateAvatarSeidJsonData9 crateAvatarSeidJsonData = new CrateAvatarSeidJsonData9();
					crateAvatarSeidJsonData.id = jsonobject["id"].I;
					crateAvatarSeidJsonData.value1 = jsonobject["value1"].ToList();
					if (CrateAvatarSeidJsonData9.DataDict.ContainsKey(crateAvatarSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典CrateAvatarSeidJsonData9.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", crateAvatarSeidJsonData.id));
					}
					else
					{
						CrateAvatarSeidJsonData9.DataDict.Add(crateAvatarSeidJsonData.id, crateAvatarSeidJsonData);
						CrateAvatarSeidJsonData9.DataList.Add(crateAvatarSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典CrateAvatarSeidJsonData9.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (CrateAvatarSeidJsonData9.OnInitFinishAction != null)
			{
				CrateAvatarSeidJsonData9.OnInitFinishAction();
			}
		}

		// Token: 0x06003E87 RID: 16007 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040039D7 RID: 14807
		public static int SEIDID = 9;

		// Token: 0x040039D8 RID: 14808
		public static Dictionary<int, CrateAvatarSeidJsonData9> DataDict = new Dictionary<int, CrateAvatarSeidJsonData9>();

		// Token: 0x040039D9 RID: 14809
		public static List<CrateAvatarSeidJsonData9> DataList = new List<CrateAvatarSeidJsonData9>();

		// Token: 0x040039DA RID: 14810
		public static Action OnInitFinishAction = new Action(CrateAvatarSeidJsonData9.OnInitFinish);

		// Token: 0x040039DB RID: 14811
		public int id;

		// Token: 0x040039DC RID: 14812
		public List<int> value1 = new List<int>();
	}
}
