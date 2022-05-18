using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000BB1 RID: 2993
	public class CrateAvatarSeidJsonData7 : IJSONClass
	{
		// Token: 0x06004A2C RID: 18988 RVA: 0x001F66B0 File Offset: 0x001F48B0
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.CrateAvatarSeidJsonData[7].list)
			{
				try
				{
					CrateAvatarSeidJsonData7 crateAvatarSeidJsonData = new CrateAvatarSeidJsonData7();
					crateAvatarSeidJsonData.id = jsonobject["id"].I;
					crateAvatarSeidJsonData.value1 = jsonobject["value1"].I;
					if (CrateAvatarSeidJsonData7.DataDict.ContainsKey(crateAvatarSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典CrateAvatarSeidJsonData7.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", crateAvatarSeidJsonData.id));
					}
					else
					{
						CrateAvatarSeidJsonData7.DataDict.Add(crateAvatarSeidJsonData.id, crateAvatarSeidJsonData);
						CrateAvatarSeidJsonData7.DataList.Add(crateAvatarSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典CrateAvatarSeidJsonData7.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (CrateAvatarSeidJsonData7.OnInitFinishAction != null)
			{
				CrateAvatarSeidJsonData7.OnInitFinishAction();
			}
		}

		// Token: 0x06004A2D RID: 18989 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004553 RID: 17747
		public static int SEIDID = 7;

		// Token: 0x04004554 RID: 17748
		public static Dictionary<int, CrateAvatarSeidJsonData7> DataDict = new Dictionary<int, CrateAvatarSeidJsonData7>();

		// Token: 0x04004555 RID: 17749
		public static List<CrateAvatarSeidJsonData7> DataList = new List<CrateAvatarSeidJsonData7>();

		// Token: 0x04004556 RID: 17750
		public static Action OnInitFinishAction = new Action(CrateAvatarSeidJsonData7.OnInitFinish);

		// Token: 0x04004557 RID: 17751
		public int id;

		// Token: 0x04004558 RID: 17752
		public int value1;
	}
}
