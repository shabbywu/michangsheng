using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020007F5 RID: 2037
	public class BuffSeidJsonData84 : IJSONClass
	{
		// Token: 0x06003DE6 RID: 15846 RVA: 0x001A7990 File Offset: 0x001A5B90
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[84].list)
			{
				try
				{
					BuffSeidJsonData84 buffSeidJsonData = new BuffSeidJsonData84();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData84.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData84.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData84.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData84.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData84.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData84.OnInitFinishAction != null)
			{
				BuffSeidJsonData84.OnInitFinishAction();
			}
		}

		// Token: 0x06003DE7 RID: 15847 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040038BD RID: 14525
		public static int SEIDID = 84;

		// Token: 0x040038BE RID: 14526
		public static Dictionary<int, BuffSeidJsonData84> DataDict = new Dictionary<int, BuffSeidJsonData84>();

		// Token: 0x040038BF RID: 14527
		public static List<BuffSeidJsonData84> DataList = new List<BuffSeidJsonData84>();

		// Token: 0x040038C0 RID: 14528
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData84.OnInitFinish);

		// Token: 0x040038C1 RID: 14529
		public int id;

		// Token: 0x040038C2 RID: 14530
		public int value1;
	}
}
