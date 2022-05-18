using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B74 RID: 2932
	public class BuffSeidJsonData58 : IJSONClass
	{
		// Token: 0x06004938 RID: 18744 RVA: 0x001F1AA8 File Offset: 0x001EFCA8
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[58].list)
			{
				try
				{
					BuffSeidJsonData58 buffSeidJsonData = new BuffSeidJsonData58();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData58.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData58.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData58.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData58.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData58.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData58.OnInitFinishAction != null)
			{
				BuffSeidJsonData58.OnInitFinishAction();
			}
		}

		// Token: 0x06004939 RID: 18745 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040043B2 RID: 17330
		public static int SEIDID = 58;

		// Token: 0x040043B3 RID: 17331
		public static Dictionary<int, BuffSeidJsonData58> DataDict = new Dictionary<int, BuffSeidJsonData58>();

		// Token: 0x040043B4 RID: 17332
		public static List<BuffSeidJsonData58> DataList = new List<BuffSeidJsonData58>();

		// Token: 0x040043B5 RID: 17333
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData58.OnInitFinish);

		// Token: 0x040043B6 RID: 17334
		public int id;

		// Token: 0x040043B7 RID: 17335
		public int value1;
	}
}
