using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020007FB RID: 2043
	public class BuffSeidJsonData9 : IJSONClass
	{
		// Token: 0x06003DFE RID: 15870 RVA: 0x001A81F0 File Offset: 0x001A63F0
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[9].list)
			{
				try
				{
					BuffSeidJsonData9 buffSeidJsonData = new BuffSeidJsonData9();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].ToList();
					if (BuffSeidJsonData9.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData9.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData9.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData9.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData9.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData9.OnInitFinishAction != null)
			{
				BuffSeidJsonData9.OnInitFinishAction();
			}
		}

		// Token: 0x06003DFF RID: 15871 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040038E4 RID: 14564
		public static int SEIDID = 9;

		// Token: 0x040038E5 RID: 14565
		public static Dictionary<int, BuffSeidJsonData9> DataDict = new Dictionary<int, BuffSeidJsonData9>();

		// Token: 0x040038E6 RID: 14566
		public static List<BuffSeidJsonData9> DataList = new List<BuffSeidJsonData9>();

		// Token: 0x040038E7 RID: 14567
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData9.OnInitFinish);

		// Token: 0x040038E8 RID: 14568
		public int id;

		// Token: 0x040038E9 RID: 14569
		public int value2;

		// Token: 0x040038EA RID: 14570
		public List<int> value1 = new List<int>();
	}
}
