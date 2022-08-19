using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000770 RID: 1904
	public class BuffSeidJsonData134 : IJSONClass
	{
		// Token: 0x06003BD4 RID: 15316 RVA: 0x0019B8F0 File Offset: 0x00199AF0
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[134].list)
			{
				try
				{
					BuffSeidJsonData134 buffSeidJsonData = new BuffSeidJsonData134();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.target = jsonobject["target"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					if (BuffSeidJsonData134.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData134.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData134.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData134.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData134.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData134.OnInitFinishAction != null)
			{
				BuffSeidJsonData134.OnInitFinishAction();
			}
		}

		// Token: 0x06003BD5 RID: 15317 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003523 RID: 13603
		public static int SEIDID = 134;

		// Token: 0x04003524 RID: 13604
		public static Dictionary<int, BuffSeidJsonData134> DataDict = new Dictionary<int, BuffSeidJsonData134>();

		// Token: 0x04003525 RID: 13605
		public static List<BuffSeidJsonData134> DataList = new List<BuffSeidJsonData134>();

		// Token: 0x04003526 RID: 13606
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData134.OnInitFinish);

		// Token: 0x04003527 RID: 13607
		public int id;

		// Token: 0x04003528 RID: 13608
		public int target;

		// Token: 0x04003529 RID: 13609
		public int value1;

		// Token: 0x0400352A RID: 13610
		public int value2;
	}
}
