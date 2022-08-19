using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020008D0 RID: 2256
	public class SaiHonRandomColorJsonData : IJSONClass
	{
		// Token: 0x06004153 RID: 16723 RVA: 0x001BF568 File Offset: 0x001BD768
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SaiHonRandomColorJsonData.list)
			{
				try
				{
					SaiHonRandomColorJsonData saiHonRandomColorJsonData = new SaiHonRandomColorJsonData();
					saiHonRandomColorJsonData.id = jsonobject["id"].I;
					saiHonRandomColorJsonData.R = jsonobject["R"].I;
					saiHonRandomColorJsonData.G = jsonobject["G"].I;
					saiHonRandomColorJsonData.B = jsonobject["B"].I;
					saiHonRandomColorJsonData.beizhu = jsonobject["beizhu"].Str;
					if (SaiHonRandomColorJsonData.DataDict.ContainsKey(saiHonRandomColorJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SaiHonRandomColorJsonData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", saiHonRandomColorJsonData.id));
					}
					else
					{
						SaiHonRandomColorJsonData.DataDict.Add(saiHonRandomColorJsonData.id, saiHonRandomColorJsonData);
						SaiHonRandomColorJsonData.DataList.Add(saiHonRandomColorJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SaiHonRandomColorJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SaiHonRandomColorJsonData.OnInitFinishAction != null)
			{
				SaiHonRandomColorJsonData.OnInitFinishAction();
			}
		}

		// Token: 0x06004154 RID: 16724 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400408A RID: 16522
		public static Dictionary<int, SaiHonRandomColorJsonData> DataDict = new Dictionary<int, SaiHonRandomColorJsonData>();

		// Token: 0x0400408B RID: 16523
		public static List<SaiHonRandomColorJsonData> DataList = new List<SaiHonRandomColorJsonData>();

		// Token: 0x0400408C RID: 16524
		public static Action OnInitFinishAction = new Action(SaiHonRandomColorJsonData.OnInitFinish);

		// Token: 0x0400408D RID: 16525
		public int id;

		// Token: 0x0400408E RID: 16526
		public int R;

		// Token: 0x0400408F RID: 16527
		public int G;

		// Token: 0x04004090 RID: 16528
		public int B;

		// Token: 0x04004091 RID: 16529
		public string beizhu;
	}
}
