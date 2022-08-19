using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020007C1 RID: 1985
	public class BuffSeidJsonData314 : IJSONClass
	{
		// Token: 0x06003D16 RID: 15638 RVA: 0x001A2CB0 File Offset: 0x001A0EB0
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[314].list)
			{
				try
				{
					BuffSeidJsonData314 buffSeidJsonData = new BuffSeidJsonData314();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.target = jsonobject["target"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					if (BuffSeidJsonData314.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData314.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData314.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData314.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData314.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData314.OnInitFinishAction != null)
			{
				BuffSeidJsonData314.OnInitFinishAction();
			}
		}

		// Token: 0x06003D17 RID: 15639 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003743 RID: 14147
		public static int SEIDID = 314;

		// Token: 0x04003744 RID: 14148
		public static Dictionary<int, BuffSeidJsonData314> DataDict = new Dictionary<int, BuffSeidJsonData314>();

		// Token: 0x04003745 RID: 14149
		public static List<BuffSeidJsonData314> DataList = new List<BuffSeidJsonData314>();

		// Token: 0x04003746 RID: 14150
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData314.OnInitFinish);

		// Token: 0x04003747 RID: 14151
		public int id;

		// Token: 0x04003748 RID: 14152
		public int target;

		// Token: 0x04003749 RID: 14153
		public int value1;

		// Token: 0x0400374A RID: 14154
		public int value2;
	}
}
