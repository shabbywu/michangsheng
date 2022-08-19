using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020007B5 RID: 1973
	public class BuffSeidJsonData219 : IJSONClass
	{
		// Token: 0x06003CE6 RID: 15590 RVA: 0x001A1B5C File Offset: 0x0019FD5C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[219].list)
			{
				try
				{
					BuffSeidJsonData219 buffSeidJsonData = new BuffSeidJsonData219();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					buffSeidJsonData.value3 = jsonobject["value3"].I;
					buffSeidJsonData.value4 = jsonobject["value4"].ToList();
					if (BuffSeidJsonData219.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData219.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData219.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData219.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData219.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData219.OnInitFinishAction != null)
			{
				BuffSeidJsonData219.OnInitFinishAction();
			}
		}

		// Token: 0x06003CE7 RID: 15591 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040036F0 RID: 14064
		public static int SEIDID = 219;

		// Token: 0x040036F1 RID: 14065
		public static Dictionary<int, BuffSeidJsonData219> DataDict = new Dictionary<int, BuffSeidJsonData219>();

		// Token: 0x040036F2 RID: 14066
		public static List<BuffSeidJsonData219> DataList = new List<BuffSeidJsonData219>();

		// Token: 0x040036F3 RID: 14067
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData219.OnInitFinish);

		// Token: 0x040036F4 RID: 14068
		public int id;

		// Token: 0x040036F5 RID: 14069
		public int value1;

		// Token: 0x040036F6 RID: 14070
		public int value2;

		// Token: 0x040036F7 RID: 14071
		public int value3;

		// Token: 0x040036F8 RID: 14072
		public List<int> value4 = new List<int>();
	}
}
