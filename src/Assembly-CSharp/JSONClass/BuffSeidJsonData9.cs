using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B92 RID: 2962
	public class BuffSeidJsonData9 : IJSONClass
	{
		// Token: 0x060049B0 RID: 18864 RVA: 0x001F3E90 File Offset: 0x001F2090
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[9].list)
			{
				try
				{
					BuffSeidJsonData9 buffSeidJsonData = new BuffSeidJsonData9();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].ToList();
					if (BuffSeidJsonData9.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData9.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData9.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData9.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData9.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData9.OnInitFinishAction != null)
			{
				BuffSeidJsonData9.OnInitFinishAction();
			}
		}

		// Token: 0x060049B1 RID: 18865 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004474 RID: 17524
		public static int SEIDID = 9;

		// Token: 0x04004475 RID: 17525
		public static Dictionary<int, BuffSeidJsonData9> DataDict = new Dictionary<int, BuffSeidJsonData9>();

		// Token: 0x04004476 RID: 17526
		public static List<BuffSeidJsonData9> DataList = new List<BuffSeidJsonData9>();

		// Token: 0x04004477 RID: 17527
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData9.OnInitFinish);

		// Token: 0x04004478 RID: 17528
		public int id;

		// Token: 0x04004479 RID: 17529
		public int value2;

		// Token: 0x0400447A RID: 17530
		public List<int> value1 = new List<int>();
	}
}
