using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200076A RID: 1898
	public class BuffSeidJsonData126 : IJSONClass
	{
		// Token: 0x06003BBC RID: 15292 RVA: 0x0019AF9C File Offset: 0x0019919C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[126].list)
			{
				try
				{
					BuffSeidJsonData126 buffSeidJsonData = new BuffSeidJsonData126();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					if (BuffSeidJsonData126.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData126.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData126.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData126.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData126.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData126.OnInitFinishAction != null)
			{
				BuffSeidJsonData126.OnInitFinishAction();
			}
		}

		// Token: 0x06003BBD RID: 15293 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040034F6 RID: 13558
		public static int SEIDID = 126;

		// Token: 0x040034F7 RID: 13559
		public static Dictionary<int, BuffSeidJsonData126> DataDict = new Dictionary<int, BuffSeidJsonData126>();

		// Token: 0x040034F8 RID: 13560
		public static List<BuffSeidJsonData126> DataList = new List<BuffSeidJsonData126>();

		// Token: 0x040034F9 RID: 13561
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData126.OnInitFinish);

		// Token: 0x040034FA RID: 13562
		public int id;

		// Token: 0x040034FB RID: 13563
		public int value1;

		// Token: 0x040034FC RID: 13564
		public int value2;
	}
}
