using System;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

namespace GUIPackage
{
	// Token: 0x02000D8A RID: 3466
	public class KeyCell : MonoBehaviour
	{
		// Token: 0x06005398 RID: 21400 RVA: 0x0003BC49 File Offset: 0x00039E49
		public void longPress()
		{
			if (this.IsPress)
			{
				if (this.keySkill.skill_ID != -1 && RealTime.time - this.StartPressTime > 0.8f)
				{
					this.OnHover(true);
					return;
				}
			}
			else
			{
				this.OnHover(false);
			}
		}

		// Token: 0x06005399 RID: 21401 RVA: 0x0022D6E8 File Offset: 0x0022B8E8
		private void Start()
		{
			this.itemDatebase = jsonData.instance.gameObject.GetComponent<ItemDatebase>();
			this.avatar = Tools.instance.getPlayer();
			if (this.isFight)
			{
				this.Icon.transform.GetChild(2).gameObject.SetActive(false);
			}
			this.KeyItemID = -1;
			this.cdFill = this.Icon.transform.GetChild(0).GetComponent<UISprite>();
		}

		// Token: 0x0600539A RID: 21402 RVA: 0x0022D764 File Offset: 0x0022B964
		private void Update()
		{
			try
			{
				if (!this.isException)
				{
					this.Show_Date();
					if (RoundManager.KeyHideCD < 0f)
					{
						this.UesKey();
					}
					this.Icon_CoolDown();
				}
			}
			catch (Exception ex)
			{
				this.isException = true;
				Debug.LogError("KeyCell.Update出现异常:" + ex.Message + "\n" + ex.StackTrace);
			}
		}

		// Token: 0x0600539B RID: 21403 RVA: 0x0022D7D4 File Offset: 0x0022B9D4
		protected void Icon_CoolDown()
		{
			if (this.keySkill.skill_ID != -1)
			{
				if (this.keySkill.CurCD != 0f)
				{
					if (this.wepen != null && this.wepen.uuid != "" && this.wepen.uuid != null && RoundManager.instance.WeaponSkillList.ContainsKey(this.wepen.uuid))
					{
						this.cdFill.fillAmount = this.keySkill.CurCD / this.keySkill.CoolDown;
						if (RoundManager.instance.WeaponSkillList[this.wepen.uuid] > 0)
						{
							this.Icon.transform.GetChild(0).GetChild(0).GetComponent<UILabel>().text = string.Concat(RoundManager.instance.WeaponSkillList[this.wepen.uuid]);
						}
					}
					else
					{
						this.cdFill.fillAmount = this.keySkill.CurCD / this.keySkill.CoolDown;
						if (this.avatar.SkillSeidFlag.ContainsKey(29) && this.avatar.SkillSeidFlag[29].ContainsKey(this.keySkill.skill_ID) && this.avatar.SkillSeidFlag[29][this.keySkill.skill_ID] > 0)
						{
							this.Icon.transform.GetChild(0).GetChild(0).GetComponent<UILabel>().text = string.Concat(this.avatar.SkillSeidFlag[29][this.keySkill.skill_ID]);
						}
					}
				}
				else
				{
					Transform transform = this.Icon.transform.Find("Sprite/Label");
					if (transform != null && transform.GetComponent<UILabel>() != null)
					{
						transform.GetComponent<UILabel>().text = "";
					}
					this.cdFill.fillAmount = 0f;
				}
				SkillCanUseType skillCanUseType = this.keySkill.CanUse(PlayerEx.Player, PlayerEx.Player, false, "");
				if (skillCanUseType != SkillCanUseType.可以使用 && skillCanUseType != SkillCanUseType.尚未冷却不能使用)
				{
					this.cdFill.fillAmount = 1f;
				}
			}
			else
			{
				this.cdFill.fillAmount = 0f;
			}
			if (this.isFight)
			{
				if (RoundManager.instance.ChoiceSkill == this.keySkill)
				{
					this.Icon.transform.GetChild(1).gameObject.SetActive(true);
				}
				else
				{
					this.Icon.transform.GetChild(1).gameObject.SetActive(false);
				}
				this.showLianJiHightLight();
			}
		}

		// Token: 0x0600539C RID: 21404 RVA: 0x0022DAAC File Offset: 0x0022BCAC
		public void showLianJiHightLight()
		{
			if (this.keySkill.skill_ID == -1)
			{
				return;
			}
			bool active = false;
			if (jsonData.instance.skillJsonData[string.Concat(this.keySkill.skill_ID)]["seid"].list.Find((JSONObject aa) => jsonData.instance.hightLightSkillID.Contains((int)aa.n)) != null)
			{
				Avatar player = Tools.instance.getPlayer();
				if (player == null || player.OtherAvatar == null)
				{
					return;
				}
				if (this.keySkill.CanUse(player, player, false, "") == SkillCanUseType.可以使用)
				{
					Avatar receiver = player;
					if (jsonData.instance.skillJsonData[string.Concat(this.keySkill.skill_ID)]["script"].str == "SkillAttack")
					{
						receiver = player.OtherAvatar;
					}
					if (this.keySkill.CanRealizeSeid(player, receiver))
					{
						active = true;
					}
				}
			}
			this.Icon.transform.GetChild(2).gameObject.SetActive(active);
		}

