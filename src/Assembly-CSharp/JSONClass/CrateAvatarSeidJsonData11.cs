using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000BA1 RID: 2977
	public class CrateAvatarSeidJsonData11 : IJSONClass
	{
		// Token: 0x060049EC RID: 18924 RVA: 0x001F539C File Offset: 0x001F359C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.CrateAvatarSeidJsonData[11].list)
			{
				try
				{
					CrateAvatarSeidJsonData11 crateAvatarSeidJsonData = new CrateAvatarSeidJsonData11();
					crateAvatarSeidJsonData.id = jsonobject["id"].I;
					crateAvatarSeidJsonData.value1 = jsonobject["value1"].I;
					crateAvatarSeidJsonData.value2 = jsonobject["value2"].I;
					if (CrateAvatarSeidJsonData11.DataDict.ContainsKey(crateAvatarSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典CrateAvatarSeidJsonData11.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", crateAvatarSeidJsonData.id));
					}
					else
					{
						CrateAvatarSeidJsonData11.DataDict.Add(crateAvatarSeidJsonData.id, crateAvatarSeidJsonData);
						CrateAvatarSeidJsonData11.DataList.Add(crateAvatarSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典CrateAvatarSeidJsonData11.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (CrateAvatarSeidJsonData11.OnInitFinishAction != null)
			{
				CrateAvatarSeidJsonData11.OnInitFinishAction();
			}
		}

		// Token: 0x060049ED RID: 18925 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040044ED RID: 17645
		public static int SEIDID = 11;

		// Token: 0x040044EE RID: 17646
		public static Dictionary<int, CrateAvatarSeidJsonData11> DataDict = new Dictionary<int, CrateAvatarSeidJsonData11>();

		// Token: 0x040044EF RID: 17647
		public static List<CrateAvatarSeidJsonData11> DataList = new List<CrateAvatarSeidJsonData11>();

		// Token: 0x040044F0 RID: 17648
		public static Action OnInitFinishAction = new Action(CrateAvatarSeidJsonData11.OnInitFinish);

		// Token: 0x040044F1 RID: 17649
		public int id;

		// Token: 0x040044F2 RID: 17650
		public int value1;

		// Token: 0x040044F3 RID: 17651
		public int value2;
	}
}
