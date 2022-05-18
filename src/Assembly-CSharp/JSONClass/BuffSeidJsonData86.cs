using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B8E RID: 2958
	public class BuffSeidJsonData86 : IJSONClass
	{
		// Token: 0x060049A0 RID: 18848 RVA: 0x001F39B4 File Offset: 0x001F1BB4
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[86].list)
			{
				try
				{
					BuffSeidJsonData86 buffSeidJsonData = new BuffSeidJsonData86();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData86.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData86.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData86.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData86.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData86.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData86.OnInitFinishAction != null)
			{
				BuffSeidJsonData86.OnInitFinishAction();
			}
		}

		// Token: 0x060049A1 RID: 18849 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004459 RID: 17497
		public static int SEIDID = 86;

		// Token: 0x0400445A RID: 17498
		public static Dictionary<int, BuffSeidJsonData86> DataDict = new Dictionary<int, BuffSeidJsonData86>();

		// Token: 0x0400445B RID: 17499
		public static List<BuffSeidJsonData86> DataList = new List<BuffSeidJsonData86>();

		// Token: 0x0400445C RID: 17500
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData86.OnInitFinish);

		// Token: 0x0400445D RID: 17501
		public int id;

		// Token: 0x0400445E RID: 17502
		public int value1;
	}
}
