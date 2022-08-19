using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020007FE RID: 2046
	public class BuffSeidJsonData93 : IJSONClass
	{
		// Token: 0x06003E0A RID: 15882 RVA: 0x001A8634 File Offset: 0x001A6834
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[93].list)
			{
				try
				{
					BuffSeidJsonData93 buffSeidJsonData = new BuffSeidJsonData93();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData93.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData93.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData93.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData93.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData93.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData93.OnInitFinishAction != null)
			{
				BuffSeidJsonData93.OnInitFinishAction();
			}
		}

		// Token: 0x06003E0B RID: 15883 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040038F8 RID: 14584
		public static int SEIDID = 93;

		// Token: 0x040038F9 RID: 14585
		public static Dictionary<int, BuffSeidJsonData93> DataDict = new Dictionary<int, BuffSeidJsonData93>();

		// Token: 0x040038FA RID: 14586
		public static List<BuffSeidJsonData93> DataList = new List<BuffSeidJsonData93>();

		// Token: 0x040038FB RID: 14587
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData93.OnInitFinish);

		// Token: 0x040038FC RID: 14588
		public int id;

		// Token: 0x040038FD RID: 14589
		public int value1;
	}
}
