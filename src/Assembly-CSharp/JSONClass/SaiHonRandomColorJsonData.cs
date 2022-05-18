using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C5D RID: 3165
	public class SaiHonRandomColorJsonData : IJSONClass
	{
		// Token: 0x06004CDD RID: 19677 RVA: 0x00207AB8 File Offset: 0x00205CB8
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

		// Token: 0x06004CDE RID: 19678 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004BD4 RID: 19412
		public static Dictionary<int, SaiHonRandomColorJsonData> DataDict = new Dictionary<int, SaiHonRandomColorJsonData>();

		// Token: 0x04004BD5 RID: 19413
		public static List<SaiHonRandomColorJsonData> DataList = new List<SaiHonRandomColorJsonData>();

		// Token: 0x04004BD6 RID: 19414
		public static Action OnInitFinishAction = new Action(SaiHonRandomColorJsonData.OnInitFinish);

		// Token: 0x04004BD7 RID: 19415
		public int id;

		// Token: 0x04004BD8 RID: 19416
		public int R;

		// Token: 0x04004BD9 RID: 19417
		public int G;

		// Token: 0x04004BDA RID: 19418
		public int B;

		// Token: 0x04004BDB RID: 19419
		public string beizhu;
	}
}
