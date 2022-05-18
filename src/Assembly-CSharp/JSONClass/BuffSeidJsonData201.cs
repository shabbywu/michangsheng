using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B3E RID: 2878
	public class BuffSeidJsonData201 : IJSONClass
	{
		// Token: 0x06004860 RID: 18528 RVA: 0x001ED504 File Offset: 0x001EB704
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[201].list)
			{
				try
				{
					BuffSeidJsonData201 buffSeidJsonData = new BuffSeidJsonData201();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData201.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData201.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData201.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData201.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData201.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData201.OnInitFinishAction != null)
			{
				BuffSeidJsonData201.OnInitFinishAction();
			}
		}

		// Token: 0x06004861 RID: 18529 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004227 RID: 16935
		public static int SEIDID = 201;

		// Token: 0x04004228 RID: 16936
		public static Dictionary<int, BuffSeidJsonData201> DataDict = new Dictionary<int, BuffSeidJsonData201>();

		// Token: 0x04004229 RID: 16937
		public static List<BuffSeidJsonData201> DataList = new List<BuffSeidJsonData201>();

		// Token: 0x0400422A RID: 16938
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData201.OnInitFinish);

		// Token: 0x0400422B RID: 16939
		public int id;

		// Token: 0x0400422C RID: 16940
		public int value1;
	}
}
