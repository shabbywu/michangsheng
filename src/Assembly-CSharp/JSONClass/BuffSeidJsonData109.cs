using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200075D RID: 1885
	public class BuffSeidJsonData109 : IJSONClass
	{
		// Token: 0x06003B88 RID: 15240 RVA: 0x00199D94 File Offset: 0x00197F94
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[109].list)
			{
				try
				{
					BuffSeidJsonData109 buffSeidJsonData = new BuffSeidJsonData109();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].ToList();
					if (BuffSeidJsonData109.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData109.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData109.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData109.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData109.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData109.OnInitFinishAction != null)
			{
				BuffSeidJsonData109.OnInitFinishAction();
			}
		}

		// Token: 0x06003B89 RID: 15241 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040034A5 RID: 13477
		public static int SEIDID = 109;

		// Token: 0x040034A6 RID: 13478
		public static Dictionary<int, BuffSeidJsonData109> DataDict = new Dictionary<int, BuffSeidJsonData109>();

		// Token: 0x040034A7 RID: 13479
		public static List<BuffSeidJsonData109> DataList = new List<BuffSeidJsonData109>();

		// Token: 0x040034A8 RID: 13480
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData109.OnInitFinish);

		// Token: 0x040034A9 RID: 13481
		public int id;

		// Token: 0x040034AA RID: 13482
		public List<int> value1 = new List<int>();
	}
}
