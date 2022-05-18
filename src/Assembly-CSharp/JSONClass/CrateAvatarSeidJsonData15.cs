using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000BA5 RID: 2981
	public class CrateAvatarSeidJsonData15 : IJSONClass
	{
		// Token: 0x060049FC RID: 18940 RVA: 0x001F5850 File Offset: 0x001F3A50
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.CrateAvatarSeidJsonData[15].list)
			{
				try
				{
					CrateAvatarSeidJsonData15 crateAvatarSeidJsonData = new CrateAvatarSeidJsonData15();
					crateAvatarSeidJsonData.id = jsonobject["id"].I;
					crateAvatarSeidJsonData.value1 = jsonobject["value1"].I;
					crateAvatarSeidJsonData.value2 = jsonobject["value2"].I;
					if (CrateAvatarSeidJsonData15.DataDict.ContainsKey(crateAvatarSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典CrateAvatarSeidJsonData15.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", crateAvatarSeidJsonData.id));
					}
					else
					{
						CrateAvatarSeidJsonData15.DataDict.Add(crateAvatarSeidJsonData.id, crateAvatarSeidJsonData);
						CrateAvatarSeidJsonData15.DataList.Add(crateAvatarSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典CrateAvatarSeidJsonData15.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (CrateAvatarSeidJsonData15.OnInitFinishAction != null)
			{
				CrateAvatarSeidJsonData15.OnInitFinishAction();
			}
		}

		// Token: 0x060049FD RID: 18941 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004506 RID: 17670
		public static int SEIDID = 15;

		// Token: 0x04004507 RID: 17671
		public static Dictionary<int, CrateAvatarSeidJsonData15> DataDict = new Dictionary<int, CrateAvatarSeidJsonData15>();

		// Token: 0x04004508 RID: 17672
		public static List<CrateAvatarSeidJsonData15> DataList = new List<CrateAvatarSeidJsonData15>();

		// Token: 0x04004509 RID: 17673
		public static Action OnInitFinishAction = new Action(CrateAvatarSeidJsonData15.OnInitFinish);

		// Token: 0x0400450A RID: 17674
		public int id;

		// Token: 0x0400450B RID: 17675
		public int value1;

		// Token: 0x0400450C RID: 17676
		public int value2;
	}
}
