using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020007D1 RID: 2001
	public class BuffSeidJsonData47 : IJSONClass
	{
		// Token: 0x06003D56 RID: 15702 RVA: 0x001A465C File Offset: 0x001A285C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[47].list)
			{
				try
				{
					BuffSeidJsonData47 buffSeidJsonData = new BuffSeidJsonData47();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData47.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData47.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData47.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData47.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData47.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData47.OnInitFinishAction != null)
			{
				BuffSeidJsonData47.OnInitFinishAction();
			}
		}

		// Token: 0x06003D57 RID: 15703 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040037CA RID: 14282
		public static int SEIDID = 47;

		// Token: 0x040037CB RID: 14283
		public static Dictionary<int, BuffSeidJsonData47> DataDict = new Dictionary<int, BuffSeidJsonData47>();

		// Token: 0x040037CC RID: 14284
		public static List<BuffSeidJsonData47> DataList = new List<BuffSeidJsonData47>();

		// Token: 0x040037CD RID: 14285
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData47.OnInitFinish);

		// Token: 0x040037CE RID: 14286
		public int id;

		// Token: 0x040037CF RID: 14287
		public int value1;
	}
}
