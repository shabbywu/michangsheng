using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020008D8 RID: 2264
	public class ShangJinPingFenData : IJSONClass
	{
		// Token: 0x06004173 RID: 16755 RVA: 0x001C0270 File Offset: 0x001BE470
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.ShangJinPingFenData.list)
			{
				try
				{
					ShangJinPingFenData shangJinPingFenData = new ShangJinPingFenData();
					shangJinPingFenData.id = jsonobject["id"].I;
					shangJinPingFenData.PingFen = jsonobject["PingFen"].I;
					shangJinPingFenData.EWaiPingFen = jsonobject["EWaiPingFen"].I;
					shangJinPingFenData.ShaShouLv = jsonobject["ShaShouLv"].I;
					if (ShangJinPingFenData.DataDict.ContainsKey(shangJinPingFenData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典ShangJinPingFenData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", shangJinPingFenData.id));
					}
					else
					{
						ShangJinPingFenData.DataDict.Add(shangJinPingFenData.id, shangJinPingFenData);
						ShangJinPingFenData.DataList.Add(shangJinPingFenData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典ShangJinPingFenData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (ShangJinPingFenData.OnInitFinishAction != null)
			{
				ShangJinPingFenData.OnInitFinishAction();
			}
		}

		// Token: 0x06004174 RID: 16756 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040040C8 RID: 16584
		public static Dictionary<int, ShangJinPingFenData> DataDict = new Dictionary<int, ShangJinPingFenData>();

		// Token: 0x040040C9 RID: 16585
		public static List<ShangJinPingFenData> DataList = new List<ShangJinPingFenData>();

		// Token: 0x040040CA RID: 16586
		public static Action OnInitFinishAction = new Action(ShangJinPingFenData.OnInitFinish);

		// Token: 0x040040CB RID: 16587
		public int id;

		// Token: 0x040040CC RID: 16588
		public int PingFen;

		// Token: 0x040040CD RID: 16589
		public int EWaiPingFen;

		// Token: 0x040040CE RID: 16590
		public int ShaShouLv;
	}
}
