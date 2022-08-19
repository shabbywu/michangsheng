using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000744 RID: 1860
	public class _ItemJsonData : IJSONClass
	{
		// Token: 0x06003B24 RID: 15140 RVA: 0x00196B50 File Offset: 0x00194D50
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
					itemJsonData.ShuaXin = jsonobject["ShuaXin"].I;
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

		// Token: 0x06003B25 RID: 15141 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400336F RID: 13167
		public static Dictionary<int, _ItemJsonData> DataDict = new Dictionary<int, _ItemJsonData>();

		// Token: 0x04003370 RID: 13168
		public static List<_ItemJsonData> DataList = new List<_ItemJsonData>();

		// Token: 0x04003371 RID: 13169
		public static Action OnInitFinishAction = new Action(_ItemJsonData.OnInitFinish);

		// Token: 0x04003372 RID: 13170
		public int id;

		// Token: 0x04003373 RID: 13171
		public int ItemIcon;

		// Token: 0x04003374 RID: 13172
		public int maxNum;

		// Token: 0x04003375 RID: 13173
		public int TuJianType;

		// Token: 0x04003376 RID: 13174
		public int ShopType;

		// Token: 0x04003377 RID: 13175
		public int WuWeiType;

		// Token: 0x04003378 RID: 13176
		public int ShuXingType;

		// Token: 0x04003379 RID: 13177
		public int type;

		// Token: 0x0400337A RID: 13178
		public int quality;

		// Token: 0x0400337B RID: 13179
		public int typePinJie;

		// Token: 0x0400337C RID: 13180
		public int StuTime;

		// Token: 0x0400337D RID: 13181
		public int vagueType;

		// Token: 0x0400337E RID: 13182
		public int price;

		// Token: 0x0400337F RID: 13183
		public int CanSale;

		// Token: 0x04003380 RID: 13184
		public int DanDu;

		// Token: 0x04003381 RID: 13185
		public int CanUse;

		// Token: 0x04003382 RID: 13186
		public int NPCCanUse;

		// Token: 0x04003383 RID: 13187
		public int yaoZhi1;

		// Token: 0x04003384 RID: 13188
		public int yaoZhi2;

		// Token: 0x04003385 RID: 13189
		public int yaoZhi3;

		// Token: 0x04003386 RID: 13190
		public int ShuaXin;

		// Token: 0x04003387 RID: 13191
		public string name;

		// Token: 0x04003388 RID: 13192
		public string FaBaoType;

		// Token: 0x04003389 RID: 13193
		public string desc;

		// Token: 0x0400338A RID: 13194
		public string desc2;

		// Token: 0x0400338B RID: 13195
		public List<int> Affix = new List<int>();

		// Token: 0x0400338C RID: 13196
		public List<int> ItemFlag = new List<int>();

		// Token: 0x0400338D RID: 13197
		public List<int> seid = new List<int>();

		// Token: 0x0400338E RID: 13198
		public List<int> wuDao = new List<int>();
	}
}
