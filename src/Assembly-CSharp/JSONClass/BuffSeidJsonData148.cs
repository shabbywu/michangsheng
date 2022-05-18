using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B14 RID: 2836
	public class BuffSeidJsonData148 : IJSONClass
	{
		// Token: 0x060047BA RID: 18362 RVA: 0x001EA250 File Offset: 0x001E8450
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[148].list)
			{
				try
				{
					BuffSeidJsonData148 buffSeidJsonData = new BuffSeidJsonData148();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData148.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData148.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData148.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData148.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData148.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData148.OnInitFinishAction != null)
			{
				BuffSeidJsonData148.OnInitFinishAction();
			}
		}

		// Token: 0x060047BB RID: 18363 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004111 RID: 16657
		public static int SEIDID = 148;

		// Token: 0x04004112 RID: 16658
		public static Dictionary<int, BuffSeidJsonData148> DataDict = new Dictionary<int, BuffSeidJsonData148>();

		// Token: 0x04004113 RID: 16659
		public static List<BuffSeidJsonData148> DataList = new List<BuffSeidJsonData148>();

		// Token: 0x04004114 RID: 16660
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData148.OnInitFinish);

		// Token: 0x04004115 RID: 16661
		public int id;

		// Token: 0x04004116 RID: 16662
		public int value1;
	}
}
