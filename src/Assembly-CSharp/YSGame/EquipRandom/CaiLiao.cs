using System;
using System.Collections.Generic;

namespace YSGame.EquipRandom
{
	// Token: 0x02000DB8 RID: 3512
	public class CaiLiao
	{
		// Token: 0x060054BF RID: 21695 RVA: 0x0003C9E5 File Offset: 0x0003ABE5
		public int ShuXingID(int EquipType)
		{
			return RandomEquip.FindShuXingIDByEquipTypeAndShuXingType(this.ShuXingType, EquipType);
		}

		// Token: 0x060054C0 RID: 21696 RVA: 0x0003C9F3 File Offset: 0x0003ABF3
		public int AttackType(int EquipType)
		{
			return jsonData.instance.LianQiShuXinLeiBie[this.ShuXingType.ToString()]["AttackType"].I;
		}

		// Token: 0x060054C1 RID: 21697 RVA: 0x00234AE0 File Offset: 0x00232CE0
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

		// Token: 0x060054C2 RID: 21698 RVA: 0x00234C74 File Offset: 0x00232E74
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

		// Token: 0x0400546C RID: 21612
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

		// Token: 0x0400546D RID: 21613
		public JSONObject Data;

		// Token: 0x0400546E RID: 21614
		public string Name;

		// Token: 0x0400546F RID: 21615
		public int Quality;

		// Token: 0x04005470 RID: 21616
		public int Type;

		// Token: 0x04005471 RID: 21617
		public int ShuXingType;

		// Token: 0x04005472 RID: 21618
		public int LingLi;

		// Token: 0x04005473 RID: 21619
		public int WuWeiType;

		// Token: 0x04005474 RID: 21620
		public int QinHe;

		// Token: 0x04005475 RID: 21621
		public int CaoKong;

		// Token: 0x04005476 RID: 21622
		public int LingXing;

		// Token: 0x04005477 RID: 21623
		public int JianGu;

		// Token: 0x04005478 RID: 21624
		public int RenXing;

		// Token: 0x04005479 RID: 21625
		public int TotalWuWei;
	}
}
