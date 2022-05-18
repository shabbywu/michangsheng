using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B7A RID: 2938
	public class BuffSeidJsonData64 : IJSONClass
	{
		// Token: 0x06004950 RID: 18768 RVA: 0x001F2198 File Offset: 0x001F0398
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[64].list)
			{
				try
				{
					BuffSeidJsonData64 buffSeidJsonData = new BuffSeidJsonData64();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					if (BuffSeidJsonData64.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData64.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData64.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData64.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData64.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData64.OnInitFinishAction != null)
			{
				BuffSeidJsonData64.OnInitFinishAction();
			}
		}

		// Token: 0x06004951 RID: 18769 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040043D6 RID: 17366
		public static int SEIDID = 64;

		// Token: 0x040043D7 RID: 17367
		public static Dictionary<int, BuffSeidJsonData64> DataDict = new Dictionary<int, BuffSeidJsonData64>();

		// Token: 0x040043D8 RID: 17368
		public static List<BuffSeidJsonData64> DataList = new List<BuffSeidJsonData64>();

		// Token: 0x040043D9 RID: 17369
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData64.OnInitFinish);

		// Token: 0x040043DA RID: 17370
		public int id;

		// Token: 0x040043DB RID: 17371
		public int value1;

		// Token: 0x040043DC RID: 17372
		public int value2;
	}
}
