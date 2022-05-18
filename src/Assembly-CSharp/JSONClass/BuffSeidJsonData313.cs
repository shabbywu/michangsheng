using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B57 RID: 2903
	public class BuffSeidJsonData313 : IJSONClass
	{
		// Token: 0x060048C4 RID: 18628 RVA: 0x001EF374 File Offset: 0x001ED574
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[313].list)
			{
				try
				{
					BuffSeidJsonData313 buffSeidJsonData = new BuffSeidJsonData313();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.target = jsonobject["target"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData313.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData313.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData313.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData313.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData313.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData313.OnInitFinishAction != null)
			{
				BuffSeidJsonData313.OnInitFinishAction();
			}
		}

		// Token: 0x060048C5 RID: 18629 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040042CC RID: 17100
		public static int SEIDID = 313;

		// Token: 0x040042CD RID: 17101
		public static Dictionary<int, BuffSeidJsonData313> DataDict = new Dictionary<int, BuffSeidJsonData313>();

		// Token: 0x040042CE RID: 17102
		public static List<BuffSeidJsonData313> DataList = new List<BuffSeidJsonData313>();

		// Token: 0x040042CF RID: 17103
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData313.OnInitFinish);

		// Token: 0x040042D0 RID: 17104
		public int id;

		// Token: 0x040042D1 RID: 17105
		public int target;

		// Token: 0x040042D2 RID: 17106
		public int value1;
	}
}
