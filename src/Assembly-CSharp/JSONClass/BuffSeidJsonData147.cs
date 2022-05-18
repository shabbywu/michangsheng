using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B13 RID: 2835
	public class BuffSeidJsonData147 : IJSONClass
	{
		// Token: 0x060047B6 RID: 18358 RVA: 0x001EA124 File Offset: 0x001E8324
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[147].list)
			{
				try
				{
					BuffSeidJsonData147 buffSeidJsonData = new BuffSeidJsonData147();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData147.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData147.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData147.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData147.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData147.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData147.OnInitFinishAction != null)
			{
				BuffSeidJsonData147.OnInitFinishAction();
			}
		}

		// Token: 0x060047B7 RID: 18359 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400410B RID: 16651
		public static int SEIDID = 147;

		// Token: 0x0400410C RID: 16652
		public static Dictionary<int, BuffSeidJsonData147> DataDict = new Dictionary<int, BuffSeidJsonData147>();

		// Token: 0x0400410D RID: 16653
		public static List<BuffSeidJsonData147> DataList = new List<BuffSeidJsonData147>();

		// Token: 0x0400410E RID: 16654
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData147.OnInitFinish);

		// Token: 0x0400410F RID: 16655
		public int id;

		// Token: 0x04004110 RID: 16656
		public int value1;
	}
}
