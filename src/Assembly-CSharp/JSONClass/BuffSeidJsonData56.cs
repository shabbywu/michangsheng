using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B72 RID: 2930
	public class BuffSeidJsonData56 : IJSONClass
	{
		// Token: 0x06004930 RID: 18736 RVA: 0x001F1844 File Offset: 0x001EFA44
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[56].list)
			{
				try
				{
					BuffSeidJsonData56 buffSeidJsonData = new BuffSeidJsonData56();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData56.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData56.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData56.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData56.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData56.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData56.OnInitFinishAction != null)
			{
				BuffSeidJsonData56.OnInitFinishAction();
			}
		}

		// Token: 0x06004931 RID: 18737 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040043A5 RID: 17317
		public static int SEIDID = 56;

		// Token: 0x040043A6 RID: 17318
		public static Dictionary<int, BuffSeidJsonData56> DataDict = new Dictionary<int, BuffSeidJsonData56>();

		// Token: 0x040043A7 RID: 17319
		public static List<BuffSeidJsonData56> DataList = new List<BuffSeidJsonData56>();

		// Token: 0x040043A8 RID: 17320
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData56.OnInitFinish);

		// Token: 0x040043A9 RID: 17321
		public int id;

		// Token: 0x040043AA RID: 17322
		public int value1;
	}
}
