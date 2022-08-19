using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020007B4 RID: 1972
	public class BuffSeidJsonData216 : IJSONClass
	{
		// Token: 0x06003CE2 RID: 15586 RVA: 0x001A19FC File Offset: 0x0019FBFC
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[216].list)
			{
				try
				{
					BuffSeidJsonData216 buffSeidJsonData = new BuffSeidJsonData216();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData216.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData216.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData216.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData216.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData216.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData216.OnInitFinishAction != null)
			{
				BuffSeidJsonData216.OnInitFinishAction();
			}
		}

		// Token: 0x06003CE3 RID: 15587 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040036EA RID: 14058
		public static int SEIDID = 216;

		// Token: 0x040036EB RID: 14059
		public static Dictionary<int, BuffSeidJsonData216> DataDict = new Dictionary<int, BuffSeidJsonData216>();

		// Token: 0x040036EC RID: 14060
		public static List<BuffSeidJsonData216> DataList = new List<BuffSeidJsonData216>();

		// Token: 0x040036ED RID: 14061
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData216.OnInitFinish);

		// Token: 0x040036EE RID: 14062
		public int id;

		// Token: 0x040036EF RID: 14063
		public int value1;
	}
}
