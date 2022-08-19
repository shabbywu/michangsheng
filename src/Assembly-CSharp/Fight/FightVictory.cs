using System;
using DG.Tweening;
using GUIPackage;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using YSGame.Fight;

namespace Fight
{
	// Token: 0x02000725 RID: 1829
	public class FightVictory : MonoBehaviour, IESCClose
	{
		// Token: 0x06003A58 RID: 14936 RVA: 0x001909FC File Offset: 0x0018EBFC
		private void Awake()
		{
			this.SetVictory();
			if (UIFightPanel.Inst != null)
			{
				UIFightPanel.Inst.Close();
			}
			ESCCloseManager.Inst.RegisterClose(this);
			base.transform.SetParent(NewUICanvas.Inst.gameObject.transform);
			base.transform.localPosition = Vector3.zero;
			base.transform.localScale = Vector3.one;
			base.transform.SetAsLastSibling();
			Tools.canClickFlag = false;
			this.monstarID = Tools.instance.MonstarID;
			this.avatar = Tools.instance.getPlayer();
			base.gameObject.SetActive(true);
			this.Init();
			this.panel.localScale = new Vector3(0.3f, 0.3f, 0.3f);
			ShortcutExtensions.DOScale(this.panel, Vector3.one, 0.5f);
		}

		// Token: 0x06003A59 RID: 14937 RVA: 0x00190AE3 File Offset: 0x0018ECE3
		private void SetVictory()
		{
			GlobalValue.SetTalk(1, 2, "Avatar.die");
			PlayerEx.Player.StaticValue.talk[1] = 2;
		}

		// Token: 0x06003A5A RID: 14938 RVA: 0x00190B04 File Offset: 0x0018ED04
		private void Init()
		{
			float num = (float)(Tools.instance.monstarMag.gameStartHP - this.avatar.HP) / (float)this.avatar.HP_Max;
			int num2 = (int)jsonData.instance.AvatarJsonData[this.monstarID.ToString()]["dropType"].n;
			foreach (JSONObject jsonobject in jsonData.instance.DropInfoJsonData.list)
			{
				if (num2 == (int)jsonobject["dropType"].n)
				{
					this.Desc.text = jsonobject["TextDesc"].Str;
					this.addMoney(jsonobject["moneydrop"].n / 100f);
					foreach (JSONObject jsonobject2 in NpcJieSuanManager.inst.npcFight.npcDrop.dropReward(jsonobject["wepen"].n / 100f, jsonobject["backpack"].n / 100f, this.monstarID).list)
					{
						Tools.instance.getPlayer().addItem(jsonobject2["ID"].I, jsonobject2["Num"].I, jsonobject2["seid"], false);
						UIIconShow component = this.ItemPrefab.Inst(this.ItemList).GetComponent<UIIconShow>();
						component.SetItem(new item(jsonobject2["ID"].I)
						{
							Seid = jsonobject2["seid"],
							itemNum = jsonobject2["Num"].I
						});
						component.Count = jsonobject2["Num"].I;
						component.CanDrag = false;
						component.BottomBG.color = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, 0);
					}
					if (Tools.instance.MonstarID >= 20000)
					{
						if (NpcJieSuanManager.inst.isCanJieSuan)
						{
							NpcJieSuanManager.inst.npcDeath.SetNpcDeath(2, Tools.instance.MonstarID, 0, false);
						}
						else
						{
							NpcJieSuanManager.inst.npcDeath.SetNpcDeath(2, Tools.instance.MonstarID, 0, true);
						}
						NpcJieSuanManager.inst.npcMap.RemoveNpcByList(Tools.instance.MonstarID);
						NpcJieSuanManager.inst.isUpDateNpcList = true;
					}
					else if (RoundManager.instance == null)
					{
						if (GlobalValue.Get(401, "Avatar.die") == Tools.instance.MonstarID)
						{
							PlayerEx.Player.nomelTaskMag.AutoNTaskSetKillAvatar(Tools.instance.MonstarID);
						}
						jsonData.instance.setMonstarDeath(Tools.instance.MonstarID, true);
						Tools.instance.AutoSetSeaMonstartDie();
					}
					else
					{
						Avatar otherAvatar = Tools.instance.getPlayer().OtherAvatar;
						if (otherAvatar != null)
						{
							otherAvatar.die();
						}
					}
					if (!PlayerEx.Player.HasDefeatNpcList.Contains(Tools.instance.MonstarID))
					{
						PlayerEx.Player.HasDefeatNpcList.Add(Tools.instance.MonstarID);
						break;
					}
					break;
				}
			}
			this.Btn.mouseUpEvent.AddListener(new UnityAction(this.Close));
			this.Btn.mouseUpEvent.AddListener(delegate()
			{
				Object.Destroy(base.gameObject);
			});
		}

		// Token: 0x06003A5B RID: 14939 RVA: 0x00190EEC File Offset: 0x0018F0EC
		private void addMoney(float percent)
		{
			int num = (int)(jsonData.instance.AvatarBackpackJsonData[string.Concat(this.monstarID)]["money"].n * percent);
			this.avatar.money += (ulong)((long)num);
			this.GetMoney.text = num.ToString();
		}

		// Token: 0x06003A5C RID: 14940 RVA: 0x00190F51 File Offset: 0x0018F151
		private void Close()
		{
			PanelMamager.CanOpenOrClose = true;
			Tools.canClickFlag = true;
			Tools.instance.loadMapScenes(Tools.instance.FinalScene, true);
			Object.Destroy(base.gameObject);
			ESCCloseManager.Inst.UnRegisterClose(this);
		}

		// Token: 0x06003A5D RID: 14941 RVA: 0x00190F8A File Offset: 0x0018F18A
		public bool TryEscClose()
		{
			this.Close();
			return true;
		}

		// Token: 0x04003280 RID: 12928
		[SerializeField]
		private Transform ItemList;

		// Token: 0x04003281 RID: 12929
		[SerializeField]
		private GameObject ItemPrefab;

		// Token: 0x04003282 RID: 12930
		[SerializeField]
		private Text Desc;

		// Token: 0x04003283 RID: 12931
		[SerializeField]
		private Text GetMoney;

		// Token: 0x04003284 RID: 12932
		[SerializeField]
		private FpBtn Btn;

		// Token: 0x04003285 RID: 12933
		[SerializeField]
		private Transform panel;

		// Token: 0x04003286 RID: 12934
		private int monstarID;

		// Token: 0x04003287 RID: 12935
		private Avatar avatar;
	}
}
