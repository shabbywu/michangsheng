using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000782 RID: 1922
	public class BuffSeidJsonData158 : IJSONClass
	{
		// Token: 0x06003C1B RID: 15387 RVA: 0x0019D2A4 File Offset: 0x0019B4A4
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[158].list)
			{
				try
				{
					BuffSeidJsonData158 buffSeidJsonData = new BuffSeidJsonData158();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.target = jsonobject["target"].I;
					if (BuffSeidJsonData158.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData158.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData158.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData158.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData158.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData158.OnInitFinishAction != null)
			{
				BuffSeidJsonData158.OnInitFinishAction();
			}
		}

		// Token: 0x06003C1C RID: 15388 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400359F RID: 13727
		public static int SEIDID = 158;

		// Token: 0x040035A0 RID: 13728
		public static Dictionary<int, BuffSeidJsonData158> DataDict = new Dictionary<int, BuffSeidJsonData158>();

		// Token: 0x040035A1 RID: 13729
		public static List<BuffSeidJsonData158> DataList = new List<BuffSeidJsonData158>();

		// Token: 0x040035A2 RID: 13730
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData158.OnInitFinish);

		// Token: 0x040035A3 RID: 13731
		public int id;

		// Token: 0x040035A4 RID: 13732
		public int target;
	}
}
