using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020007A6 RID: 1958
	public class BuffSeidJsonData201 : IJSONClass
	{
		// Token: 0x06003CAA RID: 15530 RVA: 0x001A0604 File Offset: 0x0019E804
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[201].list)
			{
				try
				{
					BuffSeidJsonData201 buffSeidJsonData = new BuffSeidJsonData201();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData201.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData201.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData201.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData201.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData201.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData201.OnInitFinishAction != null)
			{
				BuffSeidJsonData201.OnInitFinishAction();
			}
		}

		// Token: 0x06003CAB RID: 15531 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400368E RID: 13966
		public static int SEIDID = 201;

		// Token: 0x0400368F RID: 13967
		public static Dictionary<int, BuffSeidJsonData201> DataDict = new Dictionary<int, BuffSeidJsonData201>();

		// Token: 0x04003690 RID: 13968
		public static List<BuffSeidJsonData201> DataList = new List<BuffSeidJsonData201>();

		// Token: 0x04003691 RID: 13969
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData201.OnInitFinish);

		// Token: 0x04003692 RID: 13970
		public int id;

		// Token: 0x04003693 RID: 13971
		public int value1;
	}
}
