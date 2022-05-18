using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B2E RID: 2862
	public class BuffSeidJsonData18 : IJSONClass
	{
		// Token: 0x06004820 RID: 18464 RVA: 0x001EC158 File Offset: 0x001EA358
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[18].list)
			{
				try
				{
					BuffSeidJsonData18 buffSeidJsonData = new BuffSeidJsonData18();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData18.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData18.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData18.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData18.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData18.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData18.OnInitFinishAction != null)
			{
				BuffSeidJsonData18.OnInitFinishAction();
			}
		}

		// Token: 0x06004821 RID: 18465 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040041BC RID: 16828
		public static int SEIDID = 18;

		// Token: 0x040041BD RID: 16829
		public static Dictionary<int, BuffSeidJsonData18> DataDict = new Dictionary<int, BuffSeidJsonData18>();

		// Token: 0x040041BE RID: 16830
		public static List<BuffSeidJsonData18> DataList = new List<BuffSeidJsonData18>();

		// Token: 0x040041BF RID: 16831
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData18.OnInitFinish);

		// Token: 0x040041C0 RID: 16832
		public int id;

		// Token: 0x040041C1 RID: 16833
		public int value1;
	}
}
