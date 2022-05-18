using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B8D RID: 2957
	public class BuffSeidJsonData85 : IJSONClass
	{
		// Token: 0x0600499C RID: 18844 RVA: 0x001F388C File Offset: 0x001F1A8C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[85].list)
			{
				try
				{
					BuffSeidJsonData85 buffSeidJsonData = new BuffSeidJsonData85();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].ToList();
					if (BuffSeidJsonData85.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData85.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData85.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData85.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData85.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData85.OnInitFinishAction != null)
			{
				BuffSeidJsonData85.OnInitFinishAction();
			}
		}

		// Token: 0x0600499D RID: 18845 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004453 RID: 17491
		public static int SEIDID = 85;

		// Token: 0x04004454 RID: 17492
		public static Dictionary<int, BuffSeidJsonData85> DataDict = new Dictionary<int, BuffSeidJsonData85>();

		// Token: 0x04004455 RID: 17493
		public static List<BuffSeidJsonData85> DataList = new List<BuffSeidJsonData85>();

		// Token: 0x04004456 RID: 17494
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData85.OnInitFinish);

		// Token: 0x04004457 RID: 17495
		public int id;

		// Token: 0x04004458 RID: 17496
		public List<int> value1 = new List<int>();
	}
}
