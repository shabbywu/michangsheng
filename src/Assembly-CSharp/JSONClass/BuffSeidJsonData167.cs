using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200078A RID: 1930
	public class BuffSeidJsonData167 : IJSONClass
	{
		// Token: 0x06003C3A RID: 15418 RVA: 0x0019DD8C File Offset: 0x0019BF8C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[167].list)
			{
				try
				{
					BuffSeidJsonData167 buffSeidJsonData = new BuffSeidJsonData167();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.target = jsonobject["target"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData167.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData167.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData167.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData167.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData167.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData167.OnInitFinishAction != null)
			{
				BuffSeidJsonData167.OnInitFinishAction();
			}
		}

		// Token: 0x06003C3B RID: 15419 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040035D2 RID: 13778
		public static int SEIDID = 167;

		// Token: 0x040035D3 RID: 13779
		public static Dictionary<int, BuffSeidJsonData167> DataDict = new Dictionary<int, BuffSeidJsonData167>();

		// Token: 0x040035D4 RID: 13780
		public static List<BuffSeidJsonData167> DataList = new List<BuffSeidJsonData167>();

		// Token: 0x040035D5 RID: 13781
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData167.OnInitFinish);

		// Token: 0x040035D6 RID: 13782
		public int id;

		// Token: 0x040035D7 RID: 13783
		public int target;

		// Token: 0x040035D8 RID: 13784
		public int value1;
	}
}
