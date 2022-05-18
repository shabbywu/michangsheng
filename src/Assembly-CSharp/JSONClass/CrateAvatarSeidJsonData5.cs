using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000BAF RID: 2991
	public class CrateAvatarSeidJsonData5 : IJSONClass
	{
		// Token: 0x06004A24 RID: 18980 RVA: 0x001F6460 File Offset: 0x001F4660
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.CrateAvatarSeidJsonData[5].list)
			{
				try
				{
					CrateAvatarSeidJsonData5 crateAvatarSeidJsonData = new CrateAvatarSeidJsonData5();
					crateAvatarSeidJsonData.id = jsonobject["id"].I;
					crateAvatarSeidJsonData.value1 = jsonobject["value1"].I;
					if (CrateAvatarSeidJsonData5.DataDict.ContainsKey(crateAvatarSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典CrateAvatarSeidJsonData5.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", crateAvatarSeidJsonData.id));
					}
					else
					{
						CrateAvatarSeidJsonData5.DataDict.Add(crateAvatarSeidJsonData.id, crateAvatarSeidJsonData);
						CrateAvatarSeidJsonData5.DataList.Add(crateAvatarSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典CrateAvatarSeidJsonData5.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (CrateAvatarSeidJsonData5.OnInitFinishAction != null)
			{
				CrateAvatarSeidJsonData5.OnInitFinishAction();
			}
		}

		// Token: 0x06004A25 RID: 18981 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004547 RID: 17735
		public static int SEIDID = 5;

		// Token: 0x04004548 RID: 17736
		public static Dictionary<int, CrateAvatarSeidJsonData5> DataDict = new Dictionary<int, CrateAvatarSeidJsonData5>();

		// Token: 0x04004549 RID: 17737
		public static List<CrateAvatarSeidJsonData5> DataList = new List<CrateAvatarSeidJsonData5>();

		// Token: 0x0400454A RID: 17738
		public static Action OnInitFinishAction = new Action(CrateAvatarSeidJsonData5.OnInitFinish);

		// Token: 0x0400454B RID: 17739
		public int id;

		// Token: 0x0400454C RID: 17740
		public int value1;
	}
}
