using System;
using KBEngine;
using UnityEngine;

namespace GUIPackage
{
	// Token: 0x02000D8D RID: 3469
	public class KeyCellMapSkill : MonoBehaviour
	{
		// Token: 0x060053B4 RID: 21428 RVA: 0x0022E3F4 File Offset: 0x0022C5F4
		private void Start()
		{
			this.Icon = base.transform.Find("Icon").gameObject;
			this.Num = base.transform.Find("num").gameObject;
			this.KeyItemID = -1;
			base.Invoke("loadKey", 1f);
		}

		// Token: 0x060053B5 RID: 21429 RVA: 0x000042DD File Offset: 0x000024DD
		public void loadKey()
		{
		}

		// Token: 0x060053B6 RID: 21430 RVA: 0x0022E450 File Offset: 0x0022C650
		private void Update()
		{
			this.Show_Date();
			this.Icon_CoolDown();
			if (this.KeyName == null)
			{
				return;
			}
			if (Tools.instance.getPlayer().showSkillName == 0 && this.showName && this.keySkill.skill_ID != -1)
			{
				this.KeyName.SetActive(true);
				this.NameLabel.text = Tools.instance.getSkillName(this.keySkill.skill_ID, true);
				return;
			}
			this.KeyName.SetActive(false);
		}

		// Token: 0x060053B7 RID: 21431 RVA: 0x0022E4DC File Offset: 0x0022C6DC
		private void Icon_CoolDown()
		{
			if (this.keySkill.skill_ID == -1)
			{
				this.Icon.GetComponentInChildren<UISprite>().fillAmount = 0f;
				return;
			}
			if (this.keySkill.CurCD != 0f)
			{
				this.Icon.GetComponentInChildren<UISprite>().fillAmount = this.keySkill.CurCD / this.keySkill.CoolDown;
				return;
			}
			this.Icon.GetComponentInChildren<UISprite>().fillAmount = 0f;
		}

		// Token: 0x060053B8 RID: 21432 RVA: 0x000042DD File Offset: 0x000024DD
		private void UesKey()
		{
		}

		// Token: 0x060053B9 RID: 21433 RVA: 0x0003BD56 File Offset: 0x00039F56
		private void OnHover(bool isOver)
		{
			this.PCOnHover(isOver);
		}

		// Token: 0x060053BA RID: 21434 RVA: 0x0003BD5F File Offset: 0x00039F5F
		public void PCOnHover(bool isOver)
		{
			if (this.keySkill.skill_ID != -1)
			{
				if (isOver)
				{
					Singleton.skillUI.Show_Tooltip(this.keySkill);
					Singleton.skillUI.showTooltip = true;
					return;
				}
				Singleton.skillUI.showTooltip = false;
			}
		}

		// Token: 0x060053BB RID: 21435 RVA: 0x0003BD99 File Offset: 0x00039F99
		private void OnDrop(GameObject obj)
		{
			if (Input.GetMouseButtonUp(0) && this.CanClick && Singleton.skillUI.draggingSkill)
			{
				this.chengeSkill();
			}
		}

		// Token: 0x060053BC RID: 21436 RVA: 0x0022E55C File Offset: 0x0022C75C
		private void Show_Date()
		{
			if (this.keyItem.itemIcon != null)
			{
				this.Icon.GetComponent<UITexture>().mainTexture = this.keyItem.itemIcon;
			}
			else if (this.keySkill.skill_Icon != null)
			{
				this.Icon.GetComponent<UITexture>().mainTexture = this.keySkill.skill_Icon;
			}
			else
			{
				this.Icon.GetComponent<UITexture>().mainTexture = null;
			}
			if (this.PingZhi != null)
			{
				this.PingZhi.GetComponent<UITexture>().mainTexture = this.keySkill.SkillPingZhi;
			}
			if (this.keyItem.itemNum != 0)
			{
				this.Num.GetComponent<UILabel>().text = this.keyItem.itemNum.ToString();
				return;
			}
			this.Num.GetComponent<UILabel>().text = null;
		}

