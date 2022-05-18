using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B50 RID: 2896
	public class BuffSeidJsonData26 : IJSONClass
	{
		// Token: 0x060048A8 RID: 18600 RVA: 0x001EEAE0 File Offset: 0x001ECCE0
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[26].list)
			{
				try
				{
					BuffSeidJsonData26 buffSeidJsonData = new BuffSeidJsonData26();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					if (BuffSeidJsonData26.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData26.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData26.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData26.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData26.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData26.OnInitFinishAction != null)
			{
				BuffSeidJsonData26.OnInitFinishAction();
			}
		}

		// Token: 0x060048A9 RID: 18601 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400429C RID: 17052
		public static int SEIDID = 26;

		// Token: 0x0400429D RID: 17053
		public static Dictionary<int, BuffSeidJsonData26> DataDict = new Dictionary<int, BuffSeidJsonData26>();

		// Token: 0x0400429E RID: 17054
		public static List<BuffSeidJsonData26> DataList = new List<BuffSeidJsonData26>();

		// Token: 0x0400429F RID: 17055
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData26.OnInitFinish);

		// Token: 0x040042A0 RID: 17056
		public int id;

		// Token: 0x040042A1 RID: 17057
		public int value1;

		// Token: 0x040042A2 RID: 17058
		public int value2;
	}
}
