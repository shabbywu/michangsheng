using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000762 RID: 1890
	public class BuffSeidJsonData113 : IJSONClass
	{
		// Token: 0x06003B9C RID: 15260 RVA: 0x0019A488 File Offset: 0x00198688
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[113].list)
			{
				try
				{
					BuffSeidJsonData113 buffSeidJsonData = new BuffSeidJsonData113();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].ToList();
					if (BuffSeidJsonData113.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData113.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData113.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData113.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData113.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData113.OnInitFinishAction != null)
			{
				BuffSeidJsonData113.OnInitFinishAction();
			}
		}

		// Token: 0x06003B9D RID: 15261 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040034C3 RID: 13507
		public static int SEIDID = 113;

		// Token: 0x040034C4 RID: 13508
		public static Dictionary<int, BuffSeidJsonData113> DataDict = new Dictionary<int, BuffSeidJsonData113>();

		// Token: 0x040034C5 RID: 13509
		public static List<BuffSeidJsonData113> DataList = new List<BuffSeidJsonData113>();

		// Token: 0x040034C6 RID: 13510
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData113.OnInitFinish);

		// Token: 0x040034C7 RID: 13511
		public int id;

		// Token: 0x040034C8 RID: 13512
		public List<int> value1 = new List<int>();
	}
}
