using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B9F RID: 2975
	public class CrateAvatarSeidJsonData1 : IJSONClass
	{
		// Token: 0x060049E4 RID: 18916 RVA: 0x001F514C File Offset: 0x001F334C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.CrateAvatarSeidJsonData[1].list)
			{
				try
				{
					CrateAvatarSeidJsonData1 crateAvatarSeidJsonData = new CrateAvatarSeidJsonData1();
					crateAvatarSeidJsonData.id = jsonobject["id"].I;
					crateAvatarSeidJsonData.value1 = jsonobject["value1"].I;
					if (CrateAvatarSeidJsonData1.DataDict.ContainsKey(crateAvatarSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典CrateAvatarSeidJsonData1.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", crateAvatarSeidJsonData.id));
					}
					else
					{
						CrateAvatarSeidJsonData1.DataDict.Add(crateAvatarSeidJsonData.id, crateAvatarSeidJsonData);
						CrateAvatarSeidJsonData1.DataList.Add(crateAvatarSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典CrateAvatarSeidJsonData1.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (CrateAvatarSeidJsonData1.OnInitFinishAction != null)
			{
				CrateAvatarSeidJsonData1.OnInitFinishAction();
			}
		}

		// Token: 0x060049E5 RID: 18917 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040044E1 RID: 17633
		public static int SEIDID = 1;

		// Token: 0x040044E2 RID: 17634
		public static Dictionary<int, CrateAvatarSeidJsonData1> DataDict = new Dictionary<int, CrateAvatarSeidJsonData1>();

		// Token: 0x040044E3 RID: 17635
		public static List<CrateAvatarSeidJsonData1> DataList = new List<CrateAvatarSeidJsonData1>();

		// Token: 0x040044E4 RID: 17636
		public static Action OnInitFinishAction = new Action(CrateAvatarSeidJsonData1.OnInitFinish);

		// Token: 0x040044E5 RID: 17637
		public int id;

		// Token: 0x040044E6 RID: 17638
		public int value1;
	}
}
