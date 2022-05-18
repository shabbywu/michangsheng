using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000BB3 RID: 2995
	public class CrateAvatarSeidJsonData9 : IJSONClass
	{
		// Token: 0x06004A34 RID: 18996 RVA: 0x001F6900 File Offset: 0x001F4B00
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.CrateAvatarSeidJsonData[9].list)
			{
				try
				{
					CrateAvatarSeidJsonData9 crateAvatarSeidJsonData = new CrateAvatarSeidJsonData9();
					crateAvatarSeidJsonData.id = jsonobject["id"].I;
					crateAvatarSeidJsonData.value1 = jsonobject["value1"].ToList();
					if (CrateAvatarSeidJsonData9.DataDict.ContainsKey(crateAvatarSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典CrateAvatarSeidJsonData9.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", crateAvatarSeidJsonData.id));
					}
					else
					{
						CrateAvatarSeidJsonData9.DataDict.Add(crateAvatarSeidJsonData.id, crateAvatarSeidJsonData);
						CrateAvatarSeidJsonData9.DataList.Add(crateAvatarSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典CrateAvatarSeidJsonData9.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (CrateAvatarSeidJsonData9.OnInitFinishAction != null)
			{
				CrateAvatarSeidJsonData9.OnInitFinishAction();
			}
		}

		// Token: 0x06004A35 RID: 18997 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400455F RID: 17759
		public static int SEIDID = 9;

		// Token: 0x04004560 RID: 17760
		public static Dictionary<int, CrateAvatarSeidJsonData9> DataDict = new Dictionary<int, CrateAvatarSeidJsonData9>();

		// Token: 0x04004561 RID: 17761
		public static List<CrateAvatarSeidJsonData9> DataList = new List<CrateAvatarSeidJsonData9>();

		// Token: 0x04004562 RID: 17762
		public static Action OnInitFinishAction = new Action(CrateAvatarSeidJsonData9.OnInitFinish);

		// Token: 0x04004563 RID: 17763
		public int id;

		// Token: 0x04004564 RID: 17764
		public List<int> value1 = new List<int>();
	}
}
