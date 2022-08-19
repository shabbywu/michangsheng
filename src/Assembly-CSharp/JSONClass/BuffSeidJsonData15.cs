using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200077E RID: 1918
	public class BuffSeidJsonData15 : IJSONClass
	{
		// Token: 0x06003C0C RID: 15372 RVA: 0x0019CD68 File Offset: 0x0019AF68
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[15].list)
			{
				try
				{
					BuffSeidJsonData15 buffSeidJsonData = new BuffSeidJsonData15();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					if (BuffSeidJsonData15.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData15.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData15.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData15.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData15.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData15.OnInitFinishAction != null)
			{
				BuffSeidJsonData15.OnInitFinishAction();
			}
		}

		// Token: 0x06003C0D RID: 15373 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003584 RID: 13700
		public static int SEIDID = 15;

		// Token: 0x04003585 RID: 13701
		public static Dictionary<int, BuffSeidJsonData15> DataDict = new Dictionary<int, BuffSeidJsonData15>();

		// Token: 0x04003586 RID: 13702
		public static List<BuffSeidJsonData15> DataList = new List<BuffSeidJsonData15>();

		// Token: 0x04003587 RID: 13703
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData15.OnInitFinish);

		// Token: 0x04003588 RID: 13704
		public int id;

		// Token: 0x04003589 RID: 13705
		public int value1;

		// Token: 0x0400358A RID: 13706
		public int value2;
	}
}
