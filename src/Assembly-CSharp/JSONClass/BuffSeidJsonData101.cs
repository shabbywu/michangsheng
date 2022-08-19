using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000758 RID: 1880
	public class BuffSeidJsonData101 : IJSONClass
	{
		// Token: 0x06003B74 RID: 15220 RVA: 0x001996B4 File Offset: 0x001978B4
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[101].list)
			{
				try
				{
					BuffSeidJsonData101 buffSeidJsonData = new BuffSeidJsonData101();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData101.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData101.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData101.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData101.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData101.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData101.OnInitFinishAction != null)
			{
				BuffSeidJsonData101.OnInitFinishAction();
			}
		}

		// Token: 0x06003B75 RID: 15221 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003485 RID: 13445
		public static int SEIDID = 101;

		// Token: 0x04003486 RID: 13446
		public static Dictionary<int, BuffSeidJsonData101> DataDict = new Dictionary<int, BuffSeidJsonData101>();

		// Token: 0x04003487 RID: 13447
		public static List<BuffSeidJsonData101> DataList = new List<BuffSeidJsonData101>();

		// Token: 0x04003488 RID: 13448
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData101.OnInitFinish);

		// Token: 0x04003489 RID: 13449
		public int id;

		// Token: 0x0400348A RID: 13450
		public int value1;
	}
}
