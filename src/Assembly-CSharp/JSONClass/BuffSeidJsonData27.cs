using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B51 RID: 2897
	public class BuffSeidJsonData27 : IJSONClass
	{
		// Token: 0x060048AC RID: 18604 RVA: 0x001EEC1C File Offset: 0x001ECE1C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[27].list)
			{
				try
				{
					BuffSeidJsonData27 buffSeidJsonData = new BuffSeidJsonData27();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					if (BuffSeidJsonData27.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData27.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData27.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData27.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData27.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData27.OnInitFinishAction != null)
			{
				BuffSeidJsonData27.OnInitFinishAction();
			}
		}

		// Token: 0x060048AD RID: 18605 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040042A3 RID: 17059
		public static int SEIDID = 27;

		// Token: 0x040042A4 RID: 17060
		public static Dictionary<int, BuffSeidJsonData27> DataDict = new Dictionary<int, BuffSeidJsonData27>();

		// Token: 0x040042A5 RID: 17061
		public static List<BuffSeidJsonData27> DataList = new List<BuffSeidJsonData27>();

		// Token: 0x040042A6 RID: 17062
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData27.OnInitFinish);

		// Token: 0x040042A7 RID: 17063
		public int id;

		// Token: 0x040042A8 RID: 17064
		public int value1;

		// Token: 0x040042A9 RID: 17065
		public int value2;
	}
}
