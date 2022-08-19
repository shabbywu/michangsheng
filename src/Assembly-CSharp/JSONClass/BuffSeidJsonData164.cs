using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000787 RID: 1927
	public class BuffSeidJsonData164 : IJSONClass
	{
		// Token: 0x06003C2E RID: 15406 RVA: 0x0019D96C File Offset: 0x0019BB6C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[164].list)
			{
				try
				{
					BuffSeidJsonData164 buffSeidJsonData = new BuffSeidJsonData164();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData164.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData164.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData164.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData164.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData164.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData164.OnInitFinishAction != null)
			{
				BuffSeidJsonData164.OnInitFinishAction();
			}
		}

		// Token: 0x06003C2F RID: 15407 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040035C0 RID: 13760
		public static int SEIDID = 164;

		// Token: 0x040035C1 RID: 13761
		public static Dictionary<int, BuffSeidJsonData164> DataDict = new Dictionary<int, BuffSeidJsonData164>();

		// Token: 0x040035C2 RID: 13762
		public static List<BuffSeidJsonData164> DataList = new List<BuffSeidJsonData164>();

		// Token: 0x040035C3 RID: 13763
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData164.OnInitFinish);

		// Token: 0x040035C4 RID: 13764
		public int id;

		// Token: 0x040035C5 RID: 13765
		public int value1;
	}
}
