using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020007B1 RID: 1969
	public class BuffSeidJsonData213 : IJSONClass
	{
		// Token: 0x06003CD6 RID: 15574 RVA: 0x001A155C File Offset: 0x0019F75C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[213].list)
			{
				try
				{
					BuffSeidJsonData213 buffSeidJsonData = new BuffSeidJsonData213();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.panduan = jsonobject["panduan"].Str;
					if (BuffSeidJsonData213.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData213.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData213.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData213.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData213.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData213.OnInitFinishAction != null)
			{
				BuffSeidJsonData213.OnInitFinishAction();
			}
		}

		// Token: 0x06003CD7 RID: 15575 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040036D3 RID: 14035
		public static int SEIDID = 213;

		// Token: 0x040036D4 RID: 14036
		public static Dictionary<int, BuffSeidJsonData213> DataDict = new Dictionary<int, BuffSeidJsonData213>();

		// Token: 0x040036D5 RID: 14037
		public static List<BuffSeidJsonData213> DataList = new List<BuffSeidJsonData213>();

		// Token: 0x040036D6 RID: 14038
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData213.OnInitFinish);

		// Token: 0x040036D7 RID: 14039
		public int id;

		// Token: 0x040036D8 RID: 14040
		public int value1;

		// Token: 0x040036D9 RID: 14041
		public string panduan;
	}
}
