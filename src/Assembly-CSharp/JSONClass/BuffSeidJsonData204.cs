using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B41 RID: 2881
	public class BuffSeidJsonData204 : IJSONClass
	{
		// Token: 0x0600486C RID: 18540 RVA: 0x001ED888 File Offset: 0x001EBA88
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[204].list)
			{
				try
				{
					BuffSeidJsonData204 buffSeidJsonData = new BuffSeidJsonData204();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					if (BuffSeidJsonData204.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData204.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData204.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData204.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData204.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData204.OnInitFinishAction != null)
			{
				BuffSeidJsonData204.OnInitFinishAction();
			}
		}

		// Token: 0x0600486D RID: 18541 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004239 RID: 16953
		public static int SEIDID = 204;

		// Token: 0x0400423A RID: 16954
		public static Dictionary<int, BuffSeidJsonData204> DataDict = new Dictionary<int, BuffSeidJsonData204>();

		// Token: 0x0400423B RID: 16955
		public static List<BuffSeidJsonData204> DataList = new List<BuffSeidJsonData204>();

		// Token: 0x0400423C RID: 16956
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData204.OnInitFinish);

		// Token: 0x0400423D RID: 16957
		public int id;

		// Token: 0x0400423E RID: 16958
		public int value1;

		// Token: 0x0400423F RID: 16959
		public int value2;
	}
}
