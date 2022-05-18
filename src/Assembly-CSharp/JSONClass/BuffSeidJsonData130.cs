using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B05 RID: 2821
	public class BuffSeidJsonData130 : IJSONClass
	{
		// Token: 0x0600477E RID: 18302 RVA: 0x001E8F20 File Offset: 0x001E7120
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[130].list)
			{
				try
				{
					BuffSeidJsonData130 buffSeidJsonData = new BuffSeidJsonData130();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].ToList();
					buffSeidJsonData.value3 = jsonobject["value3"].ToList();
					if (BuffSeidJsonData130.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData130.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData130.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData130.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData130.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData130.OnInitFinishAction != null)
			{
				BuffSeidJsonData130.OnInitFinishAction();
			}
		}

		// Token: 0x0600477F RID: 18303 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040040A4 RID: 16548
		public static int SEIDID = 130;

		// Token: 0x040040A5 RID: 16549
		public static Dictionary<int, BuffSeidJsonData130> DataDict = new Dictionary<int, BuffSeidJsonData130>();

		// Token: 0x040040A6 RID: 16550
		public static List<BuffSeidJsonData130> DataList = new List<BuffSeidJsonData130>();

		// Token: 0x040040A7 RID: 16551
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData130.OnInitFinish);

		// Token: 0x040040A8 RID: 16552
		public int id;

		// Token: 0x040040A9 RID: 16553
		public int value1;

		// Token: 0x040040AA RID: 16554
		public List<int> value2 = new List<int>();

		// Token: 0x040040AB RID: 16555
		public List<int> value3 = new List<int>();
	}
}
