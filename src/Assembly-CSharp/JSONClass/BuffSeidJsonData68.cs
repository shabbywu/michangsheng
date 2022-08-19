using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020007E7 RID: 2023
	public class BuffSeidJsonData68 : IJSONClass
	{
		// Token: 0x06003DAE RID: 15790 RVA: 0x001A661C File Offset: 0x001A481C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[68].list)
			{
				try
				{
					BuffSeidJsonData68 buffSeidJsonData = new BuffSeidJsonData68();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData68.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData68.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData68.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData68.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData68.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData68.OnInitFinishAction != null)
			{
				BuffSeidJsonData68.OnInitFinishAction();
			}
		}

		// Token: 0x06003DAF RID: 15791 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003863 RID: 14435
		public static int SEIDID = 68;

		// Token: 0x04003864 RID: 14436
		public static Dictionary<int, BuffSeidJsonData68> DataDict = new Dictionary<int, BuffSeidJsonData68>();

		// Token: 0x04003865 RID: 14437
		public static List<BuffSeidJsonData68> DataList = new List<BuffSeidJsonData68>();

		// Token: 0x04003866 RID: 14438
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData68.OnInitFinish);

		// Token: 0x04003867 RID: 14439
		public int id;

		// Token: 0x04003868 RID: 14440
		public int value1;
	}
}
