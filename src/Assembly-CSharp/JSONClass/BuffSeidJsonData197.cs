using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020007A1 RID: 1953
	public class BuffSeidJsonData197 : IJSONClass
	{
		// Token: 0x06003C96 RID: 15510 RVA: 0x0019FEF8 File Offset: 0x0019E0F8
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[197].list)
			{
				try
				{
					BuffSeidJsonData197 buffSeidJsonData = new BuffSeidJsonData197();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.target = jsonobject["target"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData197.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData197.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData197.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData197.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData197.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData197.OnInitFinishAction != null)
			{
				BuffSeidJsonData197.OnInitFinishAction();
			}
		}

		// Token: 0x06003C97 RID: 15511 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400366E RID: 13934
		public static int SEIDID = 197;

		// Token: 0x0400366F RID: 13935
		public static Dictionary<int, BuffSeidJsonData197> DataDict = new Dictionary<int, BuffSeidJsonData197>();

		// Token: 0x04003670 RID: 13936
		public static List<BuffSeidJsonData197> DataList = new List<BuffSeidJsonData197>();

		// Token: 0x04003671 RID: 13937
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData197.OnInitFinish);

		// Token: 0x04003672 RID: 13938
		public int id;

		// Token: 0x04003673 RID: 13939
		public int target;

		// Token: 0x04003674 RID: 13940
		public int value1;
	}
}
