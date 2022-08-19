using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200080A RID: 2058
	public class CrateAvatarSeidJsonData10 : IJSONClass
	{
		// Token: 0x06003E3A RID: 15930 RVA: 0x001A9A04 File Offset: 0x001A7C04
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.CrateAvatarSeidJsonData[10].list)
			{
				try
				{
					CrateAvatarSeidJsonData10 crateAvatarSeidJsonData = new CrateAvatarSeidJsonData10();
					crateAvatarSeidJsonData.id = jsonobject["id"].I;
					crateAvatarSeidJsonData.value1 = jsonobject["value1"].ToList();
					if (CrateAvatarSeidJsonData10.DataDict.ContainsKey(crateAvatarSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典CrateAvatarSeidJsonData10.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", crateAvatarSeidJsonData.id));
					}
					else
					{
						CrateAvatarSeidJsonData10.DataDict.Add(crateAvatarSeidJsonData.id, crateAvatarSeidJsonData);
						CrateAvatarSeidJsonData10.DataList.Add(crateAvatarSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典CrateAvatarSeidJsonData10.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (CrateAvatarSeidJsonData10.OnInitFinishAction != null)
			{
				CrateAvatarSeidJsonData10.OnInitFinishAction();
			}
		}

		// Token: 0x06003E3B RID: 15931 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400395F RID: 14687
		public static int SEIDID = 10;

		// Token: 0x04003960 RID: 14688
		public static Dictionary<int, CrateAvatarSeidJsonData10> DataDict = new Dictionary<int, CrateAvatarSeidJsonData10>();

		// Token: 0x04003961 RID: 14689
		public static List<CrateAvatarSeidJsonData10> DataList = new List<CrateAvatarSeidJsonData10>();

		// Token: 0x04003962 RID: 14690
		public static Action OnInitFinishAction = new Action(CrateAvatarSeidJsonData10.OnInitFinish);

		// Token: 0x04003963 RID: 14691
		public int id;

		// Token: 0x04003964 RID: 14692
		public List<int> value1 = new List<int>();
	}
}
