using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000779 RID: 1913
	public class BuffSeidJsonData144 : IJSONClass
	{
		// Token: 0x06003BF8 RID: 15352 RVA: 0x0019C638 File Offset: 0x0019A838
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[144].list)
			{
				try
				{
					BuffSeidJsonData144 buffSeidJsonData = new BuffSeidJsonData144();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					buffSeidJsonData.value3 = jsonobject["value3"].I;
					if (BuffSeidJsonData144.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData144.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData144.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData144.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData144.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData144.OnInitFinishAction != null)
			{
				BuffSeidJsonData144.OnInitFinishAction();
			}
		}

		// Token: 0x06003BF9 RID: 15353 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003563 RID: 13667
		public static int SEIDID = 144;

		// Token: 0x04003564 RID: 13668
		public static Dictionary<int, BuffSeidJsonData144> DataDict = new Dictionary<int, BuffSeidJsonData144>();

		// Token: 0x04003565 RID: 13669
		public static List<BuffSeidJsonData144> DataList = new List<BuffSeidJsonData144>();

		// Token: 0x04003566 RID: 13670
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData144.OnInitFinish);

		// Token: 0x04003567 RID: 13671
		public int id;

		// Token: 0x04003568 RID: 13672
		public int value1;

		// Token: 0x04003569 RID: 13673
		public int value2;

		// Token: 0x0400356A RID: 13674
		public int value3;
	}
}
