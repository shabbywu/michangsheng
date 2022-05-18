using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B7E RID: 2942
	public class BuffSeidJsonData68 : IJSONClass
	{
		// Token: 0x06004960 RID: 18784 RVA: 0x001F26B8 File Offset: 0x001F08B8
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[68].list)
			{
				try
				{
					BuffSeidJsonData68 buffSeidJsonData = new BuffSeidJsonData68();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData68.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData68.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData68.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData68.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData68.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData68.OnInitFinishAction != null)
			{
				BuffSeidJsonData68.OnInitFinishAction();
			}
		}

		// Token: 0x06004961 RID: 18785 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040043F3 RID: 17395
		public static int SEIDID = 68;

		// Token: 0x040043F4 RID: 17396
		public static Dictionary<int, BuffSeidJsonData68> DataDict = new Dictionary<int, BuffSeidJsonData68>();

		// Token: 0x040043F5 RID: 17397
		public static List<BuffSeidJsonData68> DataList = new List<BuffSeidJsonData68>();

		// Token: 0x040043F6 RID: 17398
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData68.OnInitFinish);

		// Token: 0x040043F7 RID: 17399
		public int id;

		// Token: 0x040043F8 RID: 17400
		public int value1;
	}
}
