using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C13 RID: 3091
	public class LunDaoReduceData : IJSONClass
	{
		// Token: 0x06004BB5 RID: 19381 RVA: 0x001FF620 File Offset: 0x001FD820
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

		// Token: 0x06004BB6 RID: 19382 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004891 RID: 18577
		public static Dictionary<int, LunDaoReduceData> DataDict = new Dictionary<int, LunDaoReduceData>();

		// Token: 0x04004892 RID: 18578
		public static List<LunDaoReduceData> DataList = new List<LunDaoReduceData>();

		// Token: 0x04004893 RID: 18579
		public static Action OnInitFinishAction = new Action(LunDaoReduceData.OnInitFinish);

		// Token: 0x04004894 RID: 18580
		public int id;

		// Token: 0x04004895 RID: 18581
		public int ShuaiJianXiShu;
	}
}