		// Token: 0x060053BD RID: 21437 RVA: 0x0022E644 File Offset: 0x0022C844
		private void chengeSkill()
		{
			if (Singleton.skillUI.draggingSkill)
			{
				Avatar avatar = (Avatar)KBEngineApp.app.player();
				if (jsonData.instance.skillJsonData[Singleton.skillUI.dragedSkill.skill_ID.ToString()]["AttackType"].list.Find((JSONObject aaa) => (int)aaa.n == 13) != null)
				{
					UIPopTip.Inst.Pop("秘术仅在结丹时自动生效", PopTipIconType.叹号);
					Singleton.skillUI.Clear_Draged();
					return;
				}
				if (jsonData.instance.skillJsonData[Singleton.skillUI.dragedSkill.skill_ID.ToString()]["AttackType"].list.Find((JSONObject aaa) => (int)aaa.n == 14 || (int)aaa.n == 15 || (int)aaa.n == 16) != null)
				{
					UIPopTip.Inst.Pop("秘术仅在结婴时自动生效", PopTipIconType.叹号);
					Singleton.skillUI.Clear_Draged();
					return;
				}
				if (jsonData.instance.skillJsonData[Singleton.skillUI.dragedSkill.skill_ID.ToString()]["AttackType"].list.Find((JSONObject aaa) => (int)aaa.n == 18) != null)
				{
					UIPopTip.Inst.Pop("秘术仅在化神时自动生效", PopTipIconType.叹号);
					Singleton.skillUI.Clear_Draged();
					return;
				}
				Singleton.key.Clear_MapSkillKye(Singleton.skillUI.dragedSkill);
				if (this.keySkill.skill_ID != -1)
				{
					avatar.UnEquipSkill(Tools.instance.getSkillIDByKey(this.keySkill.skill_ID));
				}
				this.keySkill = Singleton.skillUI.dragedSkill;
				Singleton.skillUI.Clear_Draged();
				avatar.equipSkill(Tools.instance.getSkillIDByKey(this.keySkill.skill_ID), int.Parse(base.transform.name));
				this.KeyItemID = -1;
				this.keyItem = new item();
				return;
			}
			else
			{
				if (Singleton.key.draggingKey)
				{
					this.keyItem = Singleton.inventory.dragedItem;
					this.KeyItemID = Singleton.inventory.dragedID;
					Singleton.inventory.Clear_dragedItem();
					this.keySkill = new Skill();
					return;
				}
				if (this.keySkill.skill_ID != -1)
				{
					((Avatar)KBEngineApp.app.player()).UnEquipSkill(Tools.instance.getSkillIDByKey(this.keySkill.skill_ID));
					Singleton.skillUI.dragedSkill = this.keySkill;
					Singleton.skillUI.draggingSkill = true;
					this.keySkill = new Skill();
				}
				return;
			}
		}

		// Token: 0x060053BE RID: 21438 RVA: 0x0003BDBD File Offset: 0x00039FBD
		private void OnPress()
		{
			if (!this.CanClick)
			{
				return;
			}
			this.PCOnPress();
		}

		// Token: 0x060053BF RID: 21439 RVA: 0x0003BDCE File Offset: 0x00039FCE
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

		// Token: 0x060053C0 RID: 21440 RVA: 0x0003BE08 File Offset: 0x0003A008
		public void PCOnPress()
		{
			if (Input.GetMouseButton(0))
			{
				this.chengeSkill();
			}
		}

		// Token: 0x04005367 RID: 21351
		public GameObject Icon;

		// Token: 0x04005368 RID: 21352
		public GameObject Num;

		// Token: 0x04005369 RID: 21353
		public item keyItem;

		// Token: 0x0400536A RID: 21354
		public Skill keySkill;

		// Token: 0x0400536B RID: 21355
		public GameObject PingZhi;

		// Token: 0x0400536C RID: 21356
		public int KeyItemID;

		// Token: 0x0400536D RID: 21357
		public bool CanClick = true;

		// Token: 0x0400536E RID: 21358
		public GameObject KeyName;

		// Token: 0x0400536F RID: 21359
		public UILabel NameLabel;

		// Token: 0x04005370 RID: 21360
		public bool showName;
	}
}
