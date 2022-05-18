using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B80 RID: 2944
	public class BuffSeidJsonData7 : IJSONClass
	{
		// Token: 0x06004968 RID: 18792 RVA: 0x001F291C File Offset: 0x001F0B1C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[7].list)
			{
				try
				{
					BuffSeidJsonData7 buffSeidJsonData = new BuffSeidJsonData7();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData7.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData7.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData7.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData7.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData7.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData7.OnInitFinishAction != null)
			{
				BuffSeidJsonData7.OnInitFinishAction();
			}
		}

		// Token: 0x06004969 RID: 18793 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004400 RID: 17408
		public static int SEIDID = 7;

		// Token: 0x04004401 RID: 17409
		public static Dictionary<int, BuffSeidJsonData7> DataDict = new Dictionary<int, BuffSeidJsonData7>();

		// Token: 0x04004402 RID: 17410
		public static List<BuffSeidJsonData7> DataList = new List<BuffSeidJsonData7>();

		// Token: 0x04004403 RID: 17411
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData7.OnInitFinish);

		// Token: 0x04004404 RID: 17412
		public int id;

		// Token: 0x04004405 RID: 17413
		public int value1;
	}
}
