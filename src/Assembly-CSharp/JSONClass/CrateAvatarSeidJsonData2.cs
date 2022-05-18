using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000BA8 RID: 2984
	public class CrateAvatarSeidJsonData2 : IJSONClass
	{
		// Token: 0x06004A08 RID: 18952 RVA: 0x001F5BDC File Offset: 0x001F3DDC
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.CrateAvatarSeidJsonData[2].list)
			{
				try
				{
					CrateAvatarSeidJsonData2 crateAvatarSeidJsonData = new CrateAvatarSeidJsonData2();
					crateAvatarSeidJsonData.id = jsonobject["id"].I;
					crateAvatarSeidJsonData.value1 = jsonobject["value1"].I;
					if (CrateAvatarSeidJsonData2.DataDict.ContainsKey(crateAvatarSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典CrateAvatarSeidJsonData2.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", crateAvatarSeidJsonData.id));
					}
					else
					{
						CrateAvatarSeidJsonData2.DataDict.Add(crateAvatarSeidJsonData.id, crateAvatarSeidJsonData);
						CrateAvatarSeidJsonData2.DataList.Add(crateAvatarSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典CrateAvatarSeidJsonData2.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (CrateAvatarSeidJsonData2.OnInitFinishAction != null)
			{
				CrateAvatarSeidJsonData2.OnInitFinishAction();
			}
		}

		// Token: 0x06004A09 RID: 18953 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004519 RID: 17689
		public static int SEIDID = 2;

		// Token: 0x0400451A RID: 17690
		public static Dictionary<int, CrateAvatarSeidJsonData2> DataDict = new Dictionary<int, CrateAvatarSeidJsonData2>();

		// Token: 0x0400451B RID: 17691
		public static List<CrateAvatarSeidJsonData2> DataList = new List<CrateAvatarSeidJsonData2>();

		// Token: 0x0400451C RID: 17692
		public static Action OnInitFinishAction = new Action(CrateAvatarSeidJsonData2.OnInitFinish);

		// Token: 0x0400451D RID: 17693
		public int id;

		// Token: 0x0400451E RID: 17694
		public int value1;
	}
}
