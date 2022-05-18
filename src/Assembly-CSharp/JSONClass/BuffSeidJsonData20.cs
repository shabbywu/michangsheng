using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B3C RID: 2876
	public class BuffSeidJsonData20 : IJSONClass
	{
		// Token: 0x06004858 RID: 18520 RVA: 0x001ED2B0 File Offset: 0x001EB4B0
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[20].list)
			{
				try
				{
					BuffSeidJsonData20 buffSeidJsonData = new BuffSeidJsonData20();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].ToList();
					if (BuffSeidJsonData20.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData20.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData20.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData20.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData20.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData20.OnInitFinishAction != null)
			{
				BuffSeidJsonData20.OnInitFinishAction();
			}
		}

		// Token: 0x06004859 RID: 18521 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400421B RID: 16923
		public static int SEIDID = 20;

		// Token: 0x0400421C RID: 16924
		public static Dictionary<int, BuffSeidJsonData20> DataDict = new Dictionary<int, BuffSeidJsonData20>();

		// Token: 0x0400421D RID: 16925
		public static List<BuffSeidJsonData20> DataList = new List<BuffSeidJsonData20>();

		// Token: 0x0400421E RID: 16926
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData20.OnInitFinish);

		// Token: 0x0400421F RID: 16927
		public int id;

		// Token: 0x04004220 RID: 16928
		public List<int> value1 = new List<int>();
	}
}
