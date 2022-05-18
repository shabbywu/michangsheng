using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000BA0 RID: 2976
	public class CrateAvatarSeidJsonData10 : IJSONClass
	{
		// Token: 0x060049E8 RID: 18920 RVA: 0x001F5274 File Offset: 0x001F3474
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.CrateAvatarSeidJsonData[10].list)
			{
				try
				{
					CrateAvatarSeidJsonData10 crateAvatarSeidJsonData = new CrateAvatarSeidJsonData10();
					crateAvatarSeidJsonData.id = jsonobject["id"].I;
					crateAvatarSeidJsonData.value1 = jsonobject["value1"].ToList();
					if (CrateAvatarSeidJsonData10.DataDict.ContainsKey(crateAvatarSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典CrateAvatarSeidJsonData10.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", crateAvatarSeidJsonData.id));
					}
					else
					{
						CrateAvatarSeidJsonData10.DataDict.Add(crateAvatarSeidJsonData.id, crateAvatarSeidJsonData);
						CrateAvatarSeidJsonData10.DataList.Add(crateAvatarSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典CrateAvatarSeidJsonData10.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (CrateAvatarSeidJsonData10.OnInitFinishAction != null)
			{
				CrateAvatarSeidJsonData10.OnInitFinishAction();
			}
		}

		// Token: 0x060049E9 RID: 18921 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040044E7 RID: 17639
		public static int SEIDID = 10;

		// Token: 0x040044E8 RID: 17640
		public static Dictionary<int, CrateAvatarSeidJsonData10> DataDict = new Dictionary<int, CrateAvatarSeidJsonData10>();

		// Token: 0x040044E9 RID: 17641
		public static List<CrateAvatarSeidJsonData10> DataList = new List<CrateAvatarSeidJsonData10>();

		// Token: 0x040044EA RID: 17642
		public static Action OnInitFinishAction = new Action(CrateAvatarSeidJsonData10.OnInitFinish);

		// Token: 0x040044EB RID: 17643
		public int id;

		// Token: 0x040044EC RID: 17644
		public List<int> value1 = new List<int>();
	}
}
