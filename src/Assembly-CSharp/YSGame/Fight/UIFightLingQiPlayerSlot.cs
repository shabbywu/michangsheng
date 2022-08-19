using System;
using UnityEngine;
using UnityEngine.UI;

namespace YSGame.Fight
{
	// Token: 0x02000AC2 RID: 2754
	public class UIFightLingQiPlayerSlot : UIFightLingQiSlot
	{
		// Token: 0x06004D2D RID: 19757 RVA: 0x0021035C File Offset: 0x0020E55C
		protected override void OnLingQiCountChanged(int change)
		{
			base.OnLingQiCountChanged(change);
			if (base.LingQiType == LingQiType.魔)
			{
				if (base.LingQiCount > 0)
				{
					base.gameObject.SetActive(true);
					UIFightPanel.Inst.PlayerLingQiController.MoBG.SetActive(true);
				}
				else
				{
					base.gameObject.SetActive(false);
					UIFightPanel.Inst.PlayerLingQiController.MoBG.SetActive(false);
				}
			}
			if (change != 0 && base.LingQiCount < 0)
			{
				Debug.LogError(string.Format("设置灵气文本时出现负数:{0}", base.LingQiCount));
			}
		}

		// Token: 0x06004D2E RID: 19758 RVA: 0x002103EC File Offset: 0x0020E5EC
		public void UseSkillMoveLingQi()
		{
			if (UIFightPanel.Inst.UIFightState == UIFightState.释放技能准备灵气阶段)
			{
				if (base.LingQiType != LingQiType.魔)
				{
					if (UIFightPanel.Inst.CacheLingQiController.GetNowCacheTongLingQi().ContainsKey(base.LingQiType))
					{
						UIFightPanel.Inst.CacheLingQiController.GetTongLingQiSlot(base.LingQiType).UseSkillMoveLingQi();
						return;
					}
					if (base.LingQiCount > 0)
					{
						UIFightLingQiCacheSlot targetLingQiSlot = UIFightPanel.Inst.CacheLingQiController.GetTargetLingQiSlot(base.LingQiType);
						if (targetLingQiSlot == null)
						{
							UIPopTip.Inst.Pop("没有多余槽位", PopTipIconType.叹号);
							return;
						}
						int num = targetLingQiSlot.LimitCount - targetLingQiSlot.LingQiCount;
						if (num == 0)
						{
							UIPopTip.Inst.Pop("此灵气已经足够", PopTipIconType.叹号);
							return;
						}
						if (base.LingQiCount >= num)
						{
							base.LingQiCount -= num;
							targetLingQiSlot.LingQiCount += num;
							return;
						}
						UIPopTip.Inst.Pop("灵气不足", PopTipIconType.叹号);
						return;
					}
				}
				else
				{
					UIPopTip.Inst.Pop("无法使用魔气", PopTipIconType.叹号);
				}
			}
		}

		// Token: 0x06004D2F RID: 19759 RVA: 0x002104F4 File Offset: 0x0020E6F4
		public void XiaoSanLingQiMoveOne()
		{
			if (UIFightPanel.Inst.UIFightState == UIFightState.回合结束弃置灵气阶段 && base.LingQiCount > 0)
			{
				UIFightLingQiCacheSlot targetLingQiSlot = UIFightPanel.Inst.CacheLingQiController.GetTargetLingQiSlot(base.LingQiType);
				if (targetLingQiSlot == null)
				{
					UIPopTip.Inst.Pop("没有多余槽位", PopTipIconType.叹号);
					return;
				}
				int cacheLingQiSum = UIFightPanel.Inst.CacheLingQiController.GetCacheLingQiSum();
				if (UIFightPanel.Inst.NeedYiSanCount - cacheLingQiSum > 0)
				{
					base.LingQiCount--;
					targetLingQiSlot.LingQiCount++;
				}
			}
		}

