using System;
using System.Collections.Generic;
using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

namespace YSGame.Fight
{
	// Token: 0x02000DF7 RID: 3575
	public class UIFightAvatarStatus : MonoBehaviour
	{
		// Token: 0x06005649 RID: 22089 RVA: 0x0003DB20 File Offset: 0x0003BD20
		private void Awake()
		{
			this.BuffPrefab = Resources.Load<GameObject>("FightPrefab/UIFightBuffItem");
			Event.registerOut("UpdataBuff", this, "UpdateBuffEvent");
		}

		// Token: 0x0600564A RID: 22090 RVA: 0x0003DB43 File Offset: 0x0003BD43
		private void OnDestroy()
		{
			this.OnClear();
		}

		// Token: 0x0600564B RID: 22091 RVA: 0x0023FA9C File Offset: 0x0023DC9C
		private void Update()
		{
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

		// Token: 0x0600564C RID: 22092 RVA: 0x0023FAF0 File Offset: 0x0023DCF0
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

		// Token: 0x0600564D RID: 22093 RVA: 0x0023FB80 File Offset: 0x0023DD80
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

		// Token: 0x0600564E RID: 22094 RVA: 0x0001429C File Offset: 0x0001249C
		public void OnClear()
		{
			Event.deregisterOut(this);
		}

		// Token: 0x0600564F RID: 22095 RVA: 0x0003DB4B File Offset: 0x0003BD4B
		public void UpdateBuffEvent()
		{
			this.needRefreshBuff = true;
		}

		// Token: 0x06005650 RID: 22096 RVA: 0x0023FC98 File Offset: 0x0023DE98
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

		// Token: 0x06005651 RID: 22097 RVA: 0x0023FDAC File Offset: 0x0023DFAC
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

		// Token: 0x06005652 RID: 22098 RVA: 0x00240168 File Offset: 0x0023E368
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

		// Token: 0x06005653 RID: 22099 RVA: 0x002401AC File Offset: 0x0023E3AC
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

		// Token: 0x06005654 RID: 22100 RVA: 0x00240420 File Offset: 0x0023E620
		private bool BuffIsHideBuff106(List<int> i)
		{
			JSONObject jsonobject = jsonData.instance.BuffJsonData[i[2].ToString()];
			if (jsonobject["isHide"].I == 1)
			{
				return jsonobject["seid"].list.Find((JSONObject aa) => aa.I == 106) != null && jsonData.instance.Buff[i[2]].CanRealized(this.Avatar, null, i);
			}
			return true;
		}

		// Token: 0x040055E1 RID: 21985
		public Text NameText;

		// Token: 0x040055E2 RID: 21986
		public Image LevelImage;

		// Token: 0x040055E3 RID: 21987
		public Text LingQiText;

		// Token: 0x040055E4 RID: 21988
		public Text LingQiText2;

		// Token: 0x040055E5 RID: 21989
		public Text HPText;

		// Token: 0x040055E6 RID: 21990
		public Image HPBar;

		// Token: 0x040055E7 RID: 21991
		public RectTransform BuffList;

		// Token: 0x040055E8 RID: 21992
		public UTooltipSkillTrigger HuaShenTrigger;

		// Token: 0x040055E9 RID: 21993
		public UIFightBuffItem AvatarDaDaoBuff;

		// Token: 0x040055EA RID: 21994
		[HideInInspector]
		public Avatar Avatar;

		// Token: 0x040055EB RID: 21995
		private GameObject BuffPrefab;

		// Token: 0x040055EC RID: 21996
		public List<Sprite> JingJieSprites;

		// Token: 0x040055ED RID: 21997
		[HideInInspector]
		public int BuffCount;

		// Token: 0x040055EE RID: 21998
		private int lingYuSkillID;

		// Token: 0x040055EF RID: 21999
		private bool needRefreshBuff;

		// Token: 0x040055F0 RID: 22000
		private float cd;
	}
}
