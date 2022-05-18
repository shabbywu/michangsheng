using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C36 RID: 3126
	public class NpcQingJiaoItemData : IJSONClass
	{
		// Token: 0x06004C41 RID: 19521 RVA: 0x002032B8 File Offset: 0x002014B8
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.NpcQingJiaoItemData.list)
			{
				try
				{
					NpcQingJiaoItemData npcQingJiaoItemData = new NpcQingJiaoItemData();
					npcQingJiaoItemData.id = jsonobject["id"].I;
					npcQingJiaoItemData.ItemIcon = jsonobject["ItemIcon"].I;
					npcQingJiaoItemData.maxNum = jsonobject["maxNum"].I;
					npcQingJiaoItemData.TuJianType = jsonobject["TuJianType"].I;
					npcQingJiaoItemData.ShopType = jsonobject["ShopType"].I;
					npcQingJiaoItemData.WuWeiType = jsonobject["WuWeiType"].I;
					npcQingJiaoItemData.ShuXingType = jsonobject["ShuXingType"].I;
					npcQingJiaoItemData.type = jsonobject["type"].I;
					npcQingJiaoItemData.quality = jsonobject["quality"].I;
					npcQingJiaoItemData.typePinJie = jsonobject["typePinJie"].I;
					npcQingJiaoItemData.StuTime = jsonobject["StuTime"].I;
					npcQingJiaoItemData.vagueType = jsonobject["vagueType"].I;
					npcQingJiaoItemData.price = jsonobject["price"].I;
					npcQingJiaoItemData.CanSale = jsonobject["CanSale"].I;
					npcQingJiaoItemData.DanDu = jsonobject["DanDu"].I;
					npcQingJiaoItemData.CanUse = jsonobject["CanUse"].I;
					npcQingJiaoItemData.NPCCanUse = jsonobject["NPCCanUse"].I;
					npcQingJiaoItemData.yaoZhi1 = jsonobject["yaoZhi1"].I;
					npcQingJiaoItemData.yaoZhi2 = jsonobject["yaoZhi2"].I;
					npcQingJiaoItemData.yaoZhi3 = jsonobject["yaoZhi3"].I;
					npcQingJiaoItemData.name = jsonobject["name"].Str;
					npcQingJiaoItemData.FaBaoType = jsonobject["FaBaoType"].Str;
					npcQingJiaoItemData.desc = jsonobject["desc"].Str;
					npcQingJiaoItemData.desc2 = jsonobject["desc2"].Str;
					npcQingJiaoItemData.Affix = jsonobject["Affix"].ToList();
					npcQingJiaoItemData.ItemFlag = jsonobject["ItemFlag"].ToList();
					npcQingJiaoItemData.seid = jsonobject["seid"].ToList();
					npcQingJiaoItemData.wuDao = jsonobject["wuDao"].ToList();
					if (NpcQingJiaoItemData.DataDict.ContainsKey(npcQingJiaoItemData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典NpcQingJiaoItemData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", npcQingJiaoItemData.id));
					}
					else
					{
						NpcQingJiaoItemData.DataDict.Add(npcQingJiaoItemData.id, npcQingJiaoItemData);
						NpcQingJiaoItemData.DataList.Add(npcQingJiaoItemData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典NpcQingJiaoItemData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (NpcQingJiaoItemData.OnInitFinishAction != null)
			{
				NpcQingJiaoItemData.OnInitFinishAction();
			}
		}

		// Token: 0x06004C42 RID: 19522 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004A03 RID: 18947
		public static Dictionary<int, NpcQingJiaoItemData> DataDict = new Dictionary<int, NpcQingJiaoItemData>();

		// Token: 0x04004A04 RID: 18948
		public static List<NpcQingJiaoItemData> DataList = new List<NpcQingJiaoItemData>();

		// Token: 0x04004A05 RID: 18949
		public static Action OnInitFinishAction = new Action(NpcQingJiaoItemData.OnInitFinish);

		// Token: 0x04004A06 RID: 18950
		public int id;

		// Token: 0x04004A07 RID: 18951
		public int ItemIcon;

		// Token: 0x04004A08 RID: 18952
		public int maxNum;

		// Token: 0x04004A09 RID: 18953
		public int TuJianType;

		// Token: 0x04004A0A RID: 18954
		public int ShopType;

		// Token: 0x04004A0B RID: 18955
		public int WuWeiType;

		// Token: 0x04004A0C RID: 18956
		public int ShuXingType;

		// Token: 0x04004A0D RID: 18957
		public int type;

		// Token: 0x04004A0E RID: 18958
		public int quality;

		// Token: 0x04004A0F RID: 18959
		public int typePinJie;

		// Token: 0x04004A10 RID: 18960
		public int StuTime;

		// Token: 0x04004A11 RID: 18961
		public int vagueType;

		// Token: 0x04004A12 RID: 18962
		public int price;

		// Token: 0x04004A13 RID: 18963
		public int CanSale;

		// Token: 0x04004A14 RID: 18964
		public int DanDu;

		// Token: 0x04004A15 RID: 18965
		public int CanUse;

		// Token: 0x04004A16 RID: 18966
		public int NPCCanUse;

		// Token: 0x04004A17 RID: 18967
		public int yaoZhi1;

		// Token: 0x04004A18 RID: 18968
		public int yaoZhi2;

		// Token: 0x04004A19 RID: 18969
		public int yaoZhi3;

		// Token: 0x04004A1A RID: 18970
		public string name;

		// Token: 0x04004A1B RID: 18971
		public string FaBaoType;

		// Token: 0x04004A1C RID: 18972
		public string desc;

		// Token: 0x04004A1D RID: 18973
		public string desc2;

		// Token: 0x04004A1E RID: 18974
		public List<int> Affix = new List<int>();

		// Token: 0x04004A1F RID: 18975
		public List<int> ItemFlag = new List<int>();

		// Token: 0x04004A20 RID: 18976
		public List<int> seid = new List<int>();

		// Token: 0x04004A21 RID: 18977
		public List<int> wuDao = new List<int>();
	}
}
