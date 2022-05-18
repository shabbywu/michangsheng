using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B0B RID: 2827
	public class BuffSeidJsonData137 : IJSONClass
	{
		// Token: 0x06004796 RID: 18326 RVA: 0x001E9700 File Offset: 0x001E7900
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[137].list)
			{
				try
				{
					BuffSeidJsonData137 buffSeidJsonData = new BuffSeidJsonData137();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData137.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData137.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData137.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData137.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData137.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData137.OnInitFinishAction != null)
			{
				BuffSeidJsonData137.OnInitFinishAction();
			}
		}

		// Token: 0x06004797 RID: 18327 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040040D2 RID: 16594
		public static int SEIDID = 137;

		// Token: 0x040040D3 RID: 16595
		public static Dictionary<int, BuffSeidJsonData137> DataDict = new Dictionary<int, BuffSeidJsonData137>();

		// Token: 0x040040D4 RID: 16596
		public static List<BuffSeidJsonData137> DataList = new List<BuffSeidJsonData137>();

		// Token: 0x040040D5 RID: 16597
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData137.OnInitFinish);

		// Token: 0x040040D6 RID: 16598
		public int id;

		// Token: 0x040040D7 RID: 16599
		public int value1;
	}
}
