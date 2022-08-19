using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020007A5 RID: 1957
	public class BuffSeidJsonData200 : IJSONClass
	{
		// Token: 0x06003CA6 RID: 15526 RVA: 0x001A04A4 File Offset: 0x0019E6A4
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[200].list)
			{
				try
				{
					BuffSeidJsonData200 buffSeidJsonData = new BuffSeidJsonData200();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData200.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData200.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData200.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData200.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData200.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData200.OnInitFinishAction != null)
			{
				BuffSeidJsonData200.OnInitFinishAction();
			}
		}

		// Token: 0x06003CA7 RID: 15527 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003688 RID: 13960
		public static int SEIDID = 200;

		// Token: 0x04003689 RID: 13961
		public static Dictionary<int, BuffSeidJsonData200> DataDict = new Dictionary<int, BuffSeidJsonData200>();

		// Token: 0x0400368A RID: 13962
		public static List<BuffSeidJsonData200> DataList = new List<BuffSeidJsonData200>();

		// Token: 0x0400368B RID: 13963
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData200.OnInitFinish);

		// Token: 0x0400368C RID: 13964
		public int id;

		// Token: 0x0400368D RID: 13965
		public int value1;
	}
}
