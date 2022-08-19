using System;
using KBEngine;
using UnityEngine;

namespace GUIPackage
{
	// Token: 0x02000A62 RID: 2658
	public class KeyCellMapPassSkill : MonoBehaviour
	{
		// Token: 0x06004AA0 RID: 19104 RVA: 0x001FBBC8 File Offset: 0x001F9DC8
		private void Start()
		{
			this.Icon = base.transform.Find("Icon").gameObject;
			this.Num = base.transform.Find("num").gameObject;
			this.KeyItemID = -1;
			base.Invoke("loadKey", 0.5f);
		}

		// Token: 0x06004AA1 RID: 19105 RVA: 0x00004095 File Offset: 0x00002295
		public void loadKey()
		{
		}

		// Token: 0x06004AA2 RID: 19106 RVA: 0x001FBC24 File Offset: 0x001F9E24
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
				this.NameLabel.text = Tools.instance.getStaticSkillName(this.keySkill.skill_ID, false);
				return;
			}
			this.KeyName.SetActive(false);
		}

		// Token: 0x06004AA3 RID: 19107 RVA: 0x001FBCB0 File Offset: 0x001F9EB0
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

		// Token: 0x06004AA4 RID: 19108 RVA: 0x00004095 File Offset: 0x00002295
		private void UesKey()
		{
		}

		// Token: 0x06004AA5 RID: 19109 RVA: 0x001FBD30 File Offset: 0x001F9F30
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

		// Token: 0x06004AA6 RID: 19110 RVA: 0x001FBE16 File Offset: 0x001FA016
		private void OnHover(bool isOver)
		{
			this.PCOnHover(isOver);
		}

		// Token: 0x06004AA7 RID: 19111 RVA: 0x001FBE20 File Offset: 0x001FA020
		public void PCOnHover(bool isOver)
		{
			if (this.keySkill.skill_ID != -1)
			{
				if (isOver)
				{
					Singleton.skillUI2.Show_Tooltip(this.keySkill, 0);
					Singleton.skillUI2.showTooltip = true;
				}
				else
				{
					Singleton.skillUI2.showTooltip = false;
				}
			}
			if (isOver && Input.GetMouseButtonUp(0) && this.CanClick && Singleton.skillUI2.draggingSkill)
			{
				this.chengeSkill();
			}
		}

		// Token: 0x06004AA8 RID: 19112 RVA: 0x001FBE8C File Offset: 0x001FA08C
		private void OnDrop(GameObject obj)
		{
			if (Input.GetMouseButtonUp(0) && this.CanClick && Singleton.skillUI2.draggingSkill)
			{
				this.chengeSkill();
			}
		}

		// Token: 0x06004AA9 RID: 19113 RVA: 0x001FBEB0 File Offset: 0x001FA0B0
		private void chengeSkill()
		{
			if (Singleton.skillUI2.draggingSkill)
			{
				if (this.IsDunsu)
				{
					if (jsonData.instance.StaticSkillJsonData[Singleton.skillUI2.dragedSkill.skill_ID.ToString()]["AttackType"].n != 6f)
					{
						UIPopTip.Inst.Pop(Tools.getStr("bushidunshu"), PopTipIconType.叹号);
						return;
					}
				}
				else if (jsonData.instance.StaticSkillJsonData[Singleton.skillUI2.dragedSkill.skill_ID.ToString()]["AttackType"].n == 6f)
				{
					UIPopTip.Inst.Pop(Tools.getStr("dunsuleijineng"), PopTipIconType.叹号);
					return;
				}
				Avatar avatar = (Avatar)KBEngineApp.app.player();
				if (this.keySkill.skill_ID != -1)
				{
					avatar.UnEquipStaticSkill(Tools.instance.getStaticSkillKeyByID(this.keySkill.skill_ID));
				}
				Singleton.key2.Clear_MapSkillPassKye(Singleton.skillUI2.dragedSkill);
				this.keySkill = Singleton.skillUI2.dragedSkill;
				Singleton.skillUI2.Clear_Draged();
				avatar.equipStaticSkill(Tools.instance.getStaticSkillIDByKey(this.keySkill.skill_ID), int.Parse(base.transform.name));
				this.KeyItemID = -1;
				this.keyItem = new item();
				return;
			}
			if (Singleton.key2.draggingKey)
			{
				this.keyItem = Singleton.inventory.dragedItem;
				this.KeyItemID = Singleton.inventory.dragedID;
				Singleton.inventory.Clear_dragedItem();
				this.keySkill = new Skill();
				return;
			}
			if (this.keySkill.skill_ID != -1)
			{
				((Avatar)KBEngineApp.app.player()).UnEquipStaticSkill(Tools.instance.getStaticSkillIDByKey(this.keySkill.skill_ID));
				Singleton.skillUI2.dragedSkill = this.keySkill;
				Singleton.skillUI2.draggingSkill = true;
				this.keySkill = new Skill();
			}
		}

		// Token: 0x06004AAA RID: 19114 RVA: 0x001FC0B6 File Offset: 0x001FA2B6
		private void OnPress()
		{
			if (!this.CanClick)
			{
				return;
			}
			this.PCOnPress();
		}

		// Token: 0x06004AAB RID: 19115 RVA: 0x001FC0C7 File Offset: 0x001FA2C7
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

		// Token: 0x06004AAC RID: 19116 RVA: 0x001FC101 File Offset: 0x001FA301
		public void PCOnPress()
		{
			if (Input.GetMouseButton(0))
			{
				this.chengeSkill();
			}
		}

		// Token: 0x040049C9 RID: 18889
		public GameObject Icon;

		// Token: 0x040049CA RID: 18890
		public GameObject Num;

		// Token: 0x040049CB RID: 18891
		public item keyItem;

		// Token: 0x040049CC RID: 18892
		public Skill keySkill;

		// Token: 0x040049CD RID: 18893
		public GameObject PingZhi;

		// Token: 0x040049CE RID: 18894
		public int KeyItemID;

		// Token: 0x040049CF RID: 18895
		public GameObject KeyName;

		// Token: 0x040049D0 RID: 18896
		public UILabel NameLabel;

		// Token: 0x040049D1 RID: 18897
		public bool CanClick = true;

		// Token: 0x040049D2 RID: 18898
		public bool IsDunsu;

		// Token: 0x040049D3 RID: 18899
		public bool showName;
	}
}
