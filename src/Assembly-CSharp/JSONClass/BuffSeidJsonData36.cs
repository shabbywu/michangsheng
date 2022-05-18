using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B5E RID: 2910
	public class BuffSeidJsonData36 : IJSONClass
	{
		// Token: 0x060048E0 RID: 18656 RVA: 0x001EFCF4 File Offset: 0x001EDEF4
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[36].list)
			{
				try
				{
					BuffSeidJsonData36 buffSeidJsonData = new BuffSeidJsonData36();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					buffSeidJsonData.value3 = jsonobject["value3"].I;
					if (BuffSeidJsonData36.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData36.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData36.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData36.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData36.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData36.OnInitFinishAction != null)
			{
				BuffSeidJsonData36.OnInitFinishAction();
			}
		}

		// Token: 0x060048E1 RID: 18657 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004304 RID: 17156
		public static int SEIDID = 36;

		// Token: 0x04004305 RID: 17157
		public static Dictionary<int, BuffSeidJsonData36> DataDict = new Dictionary<int, BuffSeidJsonData36>();

		// Token: 0x04004306 RID: 17158
		public static List<BuffSeidJsonData36> DataList = new List<BuffSeidJsonData36>();

		// Token: 0x04004307 RID: 17159
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData36.OnInitFinish);

		// Token: 0x04004308 RID: 17160
		public int id;

		// Token: 0x04004309 RID: 17161
		public int value1;

		// Token: 0x0400430A RID: 17162
		public int value2;

		// Token: 0x0400430B RID: 17163
		public int value3;
	}
}
