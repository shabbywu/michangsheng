using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000BA2 RID: 2978
	public class CrateAvatarSeidJsonData12 : IJSONClass
	{
		// Token: 0x060049F0 RID: 18928 RVA: 0x001F54D8 File Offset: 0x001F36D8
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.CrateAvatarSeidJsonData[12].list)
			{
				try
				{
					CrateAvatarSeidJsonData12 crateAvatarSeidJsonData = new CrateAvatarSeidJsonData12();
					crateAvatarSeidJsonData.id = jsonobject["id"].I;
					crateAvatarSeidJsonData.value1 = jsonobject["value1"].I;
					if (CrateAvatarSeidJsonData12.DataDict.ContainsKey(crateAvatarSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典CrateAvatarSeidJsonData12.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", crateAvatarSeidJsonData.id));
					}
					else
					{
						CrateAvatarSeidJsonData12.DataDict.Add(crateAvatarSeidJsonData.id, crateAvatarSeidJsonData);
						CrateAvatarSeidJsonData12.DataList.Add(crateAvatarSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典CrateAvatarSeidJsonData12.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (CrateAvatarSeidJsonData12.OnInitFinishAction != null)
			{
				CrateAvatarSeidJsonData12.OnInitFinishAction();
			}
		}

		// Token: 0x060049F1 RID: 18929 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040044F4 RID: 17652
		public static int SEIDID = 12;

		// Token: 0x040044F5 RID: 17653
		public static Dictionary<int, CrateAvatarSeidJsonData12> DataDict = new Dictionary<int, CrateAvatarSeidJsonData12>();

		// Token: 0x040044F6 RID: 17654
		public static List<CrateAvatarSeidJsonData12> DataList = new List<CrateAvatarSeidJsonData12>();

		// Token: 0x040044F7 RID: 17655
		public static Action OnInitFinishAction = new Action(CrateAvatarSeidJsonData12.OnInitFinish);

		// Token: 0x040044F8 RID: 17656
		public int id;

		// Token: 0x040044F9 RID: 17657
		public int value1;
	}
}
