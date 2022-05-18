using System;
using KBEngine;
using UnityEngine;

namespace GUIPackage
{
	// Token: 0x02000D8C RID: 3468
	public class KeyCellMapPassSkill : MonoBehaviour
	{
		// Token: 0x060053A5 RID: 21413 RVA: 0x0022DF30 File Offset: 0x0022C130
		private void Start()
		{
			this.Icon = base.transform.Find("Icon").gameObject;
			this.Num = base.transform.Find("num").gameObject;
			this.KeyItemID = -1;
			base.Invoke("loadKey", 0.5f);
		}

		// Token: 0x060053A6 RID: 21414 RVA: 0x000042DD File Offset: 0x000024DD
		public void loadKey()
		{
		}

		// Token: 0x060053A7 RID: 21415 RVA: 0x0022DF8C File Offset: 0x0022C18C
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

		// Token: 0x060053A8 RID: 21416 RVA: 0x0022E018 File Offset: 0x0022C218
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

		// Token: 0x060053A9 RID: 21417 RVA: 0x000042DD File Offset: 0x000024DD
		private void UesKey()
		{
		}

		// Token: 0x060053AA RID: 21418 RVA: 0x0022E098 File Offset: 0x0022C298
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

		// Token: 0x060053AB RID: 21419 RVA: 0x0003BCB6 File Offset: 0x00039EB6
		private void OnHover(bool isOver)
		{
			this.PCOnHover(isOver);
		}

		// Token: 0x060053AC RID: 21420 RVA: 0x0022E180 File Offset: 0x0022C380
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

		// Token: 0x060053AD RID: 21421 RVA: 0x0003BCBF File Offset: 0x00039EBF
		private void OnDrop(GameObject obj)
		{
			if (Input.GetMouseButtonUp(0) && this.CanClick && Singleton.skillUI2.draggingSkill)
			{
				this.chengeSkill();
			}
		}

		// Token: 0x060053AE RID: 21422 RVA: 0x0022E1EC File Offset: 0x0022C3EC
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

		// Token: 0x060053AF RID: 21423 RVA: 0x0003BCE3 File Offset: 0x00039EE3
		private void OnPress()
		{
			if (!this.CanClick)
			{
				return;
			}
			this.PCOnPress();
		}

		// Token: 0x060053B0 RID: 21424 RVA: 0x0003BCF4 File Offset: 0x00039EF4
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

		// Token: 0x060053B1 RID: 21425 RVA: 0x0003BD2E File Offset: 0x00039F2E
		public void PCOnPress()
		{
			if (Input.GetMouseButton(0))
			{
				this.chengeSkill();
			}
		}

		// Token: 0x0400535C RID: 21340
		public GameObject Icon;

		// Token: 0x0400535D RID: 21341
		public GameObject Num;

		// Token: 0x0400535E RID: 21342
		public item keyItem;

		// Token: 0x0400535F RID: 21343
		public Skill keySkill;

		// Token: 0x04005360 RID: 21344
		public GameObject PingZhi;

		// Token: 0x04005361 RID: 21345
		public int KeyItemID;

		// Token: 0x04005362 RID: 21346
		public GameObject KeyName;

		// Token: 0x04005363 RID: 21347
		public UILabel NameLabel;

		// Token: 0x04005364 RID: 21348
		public bool CanClick = true;

		// Token: 0x04005365 RID: 21349
		public bool IsDunsu;

		// Token: 0x04005366 RID: 21350
		public bool showName;
	}
}
