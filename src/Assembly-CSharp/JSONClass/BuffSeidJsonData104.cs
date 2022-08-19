using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200075B RID: 1883
	public class BuffSeidJsonData104 : IJSONClass
	{
		// Token: 0x06003B80 RID: 15232 RVA: 0x00199ABC File Offset: 0x00197CBC
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[104].list)
			{
				try
				{
					BuffSeidJsonData104 buffSeidJsonData = new BuffSeidJsonData104();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					if (BuffSeidJsonData104.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData104.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData104.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData104.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData104.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData104.OnInitFinishAction != null)
			{
				BuffSeidJsonData104.OnInitFinishAction();
			}
		}

		// Token: 0x06003B81 RID: 15233 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003497 RID: 13463
		public static int SEIDID = 104;

		// Token: 0x04003498 RID: 13464
		public static Dictionary<int, BuffSeidJsonData104> DataDict = new Dictionary<int, BuffSeidJsonData104>();

		// Token: 0x04003499 RID: 13465
		public static List<BuffSeidJsonData104> DataList = new List<BuffSeidJsonData104>();

		// Token: 0x0400349A RID: 13466
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData104.OnInitFinish);

		// Token: 0x0400349B RID: 13467
		public int id;

		// Token: 0x0400349C RID: 13468
		public int value1;

		// Token: 0x0400349D RID: 13469
		public int value2;
	}
}
