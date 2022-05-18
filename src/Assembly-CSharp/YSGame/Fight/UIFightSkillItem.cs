using System;
using GUIPackage;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace YSGame.Fight
{
	// Token: 0x02000E08 RID: 3592
	public class UIFightSkillItem : MonoBehaviour
	{
		// Token: 0x1700080A RID: 2058
		// (get) Token: 0x060056C2 RID: 22210 RVA: 0x0003E03E File Offset: 0x0003C23E
		public bool HasSkill
		{
			get
			{
				return this.hasSkill;
			}
		}

		// Token: 0x1700080B RID: 2059
		// (get) Token: 0x060056C3 RID: 22211 RVA: 0x0003E046 File Offset: 0x0003C246
		// (set) Token: 0x060056C4 RID: 22212 RVA: 0x0003E078 File Offset: 0x0003C278
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

		// Token: 0x1700080C RID: 2060
		// (get) Token: 0x060056C5 RID: 22213 RVA: 0x0003E081 File Offset: 0x0003C281
		// (set) Token: 0x060056C6 RID: 22214 RVA: 0x0003E089 File Offset: 0x0003C289
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

		// Token: 0x060056C7 RID: 22215 RVA: 0x00241B5C File Offset: 0x0023FD5C
		private void Awake()
		{
			this.SkillBtn.mouseEnterEvent.AddListener(new UnityAction(this.OnEnter));
			this.SkillBtn.mouseOutEvent.AddListener(new UnityAction(this.OnExit));
			this.SkillBtn.mouseUpEvent.AddListener(new UnityAction(this.ClickSkill));
		}

		// Token: 0x060056C8 RID: 22216 RVA: 0x000042DD File Offset: 0x000024DD
		private void Start()
		{
		}

		// Token: 0x060056C9 RID: 22217 RVA: 0x0003E0A3 File Offset: 0x0003C2A3
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

		// Token: 0x060056CA RID: 22218 RVA: 0x00241BC0 File Offset: 0x0023FDC0
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

		// Token: 0x060056CB RID: 22219 RVA: 0x0003E0D7 File Offset: 0x0003C2D7
		public void OnExit()
		{
			if (this.CanClick && !this.IsSelected)
			{
				this.SelectedImage.gameObject.SetActive(false);
			}
			UIFightPanel.Inst.FightSkillTip.gameObject.SetActive(false);
		}

		// Token: 0x060056CC RID: 22220 RVA: 0x00241C80 File Offset: 0x0023FE80
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

		// Token: 0x060056CD RID: 22221 RVA: 0x0003E10F File Offset: 0x0003C30F
		public void SetSkill(GUIPackage.Skill skill)
		{
			this.hasSkill = true;
			this.nowSkill = skill;
			this.IconImage.gameObject.SetActive(true);
			this.IconImage.sprite = skill.skillIconSprite;
			this.RefreshCD();
		}

		// Token: 0x060056CE RID: 22222 RVA: 0x00241D24 File Offset: 0x0023FF24
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

		// Token: 0x060056CF RID: 22223 RVA: 0x00241EB8 File Offset: 0x002400B8
		public void ShowLianJiHightLight()
		{
			if (this.nowSkill.skill_ID == -1)
			{
				return;
			}
			bool active = false;
			if (jsonData.instance.skillJsonData[string.Concat(this.nowSkill.skill_ID)]["seid"].list.Find((JSONObject aa) => jsonData.instance.hightLightSkillID.Contains((int)aa.n)) != null)
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

		// Token: 0x060056D0 RID: 22224 RVA: 0x0003E147 File Offset: 0x0003C347
		public void Clear()
		{
			this.hasSkill = false;
			this.IconImage.gameObject.SetActive(false);
			this.CD.SetActive(false);
			this.CanClick = false;
		}

		// Token: 0x0400566A RID: 22122
		public Image IconImage;

		// Token: 0x0400566B RID: 22123
		public Image QualityUpImage;

		// Token: 0x0400566C RID: 22124
		public Image SelectedImage;

		// Token: 0x0400566D RID: 22125
		public GameObject CD;

		// Token: 0x0400566E RID: 22126
		public Text CDText;

		// Token: 0x0400566F RID: 22127
		public Text CDText2;

		// Token: 0x04005670 RID: 22128
		public KeyCode HotKey;

		// Token: 0x04005671 RID: 22129
		public FpBtn SkillBtn;

		// Token: 0x04005672 RID: 22130
		public GameObject LianJiHighlight;

		// Token: 0x04005673 RID: 22131
		private bool hasSkill;

		// Token: 0x04005674 RID: 22132
		private GUIPackage.Skill nowSkill;

		// Token: 0x04005675 RID: 22133
		private bool canClick;

		// Token: 0x04005676 RID: 22134
		private float internalCD;

		// Token: 0x04005677 RID: 22135
		private bool isSelected;
	}
}
