using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B86 RID: 2950
	public class BuffSeidJsonData76 : IJSONClass
	{
		// Token: 0x06004980 RID: 18816 RVA: 0x001F3034 File Offset: 0x001F1234
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[76].list)
			{
				try
				{
					BuffSeidJsonData76 buffSeidJsonData = new BuffSeidJsonData76();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData76.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData76.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData76.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData76.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData76.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData76.OnInitFinishAction != null)
			{
				BuffSeidJsonData76.OnInitFinishAction();
			}
		}

		// Token: 0x06004981 RID: 18817 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004426 RID: 17446
		public static int SEIDID = 76;

		// Token: 0x04004427 RID: 17447
		public static Dictionary<int, BuffSeidJsonData76> DataDict = new Dictionary<int, BuffSeidJsonData76>();

		// Token: 0x04004428 RID: 17448
		public static List<BuffSeidJsonData76> DataList = new List<BuffSeidJsonData76>();

		// Token: 0x04004429 RID: 17449
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData76.OnInitFinish);

		// Token: 0x0400442A RID: 17450
		public int id;

		// Token: 0x0400442B RID: 17451
		public int value1;
	}
}
