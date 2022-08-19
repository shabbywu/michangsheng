using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020007E2 RID: 2018
	public class BuffSeidJsonData63 : IJSONClass
	{
		// Token: 0x06003D9A RID: 15770 RVA: 0x001A5EE4 File Offset: 0x001A40E4
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[63].list)
			{
				try
				{
					BuffSeidJsonData63 buffSeidJsonData = new BuffSeidJsonData63();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData63.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData63.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData63.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData63.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData63.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData63.OnInitFinishAction != null)
			{
				BuffSeidJsonData63.OnInitFinishAction();
			}
		}

		// Token: 0x06003D9B RID: 15771 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003840 RID: 14400
		public static int SEIDID = 63;

		// Token: 0x04003841 RID: 14401
		public static Dictionary<int, BuffSeidJsonData63> DataDict = new Dictionary<int, BuffSeidJsonData63>();

		// Token: 0x04003842 RID: 14402
		public static List<BuffSeidJsonData63> DataList = new List<BuffSeidJsonData63>();

		// Token: 0x04003843 RID: 14403
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData63.OnInitFinish);

		// Token: 0x04003844 RID: 14404
		public int id;

		// Token: 0x04003845 RID: 14405
		public int value1;
	}
}
