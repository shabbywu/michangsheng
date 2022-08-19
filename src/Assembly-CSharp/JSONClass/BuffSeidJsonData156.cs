using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000780 RID: 1920
	public class BuffSeidJsonData156 : IJSONClass
	{
		// Token: 0x06003C14 RID: 15380 RVA: 0x0019D034 File Offset: 0x0019B234
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[156].list)
			{
				try
				{
					BuffSeidJsonData156 buffSeidJsonData = new BuffSeidJsonData156();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.target = jsonobject["target"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData156.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData156.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData156.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData156.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData156.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData156.OnInitFinishAction != null)
			{
				BuffSeidJsonData156.OnInitFinishAction();
			}
		}

		// Token: 0x06003C15 RID: 15381 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003591 RID: 13713
		public static int SEIDID = 156;

		// Token: 0x04003592 RID: 13714
		public static Dictionary<int, BuffSeidJsonData156> DataDict = new Dictionary<int, BuffSeidJsonData156>();

		// Token: 0x04003593 RID: 13715
		public static List<BuffSeidJsonData156> DataList = new List<BuffSeidJsonData156>();

		// Token: 0x04003594 RID: 13716
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData156.OnInitFinish);

		// Token: 0x04003595 RID: 13717
		public int id;

		// Token: 0x04003596 RID: 13718
		public int target;

		// Token: 0x04003597 RID: 13719
		public int value1;
	}
}
