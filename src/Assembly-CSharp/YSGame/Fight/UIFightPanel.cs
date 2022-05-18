using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using YSGame.TuJian;

namespace YSGame.Fight
{
	// Token: 0x02000E03 RID: 3587
	public class UIFightPanel : MonoBehaviour
	{
		// Token: 0x17000809 RID: 2057
		// (get) Token: 0x060056A5 RID: 22181 RVA: 0x002415CC File Offset: 0x0023F7CC
		public int NeedYiSanCount
		{
			get
			{
				int num = 0;
				for (int i = 0; i < 6; i++)
				{
					num += PlayerEx.Player.cardMag[i];
				}
				return num - (int)PlayerEx.Player.NowCard;
			}
		}

		// Token: 0x060056A6 RID: 22182 RVA: 0x0003DEB9 File Offset: 0x0003C0B9
		private void Awake()
		{
			UIFightPanel.Inst = this;
			this.JiLuBtn.mouseUpEvent.AddListener(new UnityAction(this.FightJiLu.ToggleOpen));
		}

		// Token: 0x060056A7 RID: 22183 RVA: 0x0003DEE2 File Offset: 0x0003C0E2
		private void Start()
		{
			this.TaoPaoBtn.mouseUpEvent.AddListener(new UnityAction(RoundManager.instance.PlayRunAway));
		}

