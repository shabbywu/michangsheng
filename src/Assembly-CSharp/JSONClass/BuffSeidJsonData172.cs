using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B28 RID: 2856
	public class BuffSeidJsonData172 : IJSONClass
	{
		// Token: 0x06004808 RID: 18440 RVA: 0x001EBA28 File Offset: 0x001E9C28
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[172].list)
			{
				try
				{
					BuffSeidJsonData172 buffSeidJsonData = new BuffSeidJsonData172();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData172.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData172.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData172.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData172.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData172.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData172.OnInitFinishAction != null)
			{
				BuffSeidJsonData172.OnInitFinishAction();
			}
		}

		// Token: 0x06004809 RID: 18441 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004196 RID: 16790
		public static int SEIDID = 172;

		// Token: 0x04004197 RID: 16791
		public static Dictionary<int, BuffSeidJsonData172> DataDict = new Dictionary<int, BuffSeidJsonData172>();

		// Token: 0x04004198 RID: 16792
		public static List<BuffSeidJsonData172> DataList = new List<BuffSeidJsonData172>();

		// Token: 0x04004199 RID: 16793
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData172.OnInitFinish);

		// Token: 0x0400419A RID: 16794
		public int id;

		// Token: 0x0400419B RID: 16795
		public int value1;
	}
}
