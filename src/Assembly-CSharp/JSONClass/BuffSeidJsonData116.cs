using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000764 RID: 1892
	public class BuffSeidJsonData116 : IJSONClass
	{
		// Token: 0x06003BA4 RID: 15268 RVA: 0x0019A74C File Offset: 0x0019894C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[116].list)
			{
				try
				{
					BuffSeidJsonData116 buffSeidJsonData = new BuffSeidJsonData116();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData116.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData116.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData116.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData116.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData116.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData116.OnInitFinishAction != null)
			{
				BuffSeidJsonData116.OnInitFinishAction();
			}
		}

		// Token: 0x06003BA5 RID: 15269 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040034CF RID: 13519
		public static int SEIDID = 116;

		// Token: 0x040034D0 RID: 13520
		public static Dictionary<int, BuffSeidJsonData116> DataDict = new Dictionary<int, BuffSeidJsonData116>();

		// Token: 0x040034D1 RID: 13521
		public static List<BuffSeidJsonData116> DataList = new List<BuffSeidJsonData116>();

		// Token: 0x040034D2 RID: 13522
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData116.OnInitFinish);

		// Token: 0x040034D3 RID: 13523
		public int id;

		// Token: 0x040034D4 RID: 13524
		public int value1;
	}
}
