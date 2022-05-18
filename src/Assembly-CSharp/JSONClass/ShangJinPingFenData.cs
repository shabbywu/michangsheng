using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C65 RID: 3173
	public class ShangJinPingFenData : IJSONClass
	{
		// Token: 0x06004CFD RID: 19709 RVA: 0x00208624 File Offset: 0x00206824
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

		// Token: 0x06004CFE RID: 19710 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004C12 RID: 19474
		public static Dictionary<int, ShangJinPingFenData> DataDict = new Dictionary<int, ShangJinPingFenData>();

		// Token: 0x04004C13 RID: 19475
		public static List<ShangJinPingFenData> DataList = new List<ShangJinPingFenData>();

		// Token: 0x04004C14 RID: 19476
		public static Action OnInitFinishAction = new Action(ShangJinPingFenData.OnInitFinish);

		// Token: 0x04004C15 RID: 19477
		public int id;

		// Token: 0x04004C16 RID: 19478
		public int PingFen;

		// Token: 0x04004C17 RID: 19479
		public int EWaiPingFen;

		// Token: 0x04004C18 RID: 19480
		public int ShaShouLv;
	}
}
