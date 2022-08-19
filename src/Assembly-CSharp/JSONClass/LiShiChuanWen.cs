using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000884 RID: 2180
	public class LiShiChuanWen : IJSONClass
	{
		// Token: 0x06004023 RID: 16419 RVA: 0x001B5D24 File Offset: 0x001B3F24
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.LiShiChuanWen.list)
			{
				try
				{
					LiShiChuanWen liShiChuanWen = new LiShiChuanWen();
					liShiChuanWen.id = jsonobject["id"].I;
					liShiChuanWen.TypeID = jsonobject["TypeID"].I;
					liShiChuanWen.StartTime = jsonobject["StartTime"].I;
					liShiChuanWen.getChuanWen = jsonobject["getChuanWen"].I;
					liShiChuanWen.cunZaiShiJian = jsonobject["cunZaiShiJian"].I;
					liShiChuanWen.NTaskID = jsonobject["NTaskID"].I;
					liShiChuanWen.EventName = jsonobject["EventName"].Str;
					liShiChuanWen.text = jsonobject["text"].Str;
					liShiChuanWen.fuhao = jsonobject["fuhao"].Str;
					liShiChuanWen.EventLv = jsonobject["EventLv"].ToList();
					if (LiShiChuanWen.DataDict.ContainsKey(liShiChuanWen.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典LiShiChuanWen.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", liShiChuanWen.id));
					}
					else
					{
						LiShiChuanWen.DataDict.Add(liShiChuanWen.id, liShiChuanWen);
						LiShiChuanWen.DataList.Add(liShiChuanWen);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典LiShiChuanWen.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (LiShiChuanWen.OnInitFinishAction != null)
			{
				LiShiChuanWen.OnInitFinishAction();
			}
		}

		// Token: 0x06004024 RID: 16420 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003D2B RID: 15659
		public static Dictionary<int, LiShiChuanWen> DataDict = new Dictionary<int, LiShiChuanWen>();

		// Token: 0x04003D2C RID: 15660
		public static List<LiShiChuanWen> DataList = new List<LiShiChuanWen>();

		// Token: 0x04003D2D RID: 15661
		public static Action OnInitFinishAction = new Action(LiShiChuanWen.OnInitFinish);

		// Token: 0x04003D2E RID: 15662
		public int id;

		// Token: 0x04003D2F RID: 15663
		public int TypeID;

		// Token: 0x04003D30 RID: 15664
		public int StartTime;

		// Token: 0x04003D31 RID: 15665
		public int getChuanWen;

		// Token: 0x04003D32 RID: 15666
		public int cunZaiShiJian;

		// Token: 0x04003D33 RID: 15667
		public int NTaskID;

		// Token: 0x04003D34 RID: 15668
		public string EventName;

		// Token: 0x04003D35 RID: 15669
		public string text;

		// Token: 0x04003D36 RID: 15670
		public string fuhao;

		// Token: 0x04003D37 RID: 15671
		public List<int> EventLv = new List<int>();
	}
}
