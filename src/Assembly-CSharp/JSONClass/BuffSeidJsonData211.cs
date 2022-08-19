using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020007B0 RID: 1968
	public class BuffSeidJsonData211 : IJSONClass
	{
		// Token: 0x06003CD2 RID: 15570 RVA: 0x001A13FC File Offset: 0x0019F5FC
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[211].list)
			{
				try
				{
					BuffSeidJsonData211 buffSeidJsonData = new BuffSeidJsonData211();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData211.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData211.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData211.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData211.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData211.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData211.OnInitFinishAction != null)
			{
				BuffSeidJsonData211.OnInitFinishAction();
			}
		}

		// Token: 0x06003CD3 RID: 15571 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040036CD RID: 14029
		public static int SEIDID = 211;

		// Token: 0x040036CE RID: 14030
		public static Dictionary<int, BuffSeidJsonData211> DataDict = new Dictionary<int, BuffSeidJsonData211>();

		// Token: 0x040036CF RID: 14031
		public static List<BuffSeidJsonData211> DataList = new List<BuffSeidJsonData211>();

		// Token: 0x040036D0 RID: 14032
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData211.OnInitFinish);

		// Token: 0x040036D1 RID: 14033
		public int id;

		// Token: 0x040036D2 RID: 14034
		public int value1;
	}
}
