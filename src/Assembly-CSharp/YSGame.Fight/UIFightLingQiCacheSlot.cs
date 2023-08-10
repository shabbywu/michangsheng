using UnityEngine;
using UnityEngine.UI;

namespace YSGame.Fight;

public class UIFightLingQiCacheSlot : UIFightLingQiSlot
{
	public int LimitCount;

	public override void SetNull()
	{
		base.SetNull();
		LimitCount = 0;
	}

	protected override void OnLingQiCountChanged(int change)
	{
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		base.OnLingQiCountChanged(change);
		_ = UIFightPanel.Inst.UIFightState;
		_ = 3;
		if (UIFightPanel.Inst.UIFightState == UIFightState.回合结束弃置灵气阶段)
		{
			int cacheLingQiSum = UIFightPanel.Inst.CacheLingQiController.GetCacheLingQiSum();
			UIFightPanel.Inst.FightCenterTip.ShowYiSan(UIFightPanel.Inst.NeedYiSanCount - cacheLingQiSum);
		}
		if (change < 0)
		{
			if (base.LingQiCount <= 0)
			{
				((Graphic)LingQiImage).color = new Color(1f, 1f, 1f, 0f);
			}
			_ = UIFightPanel.Inst.UIFightState;
			_ = 3;
		}
		UIFightPanel.Inst.CacheLingQiController.RefreshShengKe();
	}

	public void UseSkillMoveLingQi()
	{
		if (UIFightPanel.Inst.UIFightState == UIFightState.释放技能准备灵气阶段 && base.LingQiCount > 0)
		{
			UIFightPanel.Inst.PlayerLingQiController.GetTargetLingQiSlot(base.LingQiType).LingQiCount += base.LingQiCount;
			base.LingQiCount = 0;
		}
	}

	public void XiaoSanLingQiMoveOne()
	{
		if (UIFightPanel.Inst.UIFightState == UIFightState.回合结束弃置灵气阶段 && base.LingQiCount > 0)
		{
			UIFightPanel.Inst.PlayerLingQiController.GetTargetLingQiSlot(base.LingQiType).LingQiCount++;
			base.LingQiCount--;
		}
	}

	public void XiaoSanLingQiMoveAll()
	{
		if (UIFightPanel.Inst.UIFightState == UIFightState.回合结束弃置灵气阶段 && base.LingQiCount > 0)
		{
			UIFightPanel.Inst.PlayerLingQiController.GetTargetLingQiSlot(base.LingQiType).LingQiCount += base.LingQiCount;
			base.LingQiCount = 0;
		}
	}

	protected override void OnClick()
	{
		base.OnClick();
		UseSkillMoveLingQi();
	}

	protected override void OnLeftClick()
	{
		base.OnLeftClick();
		XiaoSanLingQiMoveOne();
	}

	protected override void OnRightClick()
	{
		base.OnRightClick();
		XiaoSanLingQiMoveAll();
	}

	protected override void PlayAddLingQiAnim(int count)
	{
		base.PlayAddLingQiAnim(count);
	}

	protected override void PlayRemoveLingQiAnim(int count)
	{
		base.PlayRemoveLingQiAnim(count);
		if (UIFightPanel.Inst.UIFightState == UIFightState.释放技能准备灵气阶段 || UIFightPanel.Inst.UIFightState == UIFightState.回合结束弃置灵气阶段)
		{
			UIFightLingQiPlayerSlot targetLingQiSlot = UIFightPanel.Inst.PlayerLingQiController.GetTargetLingQiSlot(base.LingQiType);
			AnimCacheToPlayer(targetLingQiSlot, count);
		}
		else
		{
			AnimToEnv(count);
		}
	}

	protected void AnimToEnv(int count)
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		if (!UIFightLingQiSlot.IgnoreEffect)
		{
			UIFightPanel.Inst.GetMoveLingQi().SetData(base.LingQiType, ((Component)this).transform.position, new Vector3(((Component)this).transform.position.x, ((Component)this).transform.position.y - 2f, ((Component)this).transform.position.z), null, count, showAnim: false, -3f);
			PlayLingQiSound();
		}
	}

	protected void AnimCacheToPlayer(UIFightLingQiPlayerSlot target, int count)
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		if (!UIFightLingQiSlot.IgnoreEffect)
		{
			UIFightPanel.Inst.GetMoveLingQi().SetData(base.LingQiType, ((Component)this).transform.position, ((Component)target).transform.position, null, count);
			PlayLingQiSound();
		}
	}
}
