using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020007A9 RID: 1961
	public class BuffSeidJsonData204 : IJSONClass
	{
		// Token: 0x06003CB6 RID: 15542 RVA: 0x001A0A24 File Offset: 0x0019EC24
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[204].list)
			{
				try
				{
					BuffSeidJsonData204 buffSeidJsonData = new BuffSeidJsonData204();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					if (BuffSeidJsonData204.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData204.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData204.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData204.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData204.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData204.OnInitFinishAction != null)
			{
				BuffSeidJsonData204.OnInitFinishAction();
			}
		}

		// Token: 0x06003CB7 RID: 15543 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040036A0 RID: 13984
		public static int SEIDID = 204;

		// Token: 0x040036A1 RID: 13985
		public static Dictionary<int, BuffSeidJsonData204> DataDict = new Dictionary<int, BuffSeidJsonData204>();

		// Token: 0x040036A2 RID: 13986
		public static List<BuffSeidJsonData204> DataList = new List<BuffSeidJsonData204>();

		// Token: 0x040036A3 RID: 13987
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData204.OnInitFinish);

		// Token: 0x040036A4 RID: 13988
		public int id;

		// Token: 0x040036A5 RID: 13989
		public int value1;

		// Token: 0x040036A6 RID: 13990
		public int value2;
	}
}
