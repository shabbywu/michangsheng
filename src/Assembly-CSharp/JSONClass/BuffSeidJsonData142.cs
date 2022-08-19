using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000778 RID: 1912
	public class BuffSeidJsonData142 : IJSONClass
	{
		// Token: 0x06003BF4 RID: 15348 RVA: 0x0019C4AC File Offset: 0x0019A6AC
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[142].list)
			{
				try
				{
					BuffSeidJsonData142 buffSeidJsonData = new BuffSeidJsonData142();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.target = jsonobject["target"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					if (BuffSeidJsonData142.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData142.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData142.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData142.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData142.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData142.OnInitFinishAction != null)
			{
				BuffSeidJsonData142.OnInitFinishAction();
			}
		}

		// Token: 0x06003BF5 RID: 15349 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400355B RID: 13659
		public static int SEIDID = 142;

		// Token: 0x0400355C RID: 13660
		public static Dictionary<int, BuffSeidJsonData142> DataDict = new Dictionary<int, BuffSeidJsonData142>();

		// Token: 0x0400355D RID: 13661
		public static List<BuffSeidJsonData142> DataList = new List<BuffSeidJsonData142>();

		// Token: 0x0400355E RID: 13662
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData142.OnInitFinish);

		// Token: 0x0400355F RID: 13663
		public int id;

		// Token: 0x04003560 RID: 13664
		public int target;

		// Token: 0x04003561 RID: 13665
		public int value1;

		// Token: 0x04003562 RID: 13666
		public int value2;
	}
}
