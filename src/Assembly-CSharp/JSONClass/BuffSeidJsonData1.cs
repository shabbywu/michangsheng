using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000756 RID: 1878
	public class BuffSeidJsonData1 : IJSONClass
	{
		// Token: 0x06003B6C RID: 15212 RVA: 0x00199404 File Offset: 0x00197604
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[1].list)
			{
				try
				{
					BuffSeidJsonData1 buffSeidJsonData = new BuffSeidJsonData1();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData1.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData1.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData1.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData1.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData1.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData1.OnInitFinishAction != null)
			{
				BuffSeidJsonData1.OnInitFinishAction();
			}
		}

		// Token: 0x06003B6D RID: 15213 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003479 RID: 13433
		public static int SEIDID = 1;

		// Token: 0x0400347A RID: 13434
		public static Dictionary<int, BuffSeidJsonData1> DataDict = new Dictionary<int, BuffSeidJsonData1>();

		// Token: 0x0400347B RID: 13435
		public static List<BuffSeidJsonData1> DataList = new List<BuffSeidJsonData1>();

		// Token: 0x0400347C RID: 13436
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData1.OnInitFinish);

		// Token: 0x0400347D RID: 13437
		public int id;

		// Token: 0x0400347E RID: 13438
		public int value1;
	}
}
