using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000799 RID: 1945
	public class BuffSeidJsonData186 : IJSONClass
	{
		// Token: 0x06003C76 RID: 15478 RVA: 0x0019F334 File Offset: 0x0019D534
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[186].list)
			{
				try
				{
					BuffSeidJsonData186 buffSeidJsonData = new BuffSeidJsonData186();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					if (BuffSeidJsonData186.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData186.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData186.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData186.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData186.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData186.OnInitFinishAction != null)
			{
				BuffSeidJsonData186.OnInitFinishAction();
			}
		}

		// Token: 0x06003C77 RID: 15479 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003637 RID: 13879
		public static int SEIDID = 186;

		// Token: 0x04003638 RID: 13880
		public static Dictionary<int, BuffSeidJsonData186> DataDict = new Dictionary<int, BuffSeidJsonData186>();

		// Token: 0x04003639 RID: 13881
		public static List<BuffSeidJsonData186> DataList = new List<BuffSeidJsonData186>();

		// Token: 0x0400363A RID: 13882
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData186.OnInitFinish);

		// Token: 0x0400363B RID: 13883
		public int id;

		// Token: 0x0400363C RID: 13884
		public int value1;

		// Token: 0x0400363D RID: 13885
		public int value2;
	}
}
