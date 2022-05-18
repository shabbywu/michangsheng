using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B54 RID: 2900
	public class BuffSeidJsonData30 : IJSONClass
	{
		// Token: 0x060048B8 RID: 18616 RVA: 0x001EEFBC File Offset: 0x001ED1BC
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[30].list)
			{
				try
				{
					BuffSeidJsonData30 buffSeidJsonData = new BuffSeidJsonData30();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					if (BuffSeidJsonData30.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData30.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData30.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData30.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData30.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData30.OnInitFinishAction != null)
			{
				BuffSeidJsonData30.OnInitFinishAction();
			}
		}

		// Token: 0x060048B9 RID: 18617 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040042B7 RID: 17079
		public static int SEIDID = 30;

		// Token: 0x040042B8 RID: 17080
		public static Dictionary<int, BuffSeidJsonData30> DataDict = new Dictionary<int, BuffSeidJsonData30>();

		// Token: 0x040042B9 RID: 17081
		public static List<BuffSeidJsonData30> DataList = new List<BuffSeidJsonData30>();

		// Token: 0x040042BA RID: 17082
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData30.OnInitFinish);

		// Token: 0x040042BB RID: 17083
		public int id;

		// Token: 0x040042BC RID: 17084
		public int value1;

		// Token: 0x040042BD RID: 17085
		public int value2;
	}
}
