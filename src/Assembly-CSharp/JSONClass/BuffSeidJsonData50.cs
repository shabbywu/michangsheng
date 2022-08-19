using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020007D5 RID: 2005
	public class BuffSeidJsonData50 : IJSONClass
	{
		// Token: 0x06003D66 RID: 15718 RVA: 0x001A4C44 File Offset: 0x001A2E44
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[50].list)
			{
				try
				{
					BuffSeidJsonData50 buffSeidJsonData = new BuffSeidJsonData50();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					if (BuffSeidJsonData50.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData50.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData50.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData50.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData50.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData50.OnInitFinishAction != null)
			{
				BuffSeidJsonData50.OnInitFinishAction();
			}
		}

		// Token: 0x06003D67 RID: 15719 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040037E7 RID: 14311
		public static int SEIDID = 50;

		// Token: 0x040037E8 RID: 14312
		public static Dictionary<int, BuffSeidJsonData50> DataDict = new Dictionary<int, BuffSeidJsonData50>();

		// Token: 0x040037E9 RID: 14313
		public static List<BuffSeidJsonData50> DataList = new List<BuffSeidJsonData50>();

		// Token: 0x040037EA RID: 14314
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData50.OnInitFinish);

		// Token: 0x040037EB RID: 14315
		public int id;

		// Token: 0x040037EC RID: 14316
		public int value1;

		// Token: 0x040037ED RID: 14317
		public int value2;
	}
}
