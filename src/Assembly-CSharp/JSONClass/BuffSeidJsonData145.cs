using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B12 RID: 2834
	public class BuffSeidJsonData145 : IJSONClass
	{
		// Token: 0x060047B2 RID: 18354 RVA: 0x001E9FE4 File Offset: 0x001E81E4
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[145].list)
			{
				try
				{
					BuffSeidJsonData145 buffSeidJsonData = new BuffSeidJsonData145();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					if (BuffSeidJsonData145.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData145.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData145.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData145.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData145.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData145.OnInitFinishAction != null)
			{
				BuffSeidJsonData145.OnInitFinishAction();
			}
		}

		// Token: 0x060047B3 RID: 18355 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004104 RID: 16644
		public static int SEIDID = 145;

		// Token: 0x04004105 RID: 16645
		public static Dictionary<int, BuffSeidJsonData145> DataDict = new Dictionary<int, BuffSeidJsonData145>();

		// Token: 0x04004106 RID: 16646
		public static List<BuffSeidJsonData145> DataList = new List<BuffSeidJsonData145>();

		// Token: 0x04004107 RID: 16647
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData145.OnInitFinish);

		// Token: 0x04004108 RID: 16648
		public int id;

		// Token: 0x04004109 RID: 16649
		public int value1;

		// Token: 0x0400410A RID: 16650
		public int value2;
	}
}
