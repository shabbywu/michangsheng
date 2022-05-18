using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B36 RID: 2870
	public class BuffSeidJsonData192 : IJSONClass
	{
		// Token: 0x06004840 RID: 18496 RVA: 0x001ECB04 File Offset: 0x001EAD04
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[192].list)
			{
				try
				{
					BuffSeidJsonData192 buffSeidJsonData = new BuffSeidJsonData192();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					if (BuffSeidJsonData192.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData192.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData192.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData192.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData192.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData192.OnInitFinishAction != null)
			{
				BuffSeidJsonData192.OnInitFinishAction();
			}
		}

		// Token: 0x06004841 RID: 18497 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040041F0 RID: 16880
		public static int SEIDID = 192;

		// Token: 0x040041F1 RID: 16881
		public static Dictionary<int, BuffSeidJsonData192> DataDict = new Dictionary<int, BuffSeidJsonData192>();

		// Token: 0x040041F2 RID: 16882
		public static List<BuffSeidJsonData192> DataList = new List<BuffSeidJsonData192>();

		// Token: 0x040041F3 RID: 16883
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData192.OnInitFinish);

		// Token: 0x040041F4 RID: 16884
		public int id;

		// Token: 0x040041F5 RID: 16885
		public int value1;

		// Token: 0x040041F6 RID: 16886
		public int value2;
	}
}
