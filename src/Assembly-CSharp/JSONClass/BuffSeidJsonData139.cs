using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000775 RID: 1909
	public class BuffSeidJsonData139 : IJSONClass
	{
		// Token: 0x06003BE8 RID: 15336 RVA: 0x0019C060 File Offset: 0x0019A260
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[139].list)
			{
				try
				{
					BuffSeidJsonData139 buffSeidJsonData = new BuffSeidJsonData139();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					if (BuffSeidJsonData139.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData139.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData139.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData139.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData139.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData139.OnInitFinishAction != null)
			{
				BuffSeidJsonData139.OnInitFinishAction();
			}
		}

		// Token: 0x06003BE9 RID: 15337 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003547 RID: 13639
		public static int SEIDID = 139;

		// Token: 0x04003548 RID: 13640
		public static Dictionary<int, BuffSeidJsonData139> DataDict = new Dictionary<int, BuffSeidJsonData139>();

		// Token: 0x04003549 RID: 13641
		public static List<BuffSeidJsonData139> DataList = new List<BuffSeidJsonData139>();

		// Token: 0x0400354A RID: 13642
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData139.OnInitFinish);

		// Token: 0x0400354B RID: 13643
		public int id;

		// Token: 0x0400354C RID: 13644
		public int value2;
	}
}
