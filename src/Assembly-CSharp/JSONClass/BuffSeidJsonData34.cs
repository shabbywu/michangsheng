using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B5C RID: 2908
	public class BuffSeidJsonData34 : IJSONClass
	{
		// Token: 0x060048D8 RID: 18648 RVA: 0x001EF9F4 File Offset: 0x001EDBF4
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[34].list)
			{
				try
				{
					BuffSeidJsonData34 buffSeidJsonData = new BuffSeidJsonData34();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					buffSeidJsonData.value3 = jsonobject["value3"].I;
					buffSeidJsonData.value4 = jsonobject["value4"].I;
					if (BuffSeidJsonData34.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData34.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData34.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData34.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData34.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData34.OnInitFinishAction != null)
			{
				BuffSeidJsonData34.OnInitFinishAction();
			}
		}

		// Token: 0x060048D9 RID: 18649 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040042F2 RID: 17138
		public static int SEIDID = 34;

		// Token: 0x040042F3 RID: 17139
		public static Dictionary<int, BuffSeidJsonData34> DataDict = new Dictionary<int, BuffSeidJsonData34>();

		// Token: 0x040042F4 RID: 17140
		public static List<BuffSeidJsonData34> DataList = new List<BuffSeidJsonData34>();

		// Token: 0x040042F5 RID: 17141
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData34.OnInitFinish);

		// Token: 0x040042F6 RID: 17142
		public int id;

		// Token: 0x040042F7 RID: 17143
		public int value1;

		// Token: 0x040042F8 RID: 17144
		public int value2;

		// Token: 0x040042F9 RID: 17145
		public int value3;

		// Token: 0x040042FA RID: 17146
		public int value4;
	}
}
