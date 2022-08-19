using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200078E RID: 1934
	public class BuffSeidJsonData170 : IJSONClass
	{
		// Token: 0x06003C4A RID: 15434 RVA: 0x0019E384 File Offset: 0x0019C584
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[170].list)
			{
				try
				{
					BuffSeidJsonData170 buffSeidJsonData = new BuffSeidJsonData170();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData170.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData170.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData170.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData170.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData170.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData170.OnInitFinishAction != null)
			{
				BuffSeidJsonData170.OnInitFinishAction();
			}
		}

		// Token: 0x06003C4B RID: 15435 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040035EF RID: 13807
		public static int SEIDID = 170;

		// Token: 0x040035F0 RID: 13808
		public static Dictionary<int, BuffSeidJsonData170> DataDict = new Dictionary<int, BuffSeidJsonData170>();

		// Token: 0x040035F1 RID: 13809
		public static List<BuffSeidJsonData170> DataList = new List<BuffSeidJsonData170>();

		// Token: 0x040035F2 RID: 13810
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData170.OnInitFinish);

		// Token: 0x040035F3 RID: 13811
		public int id;

		// Token: 0x040035F4 RID: 13812
		public int value1;
	}
}
