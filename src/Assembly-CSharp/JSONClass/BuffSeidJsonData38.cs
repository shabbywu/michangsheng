using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B60 RID: 2912
	public class BuffSeidJsonData38 : IJSONClass
	{
		// Token: 0x060048E8 RID: 18664 RVA: 0x001EFFC8 File Offset: 0x001EE1C8
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[38].list)
			{
				try
				{
					BuffSeidJsonData38 buffSeidJsonData = new BuffSeidJsonData38();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					buffSeidJsonData.value3 = jsonobject["value3"].I;
					if (BuffSeidJsonData38.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData38.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData38.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData38.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData38.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData38.OnInitFinishAction != null)
			{
				BuffSeidJsonData38.OnInitFinishAction();
			}
		}

		// Token: 0x060048E9 RID: 18665 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004315 RID: 17173
		public static int SEIDID = 38;

		// Token: 0x04004316 RID: 17174
		public static Dictionary<int, BuffSeidJsonData38> DataDict = new Dictionary<int, BuffSeidJsonData38>();

		// Token: 0x04004317 RID: 17175
		public static List<BuffSeidJsonData38> DataList = new List<BuffSeidJsonData38>();

		// Token: 0x04004318 RID: 17176
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData38.OnInitFinish);

		// Token: 0x04004319 RID: 17177
		public int id;

		// Token: 0x0400431A RID: 17178
		public int value1;

		// Token: 0x0400431B RID: 17179
		public int value2;

		// Token: 0x0400431C RID: 17180
		public int value3;
	}
}
