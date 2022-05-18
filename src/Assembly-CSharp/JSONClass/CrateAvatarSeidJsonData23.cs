using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000BAC RID: 2988
	public class CrateAvatarSeidJsonData23 : IJSONClass
	{
		// Token: 0x06004A18 RID: 18968 RVA: 0x001F60D4 File Offset: 0x001F42D4
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.CrateAvatarSeidJsonData[23].list)
			{
				try
				{
					CrateAvatarSeidJsonData23 crateAvatarSeidJsonData = new CrateAvatarSeidJsonData23();
					crateAvatarSeidJsonData.id = jsonobject["id"].I;
					crateAvatarSeidJsonData.value1 = jsonobject["value1"].ToList();
					crateAvatarSeidJsonData.value2 = jsonobject["value2"].ToList();
					if (CrateAvatarSeidJsonData23.DataDict.ContainsKey(crateAvatarSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典CrateAvatarSeidJsonData23.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", crateAvatarSeidJsonData.id));
					}
					else
					{
						CrateAvatarSeidJsonData23.DataDict.Add(crateAvatarSeidJsonData.id, crateAvatarSeidJsonData);
						CrateAvatarSeidJsonData23.DataList.Add(crateAvatarSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典CrateAvatarSeidJsonData23.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (CrateAvatarSeidJsonData23.OnInitFinishAction != null)
			{
				CrateAvatarSeidJsonData23.OnInitFinishAction();
			}
		}

		// Token: 0x06004A19 RID: 18969 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004534 RID: 17716
		public static int SEIDID = 23;

		// Token: 0x04004535 RID: 17717
		public static Dictionary<int, CrateAvatarSeidJsonData23> DataDict = new Dictionary<int, CrateAvatarSeidJsonData23>();

		// Token: 0x04004536 RID: 17718
		public static List<CrateAvatarSeidJsonData23> DataList = new List<CrateAvatarSeidJsonData23>();

		// Token: 0x04004537 RID: 17719
		public static Action OnInitFinishAction = new Action(CrateAvatarSeidJsonData23.OnInitFinish);

		// Token: 0x04004538 RID: 17720
		public int id;

		// Token: 0x04004539 RID: 17721
		public List<int> value1 = new List<int>();

		// Token: 0x0400453A RID: 17722
		public List<int> value2 = new List<int>();
	}
}
