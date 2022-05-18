using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B65 RID: 2917
	public class BuffSeidJsonData43 : IJSONClass
	{
		// Token: 0x060048FC RID: 18684 RVA: 0x001F06F0 File Offset: 0x001EE8F0
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[43].list)
			{
				try
				{
					BuffSeidJsonData43 buffSeidJsonData = new BuffSeidJsonData43();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					buffSeidJsonData.value3 = jsonobject["value3"].I;
					if (BuffSeidJsonData43.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData43.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData43.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData43.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData43.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData43.OnInitFinishAction != null)
			{
				BuffSeidJsonData43.OnInitFinishAction();
			}
		}

		// Token: 0x060048FD RID: 18685 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004340 RID: 17216
		public static int SEIDID = 43;

		// Token: 0x04004341 RID: 17217
		public static Dictionary<int, BuffSeidJsonData43> DataDict = new Dictionary<int, BuffSeidJsonData43>();

		// Token: 0x04004342 RID: 17218
		public static List<BuffSeidJsonData43> DataList = new List<BuffSeidJsonData43>();

		// Token: 0x04004343 RID: 17219
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData43.OnInitFinish);

		// Token: 0x04004344 RID: 17220
		public int id;

		// Token: 0x04004345 RID: 17221
		public int value1;

		// Token: 0x04004346 RID: 17222
		public int value2;

		// Token: 0x04004347 RID: 17223
		public int value3;
	}
}
