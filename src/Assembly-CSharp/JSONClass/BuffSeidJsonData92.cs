using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B94 RID: 2964
	public class BuffSeidJsonData92 : IJSONClass
	{
		// Token: 0x060049B8 RID: 18872 RVA: 0x001F40F4 File Offset: 0x001F22F4
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[92].list)
			{
				try
				{
					BuffSeidJsonData92 buffSeidJsonData = new BuffSeidJsonData92();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					if (BuffSeidJsonData92.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData92.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData92.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData92.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData92.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData92.OnInitFinishAction != null)
			{
				BuffSeidJsonData92.OnInitFinishAction();
			}
		}

		// Token: 0x060049B9 RID: 18873 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004481 RID: 17537
		public static int SEIDID = 92;

		// Token: 0x04004482 RID: 17538
		public static Dictionary<int, BuffSeidJsonData92> DataDict = new Dictionary<int, BuffSeidJsonData92>();

		// Token: 0x04004483 RID: 17539
		public static List<BuffSeidJsonData92> DataList = new List<BuffSeidJsonData92>();

		// Token: 0x04004484 RID: 17540
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData92.OnInitFinish);

		// Token: 0x04004485 RID: 17541
		public int id;

		// Token: 0x04004486 RID: 17542
		public int value1;

		// Token: 0x04004487 RID: 17543
		public int value2;
	}
}
