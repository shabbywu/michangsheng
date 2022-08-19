using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000822 RID: 2082
	public class CyNpcSendData : IJSONClass
	{
		// Token: 0x06003E9A RID: 16026 RVA: 0x001ABD08 File Offset: 0x001A9F08
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

		// Token: 0x06003E9B RID: 16027 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003A02 RID: 14850
		public static Dictionary<int, CyNpcSendData> DataDict = new Dictionary<int, CyNpcSendData>();

		// Token: 0x04003A03 RID: 14851
		public static List<CyNpcSendData> DataList = new List<CyNpcSendData>();

		// Token: 0x04003A04 RID: 14852
		public static Action OnInitFinishAction = new Action(CyNpcSendData.OnInitFinish);

		// Token: 0x04003A05 RID: 14853
		public int id;

		// Token: 0x04003A06 RID: 14854
		public int IsChuFa;

		// Token: 0x04003A07 RID: 14855
		public int NPCshenfen;

		// Token: 0x04003A08 RID: 14856
		public int HaoGanDu;

		// Token: 0x04003A09 RID: 14857
		public int IsOnly;

		// Token: 0x04003A0A RID: 14858
		public int XiaoXiType;

		// Token: 0x04003A0B RID: 14859
		public int Rate;

		// Token: 0x04003A0C RID: 14860
		public int XingWeiType;

		// Token: 0x04003A0D RID: 14861
		public int ItemJiaGe;

		// Token: 0x04003A0E RID: 14862
		public int GuoQiShiJian;

		// Token: 0x04003A0F RID: 14863
		public int HaoGanDuChange;

		// Token: 0x04003A10 RID: 14864
		public int DuiBaiType;

		// Token: 0x04003A11 RID: 14865
		public int QingFen;

		// Token: 0x04003A12 RID: 14866
		public string fuhao1;

		// Token: 0x04003A13 RID: 14867
		public string fuhao2;

		// Token: 0x04003A14 RID: 14868
		public string StarTime;

		// Token: 0x04003A15 RID: 14869
		public string EndTime;

		// Token: 0x04003A16 RID: 14870
		public List<int> NPCXingWei = new List<int>();

		// Token: 0x04003A17 RID: 14871
		public List<int> NPCLevel = new List<int>();

		// Token: 0x04003A18 RID: 14872
		public List<int> EventValue = new List<int>();

		// Token: 0x04003A19 RID: 14873
		public List<int> ZhuangTaiInfo = new List<int>();

		// Token: 0x04003A1A RID: 14874
		public List<int> RandomItemID = new List<int>();
	}
}
