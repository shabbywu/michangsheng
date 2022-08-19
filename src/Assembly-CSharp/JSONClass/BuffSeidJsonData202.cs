using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020007A7 RID: 1959
	public class BuffSeidJsonData202 : IJSONClass
	{
		// Token: 0x06003CAE RID: 15534 RVA: 0x001A0764 File Offset: 0x0019E964
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[202].list)
			{
				try
				{
					BuffSeidJsonData202 buffSeidJsonData = new BuffSeidJsonData202();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData202.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData202.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData202.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData202.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData202.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData202.OnInitFinishAction != null)
			{
				BuffSeidJsonData202.OnInitFinishAction();
			}
		}

		// Token: 0x06003CAF RID: 15535 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003694 RID: 13972
		public static int SEIDID = 202;

		// Token: 0x04003695 RID: 13973
		public static Dictionary<int, BuffSeidJsonData202> DataDict = new Dictionary<int, BuffSeidJsonData202>();

		// Token: 0x04003696 RID: 13974
		public static List<BuffSeidJsonData202> DataList = new List<BuffSeidJsonData202>();

		// Token: 0x04003697 RID: 13975
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData202.OnInitFinish);

		// Token: 0x04003698 RID: 13976
		public int id;

		// Token: 0x04003699 RID: 13977
		public int value1;
	}
}
