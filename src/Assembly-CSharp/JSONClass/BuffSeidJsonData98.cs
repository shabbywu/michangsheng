using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000801 RID: 2049
	public class BuffSeidJsonData98 : IJSONClass
	{
		// Token: 0x06003E16 RID: 15894 RVA: 0x001A8A3C File Offset: 0x001A6C3C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[98].list)
			{
				try
				{
					BuffSeidJsonData98 buffSeidJsonData = new BuffSeidJsonData98();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					if (BuffSeidJsonData98.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData98.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData98.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData98.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData98.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData98.OnInitFinishAction != null)
			{
				BuffSeidJsonData98.OnInitFinishAction();
			}
		}

		// Token: 0x06003E17 RID: 15895 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400390A RID: 14602
		public static int SEIDID = 98;

		// Token: 0x0400390B RID: 14603
		public static Dictionary<int, BuffSeidJsonData98> DataDict = new Dictionary<int, BuffSeidJsonData98>();

		// Token: 0x0400390C RID: 14604
		public static List<BuffSeidJsonData98> DataList = new List<BuffSeidJsonData98>();

		// Token: 0x0400390D RID: 14605
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData98.OnInitFinish);

		// Token: 0x0400390E RID: 14606
		public int id;

		// Token: 0x0400390F RID: 14607
		public int value1;

		// Token: 0x04003910 RID: 14608
		public int value2;
	}
}
