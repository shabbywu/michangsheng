using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000AF4 RID: 2804
	public class BuffSeidJsonData105 : IJSONClass
	{
		// Token: 0x0600473A RID: 18234 RVA: 0x001E7AE4 File Offset: 0x001E5CE4
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[105].list)
			{
				try
				{
					BuffSeidJsonData105 buffSeidJsonData = new BuffSeidJsonData105();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					if (BuffSeidJsonData105.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData105.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData105.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData105.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData105.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData105.OnInitFinishAction != null)
			{
				BuffSeidJsonData105.OnInitFinishAction();
			}
		}

		// Token: 0x0600473B RID: 18235 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004037 RID: 16439
		public static int SEIDID = 105;

		// Token: 0x04004038 RID: 16440
		public static Dictionary<int, BuffSeidJsonData105> DataDict = new Dictionary<int, BuffSeidJsonData105>();

		// Token: 0x04004039 RID: 16441
		public static List<BuffSeidJsonData105> DataList = new List<BuffSeidJsonData105>();

		// Token: 0x0400403A RID: 16442
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData105.OnInitFinish);

		// Token: 0x0400403B RID: 16443
		public int id;

		// Token: 0x0400403C RID: 16444
		public int value1;

		// Token: 0x0400403D RID: 16445
		public int value2;
	}
}
