using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000766 RID: 1894
	public class BuffSeidJsonData118 : IJSONClass
	{
		// Token: 0x06003BAC RID: 15276 RVA: 0x0019AA10 File Offset: 0x00198C10
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[118].list)
			{
				try
				{
					BuffSeidJsonData118 buffSeidJsonData = new BuffSeidJsonData118();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData118.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData118.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData118.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData118.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData118.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData118.OnInitFinishAction != null)
			{
				BuffSeidJsonData118.OnInitFinishAction();
			}
		}

		// Token: 0x06003BAD RID: 15277 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040034DC RID: 13532
		public static int SEIDID = 118;

		// Token: 0x040034DD RID: 13533
		public static Dictionary<int, BuffSeidJsonData118> DataDict = new Dictionary<int, BuffSeidJsonData118>();

		// Token: 0x040034DE RID: 13534
		public static List<BuffSeidJsonData118> DataList = new List<BuffSeidJsonData118>();

		// Token: 0x040034DF RID: 13535
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData118.OnInitFinish);

		// Token: 0x040034E0 RID: 13536
		public int id;

		// Token: 0x040034E1 RID: 13537
		public int value1;
	}
}
