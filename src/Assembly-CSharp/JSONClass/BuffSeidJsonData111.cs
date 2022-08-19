using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000760 RID: 1888
	public class BuffSeidJsonData111 : IJSONClass
	{
		// Token: 0x06003B94 RID: 15252 RVA: 0x0019A1C4 File Offset: 0x001983C4
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[111].list)
			{
				try
				{
					BuffSeidJsonData111 buffSeidJsonData = new BuffSeidJsonData111();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].ToList();
					if (BuffSeidJsonData111.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData111.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData111.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData111.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData111.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData111.OnInitFinishAction != null)
			{
				BuffSeidJsonData111.OnInitFinishAction();
			}
		}

		// Token: 0x06003B95 RID: 15253 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040034B7 RID: 13495
		public static int SEIDID = 111;

		// Token: 0x040034B8 RID: 13496
		public static Dictionary<int, BuffSeidJsonData111> DataDict = new Dictionary<int, BuffSeidJsonData111>();

		// Token: 0x040034B9 RID: 13497
		public static List<BuffSeidJsonData111> DataList = new List<BuffSeidJsonData111>();

		// Token: 0x040034BA RID: 13498
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData111.OnInitFinish);

		// Token: 0x040034BB RID: 13499
		public int id;

		// Token: 0x040034BC RID: 13500
		public List<int> value1 = new List<int>();
	}
}
