using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B66 RID: 2918
	public class BuffSeidJsonData44 : IJSONClass
	{
		// Token: 0x06004900 RID: 18688 RVA: 0x001F0844 File Offset: 0x001EEA44
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[44].list)
			{
				try
				{
					BuffSeidJsonData44 buffSeidJsonData = new BuffSeidJsonData44();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					buffSeidJsonData.value3 = jsonobject["value3"].I;
					buffSeidJsonData.value4 = jsonobject["value4"].I;
					if (BuffSeidJsonData44.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData44.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData44.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData44.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData44.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData44.OnInitFinishAction != null)
			{
				BuffSeidJsonData44.OnInitFinishAction();
			}
		}

		// Token: 0x06004901 RID: 18689 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004348 RID: 17224
		public static int SEIDID = 44;

		// Token: 0x04004349 RID: 17225
		public static Dictionary<int, BuffSeidJsonData44> DataDict = new Dictionary<int, BuffSeidJsonData44>();

		// Token: 0x0400434A RID: 17226
		public static List<BuffSeidJsonData44> DataList = new List<BuffSeidJsonData44>();

		// Token: 0x0400434B RID: 17227
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData44.OnInitFinish);

		// Token: 0x0400434C RID: 17228
		public int id;

		// Token: 0x0400434D RID: 17229
		public int value1;

		// Token: 0x0400434E RID: 17230
		public int value2;

		// Token: 0x0400434F RID: 17231
		public int value3;

		// Token: 0x04004350 RID: 17232
		public int value4;
	}
}
