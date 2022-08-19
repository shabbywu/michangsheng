using System;
using UnityEngine;

namespace GUIPackage
{
	// Token: 0x02000A6B RID: 2667
	public class SkillCell : MonoBehaviour
	{
		// Token: 0x06004AF0 RID: 19184 RVA: 0x001FE456 File Offset: 0x001FC656
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

		// Token: 0x06004AF1 RID: 19185 RVA: 0x001FE48C File Offset: 0x001FC68C
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

		// Token: 0x06004AF2 RID: 19186 RVA: 0x001FE655 File Offset: 0x001FC855
		private void OnPress()
		{
			if (this.skillID == -1)
			{
				return;
			}
			this.PCOnPress();
		}

		// Token: 0x06004AF3 RID: 19187 RVA: 0x001FE667 File Offset: 0x001FC867
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

		// Token: 0x06004AF4 RID: 19188 RVA: 0x001FE6A4 File Offset: 0x001FC8A4
		public void PCOnPress()
		{
			Singleton.skillUI.showTooltip = false;
			if (Input.GetMouseButton(0) && !Singleton.inventory.draggingItem && !Singleton.key.draggingKey && Singleton.skillUI.skill[this.skillID].CoolDown != 0f)
			{
				Singleton.skillUI.draggingSkill = true;
				Singleton.skillUI.dragedSkill = Singleton.skillUI.skill[this.skillID];
			}
		}

		// Token: 0x06004AF5 RID: 19189 RVA: 0x001FE727 File Offset: 0x001FC927
		private void OnHover(bool isOver)
		{
			this.PCOnHover(isOver);
		}

		// Token: 0x06004AF6 RID: 19190 RVA: 0x001FE730 File Offset: 0x001FC930
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

		// Token: 0x04004A19 RID: 18969
		public GameObject Icon;

		// Token: 0x04004A1A RID: 18970
		public GameObject Level;

		// Token: 0x04004A1B RID: 18971
		public GameObject Name;

		// Token: 0x04004A1C RID: 18972
		public GameObject PingZhi;

		// Token: 0x04004A1D RID: 18973
		public int skillID;

		// Token: 0x04004A1E RID: 18974
		public GameObject KeyName;

		// Token: 0x04004A1F RID: 18975
		public UILabel NameLabel;

		// Token: 0x04004A20 RID: 18976
		public bool showName;

		// Token: 0x04004A21 RID: 18977
		private float refreshCD;
	}
}
