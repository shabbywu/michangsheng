using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B29 RID: 2857
	public class BuffSeidJsonData173 : IJSONClass
	{
		// Token: 0x0600480C RID: 18444 RVA: 0x001EBB54 File Offset: 0x001E9D54
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[173].list)
			{
				try
				{
					BuffSeidJsonData173 buffSeidJsonData = new BuffSeidJsonData173();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData173.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData173.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData173.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData173.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData173.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData173.OnInitFinishAction != null)
			{
				BuffSeidJsonData173.OnInitFinishAction();
			}
		}

		// Token: 0x0600480D RID: 18445 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400419C RID: 16796
		public static int SEIDID = 173;

		// Token: 0x0400419D RID: 16797
		public static Dictionary<int, BuffSeidJsonData173> DataDict = new Dictionary<int, BuffSeidJsonData173>();

		// Token: 0x0400419E RID: 16798
		public static List<BuffSeidJsonData173> DataList = new List<BuffSeidJsonData173>();

		// Token: 0x0400419F RID: 16799
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData173.OnInitFinish);

		// Token: 0x040041A0 RID: 16800
		public int id;

		// Token: 0x040041A1 RID: 16801
		public int value1;
	}
}
