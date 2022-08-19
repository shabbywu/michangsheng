using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000887 RID: 2183
	public class LunDaoShouYiData : IJSONClass
	{
		// Token: 0x0600402F RID: 16431 RVA: 0x001B626C File Offset: 0x001B446C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.LunDaoShouYiData.list)
			{
				try
				{
					LunDaoShouYiData lunDaoShouYiData = new LunDaoShouYiData();
					lunDaoShouYiData.id = jsonobject["id"].I;
					lunDaoShouYiData.WuDaoExp = jsonobject["WuDaoExp"].I;
					lunDaoShouYiData.WuDaoZhi = jsonobject["WuDaoZhi"].I;
					if (LunDaoShouYiData.DataDict.ContainsKey(lunDaoShouYiData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典LunDaoShouYiData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", lunDaoShouYiData.id));
					}
					else
					{
						LunDaoShouYiData.DataDict.Add(lunDaoShouYiData.id, lunDaoShouYiData);
						LunDaoShouYiData.DataList.Add(lunDaoShouYiData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典LunDaoShouYiData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (LunDaoShouYiData.OnInitFinishAction != null)
			{
				LunDaoShouYiData.OnInitFinishAction();
			}
		}

		// Token: 0x06004030 RID: 16432 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003D47 RID: 15687
		public static Dictionary<int, LunDaoShouYiData> DataDict = new Dictionary<int, LunDaoShouYiData>();

		// Token: 0x04003D48 RID: 15688
		public static List<LunDaoShouYiData> DataList = new List<LunDaoShouYiData>();

		// Token: 0x04003D49 RID: 15689
		public static Action OnInitFinishAction = new Action(LunDaoShouYiData.OnInitFinish);

		// Token: 0x04003D4A RID: 15690
		public int id;

		// Token: 0x04003D4B RID: 15691
		public int WuDaoExp;

		// Token: 0x04003D4C RID: 15692
		public int WuDaoZhi;
	}
}
