using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200078F RID: 1935
	public class BuffSeidJsonData171 : IJSONClass
	{
		// Token: 0x06003C4E RID: 15438 RVA: 0x0019E4E4 File Offset: 0x0019C6E4
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[171].list)
			{
				try
				{
					BuffSeidJsonData171 buffSeidJsonData = new BuffSeidJsonData171();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					buffSeidJsonData.value3 = jsonobject["value3"].I;
					if (BuffSeidJsonData171.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData171.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData171.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData171.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData171.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData171.OnInitFinishAction != null)
			{
				BuffSeidJsonData171.OnInitFinishAction();
			}
		}

		// Token: 0x06003C4F RID: 15439 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040035F5 RID: 13813
		public static int SEIDID = 171;

		// Token: 0x040035F6 RID: 13814
		public static Dictionary<int, BuffSeidJsonData171> DataDict = new Dictionary<int, BuffSeidJsonData171>();

		// Token: 0x040035F7 RID: 13815
		public static List<BuffSeidJsonData171> DataList = new List<BuffSeidJsonData171>();

		// Token: 0x040035F8 RID: 13816
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData171.OnInitFinish);

		// Token: 0x040035F9 RID: 13817
		public int id;

		// Token: 0x040035FA RID: 13818
		public int value1;

		// Token: 0x040035FB RID: 13819
		public int value2;

		// Token: 0x040035FC RID: 13820
		public int value3;
	}
}
