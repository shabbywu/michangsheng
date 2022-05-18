using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B96 RID: 2966
	public class BuffSeidJsonData94 : IJSONClass
	{
		// Token: 0x060049C0 RID: 18880 RVA: 0x001F4358 File Offset: 0x001F2558
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[94].list)
			{
				try
				{
					BuffSeidJsonData94 buffSeidJsonData = new BuffSeidJsonData94();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData94.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData94.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData94.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData94.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData94.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData94.OnInitFinishAction != null)
			{
				BuffSeidJsonData94.OnInitFinishAction();
			}
		}

		// Token: 0x060049C1 RID: 18881 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400448E RID: 17550
		public static int SEIDID = 94;

		// Token: 0x0400448F RID: 17551
		public static Dictionary<int, BuffSeidJsonData94> DataDict = new Dictionary<int, BuffSeidJsonData94>();

		// Token: 0x04004490 RID: 17552
		public static List<BuffSeidJsonData94> DataList = new List<BuffSeidJsonData94>();

		// Token: 0x04004491 RID: 17553
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData94.OnInitFinish);

		// Token: 0x04004492 RID: 17554
		public int id;

		// Token: 0x04004493 RID: 17555
		public int value1;
	}
}
