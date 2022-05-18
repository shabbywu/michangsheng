using System;
using UnityEngine;
using UnityEngine.UI;

namespace YSGame.Fight
{
	// Token: 0x02000E00 RID: 3584
	public class UIFightLingQiPlayerSlot : UIFightLingQiSlot
	{
		// Token: 0x0600567C RID: 22140 RVA: 0x002409D4 File Offset: 0x0023EBD4
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

		// Token: 0x0600567D RID: 22141 RVA: 0x00240A64 File Offset: 0x0023EC64
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

		// Token: 0x0600567E RID: 22142 RVA: 0x00240B6C File Offset: 0x0023ED6C
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

		// Token: 0x0600567F RID: 22143 RVA: 0x00240BFC File Offset: 0x0023EDFC
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

		// Token: 0x06005680 RID: 22144 RVA: 0x0003DD2E File Offset: 0x0003BF2E
		protected override void OnClick()
		{
			base.OnClick();
			this.UseSkillMoveLingQi();
		}

		// Token: 0x06005681 RID: 22145 RVA: 0x0003DD3C File Offset: 0x0003BF3C
		protected override void OnLeftClick()
		{
			base.OnLeftClick();
			this.XiaoSanLingQiMoveOne();
		}

		// Token: 0x06005682 RID: 22146 RVA: 0x0003DD4A File Offset: 0x0003BF4A
		protected override void OnRightClick()
		{
			base.OnRightClick();
			this.XiaoSanLingQiMoveAll();
		}

		// Token: 0x06005683 RID: 22147 RVA: 0x0003DD58 File Offset: 0x0003BF58
		protected override void PlayAddLingQiAnim(int count)
		{
			base.PlayAddLingQiAnim(count);
			if (UIFightPanel.Inst.UIFightState != UIFightState.释放技能准备灵气阶段 && UIFightPanel.Inst.UIFightState != UIFightState.回合结束弃置灵气阶段)
			{
				this.AnimFromEnv(count);
			}
		}

		// Token: 0x06005684 RID: 22148 RVA: 0x00240CB8 File Offset: 0x0023EEB8
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

		// Token: 0x06005685 RID: 22149 RVA: 0x00240904 File Offset: 0x0023EB04
		protected void AnimToEnv(int count)
		{
			if (!UIFightLingQiSlot.IgnoreEffect)
			{
				UIFightPanel.Inst.GetMoveLingQi().SetData(base.LingQiType, base.transform.position, new Vector3(base.transform.position.x, base.transform.position.y - 2f, base.transform.position.z), null, count, false, -3f);
				base.PlayLingQiSound();
			}
		}

		// Token: 0x06005686 RID: 22150 RVA: 0x00240D0C File Offset: 0x0023EF0C
		private void AnimFromEnv(int count)
		{
			if (!UIFightLingQiSlot.IgnoreEffect)
			{
				UIFightPanel.Inst.GetMoveLingQi().SetData(base.LingQiType, new Vector3(base.transform.position.x, base.transform.position.y + 2f, base.transform.position.z), base.transform.position, null, count, false, 5f);
				base.PlayLingQiSound();
			}
		}

		// Token: 0x06005687 RID: 22151 RVA: 0x00240D8C File Offset: 0x0023EF8C
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

		// Token: 0x04005616 RID: 22038
		public Image CountTextBG;
	}
}
