using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200075F RID: 1887
	public class BuffSeidJsonData110 : IJSONClass
	{
		// Token: 0x06003B90 RID: 15248 RVA: 0x0019A058 File Offset: 0x00198258
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[110].list)
			{
				try
				{
					BuffSeidJsonData110 buffSeidJsonData = new BuffSeidJsonData110();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].ToList();
					if (BuffSeidJsonData110.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData110.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData110.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData110.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData110.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData110.OnInitFinishAction != null)
			{
				BuffSeidJsonData110.OnInitFinishAction();
			}
		}

		// Token: 0x06003B91 RID: 15249 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040034B1 RID: 13489
		public static int SEIDID = 110;

		// Token: 0x040034B2 RID: 13490
		public static Dictionary<int, BuffSeidJsonData110> DataDict = new Dictionary<int, BuffSeidJsonData110>();

		// Token: 0x040034B3 RID: 13491
		public static List<BuffSeidJsonData110> DataList = new List<BuffSeidJsonData110>();

		// Token: 0x040034B4 RID: 13492
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData110.OnInitFinish);

		// Token: 0x040034B5 RID: 13493
		public int id;

		// Token: 0x040034B6 RID: 13494
		public List<int> value1 = new List<int>();
	}
}
