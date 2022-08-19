using System;
using System.Collections.Generic;

namespace YSGame.EquipRandom
{
	// Token: 0x02000A86 RID: 2694
	public class CaiLiao
	{
		// Token: 0x06004BA3 RID: 19363 RVA: 0x00203525 File Offset: 0x00201725
		public int ShuXingID(int EquipType)
		{
			return RandomEquip.FindShuXingIDByEquipTypeAndShuXingType(this.ShuXingType, EquipType);
		}

		// Token: 0x06004BA4 RID: 19364 RVA: 0x00203533 File Offset: 0x00201733
		public int AttackType(int EquipType)
		{
			return jsonData.instance.LianQiShuXinLeiBie[this.ShuXingType.ToString()]["AttackType"].I;
		}

		// Token: 0x06004BA5 RID: 19365 RVA: 0x00203560 File Offset: 0x00201760
		public CaiLiao(JSONObject item)
		{
			this.Data = item;
			this.Name = item["name"].str.ToCN();
			this.Quality = item["quality"].I;
			this.Type = item["type"].I;
			this.ShuXingType = item["ShuXingType"].I;
			this.LingLi = CaiLiao.CaiLiaoLingLiDict[this.Quality];
			this.WuWeiType = item["WuWeiType"].I;
			JSONObject lianQiWuWeiBiao = jsonData.instance.LianQiWuWeiBiao;
			this.QinHe = lianQiWuWeiBiao[this.WuWeiType.ToString()]["value1"].I;
			this.CaoKong = lianQiWuWeiBiao[this.WuWeiType.ToString()]["value2"].I;
			this.LingXing = lianQiWuWeiBiao[this.WuWeiType.ToString()]["value3"].I;
			this.JianGu = lianQiWuWeiBiao[this.WuWeiType.ToString()]["value4"].I;
			this.RenXing = lianQiWuWeiBiao[this.WuWeiType.ToString()]["value5"].I;
			this.TotalWuWei = this.QinHe + this.CaoKong + this.LingXing + this.JianGu + this.RenXing;
		}

		// Token: 0x06004BA6 RID: 19366 RVA: 0x002036F4 File Offset: 0x002018F4
		public override string ToString()
		{
			return string.Format("{0}品{1} 类型{2} 属性类型{3} 灵力{4} 五维类型{5} 亲和{6} 操控{7} 灵性{8} 坚固{9} 韧性{10} 总五维{11}", new object[]
			{
				this.Quality,
				this.Name,
				this.Type,
				this.ShuXingType,
				this.LingLi,
				this.WuWeiType,
				this.QinHe,
				this.CaoKong,
				this.LingXing,
				this.JianGu,
				this.RenXing,
				this.TotalWuWei
			});
		}

		// Token: 0x04004AAE RID: 19118
		private static Dictionary<int, int> CaiLiaoLingLiDict = new Dictionary<int, int>
		{
			{
				1,
				1
			},
			{
				2,
				3
			},
			{
				3,
				9
			},
			{
				4,
				18
			},
			{
				5,
				30
			},
			{
				6,
				48
			}
		};

		// Token: 0x04004AAF RID: 19119
		public JSONObject Data;

		// Token: 0x04004AB0 RID: 19120
		public string Name;

		// Token: 0x04004AB1 RID: 19121
		public int Quality;

		// Token: 0x04004AB2 RID: 19122
		public int Type;

		// Token: 0x04004AB3 RID: 19123
		public int ShuXingType;

		// Token: 0x04004AB4 RID: 19124
		public int LingLi;

		// Token: 0x04004AB5 RID: 19125
		public int WuWeiType;

		// Token: 0x04004AB6 RID: 19126
		public int QinHe;

		// Token: 0x04004AB7 RID: 19127
		public int CaoKong;

		// Token: 0x04004AB8 RID: 19128
		public int LingXing;

		// Token: 0x04004AB9 RID: 19129
		public int JianGu;

		// Token: 0x04004ABA RID: 19130
		public int RenXing;

		// Token: 0x04004ABB RID: 19131
		public int TotalWuWei;
	}
}
