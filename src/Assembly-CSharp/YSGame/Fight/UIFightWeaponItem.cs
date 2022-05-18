using System;
using GUIPackage;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace YSGame.Fight
{
	// Token: 0x02000E0B RID: 3595
	public class UIFightWeaponItem : MonoBehaviour
	{
		// Token: 0x1700080D RID: 2061
		// (get) Token: 0x060056D9 RID: 22233 RVA: 0x0003E180 File Offset: 0x0003C380
		// (set) Token: 0x060056DA RID: 22234 RVA: 0x0003E1B2 File Offset: 0x0003C3B2
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

		// Token: 0x060056DB RID: 22235 RVA: 0x00242888 File Offset: 0x00240A88
		private void Awake()
		{
			this.SkillBtn.mouseEnterEvent.AddListener(new UnityAction(this.OnEnter));
			this.SkillBtn.mouseOutEvent.AddListener(new UnityAction(this.OnExit));
			this.SkillBtn.mouseUpEvent.AddListener(new UnityAction(this.ClickSkill));
			this.itemDatebase = jsonData.instance.gameObject.GetComponent<ItemDatebase>();
		}

		// Token: 0x060056DC RID: 22236 RVA: 0x0003E1BB File Offset: 0x0003C3BB
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

		// Token: 0x060056DD RID: 22237 RVA: 0x00242900 File Offset: 0x00240B00
		public void OnEnter()
		{
			if (this.hasSkill)
			{
				UIFightPanel.Inst.FightSkillTip.SetWeapon(this.nowSkill, this.nowWeapon);
				UIFightPanel.Inst.FightSkillTip.gameObject.SetActive(true);
				UIFightPanel.Inst.FightSkillTip.transform.position = new Vector3(base.transform.position.x, UIFightPanel.Inst.FightSkillTip.transform.position.y, UIFightPanel.Inst.FightSkillTip.transform.position.z);
			}
		}

		// Token: 0x060056DE RID: 22238 RVA: 0x0003E1EF File Offset: 0x0003C3EF
		public void OnExit()
		{
			UIFightPanel.Inst.FightSkillTip.gameObject.SetActive(false);
		}

		// Token: 0x060056DF RID: 22239 RVA: 0x002429A4 File Offset: 0x00240BA4
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

		// Token: 0x060056E0 RID: 22240 RVA: 0x00242AA4 File Offset: 0x00240CA4
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

		// Token: 0x060056E1 RID: 22241 RVA: 0x00242C8C File Offset: 0x00240E8C
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

		// Token: 0x060056E2 RID: 22242 RVA: 0x00242DF0 File Offset: 0x00240FF0
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

		// Token: 0x04005684 RID: 22148
		public Image IconImage;

		// Token: 0x04005685 RID: 22149
		public Image QualityUpImage;

		// Token: 0x04005686 RID: 22150
		public Image SelectedImage;

		// Token: 0x04005687 RID: 22151
		public GameObject CD;

		// Token: 0x04005688 RID: 22152
		public Text CDText;

		// Token: 0x04005689 RID: 22153
		public Text CDText2;

		// Token: 0x0400568A RID: 22154
		public KeyCode HotKey;

		// Token: 0x0400568B RID: 22155
		public FpBtn SkillBtn;

		// Token: 0x0400568C RID: 22156
		private bool hasSkill;

		// Token: 0x0400568D RID: 22157
		private GUIPackage.Skill nowSkill;

		// Token: 0x0400568E RID: 22158
		private ITEM_INFO nowWeapon;

		// Token: 0x0400568F RID: 22159
		private bool canClick;

		// Token: 0x04005690 RID: 22160
		private ItemDatebase itemDatebase;

		// Token: 0x04005691 RID: 22161
		private float internalCD;
	}
}
