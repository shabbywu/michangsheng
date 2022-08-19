using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000774 RID: 1908
	public class BuffSeidJsonData138 : IJSONClass
	{
		// Token: 0x06003BE4 RID: 15332 RVA: 0x0019BED4 File Offset: 0x0019A0D4
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[138].list)
			{
				try
				{
					BuffSeidJsonData138 buffSeidJsonData = new BuffSeidJsonData138();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.target = jsonobject["target"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					if (BuffSeidJsonData138.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData138.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData138.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData138.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData138.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData138.OnInitFinishAction != null)
			{
				BuffSeidJsonData138.OnInitFinishAction();
			}
		}

		// Token: 0x06003BE5 RID: 15333 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400353F RID: 13631
		public static int SEIDID = 138;

		// Token: 0x04003540 RID: 13632
		public static Dictionary<int, BuffSeidJsonData138> DataDict = new Dictionary<int, BuffSeidJsonData138>();

		// Token: 0x04003541 RID: 13633
		public static List<BuffSeidJsonData138> DataList = new List<BuffSeidJsonData138>();

		// Token: 0x04003542 RID: 13634
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData138.OnInitFinish);

		// Token: 0x04003543 RID: 13635
		public int id;

		// Token: 0x04003544 RID: 13636
		public int target;

		// Token: 0x04003545 RID: 13637
		public int value1;

		// Token: 0x04003546 RID: 13638
		public int value2;
	}
}
