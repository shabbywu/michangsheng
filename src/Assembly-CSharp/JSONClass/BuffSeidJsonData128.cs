using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B04 RID: 2820
	public class BuffSeidJsonData128 : IJSONClass
	{
		// Token: 0x0600477A RID: 18298 RVA: 0x001E8DE0 File Offset: 0x001E6FE0
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

		// Token: 0x0600477B RID: 18299 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400409D RID: 16541
		public static int SEIDID = 128;

		// Token: 0x0400409E RID: 16542
		public static Dictionary<int, BuffSeidJsonData128> DataDict = new Dictionary<int, BuffSeidJsonData128>();

		// Token: 0x0400409F RID: 16543
		public static List<BuffSeidJsonData128> DataList = new List<BuffSeidJsonData128>();

		// Token: 0x040040A0 RID: 16544
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData128.OnInitFinish);

		// Token: 0x040040A1 RID: 16545
		public int id;

		// Token: 0x040040A2 RID: 16546
		public int value1;

		// Token: 0x040040A3 RID: 16547
		public List<int> value2 = new List<int>();
	}
}
