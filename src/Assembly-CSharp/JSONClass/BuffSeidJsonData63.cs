using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B79 RID: 2937
	public class BuffSeidJsonData63 : IJSONClass
	{
		// Token: 0x0600494C RID: 18764 RVA: 0x001F2070 File Offset: 0x001F0270
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[63].list)
			{
				try
				{
					BuffSeidJsonData63 buffSeidJsonData = new BuffSeidJsonData63();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData63.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData63.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData63.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData63.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData63.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData63.OnInitFinishAction != null)
			{
				BuffSeidJsonData63.OnInitFinishAction();
			}
		}

		// Token: 0x0600494D RID: 18765 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040043D0 RID: 17360
		public static int SEIDID = 63;

		// Token: 0x040043D1 RID: 17361
		public static Dictionary<int, BuffSeidJsonData63> DataDict = new Dictionary<int, BuffSeidJsonData63>();

		// Token: 0x040043D2 RID: 17362
		public static List<BuffSeidJsonData63> DataList = new List<BuffSeidJsonData63>();

		// Token: 0x040043D3 RID: 17363
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData63.OnInitFinish);

		// Token: 0x040043D4 RID: 17364
		public int id;

		// Token: 0x040043D5 RID: 17365
		public int value1;
	}
}
