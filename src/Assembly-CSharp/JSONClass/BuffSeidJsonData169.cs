using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200078C RID: 1932
	public class BuffSeidJsonData169 : IJSONClass
	{
		// Token: 0x06003C42 RID: 15426 RVA: 0x0019E0B8 File Offset: 0x0019C2B8
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[169].list)
			{
				try
				{
					BuffSeidJsonData169 buffSeidJsonData = new BuffSeidJsonData169();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData169.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData169.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData169.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData169.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData169.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData169.OnInitFinishAction != null)
			{
				BuffSeidJsonData169.OnInitFinishAction();
			}
		}

		// Token: 0x06003C43 RID: 15427 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040035E2 RID: 13794
		public static int SEIDID = 169;

		// Token: 0x040035E3 RID: 13795
		public static Dictionary<int, BuffSeidJsonData169> DataDict = new Dictionary<int, BuffSeidJsonData169>();

		// Token: 0x040035E4 RID: 13796
		public static List<BuffSeidJsonData169> DataList = new List<BuffSeidJsonData169>();

		// Token: 0x040035E5 RID: 13797
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData169.OnInitFinish);

		// Token: 0x040035E6 RID: 13798
		public int id;

		// Token: 0x040035E7 RID: 13799
		public int value1;
	}
}
