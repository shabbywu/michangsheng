using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B95 RID: 2965
	public class BuffSeidJsonData93 : IJSONClass
	{
		// Token: 0x060049BC RID: 18876 RVA: 0x001F4230 File Offset: 0x001F2430
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[93].list)
			{
				try
				{
					BuffSeidJsonData93 buffSeidJsonData = new BuffSeidJsonData93();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData93.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData93.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData93.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData93.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData93.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData93.OnInitFinishAction != null)
			{
				BuffSeidJsonData93.OnInitFinishAction();
			}
		}

		// Token: 0x060049BD RID: 18877 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004488 RID: 17544
		public static int SEIDID = 93;

		// Token: 0x04004489 RID: 17545
		public static Dictionary<int, BuffSeidJsonData93> DataDict = new Dictionary<int, BuffSeidJsonData93>();

		// Token: 0x0400448A RID: 17546
		public static List<BuffSeidJsonData93> DataList = new List<BuffSeidJsonData93>();

		// Token: 0x0400448B RID: 17547
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData93.OnInitFinish);

		// Token: 0x0400448C RID: 17548
		public int id;

		// Token: 0x0400448D RID: 17549
		public int value1;
	}
}
