using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000791 RID: 1937
	public class BuffSeidJsonData173 : IJSONClass
	{
		// Token: 0x06003C56 RID: 15446 RVA: 0x0019E7D0 File Offset: 0x0019C9D0
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[173].list)
			{
				try
				{
					BuffSeidJsonData173 buffSeidJsonData = new BuffSeidJsonData173();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData173.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData173.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData173.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData173.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData173.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData173.OnInitFinishAction != null)
			{
				BuffSeidJsonData173.OnInitFinishAction();
			}
		}

		// Token: 0x06003C57 RID: 15447 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003603 RID: 13827
		public static int SEIDID = 173;

		// Token: 0x04003604 RID: 13828
		public static Dictionary<int, BuffSeidJsonData173> DataDict = new Dictionary<int, BuffSeidJsonData173>();

		// Token: 0x04003605 RID: 13829
		public static List<BuffSeidJsonData173> DataList = new List<BuffSeidJsonData173>();

		// Token: 0x04003606 RID: 13830
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData173.OnInitFinish);

		// Token: 0x04003607 RID: 13831
		public int id;

		// Token: 0x04003608 RID: 13832
		public int value1;
	}
}
