using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020007BA RID: 1978
	public class BuffSeidJsonData27 : IJSONClass
	{
		// Token: 0x06003CFA RID: 15610 RVA: 0x001A22AC File Offset: 0x001A04AC
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

		// Token: 0x06003CFB RID: 15611 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003713 RID: 14099
		public static int SEIDID = 27;

		// Token: 0x04003714 RID: 14100
		public static Dictionary<int, BuffSeidJsonData27> DataDict = new Dictionary<int, BuffSeidJsonData27>();

		// Token: 0x04003715 RID: 14101
		public static List<BuffSeidJsonData27> DataList = new List<BuffSeidJsonData27>();

		// Token: 0x04003716 RID: 14102
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData27.OnInitFinish);

		// Token: 0x04003717 RID: 14103
		public int id;

		// Token: 0x04003718 RID: 14104
		public int value1;

		// Token: 0x04003719 RID: 14105
		public int value2;
	}
}
