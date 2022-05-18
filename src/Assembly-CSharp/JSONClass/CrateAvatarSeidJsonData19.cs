using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000BA7 RID: 2983
	public class CrateAvatarSeidJsonData19 : IJSONClass
	{
		// Token: 0x06004A04 RID: 18948 RVA: 0x001F5AB4 File Offset: 0x001F3CB4
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.CrateAvatarSeidJsonData[19].list)
			{
				try
				{
					CrateAvatarSeidJsonData19 crateAvatarSeidJsonData = new CrateAvatarSeidJsonData19();
					crateAvatarSeidJsonData.id = jsonobject["id"].I;
					crateAvatarSeidJsonData.value1 = jsonobject["value1"].I;
					if (CrateAvatarSeidJsonData19.DataDict.ContainsKey(crateAvatarSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典CrateAvatarSeidJsonData19.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", crateAvatarSeidJsonData.id));
					}
					else
					{
						CrateAvatarSeidJsonData19.DataDict.Add(crateAvatarSeidJsonData.id, crateAvatarSeidJsonData);
						CrateAvatarSeidJsonData19.DataList.Add(crateAvatarSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典CrateAvatarSeidJsonData19.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (CrateAvatarSeidJsonData19.OnInitFinishAction != null)
			{
				CrateAvatarSeidJsonData19.OnInitFinishAction();
			}
		}

		// Token: 0x06004A05 RID: 18949 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004513 RID: 17683
		public static int SEIDID = 19;

		// Token: 0x04004514 RID: 17684
		public static Dictionary<int, CrateAvatarSeidJsonData19> DataDict = new Dictionary<int, CrateAvatarSeidJsonData19>();

		// Token: 0x04004515 RID: 17685
		public static List<CrateAvatarSeidJsonData19> DataList = new List<CrateAvatarSeidJsonData19>();

		// Token: 0x04004516 RID: 17686
		public static Action OnInitFinishAction = new Action(CrateAvatarSeidJsonData19.OnInitFinish);

		// Token: 0x04004517 RID: 17687
		public int id;

		// Token: 0x04004518 RID: 17688
		public int value1;
	}
}
