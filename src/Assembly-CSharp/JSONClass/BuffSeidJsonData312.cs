using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B56 RID: 2902
	public class BuffSeidJsonData312 : IJSONClass
	{
		// Token: 0x060048C0 RID: 18624 RVA: 0x001EF234 File Offset: 0x001ED434
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[312].list)
			{
				try
				{
					BuffSeidJsonData312 buffSeidJsonData = new BuffSeidJsonData312();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.panduan = jsonobject["panduan"].Str;
					if (BuffSeidJsonData312.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData312.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData312.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData312.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData312.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData312.OnInitFinishAction != null)
			{
				BuffSeidJsonData312.OnInitFinishAction();
			}
		}

		// Token: 0x060048C1 RID: 18625 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040042C5 RID: 17093
		public static int SEIDID = 312;

		// Token: 0x040042C6 RID: 17094
		public static Dictionary<int, BuffSeidJsonData312> DataDict = new Dictionary<int, BuffSeidJsonData312>();

		// Token: 0x040042C7 RID: 17095
		public static List<BuffSeidJsonData312> DataList = new List<BuffSeidJsonData312>();

		// Token: 0x040042C8 RID: 17096
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData312.OnInitFinish);

		// Token: 0x040042C9 RID: 17097
		public int id;

		// Token: 0x040042CA RID: 17098
		public int value1;

		// Token: 0x040042CB RID: 17099
		public string panduan;
	}
}
