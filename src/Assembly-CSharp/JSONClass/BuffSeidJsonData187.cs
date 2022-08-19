using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200079A RID: 1946
	public class BuffSeidJsonData187 : IJSONClass
	{
		// Token: 0x06003C7A RID: 15482 RVA: 0x0019F4A8 File Offset: 0x0019D6A8
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[187].list)
			{
				try
				{
					BuffSeidJsonData187 buffSeidJsonData = new BuffSeidJsonData187();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData187.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData187.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData187.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData187.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData187.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData187.OnInitFinishAction != null)
			{
				BuffSeidJsonData187.OnInitFinishAction();
			}
		}

		// Token: 0x06003C7B RID: 15483 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400363E RID: 13886
		public static int SEIDID = 187;

		// Token: 0x0400363F RID: 13887
		public static Dictionary<int, BuffSeidJsonData187> DataDict = new Dictionary<int, BuffSeidJsonData187>();

		// Token: 0x04003640 RID: 13888
		public static List<BuffSeidJsonData187> DataList = new List<BuffSeidJsonData187>();

		// Token: 0x04003641 RID: 13889
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData187.OnInitFinish);

		// Token: 0x04003642 RID: 13890
		public int id;

		// Token: 0x04003643 RID: 13891
		public int value1;
	}
}
