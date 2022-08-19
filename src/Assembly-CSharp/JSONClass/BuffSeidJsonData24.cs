using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020007B7 RID: 1975
	public class BuffSeidJsonData24 : IJSONClass
	{
		// Token: 0x06003CEE RID: 15598 RVA: 0x001A1E7C File Offset: 0x001A007C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[24].list)
			{
				try
				{
					BuffSeidJsonData24 buffSeidJsonData = new BuffSeidJsonData24();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData24.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData24.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData24.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData24.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData24.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData24.OnInitFinishAction != null)
			{
				BuffSeidJsonData24.OnInitFinishAction();
			}
		}

		// Token: 0x06003CEF RID: 15599 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040036FF RID: 14079
		public static int SEIDID = 24;

		// Token: 0x04003700 RID: 14080
		public static Dictionary<int, BuffSeidJsonData24> DataDict = new Dictionary<int, BuffSeidJsonData24>();

		// Token: 0x04003701 RID: 14081
		public static List<BuffSeidJsonData24> DataList = new List<BuffSeidJsonData24>();

		// Token: 0x04003702 RID: 14082
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData24.OnInitFinish);

		// Token: 0x04003703 RID: 14083
		public int id;

		// Token: 0x04003704 RID: 14084
		public int value1;
	}
}