		// Token: 0x060056A8 RID: 22184 RVA: 0x00241608 File Offset: 0x0023F808
		private void Update()
		{
			bool nowDoingLingQiAnim = this.NowDoingLingQiAnim;
			this.NowDoingLingQiAnim = false;
			using (List<UIFightMoveLingQi>.Enumerator enumerator = this.MoveLingQiList.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.IsStart)
					{
						this.NowDoingLingQiAnim = true;
						break;
					}
				}
			}
			if (nowDoingLingQiAnim != this.NowDoingLingQiAnim)
			{
				this.RefreshLingQiCount(!this.NowDoingLingQiAnim);
			}
			if (this.refreshCD < 0f)
			{
				this.refreshCD = 1f;
				this.RefreshCD();
			}
			else
			{
				this.refreshCD -= Time.deltaTime;
			}
			if (this.NeedPlayLingQiSound)
			{
				this.NeedPlayLingQiSound = false;
				this.PlayLingQiSound();
			}
		}

		// Token: 0x060056A9 RID: 22185 RVA: 0x0003DF04 File Offset: 0x0003C104
		public void RefreshLingQiCount(bool show)
		{
			this.CacheLingQiController.RefreshLingQiCountShow(show);
			this.PlayerLingQiController.RefreshLingQiCount(show);
		}

		// Token: 0x060056AA RID: 22186 RVA: 0x0003D75E File Offset: 0x0003B95E
		public void Help()
		{
			TuJianManager.Inst.OnHyperlink("2_502_0");
		}

		// Token: 0x060056AB RID: 22187 RVA: 0x0003DF1E File Offset: 0x0003C11E
		public void Close()
		{
			this.ScaleObj.SetActive(false);
		}

		// Token: 0x060056AC RID: 22188 RVA: 0x0003DF2C File Offset: 0x0003C12C
		public void Clear()
		{
			this.HuaShenLingYuBtn.transform.parent.gameObject.SetActive(false);
			this.FightJiLu.JiLuText.text = "";
		}

		// Token: 0x060056AD RID: 22189 RVA: 0x002416D0 File Offset: 0x0023F8D0
		public UIFightMoveLingQi GetMoveLingQi()
		{
			for (int i = 0; i < this.MoveLingQiList.Count; i++)
			{
				if (!this.MoveLingQiList[i].IsStart)
				{
					return this.MoveLingQiList[i];
				}
			}
			UIFightMoveLingQi component = Object.Instantiate<GameObject>(this.MoveLingQiPrefab, this.MoveLingQiRoot).GetComponent<UIFightMoveLingQi>();
			this.MoveLingQiList.Add(component);
			return component;
		}

		// Token: 0x060056AE RID: 22190 RVA: 0x00241738 File Offset: 0x0023F938
		public void CancelSkillHighlight()
		{
			foreach (UIFightSkillItem uifightSkillItem in this.FightSkills)
			{
				uifightSkillItem.IsSelected = false;
			}
		}

		// Token: 0x060056AF RID: 22191 RVA: 0x0024178C File Offset: 0x0023F98C
		public void RefreshCD()
		{
			foreach (UIFightSkillItem uifightSkillItem in this.FightSkills)
			{
				uifightSkillItem.RefreshCD();
			}
			foreach (UIFightWeaponItem uifightWeaponItem in this.FightWeapon)
			{
				uifightWeaponItem.RefreshCD();
			}
		}

		// Token: 0x060056B0 RID: 22192 RVA: 0x0024181C File Offset: 0x0023FA1C
		public void PlayLingQiSound()
		{
			int index = 18 + Random.Range(0, 5);
			MusicMag.instance.PlayEffectMusic(index, 1f);
		}

		// Token: 0x0400562F RID: 22063
		public static UIFightPanel Inst;

		// Token: 0x04005630 RID: 22064
		public List<Sprite> LingQiSprites;

		// Token: 0x04005631 RID: 22065
		public GameObject MoveLingQiPrefab;

		// Token: 0x04005632 RID: 22066
		public Transform MoveLingQiRoot;

		// Token: 0x04005633 RID: 22067
		[HideInInspector]
		public List<UIFightMoveLingQi> MoveLingQiList = new List<UIFightMoveLingQi>();

		// Token: 0x04005634 RID: 22068
		public UICacheLingQiController CacheLingQiController;

		// Token: 0x04005635 RID: 22069
		public UIPlayerLingQiController PlayerLingQiController;

		// Token: 0x04005636 RID: 22070
		public UIFightCenterButtonController FightCenterButtonController;

		// Token: 0x04005637 RID: 22071
		public UIFightCenterTip FightCenterTip;

		// Token: 0x04005638 RID: 22072
		public UIFightAvatarStatus PlayerStatus;

		// Token: 0x04005639 RID: 22073
		public UIFightAvatarStatus DiRenStatus;

		// Token: 0x0400563A RID: 22074
		public UIFightJiLu FightJiLu;

		// Token: 0x0400563B RID: 22075
		public UIFightSelectLingQi FightSelectLingQi;

		// Token: 0x0400563C RID: 22076
		public List<UIFightSkillItem> FightSkills;

		// Token: 0x0400563D RID: 22077
		public List<UIFightWeaponItem> FightWeapon;

		// Token: 0x0400563E RID: 22078
		public FpBtn HuaShenLingYuBtn;

		// Token: 0x0400563F RID: 22079
		public FpBtn HelpBtn;

		// Token: 0x04005640 RID: 22080
		public FpBtn JiLuBtn;

		// Token: 0x04005641 RID: 22081
		public FpBtn TaoPaoBtn;

		// Token: 0x04005642 RID: 22082
		public GameObject ScaleObj;

		// Token: 0x04005643 RID: 22083
		public UIFightSkillTip FightSkillTip;

		// Token: 0x04005644 RID: 22084
		public UIFightRoundCount FightRoundCount;

		// Token: 0x04005645 RID: 22085
		public List<UILingQiImageData> LingQiImageDatas;

		// Token: 0x04005646 RID: 22086
		public UIFightState UIFightState;

		// Token: 0x04005647 RID: 22087
		public bool NowDoingLingQiAnim;

		// Token: 0x04005648 RID: 22088
		public bool BanSkillAndWeapon;

		// Token: 0x04005649 RID: 22089
		private float refreshCD;

		// Token: 0x0400564A RID: 22090
		public bool NeedPlayLingQiSound;
	}
}
