using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000BA3 RID: 2979
	public class CrateAvatarSeidJsonData13 : IJSONClass
	{
		// Token: 0x060049F4 RID: 18932 RVA: 0x001F5600 File Offset: 0x001F3800
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.CrateAvatarSeidJsonData[13].list)
			{
				try
				{
					CrateAvatarSeidJsonData13 crateAvatarSeidJsonData = new CrateAvatarSeidJsonData13();
					crateAvatarSeidJsonData.id = jsonobject["id"].I;
					crateAvatarSeidJsonData.value1 = jsonobject["value1"].I;
					if (CrateAvatarSeidJsonData13.DataDict.ContainsKey(crateAvatarSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典CrateAvatarSeidJsonData13.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", crateAvatarSeidJsonData.id));
					}
					else
					{
						CrateAvatarSeidJsonData13.DataDict.Add(crateAvatarSeidJsonData.id, crateAvatarSeidJsonData);
						CrateAvatarSeidJsonData13.DataList.Add(crateAvatarSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典CrateAvatarSeidJsonData13.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (CrateAvatarSeidJsonData13.OnInitFinishAction != null)
			{
				CrateAvatarSeidJsonData13.OnInitFinishAction();
			}
		}

		// Token: 0x060049F5 RID: 18933 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040044FA RID: 17658
		public static int SEIDID = 13;

		// Token: 0x040044FB RID: 17659
		public static Dictionary<int, CrateAvatarSeidJsonData13> DataDict = new Dictionary<int, CrateAvatarSeidJsonData13>();

		// Token: 0x040044FC RID: 17660
		public static List<CrateAvatarSeidJsonData13> DataList = new List<CrateAvatarSeidJsonData13>();

		// Token: 0x040044FD RID: 17661
		public static Action OnInitFinishAction = new Action(CrateAvatarSeidJsonData13.OnInitFinish);

		// Token: 0x040044FE RID: 17662
		public int id;

		// Token: 0x040044FF RID: 17663
		public int value1;
	}
}
