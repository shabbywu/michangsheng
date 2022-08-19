using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000891 RID: 2193
	public class NanZuJianBiao : IJSONClass
	{
		// Token: 0x06004057 RID: 16471 RVA: 0x001B74C4 File Offset: 0x001B56C4
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

		// Token: 0x06004058 RID: 16472 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003DAC RID: 15788
		public static Dictionary<int, NanZuJianBiao> DataDict = new Dictionary<int, NanZuJianBiao>();

		// Token: 0x04003DAD RID: 15789
		public static List<NanZuJianBiao> DataList = new List<NanZuJianBiao>();

		// Token: 0x04003DAE RID: 15790
		public static Action OnInitFinishAction = new Action(NanZuJianBiao.OnInitFinish);

		// Token: 0x04003DAF RID: 15791
		public int id;

		// Token: 0x04003DB0 RID: 15792
		public string StrID;
	}
}
