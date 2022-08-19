using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020007D4 RID: 2004
	public class BuffSeidJsonData5 : IJSONClass
	{
		// Token: 0x06003D62 RID: 15714 RVA: 0x001A4ABC File Offset: 0x001A2CBC
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[5].list)
			{
				try
				{
					BuffSeidJsonData5 buffSeidJsonData = new BuffSeidJsonData5();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].ToList();
					buffSeidJsonData.value2 = jsonobject["value2"].ToList();
					if (BuffSeidJsonData5.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData5.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData5.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData5.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData5.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData5.OnInitFinishAction != null)
			{
				BuffSeidJsonData5.OnInitFinishAction();
			}
		}

		// Token: 0x06003D63 RID: 15715 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040037E0 RID: 14304
		public static int SEIDID = 5;

		// Token: 0x040037E1 RID: 14305
		public static Dictionary<int, BuffSeidJsonData5> DataDict = new Dictionary<int, BuffSeidJsonData5>();

		// Token: 0x040037E2 RID: 14306
		public static List<BuffSeidJsonData5> DataList = new List<BuffSeidJsonData5>();

		// Token: 0x040037E3 RID: 14307
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData5.OnInitFinish);

		// Token: 0x040037E4 RID: 14308
		public int id;

		// Token: 0x040037E5 RID: 14309
		public List<int> value1 = new List<int>();

		// Token: 0x040037E6 RID: 14310
		public List<int> value2 = new List<int>();
	}
}
