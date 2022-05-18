using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B7F RID: 2943
	public class BuffSeidJsonData69 : IJSONClass
	{
		// Token: 0x06004964 RID: 18788 RVA: 0x001F27E0 File Offset: 0x001F09E0
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[69].list)
			{
				try
				{
					BuffSeidJsonData69 buffSeidJsonData = new BuffSeidJsonData69();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					if (BuffSeidJsonData69.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData69.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData69.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData69.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData69.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData69.OnInitFinishAction != null)
			{
				BuffSeidJsonData69.OnInitFinishAction();
			}
		}

		// Token: 0x06004965 RID: 18789 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040043F9 RID: 17401
		public static int SEIDID = 69;

		// Token: 0x040043FA RID: 17402
		public static Dictionary<int, BuffSeidJsonData69> DataDict = new Dictionary<int, BuffSeidJsonData69>();

		// Token: 0x040043FB RID: 17403
		public static List<BuffSeidJsonData69> DataList = new List<BuffSeidJsonData69>();

		// Token: 0x040043FC RID: 17404
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData69.OnInitFinish);

		// Token: 0x040043FD RID: 17405
		public int id;

		// Token: 0x040043FE RID: 17406
		public int value1;

		// Token: 0x040043FF RID: 17407
		public int value2;
	}
}
