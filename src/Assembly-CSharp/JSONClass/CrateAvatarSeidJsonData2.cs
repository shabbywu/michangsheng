using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000812 RID: 2066
	public class CrateAvatarSeidJsonData2 : IJSONClass
	{
		// Token: 0x06003E5A RID: 15962 RVA: 0x001AA500 File Offset: 0x001A8700
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

		// Token: 0x06003E5B RID: 15963 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003991 RID: 14737
		public static int SEIDID = 2;

		// Token: 0x04003992 RID: 14738
		public static Dictionary<int, CrateAvatarSeidJsonData2> DataDict = new Dictionary<int, CrateAvatarSeidJsonData2>();

		// Token: 0x04003993 RID: 14739
		public static List<CrateAvatarSeidJsonData2> DataList = new List<CrateAvatarSeidJsonData2>();

		// Token: 0x04003994 RID: 14740
		public static Action OnInitFinishAction = new Action(CrateAvatarSeidJsonData2.OnInitFinish);

		// Token: 0x04003995 RID: 14741
		public int id;

		// Token: 0x04003996 RID: 14742
		public int value1;
	}
}
