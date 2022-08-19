using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020007AD RID: 1965
	public class BuffSeidJsonData209 : IJSONClass
	{
		// Token: 0x06003CC6 RID: 15558 RVA: 0x001A0FE4 File Offset: 0x0019F1E4
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[209].list)
			{
				try
				{
					BuffSeidJsonData209 buffSeidJsonData = new BuffSeidJsonData209();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData209.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData209.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData209.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData209.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData209.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData209.OnInitFinishAction != null)
			{
				BuffSeidJsonData209.OnInitFinishAction();
			}
		}

		// Token: 0x06003CC7 RID: 15559 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040036BB RID: 14011
		public static int SEIDID = 209;

		// Token: 0x040036BC RID: 14012
		public static Dictionary<int, BuffSeidJsonData209> DataDict = new Dictionary<int, BuffSeidJsonData209>();

		// Token: 0x040036BD RID: 14013
		public static List<BuffSeidJsonData209> DataList = new List<BuffSeidJsonData209>();

		// Token: 0x040036BE RID: 14014
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData209.OnInitFinish);

		// Token: 0x040036BF RID: 14015
		public int id;

		// Token: 0x040036C0 RID: 14016
		public int value1;
	}
}
