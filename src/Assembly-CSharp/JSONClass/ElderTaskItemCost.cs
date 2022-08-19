using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000836 RID: 2102
	public class ElderTaskItemCost : IJSONClass
	{
		// Token: 0x06003EEA RID: 16106 RVA: 0x001ADFE8 File Offset: 0x001AC1E8
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.ElderTaskItemCost.list)
			{
				try
				{
					ElderTaskItemCost elderTaskItemCost = new ElderTaskItemCost();
					elderTaskItemCost.quality = jsonobject["quality"].I;
					elderTaskItemCost.time = jsonobject["time"].I;
					if (ElderTaskItemCost.DataDict.ContainsKey(elderTaskItemCost.quality))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典ElderTaskItemCost.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", elderTaskItemCost.quality));
					}
					else
					{
						ElderTaskItemCost.DataDict.Add(elderTaskItemCost.quality, elderTaskItemCost);
						ElderTaskItemCost.DataList.Add(elderTaskItemCost);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典ElderTaskItemCost.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (ElderTaskItemCost.OnInitFinishAction != null)
			{
				ElderTaskItemCost.OnInitFinishAction();
			}
		}

		// Token: 0x06003EEB RID: 16107 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003AB8 RID: 15032
		public static Dictionary<int, ElderTaskItemCost> DataDict = new Dictionary<int, ElderTaskItemCost>();

		// Token: 0x04003AB9 RID: 15033
		public static List<ElderTaskItemCost> DataList = new List<ElderTaskItemCost>();

		// Token: 0x04003ABA RID: 15034
		public static Action OnInitFinishAction = new Action(ElderTaskItemCost.OnInitFinish);

		// Token: 0x04003ABB RID: 15035
		public int quality;

		// Token: 0x04003ABC RID: 15036
		public int time;
	}
}
