using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B47 RID: 2887
	public class BuffSeidJsonData210 : IJSONClass
	{
		// Token: 0x06004884 RID: 18564 RVA: 0x001EDFCC File Offset: 0x001EC1CC
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[210].list)
			{
				try
				{
					BuffSeidJsonData210 buffSeidJsonData = new BuffSeidJsonData210();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData210.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData210.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData210.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData210.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData210.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData210.OnInitFinishAction != null)
			{
				BuffSeidJsonData210.OnInitFinishAction();
			}
		}

		// Token: 0x06004885 RID: 18565 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004260 RID: 16992
		public static int SEIDID = 210;

		// Token: 0x04004261 RID: 16993
		public static Dictionary<int, BuffSeidJsonData210> DataDict = new Dictionary<int, BuffSeidJsonData210>();

		// Token: 0x04004262 RID: 16994
		public static List<BuffSeidJsonData210> DataList = new List<BuffSeidJsonData210>();

		// Token: 0x04004263 RID: 16995
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData210.OnInitFinish);

		// Token: 0x04004264 RID: 16996
		public int id;

		// Token: 0x04004265 RID: 16997
		public int value1;
	}
}
