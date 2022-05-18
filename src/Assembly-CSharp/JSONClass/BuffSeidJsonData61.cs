using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B77 RID: 2935
	public class BuffSeidJsonData61 : IJSONClass
	{
		// Token: 0x06004944 RID: 18756 RVA: 0x001F1E20 File Offset: 0x001F0020
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[61].list)
			{
				try
				{
					BuffSeidJsonData61 buffSeidJsonData = new BuffSeidJsonData61();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData61.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData61.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData61.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData61.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData61.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData61.OnInitFinishAction != null)
			{
				BuffSeidJsonData61.OnInitFinishAction();
			}
		}

		// Token: 0x06004945 RID: 18757 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040043C4 RID: 17348
		public static int SEIDID = 61;

		// Token: 0x040043C5 RID: 17349
		public static Dictionary<int, BuffSeidJsonData61> DataDict = new Dictionary<int, BuffSeidJsonData61>();

		// Token: 0x040043C6 RID: 17350
		public static List<BuffSeidJsonData61> DataList = new List<BuffSeidJsonData61>();

		// Token: 0x040043C7 RID: 17351
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData61.OnInitFinish);

		// Token: 0x040043C8 RID: 17352
		public int id;

		// Token: 0x040043C9 RID: 17353
		public int value1;
	}
}
