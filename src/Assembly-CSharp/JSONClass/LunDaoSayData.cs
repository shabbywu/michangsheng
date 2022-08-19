using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000886 RID: 2182
	public class LunDaoSayData : IJSONClass
	{
		// Token: 0x0600402B RID: 16427 RVA: 0x001B6098 File Offset: 0x001B4298
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.LunDaoSayData.list)
			{
				try
				{
					LunDaoSayData lunDaoSayData = new LunDaoSayData();
					lunDaoSayData.id = jsonobject["id"].I;
					lunDaoSayData.WuDaoId = jsonobject["WuDaoId"].I;
					lunDaoSayData.Desc1 = jsonobject["Desc1"].Str;
					lunDaoSayData.Desc2 = jsonobject["Desc2"].Str;
					lunDaoSayData.Desc3 = jsonobject["Desc3"].Str;
					lunDaoSayData.Desc4 = jsonobject["Desc4"].Str;
					lunDaoSayData.Desc5 = jsonobject["Desc5"].Str;
					if (LunDaoSayData.DataDict.ContainsKey(lunDaoSayData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典LunDaoSayData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", lunDaoSayData.id));
					}
					else
					{
						LunDaoSayData.DataDict.Add(lunDaoSayData.id, lunDaoSayData);
						LunDaoSayData.DataList.Add(lunDaoSayData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典LunDaoSayData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (LunDaoSayData.OnInitFinishAction != null)
			{
				LunDaoSayData.OnInitFinishAction();
			}
		}

		// Token: 0x0600402C RID: 16428 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003D3D RID: 15677
		public static Dictionary<int, LunDaoSayData> DataDict = new Dictionary<int, LunDaoSayData>();

		// Token: 0x04003D3E RID: 15678
		public static List<LunDaoSayData> DataList = new List<LunDaoSayData>();

		// Token: 0x04003D3F RID: 15679
		public static Action OnInitFinishAction = new Action(LunDaoSayData.OnInitFinish);

		// Token: 0x04003D40 RID: 15680
		public int id;

		// Token: 0x04003D41 RID: 15681
		public int WuDaoId;

		// Token: 0x04003D42 RID: 15682
		public string Desc1;

		// Token: 0x04003D43 RID: 15683
		public string Desc2;

		// Token: 0x04003D44 RID: 15684
		public string Desc3;

		// Token: 0x04003D45 RID: 15685
		public string Desc4;

		// Token: 0x04003D46 RID: 15686
		public string Desc5;
	}
}