		// Token: 0x06004D30 RID: 19760 RVA: 0x00210584 File Offset: 0x0020E784
		public void XiaoSanLingQiMoveAll()
		{
			if (UIFightPanel.Inst.UIFightState == UIFightState.回合结束弃置灵气阶段 && base.LingQiCount > 0)
			{
				UIFightLingQiCacheSlot targetLingQiSlot = UIFightPanel.Inst.CacheLingQiController.GetTargetLingQiSlot(base.LingQiType);
				if (targetLingQiSlot == null)
				{
					UIPopTip.Inst.Pop("没有多余槽位", PopTipIconType.叹号);
					return;
				}
				int cacheLingQiSum = UIFightPanel.Inst.CacheLingQiController.GetCacheLingQiSum();
				int num = UIFightPanel.Inst.NeedYiSanCount - cacheLingQiSum;
				if (num > 0)
				{
					if (num > base.LingQiCount)
					{
						targetLingQiSlot.LingQiCount += base.LingQiCount;
						base.LingQiCount = 0;
						return;
					}
					targetLingQiSlot.LingQiCount += num;
					base.LingQiCount -= num;
				}
			}
		}

		// Token: 0x06004D31 RID: 19761 RVA: 0x0021063E File Offset: 0x0020E83E
		protected override void OnClick()
		{
			base.OnClick();
			this.UseSkillMoveLingQi();
		}

		// Token: 0x06004D32 RID: 19762 RVA: 0x0021064C File Offset: 0x0020E84C
		protected override void OnLeftClick()
		{
			base.OnLeftClick();
			this.XiaoSanLingQiMoveOne();
		}

		// Token: 0x06004D33 RID: 19763 RVA: 0x0021065A File Offset: 0x0020E85A
		protected override void OnRightClick()
		{
			base.OnRightClick();
			this.XiaoSanLingQiMoveAll();
		}

		// Token: 0x06004D34 RID: 19764 RVA: 0x00210668 File Offset: 0x0020E868
		protected override void PlayAddLingQiAnim(int count)
		{
			base.PlayAddLingQiAnim(count);
			if (UIFightPanel.Inst.UIFightState != UIFightState.释放技能准备灵气阶段 && UIFightPanel.Inst.UIFightState != UIFightState.回合结束弃置灵气阶段)
			{
				this.AnimFromEnv(count);
			}
		}

		// Token: 0x06004D35 RID: 19765 RVA: 0x00210694 File Offset: 0x0020E894
		protected override void PlayRemoveLingQiAnim(int count)
		{
			base.PlayRemoveLingQiAnim(count);
			if (UIFightPanel.Inst.UIFightState == UIFightState.释放技能准备灵气阶段 || UIFightPanel.Inst.UIFightState == UIFightState.回合结束弃置灵气阶段)
			{
				UIFightLingQiCacheSlot targetLingQiSlot = UIFightPanel.Inst.CacheLingQiController.GetTargetLingQiSlot(base.LingQiType);
				this.AnimPlayerToCache(targetLingQiSlot, count);
				return;
			}
			this.AnimToEnv(count);
		}

		// Token: 0x06004D36 RID: 19766 RVA: 0x002106E8 File Offset: 0x0020E8E8
		protected void AnimToEnv(int count)
		{
			if (!UIFightLingQiSlot.IgnoreEffect)
			{
				UIFightPanel.Inst.GetMoveLingQi().SetData(base.LingQiType, base.transform.position, new Vector3(base.transform.position.x, base.transform.position.y - 2f, base.transform.position.z), null, count, false, -3f);
				base.PlayLingQiSound();
			}
		}

		// Token: 0x06004D37 RID: 19767 RVA: 0x00210768 File Offset: 0x0020E968
		private void AnimFromEnv(int count)
		{
			if (!UIFightLingQiSlot.IgnoreEffect)
			{
				UIFightPanel.Inst.GetMoveLingQi().SetData(base.LingQiType, new Vector3(base.transform.position.x, base.transform.position.y + 2f, base.transform.position.z), base.transform.position, null, count, false, 5f);
				base.PlayLingQiSound();
			}
		}

		// Token: 0x06004D38 RID: 19768 RVA: 0x002107E8 File Offset: 0x0020E9E8
		protected void AnimPlayerToCache(UIFightLingQiCacheSlot target, int count)
		{
			if (!UIFightLingQiSlot.IgnoreEffect)
			{
				UIFightMoveLingQi moveLingQi = UIFightPanel.Inst.GetMoveLingQi();
				moveLingQi.gameObject.SetActive(true);
				moveLingQi.SetData(base.LingQiType, base.transform.position, target.transform.position, null, count, true, 5f);
				base.PlayLingQiSound();
			}
		}

		// Token: 0x04004C3C RID: 19516
		public Image CountTextBG;
	}
}
