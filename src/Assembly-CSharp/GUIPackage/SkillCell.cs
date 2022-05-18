using System;
using UnityEngine;

namespace GUIPackage
{
	// Token: 0x02000D97 RID: 3479
	public class SkillCell : MonoBehaviour
	{
		// Token: 0x060053FA RID: 21498 RVA: 0x0003C035 File Offset: 0x0003A235
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

		// Token: 0x060053FB RID: 21499 RVA: 0x0023048C File Offset: 0x0022E68C
		public void UpdateRefresh()
		{
			if (this.skillID == -1)
			{
				this.Icon.GetComponent<UITexture>().mainTexture = null;
				this.Level.GetComponent<UILabel>().text = "";
				this.Name.GetComponent<UILabel>().text = "";
				this.PingZhi.GetComponent<UITexture>().mainTexture = null;
				if (this.KeyName != null)
				{
					this.KeyName.SetActive(false);
				}
				return;
			}
			this.Icon.GetComponent<UITexture>().mainTexture = Singleton.skillUI.skill[this.skillID].skill_Icon;
			this.Level.GetComponent<UILabel>().text = "Lv" + Singleton.skillUI.skill[this.skillID].skill_level.ToString() + "/" + Singleton.skillUI.skill[this.skillID].Max_level.ToString();
			this.Name.GetComponent<UILabel>().text = Singleton.skillUI.skill[this.skillID].skill_Name;
			this.PingZhi.GetComponent<UITexture>().mainTexture = Singleton.skillUI.skill[this.skillID].SkillPingZhi;
			if (this.KeyName == null)
			{
				return;
			}
			if (Tools.instance.getPlayer().showSkillName == 0 && this.showName)
			{
				this.KeyName.SetActive(true);
				this.NameLabel.text = Tools.instance.getSkillName(Singleton.skillUI.skill[this.skillID].skill_ID, true);
				return;
			}
			this.KeyName.SetActive(false);
		}

		// Token: 0x060053FC RID: 21500 RVA: 0x0003C068 File Offset: 0x0003A268
		private void OnPress()
		{
			if (this.skillID == -1)
			{
				return;
			}
			this.PCOnPress();
		}

		// Token: 0x060053FD RID: 21501 RVA: 0x0003C07A File Offset: 0x0003A27A
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

		// Token: 0x060053FE RID: 21502 RVA: 0x00230658 File Offset: 0x0022E858
		public void PCOnPress()
		{
			Singleton.skillUI.showTooltip = false;
			if (Input.GetMouseButton(0) && !Singleton.inventory.draggingItem && !Singleton.key.draggingKey && Singleton.skillUI.skill[this.skillID].CoolDown != 0f)
			{
				Singleton.skillUI.draggingSkill = true;
				Singleton.skillUI.dragedSkill = Singleton.skillUI.skill[this.skillID];
			}
		}

		// Token: 0x060053FF RID: 21503 RVA: 0x0003C0B4 File Offset: 0x0003A2B4
		private void OnHover(bool isOver)
		{
			this.PCOnHover(isOver);
		}

		// Token: 0x06005400 RID: 21504 RVA: 0x002306DC File Offset: 0x0022E8DC
		public void PCOnHover(bool isOver)
		{
			if (this.skillID == -1)
			{
				return;
			}
			if (isOver)
			{
				Singleton.skillUI.Show_Tooltip(Singleton.skillUI.skill[this.skillID]);
				Singleton.skillUI.showTooltip = true;
				return;
			}
			Singleton.skillUI.showTooltip = false;
		}

		// Token: 0x040053B6 RID: 21430
		public GameObject Icon;

		// Token: 0x040053B7 RID: 21431
		public GameObject Level;

		// Token: 0x040053B8 RID: 21432
		public GameObject Name;

		// Token: 0x040053B9 RID: 21433
		public GameObject PingZhi;

		// Token: 0x040053BA RID: 21434
		public int skillID;

		// Token: 0x040053BB RID: 21435
		public GameObject KeyName;

		// Token: 0x040053BC RID: 21436
		public UILabel NameLabel;

		// Token: 0x040053BD RID: 21437
		public bool showName;

		// Token: 0x040053BE RID: 21438
		private float refreshCD;
	}
}
