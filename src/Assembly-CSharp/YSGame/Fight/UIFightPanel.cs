using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using YSGame.TuJian;

namespace YSGame.Fight
{
	// Token: 0x02000AC5 RID: 2757
	public class UIFightPanel : MonoBehaviour
	{
		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x06004D56 RID: 19798 RVA: 0x0021115C File Offset: 0x0020F35C
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

		// Token: 0x06004D57 RID: 19799 RVA: 0x00211196 File Offset: 0x0020F396
		private void Awake()
		{
			UIFightPanel.Inst = this;
			this.JiLuBtn.mouseUpEvent.AddListener(new UnityAction(this.FightJiLu.ToggleOpen));
		}

		// Token: 0x06004D58 RID: 19800 RVA: 0x002111BF File Offset: 0x0020F3BF
		private void Start()
		{
			this.TaoPaoBtn.mouseUpEvent.AddListener(new UnityAction(RoundManager.instance.PlayRunAway));
		}

		// Token: 0x06004D59 RID: 19801 RVA: 0x002111E4 File Offset: 0x0020F3E4
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

		// Token: 0x06004D5A RID: 19802 RVA: 0x002112AC File Offset: 0x0020F4AC
		public void RefreshLingQiCount(bool show)
		{
			this.CacheLingQiController.RefreshLingQiCountShow(show);
			this.PlayerLingQiController.RefreshLingQiCount(show);
		}

		// Token: 0x06004D5B RID: 19803 RVA: 0x002112C6 File Offset: 0x0020F4C6
		public void Help()
		{
			TuJianManager.Inst.OnHyperlink("2_502_0");
		}

		// Token: 0x06004D5C RID: 19804 RVA: 0x002112D7 File Offset: 0x0020F4D7
		public void Close()
		{
			this.ScaleObj.SetActive(false);
		}

		// Token: 0x06004D5D RID: 19805 RVA: 0x002112E5 File Offset: 0x0020F4E5
		public void Clear()
		{
			this.HuaShenLingYuBtn.transform.parent.gameObject.SetActive(false);
			this.FightJiLu.JiLuText.text = "";
		}

		// Token: 0x06004D5E RID: 19806 RVA: 0x00211318 File Offset: 0x0020F518
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

		// Token: 0x06004D5F RID: 19807 RVA: 0x00211380 File Offset: 0x0020F580
		public void CancelSkillHighlight()
		{
			foreach (UIFightSkillItem uifightSkillItem in this.FightSkills)
			{
				uifightSkillItem.IsSelected = false;
			}
		}

		// Token: 0x06004D60 RID: 19808 RVA: 0x002113D4 File Offset: 0x0020F5D4
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

		// Token: 0x06004D61 RID: 19809 RVA: 0x00211464 File Offset: 0x0020F664
		public void PlayLingQiSound()
		{
			int index = 18 + Random.Range(0, 5);
			MusicMag.instance.PlayEffectMusic(index, 1f);
		}

		// Token: 0x04004C55 RID: 19541
		public static UIFightPanel Inst;

		// Token: 0x04004C56 RID: 19542
		public List<Sprite> LingQiSprites;

		// Token: 0x04004C57 RID: 19543
		public GameObject MoveLingQiPrefab;

		// Token: 0x04004C58 RID: 19544
		public Transform MoveLingQiRoot;

		// Token: 0x04004C59 RID: 19545
		[HideInInspector]
		public List<UIFightMoveLingQi> MoveLingQiList = new List<UIFightMoveLingQi>();

		// Token: 0x04004C5A RID: 19546
		public UICacheLingQiController CacheLingQiController;

		// Token: 0x04004C5B RID: 19547
		public UIPlayerLingQiController PlayerLingQiController;

		// Token: 0x04004C5C RID: 19548
		public UIFightCenterButtonController FightCenterButtonController;

		// Token: 0x04004C5D RID: 19549
		public UIFightCenterTip FightCenterTip;

		// Token: 0x04004C5E RID: 19550
		public UIFightAvatarStatus PlayerStatus;

		// Token: 0x04004C5F RID: 19551
		public UIFightAvatarStatus DiRenStatus;

		// Token: 0x04004C60 RID: 19552
		public UIFightJiLu FightJiLu;

		// Token: 0x04004C61 RID: 19553
		public UIFightSelectLingQi FightSelectLingQi;

		// Token: 0x04004C62 RID: 19554
		public List<UIFightSkillItem> FightSkills;

		// Token: 0x04004C63 RID: 19555
		public List<UIFightWeaponItem> FightWeapon;

		// Token: 0x04004C64 RID: 19556
		public FpBtn HuaShenLingYuBtn;

		// Token: 0x04004C65 RID: 19557
		public FpBtn HelpBtn;

		// Token: 0x04004C66 RID: 19558
		public FpBtn JiLuBtn;

		// Token: 0x04004C67 RID: 19559
		public FpBtn TaoPaoBtn;

		// Token: 0x04004C68 RID: 19560
		public GameObject ScaleObj;

		// Token: 0x04004C69 RID: 19561
		public UIFightSkillTip FightSkillTip;

		// Token: 0x04004C6A RID: 19562
		public UIFightRoundCount FightRoundCount;

		// Token: 0x04004C6B RID: 19563
		public List<UILingQiImageData> LingQiImageDatas;

		// Token: 0x04004C6C RID: 19564
		public UIFightState UIFightState;

		// Token: 0x04004C6D RID: 19565
		public bool NowDoingLingQiAnim;

		// Token: 0x04004C6E RID: 19566
		public bool BanSkillAndWeapon;

		// Token: 0x04004C6F RID: 19567
		private float refreshCD;

		// Token: 0x04004C70 RID: 19568
		public bool NeedPlayLingQiSound;
	}
}
