using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C12 RID: 3090
	public class LiShiChuanWen : IJSONClass
	{
		// Token: 0x06004BB1 RID: 19377 RVA: 0x001FF434 File Offset: 0x001FD634
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

		// Token: 0x06004BB2 RID: 19378 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004884 RID: 18564
		public static Dictionary<int, LiShiChuanWen> DataDict = new Dictionary<int, LiShiChuanWen>();

		// Token: 0x04004885 RID: 18565
		public static List<LiShiChuanWen> DataList = new List<LiShiChuanWen>();

		// Token: 0x04004886 RID: 18566
		public static Action OnInitFinishAction = new Action(LiShiChuanWen.OnInitFinish);

		// Token: 0x04004887 RID: 18567
		public int id;

		// Token: 0x04004888 RID: 18568
		public int TypeID;

		// Token: 0x04004889 RID: 18569
		public int StartTime;

		// Token: 0x0400488A RID: 18570
		public int getChuanWen;

		// Token: 0x0400488B RID: 18571
		public int cunZaiShiJian;

		// Token: 0x0400488C RID: 18572
		public int NTaskID;

		// Token: 0x0400488D RID: 18573
		public string EventName;

		// Token: 0x0400488E RID: 18574
		public string text;

		// Token: 0x0400488F RID: 18575
		public string fuhao;

		// Token: 0x04004890 RID: 18576
		public List<int> EventLv = new List<int>();
	}
}
