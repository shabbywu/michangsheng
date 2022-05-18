using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B16 RID: 2838
	public class BuffSeidJsonData15 : IJSONClass
	{
		// Token: 0x060047C2 RID: 18370 RVA: 0x001EA4A8 File Offset: 0x001E86A8
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

		// Token: 0x060047C3 RID: 18371 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400411D RID: 16669
		public static int SEIDID = 15;

		// Token: 0x0400411E RID: 16670
		public static Dictionary<int, BuffSeidJsonData15> DataDict = new Dictionary<int, BuffSeidJsonData15>();

		// Token: 0x0400411F RID: 16671
		public static List<BuffSeidJsonData15> DataList = new List<BuffSeidJsonData15>();

		// Token: 0x04004120 RID: 16672
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData15.OnInitFinish);

		// Token: 0x04004121 RID: 16673
		public int id;

		// Token: 0x04004122 RID: 16674
		public int value1;

		// Token: 0x04004123 RID: 16675
		public int value2;
	}
}
