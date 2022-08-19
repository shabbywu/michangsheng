using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200076C RID: 1900
	public class BuffSeidJsonData128 : IJSONClass
	{
		// Token: 0x06003BC4 RID: 15300 RVA: 0x0019B290 File Offset: 0x00199490
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[128].list)
			{
				try
				{
					BuffSeidJsonData128 buffSeidJsonData = new BuffSeidJsonData128();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].ToList();
					if (BuffSeidJsonData128.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData128.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData128.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData128.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData128.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData128.OnInitFinishAction != null)
			{
				BuffSeidJsonData128.OnInitFinishAction();
			}
		}

		// Token: 0x06003BC5 RID: 15301 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003504 RID: 13572
		public static int SEIDID = 128;

		// Token: 0x04003505 RID: 13573
		public static Dictionary<int, BuffSeidJsonData128> DataDict = new Dictionary<int, BuffSeidJsonData128>();

		// Token: 0x04003506 RID: 13574
		public static List<BuffSeidJsonData128> DataList = new List<BuffSeidJsonData128>();

		// Token: 0x04003507 RID: 13575
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData128.OnInitFinish);

		// Token: 0x04003508 RID: 13576
		public int id;

		// Token: 0x04003509 RID: 13577
		public int value1;

		// Token: 0x0400350A RID: 13578
		public List<int> value2 = new List<int>();
	}
}
