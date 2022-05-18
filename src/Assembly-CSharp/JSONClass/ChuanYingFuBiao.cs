using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B9E RID: 2974
	public class ChuanYingFuBiao : IJSONClass
	{
		// Token: 0x060049E0 RID: 18912 RVA: 0x001F4E2C File Offset: 0x001F302C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.ChuanYingFuBiao.list)
			{
				try
				{
					ChuanYingFuBiao chuanYingFuBiao = new ChuanYingFuBiao();
					chuanYingFuBiao.id = jsonobject["id"].I;
					chuanYingFuBiao.AvatarID = jsonobject["AvatarID"].I;
					chuanYingFuBiao.Type = jsonobject["Type"].I;
					chuanYingFuBiao.TaskID = jsonobject["TaskID"].I;
					chuanYingFuBiao.WeiTuo = jsonobject["WeiTuo"].I;
					chuanYingFuBiao.ItemID = jsonobject["ItemID"].I;
					chuanYingFuBiao.HaoGanDu = jsonobject["HaoGanDu"].I;
					chuanYingFuBiao.IsOnly = jsonobject["IsOnly"].I;
					chuanYingFuBiao.IsAdd = jsonobject["IsAdd"].I;
					chuanYingFuBiao.IsDelete = jsonobject["IsDelete"].I;
					chuanYingFuBiao.info = jsonobject["info"].Str;
					chuanYingFuBiao.StarTime = jsonobject["StarTime"].Str;
					chuanYingFuBiao.EndTime = jsonobject["EndTime"].Str;
					chuanYingFuBiao.fuhao = jsonobject["fuhao"].Str;
					chuanYingFuBiao.DelayTime = jsonobject["DelayTime"].ToList();
					chuanYingFuBiao.TaskIndex = jsonobject["TaskIndex"].ToList();
					chuanYingFuBiao.valueID = jsonobject["valueID"].ToList();
					chuanYingFuBiao.value = jsonobject["value"].ToList();
					chuanYingFuBiao.Level = jsonobject["Level"].ToList();
					chuanYingFuBiao.EventValue = jsonobject["EventValue"].ToList();
					if (ChuanYingFuBiao.DataDict.ContainsKey(chuanYingFuBiao.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典ChuanYingFuBiao.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", chuanYingFuBiao.id));
					}
					else
					{
						ChuanYingFuBiao.DataDict.Add(chuanYingFuBiao.id, chuanYingFuBiao);
						ChuanYingFuBiao.DataList.Add(chuanYingFuBiao);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典ChuanYingFuBiao.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (ChuanYingFuBiao.OnInitFinishAction != null)
			{
				ChuanYingFuBiao.OnInitFinishAction();
			}
		}

		// Token: 0x060049E1 RID: 18913 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040044CA RID: 17610
		public static Dictionary<int, ChuanYingFuBiao> DataDict = new Dictionary<int, ChuanYingFuBiao>();

		// Token: 0x040044CB RID: 17611
		public static List<ChuanYingFuBiao> DataList = new List<ChuanYingFuBiao>();

		// Token: 0x040044CC RID: 17612
		public static Action OnInitFinishAction = new Action(ChuanYingFuBiao.OnInitFinish);

		// Token: 0x040044CD RID: 17613
		public int id;

		// Token: 0x040044CE RID: 17614
		public int AvatarID;

		// Token: 0x040044CF RID: 17615
		public int Type;

		// Token: 0x040044D0 RID: 17616
		public int TaskID;

		// Token: 0x040044D1 RID: 17617
		public int WeiTuo;

		// Token: 0x040044D2 RID: 17618
		public int ItemID;

		// Token: 0x040044D3 RID: 17619
		public int HaoGanDu;

		// Token: 0x040044D4 RID: 17620
		public int IsOnly;

		// Token: 0x040044D5 RID: 17621
		public int IsAdd;

		// Token: 0x040044D6 RID: 17622
		public int IsDelete;

		// Token: 0x040044D7 RID: 17623
		public string info;

		// Token: 0x040044D8 RID: 17624
		public string StarTime;

		// Token: 0x040044D9 RID: 17625
		public string EndTime;

		// Token: 0x040044DA RID: 17626
		public string fuhao;

		// Token: 0x040044DB RID: 17627
		public List<int> DelayTime = new List<int>();

		// Token: 0x040044DC RID: 17628
		public List<int> TaskIndex = new List<int>();

		// Token: 0x040044DD RID: 17629
		public List<int> valueID = new List<int>();

		// Token: 0x040044DE RID: 17630
		public List<int> value = new List<int>();

		// Token: 0x040044DF RID: 17631
		public List<int> Level = new List<int>();

		// Token: 0x040044E0 RID: 17632
		public List<int> EventValue = new List<int>();
	}
}
