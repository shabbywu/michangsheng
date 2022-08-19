using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020007C4 RID: 1988
	public class BuffSeidJsonData33 : IJSONClass
	{
		// Token: 0x06003D22 RID: 15650 RVA: 0x001A3134 File Offset: 0x001A1334
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[33].list)
			{
				try
				{
					BuffSeidJsonData33 buffSeidJsonData = new BuffSeidJsonData33();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					buffSeidJsonData.value3 = jsonobject["value3"].I;
					if (BuffSeidJsonData33.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData33.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData33.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData33.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData33.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData33.OnInitFinishAction != null)
			{
				BuffSeidJsonData33.OnInitFinishAction();
			}
		}

		// Token: 0x06003D23 RID: 15651 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400375A RID: 14170
		public static int SEIDID = 33;

		// Token: 0x0400375B RID: 14171
		public static Dictionary<int, BuffSeidJsonData33> DataDict = new Dictionary<int, BuffSeidJsonData33>();

		// Token: 0x0400375C RID: 14172
		public static List<BuffSeidJsonData33> DataList = new List<BuffSeidJsonData33>();

		// Token: 0x0400375D RID: 14173
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData33.OnInitFinish);

		// Token: 0x0400375E RID: 14174
		public int id;

		// Token: 0x0400375F RID: 14175
		public int value1;

		// Token: 0x04003760 RID: 14176
		public int value2;

		// Token: 0x04003761 RID: 14177
		public int value3;
	}
}
