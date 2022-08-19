using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000885 RID: 2181
	public class LunDaoReduceData : IJSONClass
	{
		// Token: 0x06004027 RID: 16423 RVA: 0x001B5F4C File Offset: 0x001B414C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.LunDaoReduceData.list)
			{
				try
				{
					LunDaoReduceData lunDaoReduceData = new LunDaoReduceData();
					lunDaoReduceData.id = jsonobject["id"].I;
					lunDaoReduceData.ShuaiJianXiShu = jsonobject["ShuaiJianXiShu"].I;
					if (LunDaoReduceData.DataDict.ContainsKey(lunDaoReduceData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典LunDaoReduceData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", lunDaoReduceData.id));
					}
					else
					{
						LunDaoReduceData.DataDict.Add(lunDaoReduceData.id, lunDaoReduceData);
						LunDaoReduceData.DataList.Add(lunDaoReduceData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典LunDaoReduceData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (LunDaoReduceData.OnInitFinishAction != null)
			{
				LunDaoReduceData.OnInitFinishAction();
			}
		}

		// Token: 0x06004028 RID: 16424 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003D38 RID: 15672
		public static Dictionary<int, LunDaoReduceData> DataDict = new Dictionary<int, LunDaoReduceData>();

		// Token: 0x04003D39 RID: 15673
		public static List<LunDaoReduceData> DataList = new List<LunDaoReduceData>();

		// Token: 0x04003D3A RID: 15674
		public static Action OnInitFinishAction = new Action(LunDaoReduceData.OnInitFinish);

		// Token: 0x04003D3B RID: 15675
		public int id;

		// Token: 0x04003D3C RID: 15676
		public int ShuaiJianXiShu;
	}
}
