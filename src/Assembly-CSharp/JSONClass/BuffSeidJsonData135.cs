using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B09 RID: 2825
	public class BuffSeidJsonData135 : IJSONClass
	{
		// Token: 0x0600478E RID: 18318 RVA: 0x001E9480 File Offset: 0x001E7680
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[135].list)
			{
				try
				{
					BuffSeidJsonData135 buffSeidJsonData = new BuffSeidJsonData135();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					if (BuffSeidJsonData135.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData135.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData135.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData135.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData135.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData135.OnInitFinishAction != null)
			{
				BuffSeidJsonData135.OnInitFinishAction();
			}
		}

		// Token: 0x0600478F RID: 18319 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040040C4 RID: 16580
		public static int SEIDID = 135;

		// Token: 0x040040C5 RID: 16581
		public static Dictionary<int, BuffSeidJsonData135> DataDict = new Dictionary<int, BuffSeidJsonData135>();

		// Token: 0x040040C6 RID: 16582
		public static List<BuffSeidJsonData135> DataList = new List<BuffSeidJsonData135>();

		// Token: 0x040040C7 RID: 16583
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData135.OnInitFinish);

		// Token: 0x040040C8 RID: 16584
		public int id;

		// Token: 0x040040C9 RID: 16585
		public int value1;

		// Token: 0x040040CA RID: 16586
		public int value2;
	}
}
