using System;
using KBEngine;
using UnityEngine;

namespace GUIPackage
{
	// Token: 0x02000A63 RID: 2659
	public class KeyCellMapSkill : MonoBehaviour
	{
		// Token: 0x06004AAF RID: 19119 RVA: 0x001FC12C File Offset: 0x001FA32C
		private void Start()
		{
			this.Icon = base.transform.Find("Icon").gameObject;
			this.Num = base.transform.Find("num").gameObject;
			this.KeyItemID = -1;
			base.Invoke("loadKey", 1f);
		}

		// Token: 0x06004AB0 RID: 19120 RVA: 0x00004095 File Offset: 0x00002295
		public void loadKey()
		{
		}

		// Token: 0x06004AB1 RID: 19121 RVA: 0x001FC188 File Offset: 0x001FA388
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

		// Token: 0x06004AB2 RID: 19122 RVA: 0x001FC214 File Offset: 0x001FA414
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

		// Token: 0x06004AB3 RID: 19123 RVA: 0x00004095 File Offset: 0x00002295
		private void UesKey()
		{
		}

		// Token: 0x06004AB4 RID: 19124 RVA: 0x001FC294 File Offset: 0x001FA494
		private void OnHover(bool isOver)
		{
			this.PCOnHover(isOver);
		}

		// Token: 0x06004AB5 RID: 19125 RVA: 0x001FC29D File Offset: 0x001FA49D
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

		// Token: 0x06004AB6 RID: 19126 RVA: 0x001FC2D7 File Offset: 0x001FA4D7
		private void OnDrop(GameObject obj)
		{
			if (Input.GetMouseButtonUp(0) && this.CanClick && Singleton.skillUI.draggingSkill)
			{
				this.chengeSkill();
			}
		}

		// Token: 0x06004AB7 RID: 19127 RVA: 0x001FC2FC File Offset: 0x001FA4FC
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

		// Token: 0x06004AB8 RID: 19128 RVA: 0x001FC3E4 File Offset: 0x001FA5E4
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

		// Token: 0x06004AB9 RID: 19129 RVA: 0x001FC69D File Offset: 0x001FA89D
		private void OnPress()
		{
			if (!this.CanClick)
			{
				return;
			}
			this.PCOnPress();
		}

		// Token: 0x06004ABA RID: 19130 RVA: 0x001FC6AE File Offset: 0x001FA8AE
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

		// Token: 0x06004ABB RID: 19131 RVA: 0x001FC6E8 File Offset: 0x001FA8E8
		public void PCOnPress()
		{
			if (Input.GetMouseButton(0))
			{
				this.chengeSkill();
			}
		}

		// Token: 0x040049D4 RID: 18900
		public GameObject Icon;

		// Token: 0x040049D5 RID: 18901
		public GameObject Num;

		// Token: 0x040049D6 RID: 18902
		public item keyItem;

		// Token: 0x040049D7 RID: 18903
		public Skill keySkill;

		// Token: 0x040049D8 RID: 18904
		public GameObject PingZhi;

		// Token: 0x040049D9 RID: 18905
		public int KeyItemID;

		// Token: 0x040049DA RID: 18906
		public bool CanClick = true;

		// Token: 0x040049DB RID: 18907
		public GameObject KeyName;

		// Token: 0x040049DC RID: 18908
		public UILabel NameLabel;

		// Token: 0x040049DD RID: 18909
		public bool showName;
	}
}
