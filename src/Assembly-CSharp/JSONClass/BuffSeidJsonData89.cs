using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B91 RID: 2961
	public class BuffSeidJsonData89 : IJSONClass
	{
		// Token: 0x060049AC RID: 18860 RVA: 0x001F3D54 File Offset: 0x001F1F54
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[89].list)
			{
				try
				{
					BuffSeidJsonData89 buffSeidJsonData = new BuffSeidJsonData89();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					if (BuffSeidJsonData89.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData89.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData89.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData89.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData89.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData89.OnInitFinishAction != null)
			{
				BuffSeidJsonData89.OnInitFinishAction();
			}
		}

		// Token: 0x060049AD RID: 18861 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400446D RID: 17517
		public static int SEIDID = 89;

		// Token: 0x0400446E RID: 17518
		public static Dictionary<int, BuffSeidJsonData89> DataDict = new Dictionary<int, BuffSeidJsonData89>();

		// Token: 0x0400446F RID: 17519
		public static List<BuffSeidJsonData89> DataList = new List<BuffSeidJsonData89>();

		// Token: 0x04004470 RID: 17520
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData89.OnInitFinish);

		// Token: 0x04004471 RID: 17521
		public int id;

		// Token: 0x04004472 RID: 17522
		public int value1;

		// Token: 0x04004473 RID: 17523
		public int value2;
	}
}
