using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200076B RID: 1899
	public class BuffSeidJsonData127 : IJSONClass
	{
		// Token: 0x06003BC0 RID: 15296 RVA: 0x0019B108 File Offset: 0x00199308
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[127].list)
			{
				try
				{
					BuffSeidJsonData127 buffSeidJsonData = new BuffSeidJsonData127();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].ToList();
					buffSeidJsonData.value2 = jsonobject["value2"].ToList();
					if (BuffSeidJsonData127.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData127.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData127.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData127.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData127.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData127.OnInitFinishAction != null)
			{
				BuffSeidJsonData127.OnInitFinishAction();
			}
		}

		// Token: 0x06003BC1 RID: 15297 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040034FD RID: 13565
		public static int SEIDID = 127;

		// Token: 0x040034FE RID: 13566
		public static Dictionary<int, BuffSeidJsonData127> DataDict = new Dictionary<int, BuffSeidJsonData127>();

		// Token: 0x040034FF RID: 13567
		public static List<BuffSeidJsonData127> DataList = new List<BuffSeidJsonData127>();

		// Token: 0x04003500 RID: 13568
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData127.OnInitFinish);

		// Token: 0x04003501 RID: 13569
		public int id;

		// Token: 0x04003502 RID: 13570
		public List<int> value1 = new List<int>();

		// Token: 0x04003503 RID: 13571
		public List<int> value2 = new List<int>();
	}
}
