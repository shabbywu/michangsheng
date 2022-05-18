using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B00 RID: 2816
	public class BuffSeidJsonData122 : IJSONClass
	{
		// Token: 0x0600476A RID: 18282 RVA: 0x001E8918 File Offset: 0x001E6B18
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[122].list)
			{
				try
				{
					BuffSeidJsonData122 buffSeidJsonData = new BuffSeidJsonData122();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData122.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData122.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData122.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData122.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData122.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData122.OnInitFinishAction != null)
			{
				BuffSeidJsonData122.OnInitFinishAction();
			}
		}

		// Token: 0x0600476B RID: 18283 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004083 RID: 16515
		public static int SEIDID = 122;

		// Token: 0x04004084 RID: 16516
		public static Dictionary<int, BuffSeidJsonData122> DataDict = new Dictionary<int, BuffSeidJsonData122>();

		// Token: 0x04004085 RID: 16517
		public static List<BuffSeidJsonData122> DataList = new List<BuffSeidJsonData122>();

		// Token: 0x04004086 RID: 16518
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData122.OnInitFinish);

		// Token: 0x04004087 RID: 16519
		public int id;

		// Token: 0x04004088 RID: 16520
		public int value1;
	}
}
