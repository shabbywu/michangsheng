using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000771 RID: 1905
	public class BuffSeidJsonData135 : IJSONClass
	{
		// Token: 0x06003BD8 RID: 15320 RVA: 0x0019BA7C File Offset: 0x00199C7C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[135].list)
			{
				try
				{
					BuffSeidJsonData135 buffSeidJsonData = new BuffSeidJsonData135();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					if (BuffSeidJsonData135.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData135.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData135.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData135.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData135.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData135.OnInitFinishAction != null)
			{
				BuffSeidJsonData135.OnInitFinishAction();
			}
		}

		// Token: 0x06003BD9 RID: 15321 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400352B RID: 13611
		public static int SEIDID = 135;

		// Token: 0x0400352C RID: 13612
		public static Dictionary<int, BuffSeidJsonData135> DataDict = new Dictionary<int, BuffSeidJsonData135>();

		// Token: 0x0400352D RID: 13613
		public static List<BuffSeidJsonData135> DataList = new List<BuffSeidJsonData135>();

		// Token: 0x0400352E RID: 13614
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData135.OnInitFinish);

		// Token: 0x0400352F RID: 13615
		public int id;

		// Token: 0x04003530 RID: 13616
		public int value1;

		// Token: 0x04003531 RID: 13617
		public int value2;
	}
}
