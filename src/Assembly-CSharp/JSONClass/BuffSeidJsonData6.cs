using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B75 RID: 2933
	public class BuffSeidJsonData6 : IJSONClass
	{
		// Token: 0x0600493C RID: 18748 RVA: 0x001F1BD0 File Offset: 0x001EFDD0
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[6].list)
			{
				try
				{
					BuffSeidJsonData6 buffSeidJsonData = new BuffSeidJsonData6();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].ToList();
					if (BuffSeidJsonData6.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData6.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData6.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData6.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData6.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData6.OnInitFinishAction != null)
			{
				BuffSeidJsonData6.OnInitFinishAction();
			}
		}

		// Token: 0x0600493D RID: 18749 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040043B8 RID: 17336
		public static int SEIDID = 6;

		// Token: 0x040043B9 RID: 17337
		public static Dictionary<int, BuffSeidJsonData6> DataDict = new Dictionary<int, BuffSeidJsonData6>();

		// Token: 0x040043BA RID: 17338
		public static List<BuffSeidJsonData6> DataList = new List<BuffSeidJsonData6>();

		// Token: 0x040043BB RID: 17339
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData6.OnInitFinish);

		// Token: 0x040043BC RID: 17340
		public int id;

		// Token: 0x040043BD RID: 17341
		public List<int> value1 = new List<int>();
	}
}
