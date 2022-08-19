using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200076E RID: 1902
	public class BuffSeidJsonData131 : IJSONClass
	{
		// Token: 0x06003BCC RID: 15308 RVA: 0x0019B5BC File Offset: 0x001997BC
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[131].list)
			{
				try
				{
					BuffSeidJsonData131 buffSeidJsonData = new BuffSeidJsonData131();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].ToList();
					buffSeidJsonData.value3 = jsonobject["value3"].ToList();
					if (BuffSeidJsonData131.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData131.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData131.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData131.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData131.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData131.OnInitFinishAction != null)
			{
				BuffSeidJsonData131.OnInitFinishAction();
			}
		}

		// Token: 0x06003BCD RID: 15309 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003513 RID: 13587
		public static int SEIDID = 131;

		// Token: 0x04003514 RID: 13588
		public static Dictionary<int, BuffSeidJsonData131> DataDict = new Dictionary<int, BuffSeidJsonData131>();

		// Token: 0x04003515 RID: 13589
		public static List<BuffSeidJsonData131> DataList = new List<BuffSeidJsonData131>();

		// Token: 0x04003516 RID: 13590
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData131.OnInitFinish);

		// Token: 0x04003517 RID: 13591
		public int id;

		// Token: 0x04003518 RID: 13592
		public int value1;

		// Token: 0x04003519 RID: 13593
		public List<int> value2 = new List<int>();

		// Token: 0x0400351A RID: 13594
		public List<int> value3 = new List<int>();
	}
}
