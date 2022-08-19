using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200075A RID: 1882
	public class BuffSeidJsonData103 : IJSONClass
	{
		// Token: 0x06003B7C RID: 15228 RVA: 0x00199964 File Offset: 0x00197B64
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[103].list)
			{
				try
				{
					BuffSeidJsonData103 buffSeidJsonData = new BuffSeidJsonData103();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData103.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData103.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData103.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData103.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData103.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData103.OnInitFinishAction != null)
			{
				BuffSeidJsonData103.OnInitFinishAction();
			}
		}

		// Token: 0x06003B7D RID: 15229 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003491 RID: 13457
		public static int SEIDID = 103;

		// Token: 0x04003492 RID: 13458
		public static Dictionary<int, BuffSeidJsonData103> DataDict = new Dictionary<int, BuffSeidJsonData103>();

		// Token: 0x04003493 RID: 13459
		public static List<BuffSeidJsonData103> DataList = new List<BuffSeidJsonData103>();

		// Token: 0x04003494 RID: 13460
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData103.OnInitFinish);

		// Token: 0x04003495 RID: 13461
		public int id;

		// Token: 0x04003496 RID: 13462
		public int value1;
	}
}
