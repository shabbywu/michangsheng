using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020007E6 RID: 2022
	public class BuffSeidJsonData67 : IJSONClass
	{
		// Token: 0x06003DAA RID: 15786 RVA: 0x001A646C File Offset: 0x001A466C
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

		// Token: 0x06003DAB RID: 15787 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400385A RID: 14426
		public static int SEIDID = 67;

		// Token: 0x0400385B RID: 14427
		public static Dictionary<int, BuffSeidJsonData67> DataDict = new Dictionary<int, BuffSeidJsonData67>();

		// Token: 0x0400385C RID: 14428
		public static List<BuffSeidJsonData67> DataList = new List<BuffSeidJsonData67>();

		// Token: 0x0400385D RID: 14429
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData67.OnInitFinish);

		// Token: 0x0400385E RID: 14430
		public int id;

		// Token: 0x0400385F RID: 14431
		public int value1;

		// Token: 0x04003860 RID: 14432
		public int value2;

		// Token: 0x04003861 RID: 14433
		public int value3;

		// Token: 0x04003862 RID: 14434
		public int value4;
	}
}
