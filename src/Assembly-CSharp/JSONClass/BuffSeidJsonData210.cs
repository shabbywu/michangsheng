using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020007AF RID: 1967
	public class BuffSeidJsonData210 : IJSONClass
	{
		// Token: 0x06003CCE RID: 15566 RVA: 0x001A129C File Offset: 0x0019F49C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[210].list)
			{
				try
				{
					BuffSeidJsonData210 buffSeidJsonData = new BuffSeidJsonData210();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData210.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData210.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData210.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData210.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData210.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData210.OnInitFinishAction != null)
			{
				BuffSeidJsonData210.OnInitFinishAction();
			}
		}

		// Token: 0x06003CCF RID: 15567 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040036C7 RID: 14023
		public static int SEIDID = 210;

		// Token: 0x040036C8 RID: 14024
		public static Dictionary<int, BuffSeidJsonData210> DataDict = new Dictionary<int, BuffSeidJsonData210>();

		// Token: 0x040036C9 RID: 14025
		public static List<BuffSeidJsonData210> DataList = new List<BuffSeidJsonData210>();

		// Token: 0x040036CA RID: 14026
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData210.OnInitFinish);

		// Token: 0x040036CB RID: 14027
		public int id;

		// Token: 0x040036CC RID: 14028
		public int value1;
	}
}
