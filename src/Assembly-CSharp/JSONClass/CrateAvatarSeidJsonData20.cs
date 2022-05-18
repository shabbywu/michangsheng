using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000BA9 RID: 2985
	public class CrateAvatarSeidJsonData20 : IJSONClass
	{
		// Token: 0x06004A0C RID: 18956 RVA: 0x001F5D04 File Offset: 0x001F3F04
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.CrateAvatarSeidJsonData[20].list)
			{
				try
				{
					CrateAvatarSeidJsonData20 crateAvatarSeidJsonData = new CrateAvatarSeidJsonData20();
					crateAvatarSeidJsonData.id = jsonobject["id"].I;
					crateAvatarSeidJsonData.value1 = jsonobject["value1"].ToList();
					if (CrateAvatarSeidJsonData20.DataDict.ContainsKey(crateAvatarSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典CrateAvatarSeidJsonData20.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", crateAvatarSeidJsonData.id));
					}
					else
					{
						CrateAvatarSeidJsonData20.DataDict.Add(crateAvatarSeidJsonData.id, crateAvatarSeidJsonData);
						CrateAvatarSeidJsonData20.DataList.Add(crateAvatarSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典CrateAvatarSeidJsonData20.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (CrateAvatarSeidJsonData20.OnInitFinishAction != null)
			{
				CrateAvatarSeidJsonData20.OnInitFinishAction();
			}
		}

		// Token: 0x06004A0D RID: 18957 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400451F RID: 17695
		public static int SEIDID = 20;

		// Token: 0x04004520 RID: 17696
		public static Dictionary<int, CrateAvatarSeidJsonData20> DataDict = new Dictionary<int, CrateAvatarSeidJsonData20>();

		// Token: 0x04004521 RID: 17697
		public static List<CrateAvatarSeidJsonData20> DataList = new List<CrateAvatarSeidJsonData20>();

		// Token: 0x04004522 RID: 17698
		public static Action OnInitFinishAction = new Action(CrateAvatarSeidJsonData20.OnInitFinish);

		// Token: 0x04004523 RID: 17699
		public int id;

		// Token: 0x04004524 RID: 17700
		public List<int> value1 = new List<int>();
	}
}
