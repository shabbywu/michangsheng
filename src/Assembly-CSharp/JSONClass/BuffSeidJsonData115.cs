using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000763 RID: 1891
	public class BuffSeidJsonData115 : IJSONClass
	{
		// Token: 0x06003BA0 RID: 15264 RVA: 0x0019A5F4 File Offset: 0x001987F4
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[115].list)
			{
				try
				{
					BuffSeidJsonData115 buffSeidJsonData = new BuffSeidJsonData115();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData115.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData115.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData115.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData115.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData115.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData115.OnInitFinishAction != null)
			{
				BuffSeidJsonData115.OnInitFinishAction();
			}
		}

		// Token: 0x06003BA1 RID: 15265 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040034C9 RID: 13513
		public static int SEIDID = 115;

		// Token: 0x040034CA RID: 13514
		public static Dictionary<int, BuffSeidJsonData115> DataDict = new Dictionary<int, BuffSeidJsonData115>();

		// Token: 0x040034CB RID: 13515
		public static List<BuffSeidJsonData115> DataList = new List<BuffSeidJsonData115>();

		// Token: 0x040034CC RID: 13516
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData115.OnInitFinish);

		// Token: 0x040034CD RID: 13517
		public int id;

		// Token: 0x040034CE RID: 13518
		public int value1;
	}
}
