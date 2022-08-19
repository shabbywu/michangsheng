using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200079F RID: 1951
	public class BuffSeidJsonData195 : IJSONClass
	{
		// Token: 0x06003C8E RID: 15502 RVA: 0x0019FBCC File Offset: 0x0019DDCC
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[195].list)
			{
				try
				{
					BuffSeidJsonData195 buffSeidJsonData = new BuffSeidJsonData195();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					buffSeidJsonData.value3 = jsonobject["value3"].I;
					buffSeidJsonData.value4 = jsonobject["value4"].I;
					if (BuffSeidJsonData195.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData195.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData195.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData195.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData195.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData195.OnInitFinishAction != null)
			{
				BuffSeidJsonData195.OnInitFinishAction();
			}
		}

		// Token: 0x06003C8F RID: 15503 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400365E RID: 13918
		public static int SEIDID = 195;

		// Token: 0x0400365F RID: 13919
		public static Dictionary<int, BuffSeidJsonData195> DataDict = new Dictionary<int, BuffSeidJsonData195>();

		// Token: 0x04003660 RID: 13920
		public static List<BuffSeidJsonData195> DataList = new List<BuffSeidJsonData195>();

		// Token: 0x04003661 RID: 13921
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData195.OnInitFinish);

		// Token: 0x04003662 RID: 13922
		public int id;

		// Token: 0x04003663 RID: 13923
		public int value1;

		// Token: 0x04003664 RID: 13924
		public int value2;

		// Token: 0x04003665 RID: 13925
		public int value3;

		// Token: 0x04003666 RID: 13926
		public int value4;
	}
}
