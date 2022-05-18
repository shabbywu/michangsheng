using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B85 RID: 2949
	public class BuffSeidJsonData75 : IJSONClass
	{
		// Token: 0x0600497C RID: 18812 RVA: 0x001F2F0C File Offset: 0x001F110C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[75].list)
			{
				try
				{
					BuffSeidJsonData75 buffSeidJsonData = new BuffSeidJsonData75();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].ToList();
					if (BuffSeidJsonData75.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData75.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData75.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData75.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData75.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData75.OnInitFinishAction != null)
			{
				BuffSeidJsonData75.OnInitFinishAction();
			}
		}

		// Token: 0x0600497D RID: 18813 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004420 RID: 17440
		public static int SEIDID = 75;

		// Token: 0x04004421 RID: 17441
		public static Dictionary<int, BuffSeidJsonData75> DataDict = new Dictionary<int, BuffSeidJsonData75>();

		// Token: 0x04004422 RID: 17442
		public static List<BuffSeidJsonData75> DataList = new List<BuffSeidJsonData75>();

		// Token: 0x04004423 RID: 17443
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData75.OnInitFinish);

		// Token: 0x04004424 RID: 17444
		public int id;

		// Token: 0x04004425 RID: 17445
		public List<int> value1 = new List<int>();
	}
}
