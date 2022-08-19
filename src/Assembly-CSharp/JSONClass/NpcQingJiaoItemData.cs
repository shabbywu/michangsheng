using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020008A8 RID: 2216
	public class NpcQingJiaoItemData : IJSONClass
	{
		// Token: 0x060040B3 RID: 16563 RVA: 0x001BA2F0 File Offset: 0x001B84F0
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
					npcQingJiaoItemData.ShuaXin = jsonobject["ShuaXin"].I;
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

		// Token: 0x060040B4 RID: 16564 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003EAA RID: 16042
		public static Dictionary<int, NpcQingJiaoItemData> DataDict = new Dictionary<int, NpcQingJiaoItemData>();

		// Token: 0x04003EAB RID: 16043
		public static List<NpcQingJiaoItemData> DataList = new List<NpcQingJiaoItemData>();

		// Token: 0x04003EAC RID: 16044
		public static Action OnInitFinishAction = new Action(NpcQingJiaoItemData.OnInitFinish);

		// Token: 0x04003EAD RID: 16045
		public int id;

		// Token: 0x04003EAE RID: 16046
		public int ItemIcon;

		// Token: 0x04003EAF RID: 16047
		public int maxNum;

		// Token: 0x04003EB0 RID: 16048
		public int TuJianType;

		// Token: 0x04003EB1 RID: 16049
		public int ShopType;

		// Token: 0x04003EB2 RID: 16050
		public int WuWeiType;

		// Token: 0x04003EB3 RID: 16051
		public int ShuXingType;

		// Token: 0x04003EB4 RID: 16052
		public int type;

		// Token: 0x04003EB5 RID: 16053
		public int quality;

		// Token: 0x04003EB6 RID: 16054
		public int typePinJie;

		// Token: 0x04003EB7 RID: 16055
		public int StuTime;

		// Token: 0x04003EB8 RID: 16056
		public int vagueType;

		// Token: 0x04003EB9 RID: 16057
		public int price;

		// Token: 0x04003EBA RID: 16058
		public int CanSale;

		// Token: 0x04003EBB RID: 16059
		public int DanDu;

		// Token: 0x04003EBC RID: 16060
		public int CanUse;

		// Token: 0x04003EBD RID: 16061
		public int NPCCanUse;

		// Token: 0x04003EBE RID: 16062
		public int yaoZhi1;

		// Token: 0x04003EBF RID: 16063
		public int yaoZhi2;

		// Token: 0x04003EC0 RID: 16064
		public int yaoZhi3;

		// Token: 0x04003EC1 RID: 16065
		public int ShuaXin;

		// Token: 0x04003EC2 RID: 16066
		public string name;

		// Token: 0x04003EC3 RID: 16067
		public string FaBaoType;

		// Token: 0x04003EC4 RID: 16068
		public string desc;

		// Token: 0x04003EC5 RID: 16069
		public string desc2;

		// Token: 0x04003EC6 RID: 16070
		public List<int> Affix = new List<int>();

		// Token: 0x04003EC7 RID: 16071
		public List<int> ItemFlag = new List<int>();

		// Token: 0x04003EC8 RID: 16072
		public List<int> seid = new List<int>();

		// Token: 0x04003EC9 RID: 16073
		public List<int> wuDao = new List<int>();
	}
}
