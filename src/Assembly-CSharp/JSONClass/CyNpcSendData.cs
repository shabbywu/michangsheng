using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000BB8 RID: 3000
	public class CyNpcSendData : IJSONClass
	{
		// Token: 0x06004A48 RID: 19016 RVA: 0x001F7078 File Offset: 0x001F5278
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.CyNpcSendData.list)
			{
				try
				{
					CyNpcSendData cyNpcSendData = new CyNpcSendData();
					cyNpcSendData.id = jsonobject["id"].I;
					cyNpcSendData.IsChuFa = jsonobject["IsChuFa"].I;
					cyNpcSendData.NPCshenfen = jsonobject["NPCshenfen"].I;
					cyNpcSendData.HaoGanDu = jsonobject["HaoGanDu"].I;
					cyNpcSendData.IsOnly = jsonobject["IsOnly"].I;
					cyNpcSendData.XiaoXiType = jsonobject["XiaoXiType"].I;
					cyNpcSendData.Rate = jsonobject["Rate"].I;
					cyNpcSendData.XingWeiType = jsonobject["XingWeiType"].I;
					cyNpcSendData.ItemJiaGe = jsonobject["ItemJiaGe"].I;
					cyNpcSendData.GuoQiShiJian = jsonobject["GuoQiShiJian"].I;
					cyNpcSendData.HaoGanDuChange = jsonobject["HaoGanDuChange"].I;
					cyNpcSendData.DuiBaiType = jsonobject["DuiBaiType"].I;
					cyNpcSendData.QingFen = jsonobject["QingFen"].I;
					cyNpcSendData.fuhao1 = jsonobject["fuhao1"].Str;
					cyNpcSendData.fuhao2 = jsonobject["fuhao2"].Str;
					cyNpcSendData.StarTime = jsonobject["StarTime"].Str;
					cyNpcSendData.EndTime = jsonobject["EndTime"].Str;
					cyNpcSendData.NPCXingWei = jsonobject["NPCXingWei"].ToList();
					cyNpcSendData.NPCLevel = jsonobject["NPCLevel"].ToList();
					cyNpcSendData.EventValue = jsonobject["EventValue"].ToList();
					cyNpcSendData.ZhuangTaiInfo = jsonobject["ZhuangTaiInfo"].ToList();
					cyNpcSendData.RandomItemID = jsonobject["RandomItemID"].ToList();
					if (CyNpcSendData.DataDict.ContainsKey(cyNpcSendData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典CyNpcSendData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", cyNpcSendData.id));
					}
					else
					{
						CyNpcSendData.DataDict.Add(cyNpcSendData.id, cyNpcSendData);
						CyNpcSendData.DataList.Add(cyNpcSendData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典CyNpcSendData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (CyNpcSendData.OnInitFinishAction != null)
			{
				CyNpcSendData.OnInitFinishAction();
			}
		}

		// Token: 0x06004A49 RID: 19017 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400458A RID: 17802
		public static Dictionary<int, CyNpcSendData> DataDict = new Dictionary<int, CyNpcSendData>();

		// Token: 0x0400458B RID: 17803
		public static List<CyNpcSendData> DataList = new List<CyNpcSendData>();

		// Token: 0x0400458C RID: 17804
		public static Action OnInitFinishAction = new Action(CyNpcSendData.OnInitFinish);

		// Token: 0x0400458D RID: 17805
		public int id;

		// Token: 0x0400458E RID: 17806
		public int IsChuFa;

		// Token: 0x0400458F RID: 17807
		public int NPCshenfen;

		// Token: 0x04004590 RID: 17808
		public int HaoGanDu;

		// Token: 0x04004591 RID: 17809
		public int IsOnly;

		// Token: 0x04004592 RID: 17810
		public int XiaoXiType;

		// Token: 0x04004593 RID: 17811
		public int Rate;

		// Token: 0x04004594 RID: 17812
		public int XingWeiType;

		// Token: 0x04004595 RID: 17813
		public int ItemJiaGe;

		// Token: 0x04004596 RID: 17814
		public int GuoQiShiJian;

		// Token: 0x04004597 RID: 17815
		public int HaoGanDuChange;

		// Token: 0x04004598 RID: 17816
		public int DuiBaiType;

		// Token: 0x04004599 RID: 17817
		public int QingFen;

		// Token: 0x0400459A RID: 17818
		public string fuhao1;

		// Token: 0x0400459B RID: 17819
		public string fuhao2;

		// Token: 0x0400459C RID: 17820
		public string StarTime;

		// Token: 0x0400459D RID: 17821
		public string EndTime;

		// Token: 0x0400459E RID: 17822
		public List<int> NPCXingWei = new List<int>();

		// Token: 0x0400459F RID: 17823
		public List<int> NPCLevel = new List<int>();

		// Token: 0x040045A0 RID: 17824
		public List<int> EventValue = new List<int>();

		// Token: 0x040045A1 RID: 17825
		public List<int> ZhuangTaiInfo = new List<int>();

		// Token: 0x040045A2 RID: 17826
		public List<int> RandomItemID = new List<int>();
	}
}
