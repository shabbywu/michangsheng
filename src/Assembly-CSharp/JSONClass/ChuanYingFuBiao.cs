using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000808 RID: 2056
	public class ChuanYingFuBiao : IJSONClass
	{
		// Token: 0x06003E32 RID: 15922 RVA: 0x001A9518 File Offset: 0x001A7718
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
					chuanYingFuBiao.SPvalueID = jsonobject["SPvalueID"].I;
					chuanYingFuBiao.HaoGanDu = jsonobject["HaoGanDu"].I;
					chuanYingFuBiao.IsOnly = jsonobject["IsOnly"].I;
					chuanYingFuBiao.IsAdd = jsonobject["IsAdd"].I;
					chuanYingFuBiao.IsDelete = jsonobject["IsDelete"].I;
					chuanYingFuBiao.IsAlive = jsonobject["IsAlive"].I;
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
					chuanYingFuBiao.NPCLevel = jsonobject["NPCLevel"].ToList();
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

		// Token: 0x06003E33 RID: 15923 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400393F RID: 14655
		public static Dictionary<int, ChuanYingFuBiao> DataDict = new Dictionary<int, ChuanYingFuBiao>();

		// Token: 0x04003940 RID: 14656
		public static List<ChuanYingFuBiao> DataList = new List<ChuanYingFuBiao>();

		// Token: 0x04003941 RID: 14657
		public static Action OnInitFinishAction = new Action(ChuanYingFuBiao.OnInitFinish);

		// Token: 0x04003942 RID: 14658
		public int id;

		// Token: 0x04003943 RID: 14659
		public int AvatarID;

		// Token: 0x04003944 RID: 14660
		public int Type;

		// Token: 0x04003945 RID: 14661
		public int TaskID;

		// Token: 0x04003946 RID: 14662
		public int WeiTuo;

		// Token: 0x04003947 RID: 14663
		public int ItemID;

		// Token: 0x04003948 RID: 14664
		public int SPvalueID;

		// Token: 0x04003949 RID: 14665
		public int HaoGanDu;

		// Token: 0x0400394A RID: 14666
		public int IsOnly;

		// Token: 0x0400394B RID: 14667
		public int IsAdd;

		// Token: 0x0400394C RID: 14668
		public int IsDelete;

		// Token: 0x0400394D RID: 14669
		public int IsAlive;

		// Token: 0x0400394E RID: 14670
		public string info;

		// Token: 0x0400394F RID: 14671
		public string StarTime;

		// Token: 0x04003950 RID: 14672
		public string EndTime;

		// Token: 0x04003951 RID: 14673
		public string fuhao;

		// Token: 0x04003952 RID: 14674
		public List<int> DelayTime = new List<int>();

		// Token: 0x04003953 RID: 14675
		public List<int> TaskIndex = new List<int>();

		// Token: 0x04003954 RID: 14676
		public List<int> valueID = new List<int>();

		// Token: 0x04003955 RID: 14677
		public List<int> value = new List<int>();

		// Token: 0x04003956 RID: 14678
		public List<int> Level = new List<int>();

		// Token: 0x04003957 RID: 14679
		public List<int> EventValue = new List<int>();

		// Token: 0x04003958 RID: 14680
		public List<int> NPCLevel = new List<int>();
	}
}
