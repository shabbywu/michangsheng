using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000815 RID: 2069
	public class CrateAvatarSeidJsonData22 : IJSONClass
	{
		// Token: 0x06003E66 RID: 15974 RVA: 0x001AA91C File Offset: 0x001A8B1C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.CrateAvatarSeidJsonData[22].list)
			{
				try
				{
					CrateAvatarSeidJsonData22 crateAvatarSeidJsonData = new CrateAvatarSeidJsonData22();
					crateAvatarSeidJsonData.id = jsonobject["id"].I;
					crateAvatarSeidJsonData.value1 = jsonobject["value1"].ToList();
					crateAvatarSeidJsonData.value2 = jsonobject["value2"].ToList();
					crateAvatarSeidJsonData.value3 = jsonobject["value3"].ToList();
					crateAvatarSeidJsonData.value4 = jsonobject["value4"].ToList();
					if (CrateAvatarSeidJsonData22.DataDict.ContainsKey(crateAvatarSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典CrateAvatarSeidJsonData22.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", crateAvatarSeidJsonData.id));
					}
					else
					{
						CrateAvatarSeidJsonData22.DataDict.Add(crateAvatarSeidJsonData.id, crateAvatarSeidJsonData);
						CrateAvatarSeidJsonData22.DataList.Add(crateAvatarSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典CrateAvatarSeidJsonData22.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (CrateAvatarSeidJsonData22.OnInitFinishAction != null)
			{
				CrateAvatarSeidJsonData22.OnInitFinishAction();
			}
		}

		// Token: 0x06003E67 RID: 15975 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040039A3 RID: 14755
		public static int SEIDID = 22;

		// Token: 0x040039A4 RID: 14756
		public static Dictionary<int, CrateAvatarSeidJsonData22> DataDict = new Dictionary<int, CrateAvatarSeidJsonData22>();

		// Token: 0x040039A5 RID: 14757
		public static List<CrateAvatarSeidJsonData22> DataList = new List<CrateAvatarSeidJsonData22>();

		// Token: 0x040039A6 RID: 14758
		public static Action OnInitFinishAction = new Action(CrateAvatarSeidJsonData22.OnInitFinish);

		// Token: 0x040039A7 RID: 14759
		public int id;

		// Token: 0x040039A8 RID: 14760
		public List<int> value1 = new List<int>();

		// Token: 0x040039A9 RID: 14761
		public List<int> value2 = new List<int>();

		// Token: 0x040039AA RID: 14762
		public List<int> value3 = new List<int>();

		// Token: 0x040039AB RID: 14763
		public List<int> value4 = new List<int>();
	}
}