		// Token: 0x0600539D RID: 21405 RVA: 0x0022DBCC File Offset: 0x0022BDCC
		protected void UesKey()
		{
			if (!this.hasKeyCell)
			{
				return;
			}
			if (Input.GetKeyDown(base.name.ToLower()) && Tools.instance.getPlayer().state == 3)
			{
				if (this.keyItem.itemID != -1)
				{
					Singleton.inventory.UseItem(this.KeyItemID);
					return;
				}
				if (this.keySkill.skill_ID != -1 && !(base.name == "Tab"))
				{
					RoundManager.instance.SetChoiceSkill(ref this.keySkill);
				}
			}
		}

		// Token: 0x0600539E RID: 21406 RVA: 0x0022DC58 File Offset: 0x0022BE58
		protected void Show_Date()
		{
			if (this.keyItem.itemIcon != null)
			{
				this.Icon.GetComponent<UITexture>().mainTexture = this.keyItem.itemIcon;
			}
			else if (this.keySkill.skill_Icon != null)
			{
				if (this.KeyCallType == 1 && this.IconImage.sprite.texture != this.itemDatebase.items[this.wepen.itemId].itemIcon)
				{
					this.IconImage.sprite = Sprite.Create(this.itemDatebase.items[this.wepen.itemId].itemIcon, new Rect(0f, 0f, 128f, 128f), new Vector2(128f, 128f));
				}
				else
				{
					this.Icon.GetComponent<UITexture>().mainTexture = this.keySkill.skill_Icon;
				}
				if (this.KeyCallType == 1 && this.keySkill.ItemAddSeid != null)
				{
					this.IconImage.sprite = Sprite.Create(this.keySkill.skill_Icon, new Rect(0f, 0f, 128f, 128f), new Vector2(128f, 128f));
				}
			}
			else
			{
				this.Icon.GetComponent<UITexture>().mainTexture = null;
			}
			if (this.keyItem.itemNum != 0)
			{
				this.Num.GetComponent<UILabel>().text = this.keyItem.itemNum.ToString();
				return;
			}
			this.Num.GetComponent<UILabel>().text = null;
		}

		// Token: 0x0600539F RID: 21407 RVA: 0x0022DE14 File Offset: 0x0022C014
		protected void OnHover(bool isOver)
		{
			if (this.keySkill.skill_ID != -1)
			{
				if (isOver)
				{
					RoundManager.instance.ToolitpSkill.clearItem();
					if (this.KeyCallType == 1)
					{
						RoundManager.instance.ItemToolitpSkill.setItemText(this.keySkill.skill_ID, this.wepen);
						RoundManager.instance.ItemToolitpSkill.showTooltip = true;
						return;
					}
					RoundManager.instance.ToolitpSkill.setText(this.keySkill.skill_ID, this.keySkill);
					RoundManager.instance.ToolitpSkill.showTooltip = true;
					return;
				}
				else
				{
					RoundManager.instance.ItemToolitpSkill.showTooltip = false;
					RoundManager.instance.ToolitpSkill.showTooltip = false;
				}
			}
		}

		// Token: 0x060053A0 RID: 21408 RVA: 0x0022DED0 File Offset: 0x0022C0D0
		protected void OnPress()
		{
			if (Input.GetMouseButtonDown(0) && Tools.instance.getPlayer().state == 3 && this.keySkill.skill_ID != -1)
			{
				this.StartPressTime = RealTime.time;
				this.IsPress = true;
				RoundManager.instance.SetChoiceSkill(ref this.keySkill);
				this.OnHover(true);
			}
		}

		// Token: 0x04005349 RID: 21321
		public GameObject Icon;

		// Token: 0x0400534A RID: 21322
		public GameObject Num;

		// Token: 0x0400534B RID: 21323
		public item keyItem;

		// Token: 0x0400534C RID: 21324
		public Skill keySkill;

		// Token: 0x0400534D RID: 21325
		public int KeyItemID;

		// Token: 0x0400534E RID: 21326
		public int KeyCallType;

		// Token: 0x0400534F RID: 21327
		public Text SkillName;

		// Token: 0x04005350 RID: 21328
		public Image IconImage;

		// Token: 0x04005351 RID: 21329
		private ItemDatebase itemDatebase;

		// Token: 0x04005352 RID: 21330
		private Avatar avatar;

		// Token: 0x04005353 RID: 21331
		public ITEM_INFO wepen;

		// Token: 0x04005354 RID: 21332
		public bool isFight;

		// Token: 0x04005355 RID: 21333
		public bool hasKeyCell = true;

		// Token: 0x04005356 RID: 21334
		public float StartPressTime;

		// Token: 0x04005357 RID: 21335
		public bool IsPress;

		// Token: 0x04005358 RID: 21336
		private UISprite cdFill;

		// Token: 0x04005359 RID: 21337
		private bool isException;
	}
}
