using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B5B RID: 2907
	public class BuffSeidJsonData33 : IJSONClass
	{
		// Token: 0x060048D4 RID: 18644 RVA: 0x001EF8A0 File Offset: 0x001EDAA0
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[33].list)
			{
				try
				{
					BuffSeidJsonData33 buffSeidJsonData = new BuffSeidJsonData33();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					buffSeidJsonData.value3 = jsonobject["value3"].I;
					if (BuffSeidJsonData33.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData33.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData33.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData33.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData33.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData33.OnInitFinishAction != null)
			{
				BuffSeidJsonData33.OnInitFinishAction();
			}
		}

		// Token: 0x060048D5 RID: 18645 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040042EA RID: 17130
		public static int SEIDID = 33;

		// Token: 0x040042EB RID: 17131
		public static Dictionary<int, BuffSeidJsonData33> DataDict = new Dictionary<int, BuffSeidJsonData33>();

		// Token: 0x040042EC RID: 17132
		public static List<BuffSeidJsonData33> DataList = new List<BuffSeidJsonData33>();

		// Token: 0x040042ED RID: 17133
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData33.OnInitFinish);

		// Token: 0x040042EE RID: 17134
		public int id;

		// Token: 0x040042EF RID: 17135
		public int value1;

		// Token: 0x040042F0 RID: 17136
		public int value2;

		// Token: 0x040042F1 RID: 17137
		public int value3;
	}
}
