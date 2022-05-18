using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C15 RID: 3093
	public class LunDaoShouYiData : IJSONClass
	{
		// Token: 0x06004BBD RID: 19389 RVA: 0x001FF8F0 File Offset: 0x001FDAF0
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

		// Token: 0x06004BBE RID: 19390 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040048A0 RID: 18592
		public static Dictionary<int, LunDaoShouYiData> DataDict = new Dictionary<int, LunDaoShouYiData>();

		// Token: 0x040048A1 RID: 18593
		public static List<LunDaoShouYiData> DataList = new List<LunDaoShouYiData>();

		// Token: 0x040048A2 RID: 18594
		public static Action OnInitFinishAction = new Action(LunDaoShouYiData.OnInitFinish);

		// Token: 0x040048A3 RID: 18595
		public int id;

		// Token: 0x040048A4 RID: 18596
		public int WuDaoExp;

		// Token: 0x040048A5 RID: 18597
		public int WuDaoZhi;
	}
}
