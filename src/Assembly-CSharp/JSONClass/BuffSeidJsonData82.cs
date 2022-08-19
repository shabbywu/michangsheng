using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020007F3 RID: 2035
	public class BuffSeidJsonData82 : IJSONClass
	{
		// Token: 0x06003DDE RID: 15838 RVA: 0x001A76CC File Offset: 0x001A58CC
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[82].list)
			{
				try
				{
					BuffSeidJsonData82 buffSeidJsonData = new BuffSeidJsonData82();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					if (BuffSeidJsonData82.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData82.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData82.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData82.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData82.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData82.OnInitFinishAction != null)
			{
				BuffSeidJsonData82.OnInitFinishAction();
			}
		}

		// Token: 0x06003DDF RID: 15839 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040038B0 RID: 14512
		public static int SEIDID = 82;

		// Token: 0x040038B1 RID: 14513
		public static Dictionary<int, BuffSeidJsonData82> DataDict = new Dictionary<int, BuffSeidJsonData82>();

		// Token: 0x040038B2 RID: 14514
		public static List<BuffSeidJsonData82> DataList = new List<BuffSeidJsonData82>();

		// Token: 0x040038B3 RID: 14515
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData82.OnInitFinish);

		// Token: 0x040038B4 RID: 14516
		public int id;

		// Token: 0x040038B5 RID: 14517
		public int value1;

		// Token: 0x040038B6 RID: 14518
		public int value2;
	}
}
