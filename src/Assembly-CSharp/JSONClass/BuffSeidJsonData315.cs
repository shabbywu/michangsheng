using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B59 RID: 2905
	public class BuffSeidJsonData315 : IJSONClass
	{
		// Token: 0x060048CC RID: 18636 RVA: 0x001EF60C File Offset: 0x001ED80C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[315].list)
			{
				try
				{
					BuffSeidJsonData315 buffSeidJsonData = new BuffSeidJsonData315();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					if (BuffSeidJsonData315.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData315.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData315.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData315.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData315.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData315.OnInitFinishAction != null)
			{
				BuffSeidJsonData315.OnInitFinishAction();
			}
		}

		// Token: 0x060048CD RID: 18637 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040042DB RID: 17115
		public static int SEIDID = 315;

		// Token: 0x040042DC RID: 17116
		public static Dictionary<int, BuffSeidJsonData315> DataDict = new Dictionary<int, BuffSeidJsonData315>();

		// Token: 0x040042DD RID: 17117
		public static List<BuffSeidJsonData315> DataList = new List<BuffSeidJsonData315>();

		// Token: 0x040042DE RID: 17118
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData315.OnInitFinish);

		// Token: 0x040042DF RID: 17119
		public int id;

		// Token: 0x040042E0 RID: 17120
		public int value1;

		// Token: 0x040042E1 RID: 17121
		public int value2;
	}
}
