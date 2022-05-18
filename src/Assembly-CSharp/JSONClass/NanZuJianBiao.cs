using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C1F RID: 3103
	public class NanZuJianBiao : IJSONClass
	{
		// Token: 0x06004BE5 RID: 19429 RVA: 0x00200970 File Offset: 0x001FEB70
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.NanZuJianBiao.list)
			{
				try
				{
					NanZuJianBiao nanZuJianBiao = new NanZuJianBiao();
					nanZuJianBiao.id = jsonobject["id"].I;
					nanZuJianBiao.StrID = jsonobject["StrID"].Str;
					if (NanZuJianBiao.DataDict.ContainsKey(nanZuJianBiao.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典NanZuJianBiao.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", nanZuJianBiao.id));
					}
					else
					{
						NanZuJianBiao.DataDict.Add(nanZuJianBiao.id, nanZuJianBiao);
						NanZuJianBiao.DataList.Add(nanZuJianBiao);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典NanZuJianBiao.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (NanZuJianBiao.OnInitFinishAction != null)
			{
				NanZuJianBiao.OnInitFinishAction();
			}
		}

		// Token: 0x06004BE6 RID: 19430 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004905 RID: 18693
		public static Dictionary<int, NanZuJianBiao> DataDict = new Dictionary<int, NanZuJianBiao>();

		// Token: 0x04004906 RID: 18694
		public static List<NanZuJianBiao> DataList = new List<NanZuJianBiao>();

		// Token: 0x04004907 RID: 18695
		public static Action OnInitFinishAction = new Action(NanZuJianBiao.OnInitFinish);

		// Token: 0x04004908 RID: 18696
		public int id;

		// Token: 0x04004909 RID: 18697
		public string StrID;
	}
}
