using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020007CA RID: 1994
	public class BuffSeidJsonData39 : IJSONClass
	{
		// Token: 0x06003D3A RID: 15674 RVA: 0x001A3AD0 File Offset: 0x001A1CD0
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[39].list)
			{
				try
				{
					BuffSeidJsonData39 buffSeidJsonData = new BuffSeidJsonData39();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					buffSeidJsonData.value3 = jsonobject["value3"].I;
					buffSeidJsonData.value4 = jsonobject["value4"].I;
					if (BuffSeidJsonData39.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData39.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData39.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData39.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData39.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData39.OnInitFinishAction != null)
			{
				BuffSeidJsonData39.OnInitFinishAction();
			}
		}

		// Token: 0x06003D3B RID: 15675 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400378D RID: 14221
		public static int SEIDID = 39;

		// Token: 0x0400378E RID: 14222
		public static Dictionary<int, BuffSeidJsonData39> DataDict = new Dictionary<int, BuffSeidJsonData39>();

		// Token: 0x0400378F RID: 14223
		public static List<BuffSeidJsonData39> DataList = new List<BuffSeidJsonData39>();

		// Token: 0x04003790 RID: 14224
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData39.OnInitFinish);

		// Token: 0x04003791 RID: 14225
		public int id;

		// Token: 0x04003792 RID: 14226
		public int value1;

		// Token: 0x04003793 RID: 14227
		public int value2;

		// Token: 0x04003794 RID: 14228
		public int value3;

		// Token: 0x04003795 RID: 14229
		public int value4;
	}
}
