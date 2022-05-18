using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000AF1 RID: 2801
	public class BuffSeidJsonData102 : IJSONClass
	{
		// Token: 0x0600472E RID: 18222 RVA: 0x001E7758 File Offset: 0x001E5958
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[102].list)
			{
				try
				{
					BuffSeidJsonData102 buffSeidJsonData = new BuffSeidJsonData102();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData102.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData102.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData102.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData102.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData102.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData102.OnInitFinishAction != null)
			{
				BuffSeidJsonData102.OnInitFinishAction();
			}
		}

		// Token: 0x0600472F RID: 18223 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004024 RID: 16420
		public static int SEIDID = 102;

		// Token: 0x04004025 RID: 16421
		public static Dictionary<int, BuffSeidJsonData102> DataDict = new Dictionary<int, BuffSeidJsonData102>();

		// Token: 0x04004026 RID: 16422
		public static List<BuffSeidJsonData102> DataList = new List<BuffSeidJsonData102>();

		// Token: 0x04004027 RID: 16423
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData102.OnInitFinish);

		// Token: 0x04004028 RID: 16424
		public int id;

		// Token: 0x04004029 RID: 16425
		public int value1;
	}
}
