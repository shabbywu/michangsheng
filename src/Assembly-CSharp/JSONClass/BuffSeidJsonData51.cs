using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020007D6 RID: 2006
	public class BuffSeidJsonData51 : IJSONClass
	{
		// Token: 0x06003D6A RID: 15722 RVA: 0x001A4DB0 File Offset: 0x001A2FB0
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[51].list)
			{
				try
				{
					BuffSeidJsonData51 buffSeidJsonData = new BuffSeidJsonData51();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					if (BuffSeidJsonData51.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData51.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData51.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData51.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData51.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData51.OnInitFinishAction != null)
			{
				BuffSeidJsonData51.OnInitFinishAction();
			}
		}

		// Token: 0x06003D6B RID: 15723 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040037EE RID: 14318
		public static int SEIDID = 51;

		// Token: 0x040037EF RID: 14319
		public static Dictionary<int, BuffSeidJsonData51> DataDict = new Dictionary<int, BuffSeidJsonData51>();

		// Token: 0x040037F0 RID: 14320
		public static List<BuffSeidJsonData51> DataList = new List<BuffSeidJsonData51>();

		// Token: 0x040037F1 RID: 14321
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData51.OnInitFinish);

		// Token: 0x040037F2 RID: 14322
		public int id;

		// Token: 0x040037F3 RID: 14323
		public int value1;

		// Token: 0x040037F4 RID: 14324
		public int value2;
	}
}
