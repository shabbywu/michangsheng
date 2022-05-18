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
	// Token: 0x02000A7C RID: 2684
	public class FightVictory : MonoBehaviour, IESCClose
	{
		// Token: 0x060044FA RID: 17658 RVA: 0x001D7FB0 File Offset: 0x001D61B0
		private void Awake()
		{
			UIFightPanel.Inst.Close();
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
			ESCCloseManager.Inst.RegisterClose(this);
		}

		// Token: 0x060044FB RID: 17659 RVA: 0x001D8084 File Offset: 0x001D6284
		private void Init()
		{
			float num = (float)(Tools.instance.monstarMag.gameStartHP - this.avatar.HP) / (float)this.avatar.HP_Max * 100f;
			int num2 = (int)jsonData.instance.AvatarJsonData[this.monstarID.ToString()]["dropType"].n;
			foreach (JSONObject jsonobject in jsonData.instance.DropInfoJsonData.list)
			{
				if (num2 == (int)jsonobject["dropType"].n && RoundManager.instance != null && (float)RoundManager.instance.StaticRoundNum <= jsonobject["round"].n && num <= jsonobject["loseHp"].n)
				{
					this.Desc.text = jsonobject["TextDesc"].Str;
					this.addMoney(jsonobject["moneydrop"].n / 100f);
					foreach (JSONObject jsonobject2 in NpcJieSuanManager.inst.npcFight.npcDrop.dropReward(jsonobject["wepen"].n / 100f, jsonobject["backpack"].n / 100f, this.monstarID).list)
					{
						Tools.instance.getPlayer().addItem((int)jsonobject2["ID"].n, (int)jsonobject2["Num"].n, jsonobject2["seid"], false);
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
						break;
					}
					Tools.instance.getPlayer().OtherAvatar.die();
					break;
				}
			}
			this.Btn.mouseUpEvent.AddListener(new UnityAction(FightResultMag.inst.VictoryClick));
			this.Btn.mouseUpEvent.AddListener(delegate()
			{
				Object.Destroy(base.gameObject);
			});
		}

		// Token: 0x060044FC RID: 17660 RVA: 0x001D8420 File Offset: 0x001D6620
		private void addMoney(float percent)
		{
			int num = (int)(jsonData.instance.AvatarBackpackJsonData[string.Concat(this.monstarID)]["money"].n * percent);
			this.avatar.money += (ulong)((long)num);
			this.GetMoney.text = num.ToString();
		}

		// Token: 0x060044FD RID: 17661 RVA: 0x000315D0 File Offset: 0x0002F7D0
		private void Update()
		{
			if (Input.GetKeyUp(27))
			{
				FightResultMag.inst.VictoryClick();
				Object.Destroy(base.gameObject);
			}
		}

		// Token: 0x060044FE RID: 17662 RVA: 0x000315F0 File Offset: 0x0002F7F0
		private void Close()
		{
			FightResultMag.inst.VictoryClick();
			Object.Destroy(base.gameObject);
			ESCCloseManager.Inst.UnRegisterClose(this);
		}

		// Token: 0x060044FF RID: 17663 RVA: 0x00031612 File Offset: 0x0002F812
		public bool TryEscClose()
		{
			this.Close();
			return true;
		}

		// Token: 0x04003D1B RID: 15643
		[SerializeField]
		private Transform ItemList;

		// Token: 0x04003D1C RID: 15644
		[SerializeField]
		private GameObject ItemPrefab;

		// Token: 0x04003D1D RID: 15645
		[SerializeField]
		private Text Desc;

		// Token: 0x04003D1E RID: 15646
		[SerializeField]
		private Text GetMoney;

		// Token: 0x04003D1F RID: 15647
		[SerializeField]
		private FpBtn Btn;

		// Token: 0x04003D20 RID: 15648
		[SerializeField]
		private Transform panel;

		// Token: 0x04003D21 RID: 15649
		private int monstarID;

		// Token: 0x04003D22 RID: 15650
		private Avatar avatar;
	}
}
