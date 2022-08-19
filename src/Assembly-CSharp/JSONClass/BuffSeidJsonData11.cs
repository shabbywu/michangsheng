using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200075E RID: 1886
	public class BuffSeidJsonData11 : IJSONClass
	{
		// Token: 0x06003B8C RID: 15244 RVA: 0x00199F00 File Offset: 0x00198100
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[11].list)
			{
				try
				{
					BuffSeidJsonData11 buffSeidJsonData = new BuffSeidJsonData11();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData11.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData11.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData11.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData11.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData11.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData11.OnInitFinishAction != null)
			{
				BuffSeidJsonData11.OnInitFinishAction();
			}
		}

		// Token: 0x06003B8D RID: 15245 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040034AB RID: 13483
		public static int SEIDID = 11;

		// Token: 0x040034AC RID: 13484
		public static Dictionary<int, BuffSeidJsonData11> DataDict = new Dictionary<int, BuffSeidJsonData11>();

		// Token: 0x040034AD RID: 13485
		public static List<BuffSeidJsonData11> DataList = new List<BuffSeidJsonData11>();

		// Token: 0x040034AE RID: 13486
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData11.OnInitFinish);

		// Token: 0x040034AF RID: 13487
		public int id;

		// Token: 0x040034B0 RID: 13488
		public int value1;
	}
}
