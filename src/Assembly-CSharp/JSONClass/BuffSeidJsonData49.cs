using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B6A RID: 2922
	public class BuffSeidJsonData49 : IJSONClass
	{
		// Token: 0x06004910 RID: 18704 RVA: 0x001F0DC0 File Offset: 0x001EEFC0
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[49].list)
			{
				try
				{
					BuffSeidJsonData49 buffSeidJsonData = new BuffSeidJsonData49();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					buffSeidJsonData.value3 = jsonobject["value3"].I;
					if (BuffSeidJsonData49.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData49.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData49.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData49.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData49.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData49.OnInitFinishAction != null)
			{
				BuffSeidJsonData49.OnInitFinishAction();
			}
		}

		// Token: 0x06004911 RID: 18705 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004368 RID: 17256
		public static int SEIDID = 49;

		// Token: 0x04004369 RID: 17257
		public static Dictionary<int, BuffSeidJsonData49> DataDict = new Dictionary<int, BuffSeidJsonData49>();

		// Token: 0x0400436A RID: 17258
		public static List<BuffSeidJsonData49> DataList = new List<BuffSeidJsonData49>();

		// Token: 0x0400436B RID: 17259
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData49.OnInitFinish);

		// Token: 0x0400436C RID: 17260
		public int id;

		// Token: 0x0400436D RID: 17261
		public int value1;

		// Token: 0x0400436E RID: 17262
		public int value2;

		// Token: 0x0400436F RID: 17263
		public int value3;
	}
}
