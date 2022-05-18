using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B62 RID: 2914
	public class BuffSeidJsonData40 : IJSONClass
	{
		// Token: 0x060048F0 RID: 18672 RVA: 0x001F029C File Offset: 0x001EE49C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[40].list)
			{
				try
				{
					BuffSeidJsonData40 buffSeidJsonData = new BuffSeidJsonData40();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					buffSeidJsonData.value3 = jsonobject["value3"].I;
					if (BuffSeidJsonData40.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData40.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData40.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData40.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData40.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData40.OnInitFinishAction != null)
			{
				BuffSeidJsonData40.OnInitFinishAction();
			}
		}

		// Token: 0x060048F1 RID: 18673 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004326 RID: 17190
		public static int SEIDID = 40;

		// Token: 0x04004327 RID: 17191
		public static Dictionary<int, BuffSeidJsonData40> DataDict = new Dictionary<int, BuffSeidJsonData40>();

		// Token: 0x04004328 RID: 17192
		public static List<BuffSeidJsonData40> DataList = new List<BuffSeidJsonData40>();

		// Token: 0x04004329 RID: 17193
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData40.OnInitFinish);

		// Token: 0x0400432A RID: 17194
		public int id;

		// Token: 0x0400432B RID: 17195
		public int value1;

		// Token: 0x0400432C RID: 17196
		public int value2;

		// Token: 0x0400432D RID: 17197
		public int value3;
	}
}
