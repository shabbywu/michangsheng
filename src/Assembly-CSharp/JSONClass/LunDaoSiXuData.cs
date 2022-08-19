using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000888 RID: 2184
	public class LunDaoSiXuData : IJSONClass
	{
		// Token: 0x06004033 RID: 16435 RVA: 0x001B63D0 File Offset: 0x001B45D0
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.LunDaoSiXuData.list)
			{
				try
				{
					LunDaoSiXuData lunDaoSiXuData = new LunDaoSiXuData();
					lunDaoSiXuData.id = jsonobject["id"].I;
					lunDaoSiXuData.PinJie = jsonobject["PinJie"].I;
					lunDaoSiXuData.SiXvXiaoLv = jsonobject["SiXvXiaoLv"].I;
					if (LunDaoSiXuData.DataDict.ContainsKey(lunDaoSiXuData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典LunDaoSiXuData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", lunDaoSiXuData.id));
					}
					else
					{
						LunDaoSiXuData.DataDict.Add(lunDaoSiXuData.id, lunDaoSiXuData);
						LunDaoSiXuData.DataList.Add(lunDaoSiXuData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典LunDaoSiXuData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (LunDaoSiXuData.OnInitFinishAction != null)
			{
				LunDaoSiXuData.OnInitFinishAction();
			}
		}

		// Token: 0x06004034 RID: 16436 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003D4D RID: 15693
		public static Dictionary<int, LunDaoSiXuData> DataDict = new Dictionary<int, LunDaoSiXuData>();

		// Token: 0x04003D4E RID: 15694
		public static List<LunDaoSiXuData> DataList = new List<LunDaoSiXuData>();

		// Token: 0x04003D4F RID: 15695
		public static Action OnInitFinishAction = new Action(LunDaoSiXuData.OnInitFinish);

		// Token: 0x04003D50 RID: 15696
		public int id;

		// Token: 0x04003D51 RID: 15697
		public int PinJie;

		// Token: 0x04003D52 RID: 15698
		public int SiXvXiaoLv;
	}
}
