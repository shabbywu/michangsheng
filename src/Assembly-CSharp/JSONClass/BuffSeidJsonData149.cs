using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200077D RID: 1917
	public class BuffSeidJsonData149 : IJSONClass
	{
		// Token: 0x06003C08 RID: 15368 RVA: 0x0019CBF8 File Offset: 0x0019ADF8
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[149].list)
			{
				try
				{
					BuffSeidJsonData149 buffSeidJsonData = new BuffSeidJsonData149();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].ToList();
					if (BuffSeidJsonData149.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData149.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData149.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData149.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData149.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData149.OnInitFinishAction != null)
			{
				BuffSeidJsonData149.OnInitFinishAction();
			}
		}

		// Token: 0x06003C09 RID: 15369 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400357E RID: 13694
		public static int SEIDID = 149;

		// Token: 0x0400357F RID: 13695
		public static Dictionary<int, BuffSeidJsonData149> DataDict = new Dictionary<int, BuffSeidJsonData149>();

		// Token: 0x04003580 RID: 13696
		public static List<BuffSeidJsonData149> DataList = new List<BuffSeidJsonData149>();

		// Token: 0x04003581 RID: 13697
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData149.OnInitFinish);

		// Token: 0x04003582 RID: 13698
		public int id;

		// Token: 0x04003583 RID: 13699
		public List<int> value1 = new List<int>();
	}
}
