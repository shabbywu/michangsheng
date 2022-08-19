using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020007A4 RID: 1956
	public class BuffSeidJsonData20 : IJSONClass
	{
		// Token: 0x06003CA2 RID: 15522 RVA: 0x001A0338 File Offset: 0x0019E538
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[20].list)
			{
				try
				{
					BuffSeidJsonData20 buffSeidJsonData = new BuffSeidJsonData20();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].ToList();
					if (BuffSeidJsonData20.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData20.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData20.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData20.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData20.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData20.OnInitFinishAction != null)
			{
				BuffSeidJsonData20.OnInitFinishAction();
			}
		}

		// Token: 0x06003CA3 RID: 15523 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003682 RID: 13954
		public static int SEIDID = 20;

		// Token: 0x04003683 RID: 13955
		public static Dictionary<int, BuffSeidJsonData20> DataDict = new Dictionary<int, BuffSeidJsonData20>();

		// Token: 0x04003684 RID: 13956
		public static List<BuffSeidJsonData20> DataList = new List<BuffSeidJsonData20>();

		// Token: 0x04003685 RID: 13957
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData20.OnInitFinish);

		// Token: 0x04003686 RID: 13958
		public int id;

		// Token: 0x04003687 RID: 13959
		public List<int> value1 = new List<int>();
	}
}
