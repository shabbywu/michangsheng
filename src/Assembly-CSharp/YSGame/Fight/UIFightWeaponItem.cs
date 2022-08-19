using System;
using GUIPackage;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace YSGame.Fight
{
	// Token: 0x02000ACC RID: 2764
	public class UIFightWeaponItem : MonoBehaviour
	{
		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x06004D87 RID: 19847 RVA: 0x00212706 File Offset: 0x00210906
		// (set) Token: 0x06004D88 RID: 19848 RVA: 0x00212738 File Offset: 0x00210938
		private bool CanClick
		{
			get
			{
				return !UIFightPanel.Inst.BanSkillAndWeapon && (UIFightPanel.Inst.UIFightState == UIFightState.自己回合普通状态 || UIFightPanel.Inst.UIFightState == UIFightState.释放技能准备灵气阶段) && this.canClick;
			}
			set
			{
				this.canClick = value;
			}
		}

		// Token: 0x06004D89 RID: 19849 RVA: 0x00212744 File Offset: 0x00210944
		private void Awake()
		{
			this.SkillBtn.mouseEnterEvent.AddListener(new UnityAction(this.OnEnter));
			this.SkillBtn.mouseOutEvent.AddListener(new UnityAction(this.OnExit));
			this.SkillBtn.mouseUpEvent.AddListener(new UnityAction(this.ClickSkill));
			this.itemDatebase = jsonData.instance.gameObject.GetComponent<ItemDatebase>();
		}

		// Token: 0x06004D8A RID: 19850 RVA: 0x002127BA File Offset: 0x002109BA
		private void Update()
		{
			if (Input.GetKeyUp(this.HotKey))
			{
				this.ClickSkill();
			}
			if (this.internalCD > 0f)
			{
				this.internalCD -= Time.deltaTime;
			}
		}

		// Token: 0x06004D8B RID: 19851 RVA: 0x002127F0 File Offset: 0x002109F0
		public void OnEnter()
		{
			if (this.hasSkill)
			{
				UIFightPanel.Inst.FightSkillTip.SetWeapon(this.nowSkill, this.nowWeapon);
				UIFightPanel.Inst.FightSkillTip.gameObject.SetActive(true);
				UIFightPanel.Inst.FightSkillTip.transform.position = new Vector3(base.transform.position.x, UIFightPanel.Inst.FightSkillTip.transform.position.y, UIFightPanel.Inst.FightSkillTip.transform.position.z);
			}
		}

		// Token: 0x06004D8C RID: 19852 RVA: 0x00212893 File Offset: 0x00210A93
		public void OnExit()
		{
			UIFightPanel.Inst.FightSkillTip.gameObject.SetActive(false);
		}

		// Token: 0x06004D8D RID: 19853 RVA: 0x002128AC File Offset: 0x00210AAC
		private void ClickSkill()
		{
			if (this.internalCD <= 0f)
			{
				this.internalCD = 0.3f;
				if (this.hasSkill)
				{
					if (this.CanClick)
					{
						Debug.Log("点击了武器" + this.nowSkill.skill_Name);
						UIFightPanel.Inst.CancelSkillHighlight();
						UIFightPanel.Inst.CacheLingQiController.MoveAllLingQiToPlayer();
						UIFightPanel.Inst.CacheLingQiController.ChangeCacheSlotNumber(0);
						RoundManager.instance.ChoiceSkill = this.nowSkill;
						RoundManager.instance.UseSkill(this.nowWeapon.uuid, true);
						if (this == UIFightPanel.Inst.FightWeapon[0])
						{
							MessageMag.Instance.Send(FightFaBaoShow.PlayerUseWeaponMsgKey, null);
							return;
						}
					}
					else
					{
						if (UIFightPanel.Inst.UIFightState == UIFightState.回合结束弃置灵气阶段)
						{
							UIPopTip.Inst.Pop("当前正在消散灵气阶段", PopTipIconType.叹号);
							return;
						}
						UIPopTip.Inst.Pop("不满足释放条件", PopTipIconType.叹号);
					}
				}
			}
		}

		// Token: 0x06004D8E RID: 19854 RVA: 0x002129AC File Offset: 0x00210BAC
		public void SetWeapon(GUIPackage.Skill skill, ITEM_INFO weapon)
		{
			this.hasSkill = true;
			this.nowSkill = skill;
			this.nowWeapon = weapon;
			this.nowSkill.weaponuuid = weapon.uuid;
			this.IconImage.gameObject.SetActive(true);
			if (this.IconImage.sprite.texture != this.itemDatebase.items[this.nowWeapon.itemId].itemIcon)
			{
				this.IconImage.sprite = Sprite.Create(this.itemDatebase.items[this.nowWeapon.itemId].itemIcon, new Rect(0f, 0f, 128f, 128f), new Vector2(128f, 128f));
				this.QualityUpImage.sprite = this.itemDatebase.items[this.nowWeapon.itemId].newitemPingZhiSprite;
			}
			else
			{
				this.IconImage.sprite = Sprite.Create(this.nowSkill.skill_Icon, new Rect(0f, 0f, 128f, 128f), new Vector2(128f, 128f));
			}
			if (skill.ItemAddSeid != null)
			{
				this.IconImage.sprite = Sprite.Create(this.nowSkill.skill_Icon, new Rect(0f, 0f, 128f, 128f), new Vector2(128f, 128f));
				int i = weapon.Seid["quality"].I;
				this.QualityUpImage.sprite = ResManager.inst.LoadSprite("NewUI/Inventory/Icon/MCS_icon_quality_" + (i + 1));
			}
			this.QualityUpImage.gameObject.SetActive(true);
			this.RefreshCD();
		}

		// Token: 0x06004D8F RID: 19855 RVA: 0x00212B94 File Offset: 0x00210D94
		public void RefreshCD()
		{
			if (this.hasSkill)
			{
				if (this.nowSkill.CurCD != 0f)
				{
					this.CD.SetActive(true);
					this.CanClick = false;
					int num = (int)this.nowSkill.CurCD;
					Avatar player = PlayerEx.Player;
					if (this.nowWeapon.uuid != "" && RoundManager.instance.WeaponSkillList.ContainsKey(this.nowWeapon.uuid))
					{
						num = RoundManager.instance.WeaponSkillList[this.nowWeapon.uuid];
					}
					if (num > 9)
					{
						this.CDText.text = "";
						this.CDText2.gameObject.SetActive(false);
					}
					else
					{
						this.CDText.text = num.ToString();
						this.CDText2.gameObject.SetActive(true);
					}
				}
				else
				{
					this.CD.SetActive(false);
					this.CanClick = true;
				}
				SkillCanUseType skillCanUseType = this.nowSkill.CanUse(PlayerEx.Player, PlayerEx.Player, false, "");
				if (skillCanUseType != SkillCanUseType.可以使用 && skillCanUseType != SkillCanUseType.尚未冷却不能使用)
				{
					this.CD.SetActive(true);
					this.CDText.text = "";
					this.CDText2.gameObject.SetActive(false);
					this.CanClick = false;
					return;
				}
			}
			else
			{
				this.Clear();
			}
		}

		// Token: 0x06004D90 RID: 19856 RVA: 0x00212CF5 File Offset: 0x00210EF5
		public void SetLock(bool isLock)
		{
			this.Lock.SetActive(isLock);
		}

		// Token: 0x06004D91 RID: 19857 RVA: 0x00212D04 File Offset: 0x00210F04
		public void Clear()
		{
			this.hasSkill = false;
			this.IconImage.gameObject.SetActive(false);
			this.QualityUpImage.gameObject.SetActive(false);
			this.CD.SetActive(false);
			this.CDText.text = "";
			this.CDText2.gameObject.SetActive(false);
			this.CanClick = false;
		}

		// Token: 0x04004CA8 RID: 19624
		public Image IconImage;

		// Token: 0x04004CA9 RID: 19625
		public Image QualityUpImage;

		// Token: 0x04004CAA RID: 19626
		public Image SelectedImage;

		// Token: 0x04004CAB RID: 19627
		public GameObject CD;

		// Token: 0x04004CAC RID: 19628
		public Text CDText;

		// Token: 0x04004CAD RID: 19629
		public Text CDText2;

		// Token: 0x04004CAE RID: 19630
		public KeyCode HotKey;

		// Token: 0x04004CAF RID: 19631
		public FpBtn SkillBtn;

		// Token: 0x04004CB0 RID: 19632
		public GameObject Lock;

		// Token: 0x04004CB1 RID: 19633
		private bool hasSkill;

		// Token: 0x04004CB2 RID: 19634
		private GUIPackage.Skill nowSkill;

		// Token: 0x04004CB3 RID: 19635
		private ITEM_INFO nowWeapon;

		// Token: 0x04004CB4 RID: 19636
		private bool canClick;

		// Token: 0x04004CB5 RID: 19637
		private ItemDatebase itemDatebase;

		// Token: 0x04004CB6 RID: 19638
		private float internalCD;
	}
}
