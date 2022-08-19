using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000777 RID: 1911
	public class BuffSeidJsonData141 : IJSONClass
	{
		// Token: 0x06003BF0 RID: 15344 RVA: 0x0019C34C File Offset: 0x0019A54C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[141].list)
			{
				try
				{
					BuffSeidJsonData141 buffSeidJsonData = new BuffSeidJsonData141();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData141.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData141.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData141.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData141.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData141.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData141.OnInitFinishAction != null)
			{
				BuffSeidJsonData141.OnInitFinishAction();
			}
		}

		// Token: 0x06003BF1 RID: 15345 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003555 RID: 13653
		public static int SEIDID = 141;

		// Token: 0x04003556 RID: 13654
		public static Dictionary<int, BuffSeidJsonData141> DataDict = new Dictionary<int, BuffSeidJsonData141>();

		// Token: 0x04003557 RID: 13655
		public static List<BuffSeidJsonData141> DataList = new List<BuffSeidJsonData141>();

		// Token: 0x04003558 RID: 13656
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData141.OnInitFinish);

		// Token: 0x04003559 RID: 13657
		public int id;

		// Token: 0x0400355A RID: 13658
		public int value1;
	}
}
