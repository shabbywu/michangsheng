using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B73 RID: 2931
	public class BuffSeidJsonData57 : IJSONClass
	{
		// Token: 0x06004934 RID: 18740 RVA: 0x001F196C File Offset: 0x001EFB6C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[57].list)
			{
				try
				{
					BuffSeidJsonData57 buffSeidJsonData = new BuffSeidJsonData57();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					if (BuffSeidJsonData57.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData57.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData57.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData57.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData57.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData57.OnInitFinishAction != null)
			{
				BuffSeidJsonData57.OnInitFinishAction();
			}
		}

		// Token: 0x06004935 RID: 18741 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040043AB RID: 17323
		public static int SEIDID = 57;

		// Token: 0x040043AC RID: 17324
		public static Dictionary<int, BuffSeidJsonData57> DataDict = new Dictionary<int, BuffSeidJsonData57>();

		// Token: 0x040043AD RID: 17325
		public static List<BuffSeidJsonData57> DataList = new List<BuffSeidJsonData57>();

		// Token: 0x040043AE RID: 17326
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData57.OnInitFinish);

		// Token: 0x040043AF RID: 17327
		public int id;

		// Token: 0x040043B0 RID: 17328
		public int value1;

		// Token: 0x040043B1 RID: 17329
		public int value2;
	}
}
