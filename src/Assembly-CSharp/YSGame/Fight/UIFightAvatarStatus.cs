using System;
using System.Collections.Generic;
using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

namespace YSGame.Fight
{
	// Token: 0x02000ABB RID: 2747
	public class UIFightAvatarStatus : MonoBehaviour
	{
		// Token: 0x06004D00 RID: 19712 RVA: 0x0020F237 File Offset: 0x0020D437
		private void Awake()
		{
			this.BuffPrefab = Resources.Load<GameObject>("FightPrefab/UIFightBuffItem");
			Event.registerOut("UpdataBuff", this, "UpdateBuffEvent");
		}

		// Token: 0x06004D01 RID: 19713 RVA: 0x0020F25A File Offset: 0x0020D45A
		private void OnDestroy()
		{
			this.OnClear();
		}

		// Token: 0x06004D02 RID: 19714 RVA: 0x0020F264 File Offset: 0x0020D464
		private void Update()
		{
			if (this.NoRefresh)
			{
				return;
			}
			this.UpdateStatus(null);
			if (this.cd < 0f)
			{
				this.cd = 0.1f;
				if (this.needRefreshBuff)
				{
					this.needRefreshBuff = false;
					this.UpdateBuff2();
					return;
				}
			}
			else
			{
				this.cd -= Time.deltaTime;
			}
		}

		// Token: 0x06004D03 RID: 19715 RVA: 0x0020F2C4 File Offset: 0x0020D4C4
		public void Init(Avatar avatar)
		{
			this.Avatar = avatar;
			this.NameText.text = this.Avatar.name;
			int levelType = avatar.getLevelType();
			this.LevelImage.sprite = this.JingJieSprites[levelType - 1];
			this.AvatarDaDaoBuff.Avatar = avatar;
			this.AvatarDaDaoBuff.BuffID = 10008;
			this.lingYuSkillID = this.Avatar.HuaShenLingYuSkill.I;
			this.HuaShenTrigger.SkillID = this.lingYuSkillID;
		}

		// Token: 0x06004D04 RID: 19716 RVA: 0x0020F354 File Offset: 0x0020D554
		private void UpdateStatus(MessageData message)
		{
			if (this.Avatar != null)
			{
				this.HPText.text = string.Format("{0}/{1}", this.Avatar.HP, this.Avatar.HP_Max);
				this.HPBar.fillAmount = (float)this.Avatar.HP / (float)this.Avatar.HP_Max;
				string text = string.Format("{0}/{1}", this.Avatar.cardMag.getCardNum(), this.Avatar.NowCard);
				if (this.LingQiText != null)
				{
					this.LingQiText.text = text;
				}
				if (this.LingQiText2 != null)
				{
					this.LingQiText2.text = text;
				}
				if (this.lingYuSkillID > 0 && this.Avatar.spell.HasBuff(10017))
				{
					this.LevelImage.sprite = this.JingJieSprites[this.JingJieSprites.Count - 1];
				}
			}
		}

		// Token: 0x06004D05 RID: 19717 RVA: 0x000826BE File Offset: 0x000808BE
		public void OnClear()
		{
			Event.deregisterOut(this);
		}

		// Token: 0x06004D06 RID: 19718 RVA: 0x0020F46C File Offset: 0x0020D66C
		public void UpdateBuffEvent()
		{
			this.needRefreshBuff = true;
		}

		// Token: 0x06004D07 RID: 19719 RVA: 0x0020F478 File Offset: 0x0020D678
		public void UpdateBuff2()
		{
			if (this.Avatar == null)
			{
				return;
			}
			if (this.Avatar.isPlayer() && RoundManager.instance.PlayerFightEventProcessor != null)
			{
				RoundManager.instance.PlayerFightEventProcessor.OnUpdateBuff();
			}
			for (int i = this.BuffList.childCount - 1; i >= 0; i--)
			{
				Object.Destroy(this.BuffList.GetChild(i).gameObject);
			}
			foreach (List<int> list in this.Avatar.bufflist)
			{
				if (this.BuffIsHideBuff106(list))
				{
					this.CreateBuffIcon(list);
				}
			}
			int num = 0;
			foreach (List<int> i2 in this.Avatar.bufflist)
			{
				if (this.BuffIsHideBuff106(i2))
				{
					num++;
				}
			}
		}

