using System;
using System.Collections.Generic;
using GUIPackage;
using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace YSGame.Fight
{
	// Token: 0x02000ACA RID: 2762
	public class UIFightSkillItem : MonoBehaviour
	{
		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x06004D73 RID: 19827 RVA: 0x00211881 File Offset: 0x0020FA81
		public bool HasSkill
		{
			get
			{
				return this.hasSkill;
			}
		}

		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x06004D74 RID: 19828 RVA: 0x00211889 File Offset: 0x0020FA89
		// (set) Token: 0x06004D75 RID: 19829 RVA: 0x002118BB File Offset: 0x0020FABB
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

		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x06004D76 RID: 19830 RVA: 0x002118C4 File Offset: 0x0020FAC4
		// (set) Token: 0x06004D77 RID: 19831 RVA: 0x002118CC File Offset: 0x0020FACC
		public bool IsSelected
		{
			get
			{
				return this.isSelected;
			}
			set
			{
				this.isSelected = value;
				this.SelectedImage.gameObject.SetActive(value);
			}
		}

		// Token: 0x06004D78 RID: 19832 RVA: 0x002118E8 File Offset: 0x0020FAE8
		private void Awake()
		{
			this.SkillBtn.mouseEnterEvent.AddListener(new UnityAction(this.OnEnter));
			this.SkillBtn.mouseOutEvent.AddListener(new UnityAction(this.OnExit));
			this.SkillBtn.mouseUpEvent.AddListener(new UnityAction(this.ClickSkill));
		}

		// Token: 0x06004D79 RID: 19833 RVA: 0x00004095 File Offset: 0x00002295
		private void Start()
		{
		}

		// Token: 0x06004D7A RID: 19834 RVA: 0x00211949 File Offset: 0x0020FB49
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

		// Token: 0x06004D7B RID: 19835 RVA: 0x00211980 File Offset: 0x0020FB80
		public void OnEnter()
		{
			if (this.CanClick && !this.IsSelected)
			{
				this.SelectedImage.gameObject.SetActive(true);
			}
			if (this.hasSkill)
			{
				UIFightPanel.Inst.FightSkillTip.SetSkill(this.nowSkill);
				UIFightPanel.Inst.FightSkillTip.gameObject.SetActive(true);
				UIFightPanel.Inst.FightSkillTip.transform.position = new Vector3(base.transform.position.x, UIFightPanel.Inst.FightSkillTip.transform.position.y, UIFightPanel.Inst.FightSkillTip.transform.position.z);
			}
		}

		// Token: 0x06004D7C RID: 19836 RVA: 0x00211A3E File Offset: 0x0020FC3E
		public void OnExit()
		{
			if (this.CanClick && !this.IsSelected)
			{
				this.SelectedImage.gameObject.SetActive(false);
			}
			UIFightPanel.Inst.FightSkillTip.gameObject.SetActive(false);
		}

		// Token: 0x06004D7D RID: 19837 RVA: 0x00211A78 File Offset: 0x0020FC78
		private void ClickSkill()
		{
			if (this.internalCD <= 0f)
			{
				this.internalCD = 0.3f;
				if (this.hasSkill)
				{
					if (this.CanClick)
					{
						Debug.Log("点击了技能" + this.nowSkill.skill_Name);
						UIFightPanel.Inst.CancelSkillHighlight();
						this.IsSelected = true;
						RoundManager.instance.SetChoiceSkill(ref this.nowSkill);
						return;
					}
					if (UIFightPanel.Inst.UIFightState == UIFightState.回合结束弃置灵气阶段)
					{
						UIPopTip.Inst.Pop("当前正在消散灵气阶段", PopTipIconType.叹号);
						return;
					}
					UIPopTip.Inst.Pop("不满足释放条件", PopTipIconType.叹号);
				}
			}
		}

		// Token: 0x06004D7E RID: 19838 RVA: 0x00211B1A File Offset: 0x0020FD1A
		public void SetSkill(GUIPackage.Skill skill)
		{
			this.hasSkill = true;
			this.nowSkill = skill;
			this.IconImage.gameObject.SetActive(true);
			this.IconImage.sprite = skill.skillIconSprite;
			this.RefreshCD();
		}

		// Token: 0x06004D7F RID: 19839 RVA: 0x00211B54 File Offset: 0x0020FD54
		public void RefreshCD()
		{
			if (this.hasSkill)
			{
				if (this.nowSkill.CurCD != 0f)
				{
					this.CD.SetActive(true);
					this.CanClick = false;
					this.IsSelected = false;
					int num = (int)this.nowSkill.CurCD;
					Avatar player = PlayerEx.Player;
					if (player.SkillSeidFlag.ContainsKey(29) && player.SkillSeidFlag[29].ContainsKey(this.nowSkill.skill_ID) && player.SkillSeidFlag[29][this.nowSkill.skill_ID] > 0)
					{
						num = player.SkillSeidFlag[29][this.nowSkill.skill_ID];
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
					this.IsSelected = false;
				}
				this.ShowLianJiHightLight();
				return;
			}
			this.Clear();
		}

		// Token: 0x06004D80 RID: 19840 RVA: 0x00211CE8 File Offset: 0x0020FEE8
		public void ShowLianJiHightLight()
		{
			if (this.nowSkill.skill_ID == -1)
			{
				return;
			}
			bool active = false;
			List<int> seid = _skillJsonData.DataDict[this.nowSkill.skill_ID].seid;
			bool flag = false;
			foreach (int item in seid)
			{
				if (jsonData.instance.hightLightSkillID.Contains(item))
				{
					flag = true;
					break;
				}
			}
			if (flag)
			{
				Avatar player = Tools.instance.getPlayer();
				if (player == null || player.OtherAvatar == null)
				{
					return;
				}
				if (this.nowSkill.CanUse(player, player, false, "") == SkillCanUseType.可以使用)
				{
					Avatar receiver = player;
					if (jsonData.instance.skillJsonData[string.Concat(this.nowSkill.skill_ID)]["script"].str == "SkillAttack")
					{
						receiver = player.OtherAvatar;
					}
					if (this.nowSkill.CanRealizeSeid(player, receiver))
					{
						active = true;
					}
				}
			}
			this.LianJiHighlight.SetActive(active);
		}

		// Token: 0x06004D81 RID: 19841 RVA: 0x00211E18 File Offset: 0x00210018
		public void Clear()
		{
			this.hasSkill = false;
			this.IconImage.gameObject.SetActive(false);
			this.CD.SetActive(false);
			this.CanClick = false;
		}

		// Token: 0x04004C90 RID: 19600
		public Image IconImage;

		// Token: 0x04004C91 RID: 19601
		public Image QualityUpImage;

		// Token: 0x04004C92 RID: 19602
		public Image SelectedImage;

		// Token: 0x04004C93 RID: 19603
		public GameObject CD;

		// Token: 0x04004C94 RID: 19604
		public Text CDText;

		// Token: 0x04004C95 RID: 19605
		public Text CDText2;

		// Token: 0x04004C96 RID: 19606
		public KeyCode HotKey;

		// Token: 0x04004C97 RID: 19607
		public FpBtn SkillBtn;

		// Token: 0x04004C98 RID: 19608
		public GameObject LianJiHighlight;

		// Token: 0x04004C99 RID: 19609
		private bool hasSkill;

		// Token: 0x04004C9A RID: 19610
		private GUIPackage.Skill nowSkill;

		// Token: 0x04004C9B RID: 19611
		private bool canClick;

		// Token: 0x04004C9C RID: 19612
		private float internalCD;

		// Token: 0x04004C9D RID: 19613
		private bool isSelected;
	}
}
