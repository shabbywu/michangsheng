using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200077C RID: 1916
	public class BuffSeidJsonData148 : IJSONClass
	{
		// Token: 0x06003C04 RID: 15364 RVA: 0x0019CA98 File Offset: 0x0019AC98
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[148].list)
			{
				try
				{
					BuffSeidJsonData148 buffSeidJsonData = new BuffSeidJsonData148();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData148.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData148.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData148.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData148.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData148.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData148.OnInitFinishAction != null)
			{
				BuffSeidJsonData148.OnInitFinishAction();
			}
		}

		// Token: 0x06003C05 RID: 15365 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003578 RID: 13688
		public static int SEIDID = 148;

		// Token: 0x04003579 RID: 13689
		public static Dictionary<int, BuffSeidJsonData148> DataDict = new Dictionary<int, BuffSeidJsonData148>();

		// Token: 0x0400357A RID: 13690
		public static List<BuffSeidJsonData148> DataList = new List<BuffSeidJsonData148>();

		// Token: 0x0400357B RID: 13691
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData148.OnInitFinish);

		// Token: 0x0400357C RID: 13692
		public int id;

		// Token: 0x0400357D RID: 13693
		public int value1;
	}
}
