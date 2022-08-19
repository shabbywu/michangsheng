using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020007CB RID: 1995
	public class BuffSeidJsonData40 : IJSONClass
	{
		// Token: 0x06003D3E RID: 15678 RVA: 0x001A3C80 File Offset: 0x001A1E80
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

		// Token: 0x06003D3F RID: 15679 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003796 RID: 14230
		public static int SEIDID = 40;

		// Token: 0x04003797 RID: 14231
		public static Dictionary<int, BuffSeidJsonData40> DataDict = new Dictionary<int, BuffSeidJsonData40>();

		// Token: 0x04003798 RID: 14232
		public static List<BuffSeidJsonData40> DataList = new List<BuffSeidJsonData40>();

		// Token: 0x04003799 RID: 14233
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData40.OnInitFinish);

		// Token: 0x0400379A RID: 14234
		public int id;

		// Token: 0x0400379B RID: 14235
		public int value1;

		// Token: 0x0400379C RID: 14236
		public int value2;

		// Token: 0x0400379D RID: 14237
		public int value3;
	}
}
