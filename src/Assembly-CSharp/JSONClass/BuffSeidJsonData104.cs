using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000AF3 RID: 2803
	public class BuffSeidJsonData104 : IJSONClass
	{
		// Token: 0x06004736 RID: 18230 RVA: 0x001E79A8 File Offset: 0x001E5BA8
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

		// Token: 0x06004737 RID: 18231 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004030 RID: 16432
		public static int SEIDID = 104;

		// Token: 0x04004031 RID: 16433
		public static Dictionary<int, BuffSeidJsonData104> DataDict = new Dictionary<int, BuffSeidJsonData104>();

		// Token: 0x04004032 RID: 16434
		public static List<BuffSeidJsonData104> DataList = new List<BuffSeidJsonData104>();

		// Token: 0x04004033 RID: 16435
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData104.OnInitFinish);

		// Token: 0x04004034 RID: 16436
		public int id;

		// Token: 0x04004035 RID: 16437
		public int value1;

		// Token: 0x04004036 RID: 16438
		public int value2;
	}
}
