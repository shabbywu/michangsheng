using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B70 RID: 2928
	public class BuffSeidJsonData54 : IJSONClass
	{
		// Token: 0x06004928 RID: 18728 RVA: 0x001F159C File Offset: 0x001EF79C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[54].list)
			{
				try
				{
					BuffSeidJsonData54 buffSeidJsonData = new BuffSeidJsonData54();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					buffSeidJsonData.value3 = jsonobject["value3"].I;
					buffSeidJsonData.value4 = jsonobject["value4"].I;
					if (BuffSeidJsonData54.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData54.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData54.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData54.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData54.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData54.OnInitFinishAction != null)
			{
				BuffSeidJsonData54.OnInitFinishAction();
			}
		}

		// Token: 0x06004929 RID: 18729 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004396 RID: 17302
		public static int SEIDID = 54;

		// Token: 0x04004397 RID: 17303
		public static Dictionary<int, BuffSeidJsonData54> DataDict = new Dictionary<int, BuffSeidJsonData54>();

		// Token: 0x04004398 RID: 17304
		public static List<BuffSeidJsonData54> DataList = new List<BuffSeidJsonData54>();

		// Token: 0x04004399 RID: 17305
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData54.OnInitFinish);

		// Token: 0x0400439A RID: 17306
		public int id;

		// Token: 0x0400439B RID: 17307
		public int value1;

		// Token: 0x0400439C RID: 17308
		public int value2;

		// Token: 0x0400439D RID: 17309
		public int value3;

		// Token: 0x0400439E RID: 17310
		public int value4;
	}
}
