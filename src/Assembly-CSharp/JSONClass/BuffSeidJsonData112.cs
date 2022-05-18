using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000AF9 RID: 2809
	public class BuffSeidJsonData112 : IJSONClass
	{
		// Token: 0x0600474E RID: 18254 RVA: 0x001E80C0 File Offset: 0x001E62C0
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[112].list)
			{
				try
				{
					BuffSeidJsonData112 buffSeidJsonData = new BuffSeidJsonData112();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData112.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData112.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData112.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData112.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData112.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData112.OnInitFinishAction != null)
			{
				BuffSeidJsonData112.OnInitFinishAction();
			}
		}

		// Token: 0x0600474F RID: 18255 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004056 RID: 16470
		public static int SEIDID = 112;

		// Token: 0x04004057 RID: 16471
		public static Dictionary<int, BuffSeidJsonData112> DataDict = new Dictionary<int, BuffSeidJsonData112>();

		// Token: 0x04004058 RID: 16472
		public static List<BuffSeidJsonData112> DataList = new List<BuffSeidJsonData112>();

		// Token: 0x04004059 RID: 16473
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData112.OnInitFinish);

		// Token: 0x0400405A RID: 16474
		public int id;

		// Token: 0x0400405B RID: 16475
		public int value1;
	}
}
