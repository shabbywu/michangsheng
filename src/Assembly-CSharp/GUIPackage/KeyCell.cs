using System;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

namespace GUIPackage
{
	// Token: 0x02000A61 RID: 2657
	public class KeyCell : MonoBehaviour
	{
		// Token: 0x06004A96 RID: 19094 RVA: 0x001FB334 File Offset: 0x001F9534
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

		// Token: 0x06004A97 RID: 19095 RVA: 0x001FB370 File Offset: 0x001F9570
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

		// Token: 0x06004A98 RID: 19096 RVA: 0x001FB3EC File Offset: 0x001F95EC
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

		// Token: 0x06004A99 RID: 19097 RVA: 0x001FB45C File Offset: 0x001F965C
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

		// Token: 0x06004A9A RID: 19098 RVA: 0x001FB734 File Offset: 0x001F9934
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

		// Token: 0x06004A9B RID: 19099 RVA: 0x001FB854 File Offset: 0x001F9A54
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

		// Token: 0x06004A9C RID: 19100 RVA: 0x001FB8E0 File Offset: 0x001F9AE0
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

		// Token: 0x06004A9D RID: 19101 RVA: 0x001FBA9C File Offset: 0x001F9C9C
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

		// Token: 0x06004A9E RID: 19102 RVA: 0x001FBB58 File Offset: 0x001F9D58
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

		// Token: 0x040049B8 RID: 18872
		public GameObject Icon;

		// Token: 0x040049B9 RID: 18873
		public GameObject Num;

		// Token: 0x040049BA RID: 18874
		public item keyItem;

		// Token: 0x040049BB RID: 18875
		public Skill keySkill;

		// Token: 0x040049BC RID: 18876
		public int KeyItemID;

		// Token: 0x040049BD RID: 18877
		public int KeyCallType;

		// Token: 0x040049BE RID: 18878
		public Text SkillName;

		// Token: 0x040049BF RID: 18879
		public Image IconImage;

		// Token: 0x040049C0 RID: 18880
		private ItemDatebase itemDatebase;

		// Token: 0x040049C1 RID: 18881
		private Avatar avatar;

		// Token: 0x040049C2 RID: 18882
		public ITEM_INFO wepen;

		// Token: 0x040049C3 RID: 18883
		public bool isFight;

		// Token: 0x040049C4 RID: 18884
		public bool hasKeyCell = true;

		// Token: 0x040049C5 RID: 18885
		public float StartPressTime;

		// Token: 0x040049C6 RID: 18886
		public bool IsPress;

		// Token: 0x040049C7 RID: 18887
		private UISprite cdFill;

		// Token: 0x040049C8 RID: 18888
		private bool isException;
	}
}
