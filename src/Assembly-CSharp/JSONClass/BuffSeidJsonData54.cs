using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020007D9 RID: 2009
	public class BuffSeidJsonData54 : IJSONClass
	{
		// Token: 0x06003D76 RID: 15734 RVA: 0x001A5250 File Offset: 0x001A3450
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[54].list)
			{
				try
				{
					BuffSeidJsonData54 buffSeidJsonData = new BuffSeidJsonData54();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					buffSeidJsonData.value3 = jsonobject["value3"].I;
					buffSeidJsonData.value4 = jsonobject["value4"].I;
					if (BuffSeidJsonData54.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData54.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData54.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData54.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData54.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData54.OnInitFinishAction != null)
			{
				BuffSeidJsonData54.OnInitFinishAction();
			}
		}

		// Token: 0x06003D77 RID: 15735 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003806 RID: 14342
		public static int SEIDID = 54;

		// Token: 0x04003807 RID: 14343
		public static Dictionary<int, BuffSeidJsonData54> DataDict = new Dictionary<int, BuffSeidJsonData54>();

		// Token: 0x04003808 RID: 14344
		public static List<BuffSeidJsonData54> DataList = new List<BuffSeidJsonData54>();

		// Token: 0x04003809 RID: 14345
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData54.OnInitFinish);

		// Token: 0x0400380A RID: 14346
		public int id;

		// Token: 0x0400380B RID: 14347
		public int value1;

		// Token: 0x0400380C RID: 14348
		public int value2;

		// Token: 0x0400380D RID: 14349
		public int value3;

		// Token: 0x0400380E RID: 14350
		public int value4;
	}
}
