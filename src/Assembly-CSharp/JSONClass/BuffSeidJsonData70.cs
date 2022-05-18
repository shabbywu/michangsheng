using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B81 RID: 2945
	public class BuffSeidJsonData70 : IJSONClass
	{
		// Token: 0x0600496C RID: 18796 RVA: 0x001F2A44 File Offset: 0x001F0C44
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[70].list)
			{
				try
				{
					BuffSeidJsonData70 buffSeidJsonData = new BuffSeidJsonData70();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].ToList();
					if (BuffSeidJsonData70.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData70.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData70.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData70.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData70.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData70.OnInitFinishAction != null)
			{
				BuffSeidJsonData70.OnInitFinishAction();
			}
		}

		// Token: 0x0600496D RID: 18797 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004406 RID: 17414
		public static int SEIDID = 70;

		// Token: 0x04004407 RID: 17415
		public static Dictionary<int, BuffSeidJsonData70> DataDict = new Dictionary<int, BuffSeidJsonData70>();

		// Token: 0x04004408 RID: 17416
		public static List<BuffSeidJsonData70> DataList = new List<BuffSeidJsonData70>();

		// Token: 0x04004409 RID: 17417
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData70.OnInitFinish);

		// Token: 0x0400440A RID: 17418
		public int id;

		// Token: 0x0400440B RID: 17419
		public int value2;

		// Token: 0x0400440C RID: 17420
		public List<int> value1 = new List<int>();
	}
}
