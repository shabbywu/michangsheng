using System;
using UnityEngine;

namespace YSGame.Fight
{
	// Token: 0x02000AC1 RID: 2753
	public class UIFightLingQiCacheSlot : UIFightLingQiSlot
	{
		// Token: 0x06004D20 RID: 19744 RVA: 0x0021004B File Offset: 0x0020E24B
		public override void SetNull()
		{
			base.SetNull();
			this.LimitCount = 0;
		}

		// Token: 0x06004D21 RID: 19745 RVA: 0x0021005C File Offset: 0x0020E25C
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

		// Token: 0x06004D22 RID: 19746 RVA: 0x00210104 File Offset: 0x0020E304
		public void UseSkillMoveLingQi()
		{
			if (UIFightPanel.Inst.UIFightState == UIFightState.释放技能准备灵气阶段 && base.LingQiCount > 0)
			{
				UIFightPanel.Inst.PlayerLingQiController.GetTargetLingQiSlot(base.LingQiType).LingQiCount += base.LingQiCount;
				base.LingQiCount = 0;
			}
		}

		// Token: 0x06004D23 RID: 19747 RVA: 0x00210158 File Offset: 0x0020E358
		public void XiaoSanLingQiMoveOne()
		{
			if (UIFightPanel.Inst.UIFightState == UIFightState.回合结束弃置灵气阶段 && base.LingQiCount > 0)
			{
				UIFightPanel.Inst.PlayerLingQiController.GetTargetLingQiSlot(base.LingQiType).LingQiCount++;
				base.LingQiCount--;
			}
		}

		// Token: 0x06004D24 RID: 19748 RVA: 0x002101AC File Offset: 0x0020E3AC
		public void XiaoSanLingQiMoveAll()
		{
			if (UIFightPanel.Inst.UIFightState == UIFightState.回合结束弃置灵气阶段 && base.LingQiCount > 0)
			{
				UIFightPanel.Inst.PlayerLingQiController.GetTargetLingQiSlot(base.LingQiType).LingQiCount += base.LingQiCount;
				base.LingQiCount = 0;
			}
		}

		// Token: 0x06004D25 RID: 19749 RVA: 0x002101FD File Offset: 0x0020E3FD
		protected override void OnClick()
		{
			base.OnClick();
			this.UseSkillMoveLingQi();
		}

		// Token: 0x06004D26 RID: 19750 RVA: 0x0021020B File Offset: 0x0020E40B
		protected override void OnLeftClick()
		{
			base.OnLeftClick();
			this.XiaoSanLingQiMoveOne();
		}

		// Token: 0x06004D27 RID: 19751 RVA: 0x00210219 File Offset: 0x0020E419
		protected override void OnRightClick()
		{
			base.OnRightClick();
			this.XiaoSanLingQiMoveAll();
		}

		// Token: 0x06004D28 RID: 19752 RVA: 0x00210227 File Offset: 0x0020E427
		protected override void PlayAddLingQiAnim(int count)
		{
			base.PlayAddLingQiAnim(count);
		}

		// Token: 0x06004D29 RID: 19753 RVA: 0x00210230 File Offset: 0x0020E430
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

		// Token: 0x06004D2A RID: 19754 RVA: 0x00210284 File Offset: 0x0020E484
		protected void AnimToEnv(int count)
		{
			if (!UIFightLingQiSlot.IgnoreEffect)
			{
				UIFightPanel.Inst.GetMoveLingQi().SetData(base.LingQiType, base.transform.position, new Vector3(base.transform.position.x, base.transform.position.y - 2f, base.transform.position.z), null, count, false, -3f);
				base.PlayLingQiSound();
			}
		}

		// Token: 0x06004D2B RID: 19755 RVA: 0x00210304 File Offset: 0x0020E504
		protected void AnimCacheToPlayer(UIFightLingQiPlayerSlot target, int count)
		{
			if (!UIFightLingQiSlot.IgnoreEffect)
			{
				UIFightPanel.Inst.GetMoveLingQi().SetData(base.LingQiType, base.transform.position, target.transform.position, null, count, false, 5f);
				base.PlayLingQiSound();
			}
		}

		// Token: 0x04004C3B RID: 19515
		public int LimitCount;
	}
}
