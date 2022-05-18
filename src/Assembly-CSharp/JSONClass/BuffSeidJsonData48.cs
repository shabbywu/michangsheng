using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B69 RID: 2921
	public class BuffSeidJsonData48 : IJSONClass
	{
		// Token: 0x0600490C RID: 18700 RVA: 0x001F0C6C File Offset: 0x001EEE6C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[48].list)
			{
				try
				{
					BuffSeidJsonData48 buffSeidJsonData = new BuffSeidJsonData48();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					buffSeidJsonData.value3 = jsonobject["value3"].I;
					if (BuffSeidJsonData48.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData48.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData48.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData48.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData48.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData48.OnInitFinishAction != null)
			{
				BuffSeidJsonData48.OnInitFinishAction();
			}
		}

		// Token: 0x0600490D RID: 18701 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004360 RID: 17248
		public static int SEIDID = 48;

		// Token: 0x04004361 RID: 17249
		public static Dictionary<int, BuffSeidJsonData48> DataDict = new Dictionary<int, BuffSeidJsonData48>();

		// Token: 0x04004362 RID: 17250
		public static List<BuffSeidJsonData48> DataList = new List<BuffSeidJsonData48>();

		// Token: 0x04004363 RID: 17251
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData48.OnInitFinish);

		// Token: 0x04004364 RID: 17252
		public int id;

		// Token: 0x04004365 RID: 17253
		public int value1;

		// Token: 0x04004366 RID: 17254
		public int value2;

		// Token: 0x04004367 RID: 17255
		public int value3;
	}
}
