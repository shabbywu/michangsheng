using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000ADC RID: 2780
	public class _ItemJsonData : IJSONClass
	{
		// Token: 0x060046DA RID: 18138 RVA: 0x001E4EF0 File Offset: 0x001E30F0
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance._ItemJsonData.list)
			{
				try
				{
					_ItemJsonData itemJsonData = new _ItemJsonData();
					itemJsonData.id = jsonobject["id"].I;
					itemJsonData.ItemIcon = jsonobject["ItemIcon"].I;
					itemJsonData.maxNum = jsonobject["maxNum"].I;
					itemJsonData.TuJianType = jsonobject["TuJianType"].I;
					itemJsonData.ShopType = jsonobject["ShopType"].I;
					itemJsonData.WuWeiType = jsonobject["WuWeiType"].I;
					itemJsonData.ShuXingType = jsonobject["ShuXingType"].I;
					itemJsonData.type = jsonobject["type"].I;
					itemJsonData.quality = jsonobject["quality"].I;
					itemJsonData.typePinJie = jsonobject["typePinJie"].I;
					itemJsonData.StuTime = jsonobject["StuTime"].I;
					itemJsonData.vagueType = jsonobject["vagueType"].I;
					itemJsonData.price = jsonobject["price"].I;
					itemJsonData.CanSale = jsonobject["CanSale"].I;
					itemJsonData.DanDu = jsonobject["DanDu"].I;
					itemJsonData.CanUse = jsonobject["CanUse"].I;
					itemJsonData.NPCCanUse = jsonobject["NPCCanUse"].I;
					itemJsonData.yaoZhi1 = jsonobject["yaoZhi1"].I;
					itemJsonData.yaoZhi2 = jsonobject["yaoZhi2"].I;
					itemJsonData.yaoZhi3 = jsonobject["yaoZhi3"].I;
					itemJsonData.name = jsonobject["name"].Str;
					itemJsonData.FaBaoType = jsonobject["FaBaoType"].Str;
					itemJsonData.desc = jsonobject["desc"].Str;
					itemJsonData.desc2 = jsonobject["desc2"].Str;
					itemJsonData.Affix = jsonobject["Affix"].ToList();
					itemJsonData.ItemFlag = jsonobject["ItemFlag"].ToList();
					itemJsonData.seid = jsonobject["seid"].ToList();
					itemJsonData.wuDao = jsonobject["wuDao"].ToList();
					if (_ItemJsonData.DataDict.ContainsKey(itemJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典_ItemJsonData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", itemJsonData.id));
					}
					else
					{
						_ItemJsonData.DataDict.Add(itemJsonData.id, itemJsonData);
						_ItemJsonData.DataList.Add(itemJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典_ItemJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (_ItemJsonData.OnInitFinishAction != null)
			{
				_ItemJsonData.OnInitFinishAction();
			}
		}

		// Token: 0x060046DB RID: 18139 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003F09 RID: 16137
		public static Dictionary<int, _ItemJsonData> DataDict = new Dictionary<int, _ItemJsonData>();

		// Token: 0x04003F0A RID: 16138
		public static List<_ItemJsonData> DataList = new List<_ItemJsonData>();

		// Token: 0x04003F0B RID: 16139
		public static Action OnInitFinishAction = new Action(_ItemJsonData.OnInitFinish);

		// Token: 0x04003F0C RID: 16140
		public int id;

		// Token: 0x04003F0D RID: 16141
		public int ItemIcon;

		// Token: 0x04003F0E RID: 16142
		public int maxNum;

		// Token: 0x04003F0F RID: 16143
		public int TuJianType;

		// Token: 0x04003F10 RID: 16144
		public int ShopType;

		// Token: 0x04003F11 RID: 16145
		public int WuWeiType;

		// Token: 0x04003F12 RID: 16146
		public int ShuXingType;

		// Token: 0x04003F13 RID: 16147
		public int type;

		// Token: 0x04003F14 RID: 16148
		public int quality;

		// Token: 0x04003F15 RID: 16149
		public int typePinJie;

		// Token: 0x04003F16 RID: 16150
		public int StuTime;

		// Token: 0x04003F17 RID: 16151
		public int vagueType;

		// Token: 0x04003F18 RID: 16152
		public int price;

		// Token: 0x04003F19 RID: 16153
		public int CanSale;

		// Token: 0x04003F1A RID: 16154
		public int DanDu;

		// Token: 0x04003F1B RID: 16155
		public int CanUse;

		// Token: 0x04003F1C RID: 16156
		public int NPCCanUse;

		// Token: 0x04003F1D RID: 16157
		public int yaoZhi1;

		// Token: 0x04003F1E RID: 16158
		public int yaoZhi2;

		// Token: 0x04003F1F RID: 16159
		public int yaoZhi3;

		// Token: 0x04003F20 RID: 16160
		public string name;

		// Token: 0x04003F21 RID: 16161
		public string FaBaoType;

		// Token: 0x04003F22 RID: 16162
		public string desc;

		// Token: 0x04003F23 RID: 16163
		public string desc2;

		// Token: 0x04003F24 RID: 16164
		public List<int> Affix = new List<int>();

		// Token: 0x04003F25 RID: 16165
		public List<int> ItemFlag = new List<int>();

		// Token: 0x04003F26 RID: 16166
		public List<int> seid = new List<int>();

		// Token: 0x04003F27 RID: 16167
		public List<int> wuDao = new List<int>();
	}
}
