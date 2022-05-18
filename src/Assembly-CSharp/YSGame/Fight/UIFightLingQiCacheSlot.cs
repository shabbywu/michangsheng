using System;
using UnityEngine;

namespace YSGame.Fight
{
	// Token: 0x02000DFF RID: 3583
	public class UIFightLingQiCacheSlot : UIFightLingQiSlot
	{
		// Token: 0x0600566F RID: 22127 RVA: 0x0003DCE4 File Offset: 0x0003BEE4
		public override void SetNull()
		{
			base.SetNull();
			this.LimitCount = 0;
		}

		// Token: 0x06005670 RID: 22128 RVA: 0x0024070C File Offset: 0x0023E90C
		protected override void OnLingQiCountChanged(int change)
		{
			base.OnLingQiCountChanged(change);
			UIFightState uifightState = UIFightPanel.Inst.UIFightState;
			if (UIFightPanel.Inst.UIFightState == UIFightState.回合结束弃置灵气阶段)
			{
				int cacheLingQiSum = UIFightPanel.Inst.CacheLingQiController.GetCacheLingQiSum();
				UIFightPanel.Inst.FightCenterTip.ShowYiSan(UIFightPanel.Inst.NeedYiSanCount - cacheLingQiSum);
			}
			if (change < 0)
			{
				if (base.LingQiCount <= 0)
				{
					this.LingQiImage.color = new Color(1f, 1f, 1f, 0f);
				}
				UIFightState uifightState2 = UIFightPanel.Inst.UIFightState;
			}
			UIFightPanel.Inst.CacheLingQiController.RefreshShengKe();
		}

		// Token: 0x06005671 RID: 22129 RVA: 0x002407B4 File Offset: 0x0023E9B4
		public void UseSkillMoveLingQi()
		{
			if (UIFightPanel.Inst.UIFightState == UIFightState.释放技能准备灵气阶段 && base.LingQiCount > 0)
			{
				UIFightPanel.Inst.PlayerLingQiController.GetTargetLingQiSlot(base.LingQiType).LingQiCount += base.LingQiCount;
				base.LingQiCount = 0;
			}
		}

		// Token: 0x06005672 RID: 22130 RVA: 0x00240808 File Offset: 0x0023EA08
		public void XiaoSanLingQiMoveOne()
		{
			if (UIFightPanel.Inst.UIFightState == UIFightState.回合结束弃置灵气阶段 && base.LingQiCount > 0)
			{
				UIFightPanel.Inst.PlayerLingQiController.GetTargetLingQiSlot(base.LingQiType).LingQiCount++;
				base.LingQiCount--;
			}
		}

		// Token: 0x06005673 RID: 22131 RVA: 0x0024085C File Offset: 0x0023EA5C
		public void XiaoSanLingQiMoveAll()
		{
			if (UIFightPanel.Inst.UIFightState == UIFightState.回合结束弃置灵气阶段 && base.LingQiCount > 0)
			{
				UIFightPanel.Inst.PlayerLingQiController.GetTargetLingQiSlot(base.LingQiType).LingQiCount += base.LingQiCount;
				base.LingQiCount = 0;
			}
		}

		// Token: 0x06005674 RID: 22132 RVA: 0x0003DCF3 File Offset: 0x0003BEF3
		protected override void OnClick()
		{
			base.OnClick();
			this.UseSkillMoveLingQi();
		}

		// Token: 0x06005675 RID: 22133 RVA: 0x0003DD01 File Offset: 0x0003BF01
		protected override void OnLeftClick()
		{
			base.OnLeftClick();
			this.XiaoSanLingQiMoveOne();
		}

		// Token: 0x06005676 RID: 22134 RVA: 0x0003DD0F File Offset: 0x0003BF0F
		protected override void OnRightClick()
		{
			base.OnRightClick();
			this.XiaoSanLingQiMoveAll();
		}

		// Token: 0x06005677 RID: 22135 RVA: 0x0003DD1D File Offset: 0x0003BF1D
		protected override void PlayAddLingQiAnim(int count)
		{
			base.PlayAddLingQiAnim(count);
		}

		// Token: 0x06005678 RID: 22136 RVA: 0x002408B0 File Offset: 0x0023EAB0
		protected override void PlayRemoveLingQiAnim(int count)
		{
			base.PlayRemoveLingQiAnim(count);
			if (UIFightPanel.Inst.UIFightState == UIFightState.释放技能准备灵气阶段 || UIFightPanel.Inst.UIFightState == UIFightState.回合结束弃置灵气阶段)
			{
				UIFightLingQiPlayerSlot targetLingQiSlot = UIFightPanel.Inst.PlayerLingQiController.GetTargetLingQiSlot(base.LingQiType);
				this.AnimCacheToPlayer(targetLingQiSlot, count);
				return;
			}
			this.AnimToEnv(count);
		}

		// Token: 0x06005679 RID: 22137 RVA: 0x00240904 File Offset: 0x0023EB04
		protected void AnimToEnv(int count)
		{
			if (!UIFightLingQiSlot.IgnoreEffect)
			{
				UIFightPanel.Inst.GetMoveLingQi().SetData(base.LingQiType, base.transform.position, new Vector3(base.transform.position.x, base.transform.position.y - 2f, base.transform.position.z), null, count, false, -3f);
				base.PlayLingQiSound();
			}
		}

		// Token: 0x0600567A RID: 22138 RVA: 0x00240984 File Offset: 0x0023EB84
		protected void AnimCacheToPlayer(UIFightLingQiPlayerSlot target, int count)
		{
			if (!UIFightLingQiSlot.IgnoreEffect)
			{
				UIFightPanel.Inst.GetMoveLingQi().SetData(base.LingQiType, base.transform.position, target.transform.position, null, count, false, 5f);
				base.PlayLingQiSound();
			}
		}

		// Token: 0x04005615 RID: 22037
		public int LimitCount;
	}
}
