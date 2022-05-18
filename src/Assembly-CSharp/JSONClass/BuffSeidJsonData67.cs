using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B7D RID: 2941
	public class BuffSeidJsonData67 : IJSONClass
	{
		// Token: 0x0600495C RID: 18780 RVA: 0x001F2538 File Offset: 0x001F0738
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[67].list)
			{
				try
				{
					BuffSeidJsonData67 buffSeidJsonData = new BuffSeidJsonData67();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					buffSeidJsonData.value3 = jsonobject["value3"].I;
					buffSeidJsonData.value4 = jsonobject["value4"].I;
					if (BuffSeidJsonData67.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData67.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData67.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData67.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData67.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData67.OnInitFinishAction != null)
			{
				BuffSeidJsonData67.OnInitFinishAction();
			}
		}

		// Token: 0x0600495D RID: 18781 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040043EA RID: 17386
		public static int SEIDID = 67;

		// Token: 0x040043EB RID: 17387
		public static Dictionary<int, BuffSeidJsonData67> DataDict = new Dictionary<int, BuffSeidJsonData67>();

		// Token: 0x040043EC RID: 17388
		public static List<BuffSeidJsonData67> DataList = new List<BuffSeidJsonData67>();

		// Token: 0x040043ED RID: 17389
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData67.OnInitFinish);

		// Token: 0x040043EE RID: 17390
		public int id;

		// Token: 0x040043EF RID: 17391
		public int value1;

		// Token: 0x040043F0 RID: 17392
		public int value2;

		// Token: 0x040043F1 RID: 17393
		public int value3;

		// Token: 0x040043F2 RID: 17394
		public int value4;
	}
}
