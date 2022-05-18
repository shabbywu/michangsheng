using System;
using JSONClass;
using KBEngine;
using UnityEngine;

namespace Bag
{
	// Token: 0x02000D4A RID: 3402
	public class BagTianJieSkill : ISkill
	{
		// Token: 0x060050DE RID: 20702 RVA: 0x0000403D File Offset: 0x0000223D
		public BagTianJieSkill()
		{
		}

		// Token: 0x060050DF RID: 20703 RVA: 0x0021B608 File Offset: 0x00219808
		public BagTianJieSkill(TianJieMiShuData miShu)
		{
			this.MiShu = miShu;
			this.BindSkill = new ActiveSkill();
			this.BindSkill.SetSkill(this.MiShu.Skill_ID, PlayerEx.Player.getLevelType());
			Avatar player = PlayerEx.Player;
			if (this.MiShu.Type == 0)
			{
				if (player.TianJieCanLingWuSkills.StringListContains(this.MiShu.id))
				{
					this.IsGanYing = true;
				}
				if (player.TianJieYiLingWuSkills.StringListContains(this.MiShu.id))
				{
					this.IsLingWu = true;
				}
				if (this.IsGanYing && !this.IsLingWu)
				{
					this.IsCanLingWu = true;
					return;
				}
			}
			else
			{
				int id = -1;
				float num;
				if (float.TryParse(this.MiShu.PanDing, out num))
				{
					id = (int)num;
				}
				else
				{
					Debug.LogError("天劫秘术解析判定时出错，无法解析为数字。秘术ID:" + miShu.id + "，需要解析的文本:" + miShu.PanDing);
				}
				if (player.TianJieYiLingWuSkills.StringListContains(this.MiShu.id))
				{
					this.IsLingWu = true;
				}
				if (!this.IsLingWu)
				{
					if (this.MiShu.Type == 1)
					{
						if (player.checkHasStudyWuDaoSkillByID(id))
						{
							this.IsCanLingWu = true;
							return;
						}
					}
					else if (this.MiShu.Type == 2 && GlobalValue.Get(id, "unknow") == 1)
					{
						this.IsCanLingWu = true;
					}
				}
			}
		}

		// Token: 0x060050E0 RID: 20704 RVA: 0x0003A34A File Offset: 0x0003854A
		public Sprite GetIconSprite()
		{
			return this.BindSkill.GetIconSprite();
		}

		// Token: 0x060050E1 RID: 20705 RVA: 0x0003A357 File Offset: 0x00038557
		public Sprite GetQualitySprite()
		{
			return this.BindSkill.GetQualitySprite();
		}

		// Token: 0x060050E2 RID: 20706 RVA: 0x0003A364 File Offset: 0x00038564
		public Sprite GetQualityUpSprite()
		{
			return this.BindSkill.GetQualityUpSprite();
		}

		// Token: 0x04005206 RID: 20998
		public ActiveSkill BindSkill;

		// Token: 0x04005207 RID: 20999
		public int Quality;

		// Token: 0x04005208 RID: 21000
		public TianJieMiShuData MiShu;

		// Token: 0x04005209 RID: 21001
		public bool IsLingWu;

		// Token: 0x0400520A RID: 21002
		public bool IsGanYing;

		// Token: 0x0400520B RID: 21003
		public bool IsCanLingWu;
	}
}
