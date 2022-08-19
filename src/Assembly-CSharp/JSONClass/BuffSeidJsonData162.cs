using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000786 RID: 1926
	public class BuffSeidJsonData162 : IJSONClass
	{
		// Token: 0x06003C2A RID: 15402 RVA: 0x0019D7FC File Offset: 0x0019B9FC
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[162].list)
			{
				try
				{
					BuffSeidJsonData162 buffSeidJsonData = new BuffSeidJsonData162();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].ToList();
					if (BuffSeidJsonData162.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData162.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData162.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData162.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData162.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData162.OnInitFinishAction != null)
			{
				BuffSeidJsonData162.OnInitFinishAction();
			}
		}

		// Token: 0x06003C2B RID: 15403 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040035BA RID: 13754
		public static int SEIDID = 162;

		// Token: 0x040035BB RID: 13755
		public static Dictionary<int, BuffSeidJsonData162> DataDict = new Dictionary<int, BuffSeidJsonData162>();

		// Token: 0x040035BC RID: 13756
		public static List<BuffSeidJsonData162> DataList = new List<BuffSeidJsonData162>();

		// Token: 0x040035BD RID: 13757
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData162.OnInitFinish);

		// Token: 0x040035BE RID: 13758
		public int id;

		// Token: 0x040035BF RID: 13759
		public List<int> value1 = new List<int>();
	}
}
