using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B2D RID: 2861
	public class BuffSeidJsonData179 : IJSONClass
	{
		// Token: 0x0600481C RID: 18460 RVA: 0x001EC02C File Offset: 0x001EA22C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[179].list)
			{
				try
				{
					BuffSeidJsonData179 buffSeidJsonData = new BuffSeidJsonData179();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData179.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData179.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData179.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData179.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData179.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData179.OnInitFinishAction != null)
			{
				BuffSeidJsonData179.OnInitFinishAction();
			}
		}

		// Token: 0x0600481D RID: 18461 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040041B6 RID: 16822
		public static int SEIDID = 179;

		// Token: 0x040041B7 RID: 16823
		public static Dictionary<int, BuffSeidJsonData179> DataDict = new Dictionary<int, BuffSeidJsonData179>();

		// Token: 0x040041B8 RID: 16824
		public static List<BuffSeidJsonData179> DataList = new List<BuffSeidJsonData179>();

		// Token: 0x040041B9 RID: 16825
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData179.OnInitFinish);

		// Token: 0x040041BA RID: 16826
		public int id;

		// Token: 0x040041BB RID: 16827
		public int value1;
	}
}