		// Token: 0x06004D08 RID: 19720 RVA: 0x0020F58C File Offset: 0x0020D78C
		public void UpdateBuff()
		{
			if (this.Avatar == null)
			{
				return;
			}
			int num = 0;
			foreach (List<int> i in this.Avatar.bufflist)
			{
				if (this.BuffIsHideBuff106(i))
				{
					num++;
				}
			}
			if (this.BuffCount != num)
			{
				this.BuffCount = num;
				for (int j = this.BuffList.childCount - 1; j >= 0; j--)
				{
					Transform child = this.BuffList.GetChild(j);
					if (child.gameObject.activeSelf)
					{
						if (this.BuffIsHideBuff106(child.GetComponent<UIFightBuffItem>().AvatarBuff))
						{
							if (child.GetComponent<UIFightBuffItem>().AvatarBuff[1] <= 0)
							{
								Object.DestroyImmediate(child.gameObject);
							}
						}
						else
						{
							Object.DestroyImmediate(child.gameObject);
						}
					}
				}
				foreach (List<int> list in this.Avatar.bufflist)
				{
					if (this.BuffIsHideBuff106(list))
					{
						bool flag = false;
						for (int k = 0; k < this.BuffList.childCount; k++)
						{
							UIFightBuffItem component = this.BuffList.GetChild(k).GetComponent<UIFightBuffItem>();
							if (this.CheckBuffEquals(component.AvatarBuff, list))
							{
								flag = true;
								break;
							}
						}
						if (!flag)
						{
							this.CreateBuffIcon(list);
						}
					}
				}
				for (int l = 2; l < this.BuffList.childCount - 1; l++)
				{
					for (int m = 2; m < this.BuffList.childCount - 1 - l; m++)
					{
						int i2 = jsonData.instance.BuffJsonData[this.BuffList.GetChild(l).GetComponent<UIFightBuffItem>().AvatarBuff[2].ToString()]["bufftype"].I;
						int i3 = jsonData.instance.BuffJsonData[this.BuffList.GetChild(l + 1).GetComponent<UIFightBuffItem>().AvatarBuff[2].ToString()]["bufftype"].I;
						if (i2 > i3)
						{
							this.BuffList.GetChild(m).SetSiblingIndex(m + 1);
						}
					}
				}
				return;
			}
			List<List<int>> list2 = new List<List<int>>();
			foreach (object obj in this.BuffList)
			{
				Transform transform = (Transform)obj;
				foreach (List<int> list3 in this.Avatar.bufflist)
				{
					UIFightBuffItem component2 = transform.GetComponent<UIFightBuffItem>();
					if (component2.BuffID == list3[2] && !list2.Contains(list3))
					{
						list2.Add(list3);
						if (_BuffJsonData.DataDict[list3[2]].ShowOnlyOne == 1)
						{
							component2.BuffCountText.text = "1";
						}
						else
						{
							component2.BuffCountText.text = list3[1].ToString();
						}
						transform.GetComponent<UIFightBuffItem>().BuffRound = list3[1];
						transform.GetComponent<UIFightBuffItem>().AvatarBuff = list3;
						break;
					}
				}
			}
		}

