using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000798 RID: 1944
	public class BuffSeidJsonData185 : IJSONClass
	{
		// Token: 0x06003C72 RID: 15474 RVA: 0x0019F1A4 File Offset: 0x0019D3A4
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[185].list)
			{
				try
				{
					BuffSeidJsonData185 buffSeidJsonData = new BuffSeidJsonData185();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].ToList();
					buffSeidJsonData.value2 = jsonobject["value2"].ToList();
					if (BuffSeidJsonData185.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData185.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData185.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData185.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData185.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData185.OnInitFinishAction != null)
			{
				BuffSeidJsonData185.OnInitFinishAction();
			}
		}

		// Token: 0x06003C73 RID: 15475 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003630 RID: 13872
		public static int SEIDID = 185;

		// Token: 0x04003631 RID: 13873
		public static Dictionary<int, BuffSeidJsonData185> DataDict = new Dictionary<int, BuffSeidJsonData185>();

		// Token: 0x04003632 RID: 13874
		public static List<BuffSeidJsonData185> DataList = new List<BuffSeidJsonData185>();

		// Token: 0x04003633 RID: 13875
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData185.OnInitFinish);

		// Token: 0x04003634 RID: 13876
		public int id;

		// Token: 0x04003635 RID: 13877
		public List<int> value1 = new List<int>();

		// Token: 0x04003636 RID: 13878
		public List<int> value2 = new List<int>();
	}
}
