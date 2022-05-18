using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B8A RID: 2954
	public class BuffSeidJsonData82 : IJSONClass
	{
		// Token: 0x06004990 RID: 18832 RVA: 0x001F3500 File Offset: 0x001F1700
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[82].list)
			{
				try
				{
					BuffSeidJsonData82 buffSeidJsonData = new BuffSeidJsonData82();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					if (BuffSeidJsonData82.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData82.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData82.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData82.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData82.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData82.OnInitFinishAction != null)
			{
				BuffSeidJsonData82.OnInitFinishAction();
			}
		}

		// Token: 0x06004991 RID: 18833 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004440 RID: 17472
		public static int SEIDID = 82;

		// Token: 0x04004441 RID: 17473
		public static Dictionary<int, BuffSeidJsonData82> DataDict = new Dictionary<int, BuffSeidJsonData82>();

		// Token: 0x04004442 RID: 17474
		public static List<BuffSeidJsonData82> DataList = new List<BuffSeidJsonData82>();

		// Token: 0x04004443 RID: 17475
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData82.OnInitFinish);

		// Token: 0x04004444 RID: 17476
		public int id;

		// Token: 0x04004445 RID: 17477
		public int value1;

		// Token: 0x04004446 RID: 17478
		public int value2;
	}
}