		// Token: 0x06004D09 RID: 19721 RVA: 0x0020F948 File Offset: 0x0020DB48
		private bool CheckBuffEquals(List<int> a, List<int> b)
		{
			if (a.Count != b.Count)
			{
				return false;
			}
			for (int i = 0; i < a.Count; i++)
			{
				if (a[i] != b[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06004D0A RID: 19722 RVA: 0x0020F98C File Offset: 0x0020DB8C
		public void CreateBuffIcon(List<int> buff)
		{
			GameObject gameObject = Object.Instantiate<GameObject>(this.BuffPrefab);
			UIFightBuffItem component = gameObject.GetComponent<UIFightBuffItem>();
			component.Avatar = this.Avatar;
			_BuffJsonData buffJsonData = _BuffJsonData.DataDict[buff[2]];
			if (buffJsonData.ShowOnlyOne == 1)
			{
				component.BuffCountText.text = "1";
			}
			else
			{
				component.BuffCountText.text = buff[1].ToString();
			}
			Sprite sprite;
			if (buffJsonData.BuffIcon == 0)
			{
				sprite = ResManager.inst.LoadSprite("Buff Icon/" + buff[2]);
			}
			else
			{
				sprite = ResManager.inst.LoadSprite("Buff Icon/" + buffJsonData.BuffIcon);
			}
			if (this.Avatar.isPlayer() && this.Avatar.fightTemp.LianQiEquipDictionary.Keys.Count > 0)
			{
				int key = buff[2];
				if (this.Avatar.fightTemp.LianQiEquipDictionary.ContainsKey(key))
				{
					JSONObject jsonobject = this.Avatar.fightTemp.LianQiEquipDictionary[key];
					if (jsonobject != null && jsonobject.HasField("ItemIcon"))
					{
						sprite = ResManager.inst.LoadSprite(jsonobject["ItemIcon"].str);
					}
				}
			}
			if (!this.Avatar.isPlayer() && RoundManager.instance.newNpcFightManager != null && RoundManager.instance.newNpcFightManager.LianQiEquipDictionary.Keys.Count > 0)
			{
				int key2 = buff[2];
				if (RoundManager.instance.newNpcFightManager.LianQiEquipDictionary.ContainsKey(key2))
				{
					JSONObject jsonobject2 = RoundManager.instance.newNpcFightManager.LianQiEquipDictionary[key2];
					if (jsonobject2 != null && jsonobject2.HasField("ItemIcon"))
					{
						sprite = ResManager.inst.LoadSprite(jsonobject2["ItemIcon"].str);
					}
				}
			}
			if (sprite == null)
			{
				sprite = ResManager.inst.LoadSprite("Buff Icon/0");
			}
			component.BuffIconImage.sprite = sprite;
			component.BuffID = buff[2];
			component.BuffRound = buff[1];
			component.AvatarBuff = buff;
			gameObject.transform.SetParent(this.BuffList);
			gameObject.SetActive(true);
			gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
		}

		// Token: 0x06004D0B RID: 19723 RVA: 0x0020FC00 File Offset: 0x0020DE00
		private bool BuffIsHideBuff106(List<int> i)
		{
			JSONObject jsonobject = jsonData.instance.BuffJsonData[i[2].ToString()];
			if (jsonobject["isHide"].I == 1)
			{
				return jsonobject["seid"].list.Find((JSONObject aa) => aa.I == 106) != null && jsonData.instance.Buff[i[2]].CanRealized(this.Avatar, null, i);
			}
			return true;
		}

		// Token: 0x04004C0A RID: 19466
		public bool NoRefresh;

		// Token: 0x04004C0B RID: 19467
		public Text NameText;

		// Token: 0x04004C0C RID: 19468
		public Image LevelImage;

		// Token: 0x04004C0D RID: 19469
		public Text LingQiText;

		// Token: 0x04004C0E RID: 19470
		public Text LingQiText2;

		// Token: 0x04004C0F RID: 19471
		public Text HPText;

		// Token: 0x04004C10 RID: 19472
		public Image HPBar;

		// Token: 0x04004C11 RID: 19473
		public RectTransform BuffList;

		// Token: 0x04004C12 RID: 19474
		public UTooltipSkillTrigger HuaShenTrigger;

		// Token: 0x04004C13 RID: 19475
		public UIFightBuffItem AvatarDaDaoBuff;

		// Token: 0x04004C14 RID: 19476
		[HideInInspector]
		public Avatar Avatar;

		// Token: 0x04004C15 RID: 19477
		private GameObject BuffPrefab;

		// Token: 0x04004C16 RID: 19478
		public List<Sprite> JingJieSprites;

		// Token: 0x04004C17 RID: 19479
		[HideInInspector]
		public int BuffCount;

		// Token: 0x04004C18 RID: 19480
		private int lingYuSkillID;

		// Token: 0x04004C19 RID: 19481
		private bool needRefreshBuff;

		// Token: 0x04004C1A RID: 19482
		private float cd;
	}
}
