using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200079B RID: 1947
	public class BuffSeidJsonData188 : IJSONClass
	{
		// Token: 0x06003C7E RID: 15486 RVA: 0x0019F608 File Offset: 0x0019D808
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[188].list)
			{
				try
				{
					BuffSeidJsonData188 buffSeidJsonData = new BuffSeidJsonData188();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData188.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData188.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData188.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData188.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData188.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData188.OnInitFinishAction != null)
			{
				BuffSeidJsonData188.OnInitFinishAction();
			}
		}

		// Token: 0x06003C7F RID: 15487 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003644 RID: 13892
		public static int SEIDID = 188;

		// Token: 0x04003645 RID: 13893
		public static Dictionary<int, BuffSeidJsonData188> DataDict = new Dictionary<int, BuffSeidJsonData188>();

		// Token: 0x04003646 RID: 13894
		public static List<BuffSeidJsonData188> DataList = new List<BuffSeidJsonData188>();

		// Token: 0x04003647 RID: 13895
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData188.OnInitFinish);

		// Token: 0x04003648 RID: 13896
		public int id;

		// Token: 0x04003649 RID: 13897
		public int value1;
	}
}
