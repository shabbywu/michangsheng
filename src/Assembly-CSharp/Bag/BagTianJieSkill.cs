using System;
using JSONClass;
using KBEngine;
using UnityEngine;

namespace Bag
{
	// Token: 0x020009C1 RID: 2497
	public class BagTianJieSkill : ISkill
	{
		// Token: 0x06004579 RID: 17785 RVA: 0x000027FC File Offset: 0x000009FC
		public BagTianJieSkill()
		{
		}

		// Token: 0x0600457A RID: 17786 RVA: 0x001D76DC File Offset: 0x001D58DC
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

		// Token: 0x0600457B RID: 17787 RVA: 0x001D7834 File Offset: 0x001D5A34
		public Sprite GetIconSprite()
		{
			return this.BindSkill.GetIconSprite();
		}

		// Token: 0x0600457C RID: 17788 RVA: 0x001D7841 File Offset: 0x001D5A41
		public Sprite GetQualitySprite()
		{
			return this.BindSkill.GetQualitySprite();
		}

		// Token: 0x0600457D RID: 17789 RVA: 0x001D784E File Offset: 0x001D5A4E
		public Sprite GetQualityUpSprite()
		{
			return this.BindSkill.GetQualityUpSprite();
		}

		// Token: 0x04004702 RID: 18178
		public ActiveSkill BindSkill;

		// Token: 0x04004703 RID: 18179
		public int Quality;

		// Token: 0x04004704 RID: 18180
		public TianJieMiShuData MiShu;

		// Token: 0x04004705 RID: 18181
		public bool IsLingWu;

		// Token: 0x04004706 RID: 18182
		public bool IsGanYing;

		// Token: 0x04004707 RID: 18183
		public bool IsCanLingWu;
	}
}
