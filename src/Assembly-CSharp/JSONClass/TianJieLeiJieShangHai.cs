using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000976 RID: 2422
	public class TianJieLeiJieShangHai : IJSONClass
	{
		// Token: 0x060043EC RID: 17388 RVA: 0x001CEEE0 File Offset: 0x001CD0E0
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.TianJieLeiJieShangHai.list)
			{
				try
				{
					TianJieLeiJieShangHai tianJieLeiJieShangHai = new TianJieLeiJieShangHai();
					tianJieLeiJieShangHai.id = jsonobject["id"].I;
					tianJieLeiJieShangHai.Damage = jsonobject["Damage"].I;
					if (TianJieLeiJieShangHai.DataDict.ContainsKey(tianJieLeiJieShangHai.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典TianJieLeiJieShangHai.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", tianJieLeiJieShangHai.id));
					}
					else
					{
						TianJieLeiJieShangHai.DataDict.Add(tianJieLeiJieShangHai.id, tianJieLeiJieShangHai);
						TianJieLeiJieShangHai.DataList.Add(tianJieLeiJieShangHai);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典TianJieLeiJieShangHai.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (TianJieLeiJieShangHai.OnInitFinishAction != null)
			{
				TianJieLeiJieShangHai.OnInitFinishAction();
			}
		}

		// Token: 0x060043ED RID: 17389 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004536 RID: 17718
		public static Dictionary<int, TianJieLeiJieShangHai> DataDict = new Dictionary<int, TianJieLeiJieShangHai>();

		// Token: 0x04004537 RID: 17719
		public static List<TianJieLeiJieShangHai> DataList = new List<TianJieLeiJieShangHai>();

		// Token: 0x04004538 RID: 17720
		public static Action OnInitFinishAction = new Action(TianJieLeiJieShangHai.OnInitFinish);

		// Token: 0x04004539 RID: 17721
		public int id;

		// Token: 0x0400453A RID: 17722
		public int Damage;
	}
}
