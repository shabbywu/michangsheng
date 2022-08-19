using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200078D RID: 1933
	public class BuffSeidJsonData17 : IJSONClass
	{
		// Token: 0x06003C46 RID: 15430 RVA: 0x0019E218 File Offset: 0x0019C418
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[17].list)
			{
				try
				{
					BuffSeidJsonData17 buffSeidJsonData = new BuffSeidJsonData17();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					if (BuffSeidJsonData17.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData17.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData17.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData17.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData17.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData17.OnInitFinishAction != null)
			{
				BuffSeidJsonData17.OnInitFinishAction();
			}
		}

		// Token: 0x06003C47 RID: 15431 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040035E8 RID: 13800
		public static int SEIDID = 17;

		// Token: 0x040035E9 RID: 13801
		public static Dictionary<int, BuffSeidJsonData17> DataDict = new Dictionary<int, BuffSeidJsonData17>();

		// Token: 0x040035EA RID: 13802
		public static List<BuffSeidJsonData17> DataList = new List<BuffSeidJsonData17>();

		// Token: 0x040035EB RID: 13803
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData17.OnInitFinish);

		// Token: 0x040035EC RID: 13804
		public int id;

		// Token: 0x040035ED RID: 13805
		public int value1;

		// Token: 0x040035EE RID: 13806
		public int value2;
	}
}
