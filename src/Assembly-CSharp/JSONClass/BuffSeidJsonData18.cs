using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000796 RID: 1942
	public class BuffSeidJsonData18 : IJSONClass
	{
		// Token: 0x06003C6A RID: 15466 RVA: 0x0019EED8 File Offset: 0x0019D0D8
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

		// Token: 0x06003C6B RID: 15467 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003623 RID: 13859
		public static int SEIDID = 18;

		// Token: 0x04003624 RID: 13860
		public static Dictionary<int, BuffSeidJsonData18> DataDict = new Dictionary<int, BuffSeidJsonData18>();

		// Token: 0x04003625 RID: 13861
		public static List<BuffSeidJsonData18> DataList = new List<BuffSeidJsonData18>();

		// Token: 0x04003626 RID: 13862
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData18.OnInitFinish);

		// Token: 0x04003627 RID: 13863
		public int id;

		// Token: 0x04003628 RID: 13864
		public int value1;
	}
}
