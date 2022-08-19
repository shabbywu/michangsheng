using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020007D3 RID: 2003
	public class BuffSeidJsonData49 : IJSONClass
	{
		// Token: 0x06003D5E RID: 15710 RVA: 0x001A4938 File Offset: 0x001A2B38
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[49].list)
			{
				try
				{
					BuffSeidJsonData49 buffSeidJsonData = new BuffSeidJsonData49();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					buffSeidJsonData.value3 = jsonobject["value3"].I;
					if (BuffSeidJsonData49.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData49.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData49.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData49.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData49.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData49.OnInitFinishAction != null)
			{
				BuffSeidJsonData49.OnInitFinishAction();
			}
		}

		// Token: 0x06003D5F RID: 15711 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040037D8 RID: 14296
		public static int SEIDID = 49;

		// Token: 0x040037D9 RID: 14297
		public static Dictionary<int, BuffSeidJsonData49> DataDict = new Dictionary<int, BuffSeidJsonData49>();

		// Token: 0x040037DA RID: 14298
		public static List<BuffSeidJsonData49> DataList = new List<BuffSeidJsonData49>();

		// Token: 0x040037DB RID: 14299
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData49.OnInitFinish);

		// Token: 0x040037DC RID: 14300
		public int id;

		// Token: 0x040037DD RID: 14301
		public int value1;

		// Token: 0x040037DE RID: 14302
		public int value2;

		// Token: 0x040037DF RID: 14303
		public int value3;
	}
}
