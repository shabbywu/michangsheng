using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020007D2 RID: 2002
	public class BuffSeidJsonData48 : IJSONClass
	{
		// Token: 0x06003D5A RID: 15706 RVA: 0x001A47B4 File Offset: 0x001A29B4
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[48].list)
			{
				try
				{
					BuffSeidJsonData48 buffSeidJsonData = new BuffSeidJsonData48();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					buffSeidJsonData.value3 = jsonobject["value3"].I;
					if (BuffSeidJsonData48.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData48.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData48.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData48.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData48.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData48.OnInitFinishAction != null)
			{
				BuffSeidJsonData48.OnInitFinishAction();
			}
		}

		// Token: 0x06003D5B RID: 15707 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040037D0 RID: 14288
		public static int SEIDID = 48;

		// Token: 0x040037D1 RID: 14289
		public static Dictionary<int, BuffSeidJsonData48> DataDict = new Dictionary<int, BuffSeidJsonData48>();

		// Token: 0x040037D2 RID: 14290
		public static List<BuffSeidJsonData48> DataList = new List<BuffSeidJsonData48>();

		// Token: 0x040037D3 RID: 14291
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData48.OnInitFinish);

		// Token: 0x040037D4 RID: 14292
		public int id;

		// Token: 0x040037D5 RID: 14293
		public int value1;

		// Token: 0x040037D6 RID: 14294
		public int value2;

		// Token: 0x040037D7 RID: 14295
		public int value3;
	}
}
