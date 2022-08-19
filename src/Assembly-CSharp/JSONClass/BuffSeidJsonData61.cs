using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020007E0 RID: 2016
	public class BuffSeidJsonData61 : IJSONClass
	{
		// Token: 0x06003D92 RID: 15762 RVA: 0x001A5C34 File Offset: 0x001A3E34
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[61].list)
			{
				try
				{
					BuffSeidJsonData61 buffSeidJsonData = new BuffSeidJsonData61();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData61.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData61.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData61.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData61.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData61.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData61.OnInitFinishAction != null)
			{
				BuffSeidJsonData61.OnInitFinishAction();
			}
		}

		// Token: 0x06003D93 RID: 15763 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003834 RID: 14388
		public static int SEIDID = 61;

		// Token: 0x04003835 RID: 14389
		public static Dictionary<int, BuffSeidJsonData61> DataDict = new Dictionary<int, BuffSeidJsonData61>();

		// Token: 0x04003836 RID: 14390
		public static List<BuffSeidJsonData61> DataList = new List<BuffSeidJsonData61>();

		// Token: 0x04003837 RID: 14391
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData61.OnInitFinish);

		// Token: 0x04003838 RID: 14392
		public int id;

		// Token: 0x04003839 RID: 14393
		public int value1;
	}
}
