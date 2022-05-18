using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B4E RID: 2894
	public class BuffSeidJsonData24 : IJSONClass
	{
		// Token: 0x060048A0 RID: 18592 RVA: 0x001EE87C File Offset: 0x001ECA7C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[24].list)
			{
				try
				{
					BuffSeidJsonData24 buffSeidJsonData = new BuffSeidJsonData24();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData24.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData24.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData24.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData24.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData24.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData24.OnInitFinishAction != null)
			{
				BuffSeidJsonData24.OnInitFinishAction();
			}
		}

		// Token: 0x060048A1 RID: 18593 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400428F RID: 17039
		public static int SEIDID = 24;

		// Token: 0x04004290 RID: 17040
		public static Dictionary<int, BuffSeidJsonData24> DataDict = new Dictionary<int, BuffSeidJsonData24>();

		// Token: 0x04004291 RID: 17041
		public static List<BuffSeidJsonData24> DataList = new List<BuffSeidJsonData24>();

		// Token: 0x04004292 RID: 17042
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData24.OnInitFinish);

		// Token: 0x04004293 RID: 17043
		public int id;

		// Token: 0x04004294 RID: 17044
		public int value1;
	}
}
