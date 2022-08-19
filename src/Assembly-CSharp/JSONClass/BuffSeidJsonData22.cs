using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020007B6 RID: 1974
	public class BuffSeidJsonData22 : IJSONClass
	{
		// Token: 0x06003CEA RID: 15594 RVA: 0x001A1D24 File Offset: 0x0019FF24
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[22].list)
			{
				try
				{
					BuffSeidJsonData22 buffSeidJsonData = new BuffSeidJsonData22();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData22.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData22.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData22.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData22.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData22.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData22.OnInitFinishAction != null)
			{
				BuffSeidJsonData22.OnInitFinishAction();
			}
		}

		// Token: 0x06003CEB RID: 15595 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040036F9 RID: 14073
		public static int SEIDID = 22;

		// Token: 0x040036FA RID: 14074
		public static Dictionary<int, BuffSeidJsonData22> DataDict = new Dictionary<int, BuffSeidJsonData22>();

		// Token: 0x040036FB RID: 14075
		public static List<BuffSeidJsonData22> DataList = new List<BuffSeidJsonData22>();

		// Token: 0x040036FC RID: 14076
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData22.OnInitFinish);

		// Token: 0x040036FD RID: 14077
		public int id;

		// Token: 0x040036FE RID: 14078
		public int value1;
	}
}
