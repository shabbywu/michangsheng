using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020007EE RID: 2030
	public class BuffSeidJsonData75 : IJSONClass
	{
		// Token: 0x06003DCA RID: 15818 RVA: 0x001A6FD4 File Offset: 0x001A51D4
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[75].list)
			{
				try
				{
					BuffSeidJsonData75 buffSeidJsonData = new BuffSeidJsonData75();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].ToList();
					if (BuffSeidJsonData75.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData75.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData75.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData75.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData75.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData75.OnInitFinishAction != null)
			{
				BuffSeidJsonData75.OnInitFinishAction();
			}
		}

		// Token: 0x06003DCB RID: 15819 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003890 RID: 14480
		public static int SEIDID = 75;

		// Token: 0x04003891 RID: 14481
		public static Dictionary<int, BuffSeidJsonData75> DataDict = new Dictionary<int, BuffSeidJsonData75>();

		// Token: 0x04003892 RID: 14482
		public static List<BuffSeidJsonData75> DataList = new List<BuffSeidJsonData75>();

		// Token: 0x04003893 RID: 14483
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData75.OnInitFinish);

		// Token: 0x04003894 RID: 14484
		public int id;

		// Token: 0x04003895 RID: 14485
		public List<int> value1 = new List<int>();
	}
}
