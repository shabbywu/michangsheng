using System;
using System.Collections.Generic;
using KBEngine;
using UnityEngine;

namespace GUIPackage
{
	// Token: 0x02000A6D RID: 2669
	public class SkillStaticCell : MonoBehaviour
	{
		// Token: 0x06004AFE RID: 19198 RVA: 0x001FE939 File Offset: 0x001FCB39
		private void Start()
		{
			this.avatar = Tools.instance.getPlayer();
		}

		// Token: 0x06004AFF RID: 19199 RVA: 0x001FE94B File Offset: 0x001FCB4B
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

		// Token: 0x06004B00 RID: 19200 RVA: 0x001FE980 File Offset: 0x001FCB80
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

		// Token: 0x06004B01 RID: 19201 RVA: 0x001FEB80 File Offset: 0x001FCD80
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

		// Token: 0x06004B02 RID: 19202 RVA: 0x001FEBF4 File Offset: 0x001FCDF4
		protected virtual void OnPress()
		{
			if (this.skillID == -1)
			{
				return;
			}
			this.PCOnPress();
		}

		// Token: 0x06004B03 RID: 19203 RVA: 0x001FEC06 File Offset: 0x001FCE06
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

		// Token: 0x06004B04 RID: 19204 RVA: 0x001FEC40 File Offset: 0x001FCE40
		public void PCOnPress()
		{
			this.skill_UIST.showTooltip = false;
			if (Input.GetMouseButton(0) && !Singleton.inventory.draggingItem && !Singleton.key.draggingKey && this.skill_UIST.skill[this.skillID].CoolDown != 0f)
			{
				this.skill_UIST.draggingSkill = true;
				this.skill_UIST.dragedSkill = this.skill_UIST.skill[this.skillID];
			}
		}

		// Token: 0x06004B05 RID: 19205 RVA: 0x001FECC8 File Offset: 0x001FCEC8
		public virtual void SetShow_Tooltip()
		{
			this.skill_UIST.Show_Tooltip(this.skill_UIST.skill[this.skillID], 0);
		}

		// Token: 0x06004B06 RID: 19206 RVA: 0x001FECEC File Offset: 0x001FCEEC
		private void OnHover(bool isOver)
		{
			this.PCOnHover(isOver);
		}

		// Token: 0x06004B07 RID: 19207 RVA: 0x001FECF5 File Offset: 0x001FCEF5
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

		// Token: 0x06004B08 RID: 19208 RVA: 0x001FED23 File Offset: 0x001FCF23
		public void skillUPCell()
		{
			this.skill_UIST.SkillUP(this.skillID);
		}

		// Token: 0x04004A26 RID: 18982
		public GameObject Icon;

		// Token: 0x04004A27 RID: 18983
		public GameObject Level;

		// Token: 0x04004A28 RID: 18984
		public GameObject Name;

		// Token: 0x04004A29 RID: 18985
		public GameObject PingZhi;

		// Token: 0x04004A2A RID: 18986
		public int skillID;

		// Token: 0x04004A2B RID: 18987
		public Skill_UIST skill_UIST;

		// Token: 0x04004A2C RID: 18988
		public GameObject KeyName;

		// Token: 0x04004A2D RID: 18989
		public UILabel NameLabel;

		// Token: 0x04004A2E RID: 18990
		public bool showName;

		// Token: 0x04004A2F RID: 18991
		private Avatar avatar;

		// Token: 0x04004A30 RID: 18992
		public GameObject Dengji;

		// Token: 0x04004A31 RID: 18993
		public UITexture uITexture;

		// Token: 0x04004A32 RID: 18994
		public List<Texture> sprites;

		// Token: 0x04004A33 RID: 18995
		public bool showDengji;

		// Token: 0x04004A34 RID: 18996
		private float refreshCD;
	}
}
