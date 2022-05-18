using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C14 RID: 3092
	public class LunDaoSayData : IJSONClass
	{
		// Token: 0x06004BB9 RID: 19385 RVA: 0x001FF744 File Offset: 0x001FD944
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

		// Token: 0x06004BBA RID: 19386 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004896 RID: 18582
		public static Dictionary<int, LunDaoSayData> DataDict = new Dictionary<int, LunDaoSayData>();

		// Token: 0x04004897 RID: 18583
		public static List<LunDaoSayData> DataList = new List<LunDaoSayData>();

		// Token: 0x04004898 RID: 18584
		public static Action OnInitFinishAction = new Action(LunDaoSayData.OnInitFinish);

		// Token: 0x04004899 RID: 18585
		public int id;

		// Token: 0x0400489A RID: 18586
		public int WuDaoId;

		// Token: 0x0400489B RID: 18587
		public string Desc1;

		// Token: 0x0400489C RID: 18588
		public string Desc2;

		// Token: 0x0400489D RID: 18589
		public string Desc3;

		// Token: 0x0400489E RID: 18590
		public string Desc4;

		// Token: 0x0400489F RID: 18591
		public string Desc5;
	}
}
