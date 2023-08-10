using UnityEngine;
using UnityEngine.UI;

namespace YSGame.Fight;

public class UIFightLingQiPlayerSlot : UIFightLingQiSlot
{
	public Image CountTextBG;

	protected override void OnLingQiCountChanged(int change)
	{
		base.OnLingQiCountChanged(change);
		if (base.LingQiType == LingQiType.魔)
		{
			if (base.LingQiCount > 0)
			{
				((Component)this).gameObject.SetActive(true);
				UIFightPanel.Inst.PlayerLingQiController.MoBG.SetActive(true);
			}
			else
			{
				((Component)this).gameObject.SetActive(false);
				UIFightPanel.Inst.PlayerLingQiController.MoBG.SetActive(false);
			}
		}
		if (change != 0 && base.LingQiCount < 0)
		{
			Debug.LogError((object)$"设置灵气文本时出现负数:{base.LingQiCount}");
		}
	}

	public void UseSkillMoveLingQi()
	{
		if (UIFightPanel.Inst.UIFightState != UIFightState.释放技能准备灵气阶段)
		{
			return;
		}
		if (base.LingQiType != LingQiType.魔)
		{
			if (UIFightPanel.Inst.CacheLingQiController.GetNowCacheTongLingQi().ContainsKey(base.LingQiType))
			{
				UIFightPanel.Inst.CacheLingQiController.GetTongLingQiSlot(base.LingQiType).UseSkillMoveLingQi();
			}
			else
			{
				if (base.LingQiCount <= 0)
				{
					return;
				}
				UIFightLingQiCacheSlot targetLingQiSlot = UIFightPanel.Inst.CacheLingQiController.GetTargetLingQiSlot(base.LingQiType);
				if ((Object)(object)targetLingQiSlot == (Object)null)
				{
					UIPopTip.Inst.Pop("没有多余槽位");
					return;
				}
				int num = targetLingQiSlot.LimitCount - targetLingQiSlot.LingQiCount;
				if (num == 0)
				{
					UIPopTip.Inst.Pop("此灵气已经足够");
				}
				else if (base.LingQiCount >= num)
				{
					base.LingQiCount -= num;
					targetLingQiSlot.LingQiCount += num;
				}
				else
				{
					UIPopTip.Inst.Pop("灵气不足");
				}
			}
		}
		else
		{
			UIPopTip.Inst.Pop("无法使用魔气");
		}
	}

	public void XiaoSanLingQiMoveOne()
	{
		if (UIFightPanel.Inst.UIFightState != UIFightState.回合结束弃置灵气阶段 || base.LingQiCount <= 0)
		{
			return;
		}
		UIFightLingQiCacheSlot targetLingQiSlot = UIFightPanel.Inst.CacheLingQiController.GetTargetLingQiSlot(base.LingQiType);
		if ((Object)(object)targetLingQiSlot == (Object)null)
		{
			UIPopTip.Inst.Pop("没有多余槽位");
			return;
		}
		int cacheLingQiSum = UIFightPanel.Inst.CacheLingQiController.GetCacheLingQiSum();
		if (UIFightPanel.Inst.NeedYiSanCount - cacheLingQiSum > 0)
		{
			base.LingQiCount--;
			targetLingQiSlot.LingQiCount++;
		}
	}

	public void XiaoSanLingQiMoveAll()
	{
		if (UIFightPanel.Inst.UIFightState != UIFightState.回合结束弃置灵气阶段 || base.LingQiCount <= 0)
		{
			return;
		}
		UIFightLingQiCacheSlot targetLingQiSlot = UIFightPanel.Inst.CacheLingQiController.GetTargetLingQiSlot(base.LingQiType);
		if ((Object)(object)targetLingQiSlot == (Object)null)
		{
			UIPopTip.Inst.Pop("没有多余槽位");
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
			}
			else
			{
				targetLingQiSlot.LingQiCount += num;
				base.LingQiCount -= num;
			}
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
		if (UIFightPanel.Inst.UIFightState != UIFightState.释放技能准备灵气阶段 && UIFightPanel.Inst.UIFightState != UIFightState.回合结束弃置灵气阶段)
		{
			AnimFromEnv(count);
		}
	}

	protected override void PlayRemoveLingQiAnim(int count)
	{
		base.PlayRemoveLingQiAnim(count);
		if (UIFightPanel.Inst.UIFightState == UIFightState.释放技能准备灵气阶段 || UIFightPanel.Inst.UIFightState == UIFightState.回合结束弃置灵气阶段)
		{
			UIFightLingQiCacheSlot targetLingQiSlot = UIFightPanel.Inst.CacheLingQiController.GetTargetLingQiSlot(base.LingQiType);
			AnimPlayerToCache(targetLingQiSlot, count);
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

	private void AnimFromEnv(int count)
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		if (!UIFightLingQiSlot.IgnoreEffect)
		{
			UIFightPanel.Inst.GetMoveLingQi().SetData(base.LingQiType, new Vector3(((Component)this).transform.position.x, ((Component)this).transform.position.y + 2f, ((Component)this).transform.position.z), ((Component)this).transform.position, null, count);
			PlayLingQiSound();
		}
	}

	protected void AnimPlayerToCache(UIFightLingQiCacheSlot target, int count)
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		if (!UIFightLingQiSlot.IgnoreEffect)
		{
			UIFightMoveLingQi moveLingQi = UIFightPanel.Inst.GetMoveLingQi();
			((Component)moveLingQi).gameObject.SetActive(true);
			moveLingQi.SetData(base.LingQiType, ((Component)this).transform.position, ((Component)target).transform.position, null, count, showAnim: true);
			PlayLingQiSound();
		}
	}
}
