using System;
using System.Collections.Generic;
using KBEngine;
using UnityEngine;

namespace GUIPackage
{
	// Token: 0x02000D9A RID: 3482
	public class SkillStaticCell : MonoBehaviour
	{
		// Token: 0x0600540A RID: 21514 RVA: 0x0003C138 File Offset: 0x0003A338
		private void Start()
		{
			this.avatar = Tools.instance.getPlayer();
		}

		// Token: 0x0600540B RID: 21515 RVA: 0x0003C14A File Offset: 0x0003A34A
		private void Update()
		{
			if (this.refreshCD < 0f)
			{
				this.UpdateRefresh();
				this.refreshCD = 0.2f;
				return;
			}
			this.refreshCD -= Time.deltaTime;
		}

		// Token: 0x0600540C RID: 21516 RVA: 0x00230870 File Offset: 0x0022EA70
		public void UpdateRefresh()
		{
			if (this.skillID == -1)
			{
				this.Icon.GetComponent<UITexture>().mainTexture = null;
				this.Level.GetComponent<UILabel>().text = "";
				this.Name.GetComponent<UILabel>().text = "";
				if (this.PingZhi != null)
				{
					this.PingZhi.GetComponent<UITexture>().mainTexture = null;
				}
				if (this.KeyName != null)
				{
					this.KeyName.SetActive(false);
				}
				return;
			}
			this.Icon.GetComponent<UITexture>().mainTexture = this.skill_UIST.skill[this.skillID].skill_Icon;
			this.Level.GetComponent<UILabel>().text = "Lv" + this.getSkillLevel(this.skill_UIST.skill[this.skillID].skill_ID).ToString() + "/" + Singleton.skillUI2.skill[this.skillID].Max_level.ToString();
			this.Name.GetComponent<UILabel>().text = this.skill_UIST.skill[this.skillID].skill_Name;
			if (this.PingZhi != null)
			{
				this.PingZhi.GetComponent<UITexture>().mainTexture = this.skill_UIST.skill[this.skillID].SkillPingZhi;
			}
			if (this.KeyName == null)
			{
				return;
			}
			if (Tools.instance.getPlayer().showSkillName == 0 && this.showName)
			{
				this.KeyName.SetActive(true);
				this.NameLabel.text = Tools.instance.getStaticSkillName(this.skill_UIST.skill[this.skillID].skill_ID, false);
			}
			else
			{
				this.KeyName.SetActive(false);
			}
			this.Dengji.SetActive(false);
		}

		// Token: 0x0600540D RID: 21517 RVA: 0x00230A70 File Offset: 0x0022EC70
		public int getSkillLevel(int SkillID)
		{
			int staticSkillIDByKey = Tools.instance.getStaticSkillIDByKey(SkillID);
			foreach (SkillItem skillItem in this.avatar.hasStaticSkillList)
			{
				if (staticSkillIDByKey == skillItem.itemId)
				{
					return skillItem.level;
				}
			}
			return 0;
		}

		// Token: 0x0600540E RID: 21518 RVA: 0x0003C17D File Offset: 0x0003A37D
		protected virtual void OnPress()
		{
			if (this.skillID == -1)
			{
				return;
			}
			this.PCOnPress();
		}

		// Token: 0x0600540F RID: 21519 RVA: 0x0003C18F File Offset: 0x0003A38F
		public void MobilePress()
		{
			this.PCOnHover(true);
			Singleton.ToolTipsBackGround.openTooltips();
			TooltipsBackgroundi toolTipsBackGround = Singleton.ToolTipsBackGround;
			toolTipsBackGround.CloseAction = delegate()
			{
				this.PCOnHover(false);
			};
			toolTipsBackGround.use.gameObject.SetActive(false);
		}

		// Token: 0x06005410 RID: 21520 RVA: 0x00230AE4 File Offset: 0x0022ECE4
		public void PCOnPress()
		{
			this.skill_UIST.showTooltip = false;
			if (Input.GetMouseButton(0) && !Singleton.inventory.draggingItem && !Singleton.key.draggingKey && this.skill_UIST.skill[this.skillID].CoolDown != 0f)
			{
				this.skill_UIST.draggingSkill = true;
				this.skill_UIST.dragedSkill = this.skill_UIST.skill[this.skillID];
			}
		}

		// Token: 0x06005411 RID: 21521 RVA: 0x0003C1C9 File Offset: 0x0003A3C9
		public virtual void SetShow_Tooltip()
		{
			this.skill_UIST.Show_Tooltip(this.skill_UIST.skill[this.skillID], 0);
		}

		// Token: 0x06005412 RID: 21522 RVA: 0x0003C1ED File Offset: 0x0003A3ED
		private void OnHover(bool isOver)
		{
			this.PCOnHover(isOver);
		}

		// Token: 0x06005413 RID: 21523 RVA: 0x0003C1F6 File Offset: 0x0003A3F6
		public void PCOnHover(bool isOver)
		{
			if (this.skillID == -1)
			{
				return;
			}
			if (isOver)
			{
				this.SetShow_Tooltip();
				this.skill_UIST.showTooltip = true;
				return;
			}
			this.skill_UIST.showTooltip = false;
		}

		// Token: 0x06005414 RID: 21524 RVA: 0x0003C224 File Offset: 0x0003A424
		public void skillUPCell()
		{
			this.skill_UIST.SkillUP(this.skillID);
		}

		// Token: 0x040053C5 RID: 21445
		public GameObject Icon;

		// Token: 0x040053C6 RID: 21446
		public GameObject Level;

		// Token: 0x040053C7 RID: 21447
		public GameObject Name;

		// Token: 0x040053C8 RID: 21448
		public GameObject PingZhi;

		// Token: 0x040053C9 RID: 21449
		public int skillID;

		// Token: 0x040053CA RID: 21450
		public Skill_UIST skill_UIST;

		// Token: 0x040053CB RID: 21451
		public GameObject KeyName;

		// Token: 0x040053CC RID: 21452
		public UILabel NameLabel;

		// Token: 0x040053CD RID: 21453
		public bool showName;

		// Token: 0x040053CE RID: 21454
		private Avatar avatar;

		// Token: 0x040053CF RID: 21455
		public GameObject Dengji;

		// Token: 0x040053D0 RID: 21456
		public UITexture uITexture;

		// Token: 0x040053D1 RID: 21457
		public List<Texture> sprites;

		// Token: 0x040053D2 RID: 21458
		public bool showDengji;

		// Token: 0x040053D3 RID: 21459
		private float refreshCD;
	}
}
