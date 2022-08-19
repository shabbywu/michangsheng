using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000813 RID: 2067
	public class CrateAvatarSeidJsonData20 : IJSONClass
	{
		// Token: 0x06003E5E RID: 15966 RVA: 0x001AA658 File Offset: 0x001A8858
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

		// Token: 0x06003E5F RID: 15967 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003997 RID: 14743
		public static int SEIDID = 20;

		// Token: 0x04003998 RID: 14744
		public static Dictionary<int, CrateAvatarSeidJsonData20> DataDict = new Dictionary<int, CrateAvatarSeidJsonData20>();

		// Token: 0x04003999 RID: 14745
		public static List<CrateAvatarSeidJsonData20> DataList = new List<CrateAvatarSeidJsonData20>();

		// Token: 0x0400399A RID: 14746
		public static Action OnInitFinishAction = new Action(CrateAvatarSeidJsonData20.OnInitFinish);

		// Token: 0x0400399B RID: 14747
		public int id;

		// Token: 0x0400399C RID: 14748
		public List<int> value1 = new List<int>();
	}
}
