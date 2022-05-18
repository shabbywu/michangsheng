using System;
using DG.Tweening;
using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

namespace QiYu
{
	// Token: 0x02000A77 RID: 2679
	public class QiYuUIMag : MonoBehaviour
	{
		// Token: 0x060044E5 RID: 17637 RVA: 0x001D7B00 File Offset: 0x001D5D00
		public void Show(int id)
		{
			QiYuUIMag.Inst = this;
			base.transform.SetParent(NewUICanvas.Inst.gameObject.transform);
			base.transform.localPosition = Vector3.zero;
			base.transform.localScale = Vector3.one;
			base.transform.SetAsLastSibling();
			this.Panel.localScale = new Vector3(0.3f, 0.3f, 0.3f);
			ShortcutExtensions.DOScale(this.Panel, Vector3.one, 0.5f);
			this.EventId = id;
			PanelMamager.CanOpenOrClose = false;
			Tools.canClickFlag = false;
			this.Init();
		}

		// Token: 0x060044E6 RID: 17638 RVA: 0x001D7BA8 File Offset: 0x001D5DA8
		private void Init()
		{
			AllMapShiJianOptionJsonData data = AllMapShiJianOptionJsonData.DataDict[this.EventId];
			this.EventName.text = data.EventName;
			this.EventContent.text = "\u3000\u3000" + data.desc.Replace("{huanhang}", "\n");
			if (data.option1 > 0)
			{
				this.Option.Inst(this.OptionList).GetComponent<QiYuOption>().Init(data.optionDesc1, delegate
				{
					this.OptionAction(data.option1);
					this.OptionList.gameObject.SetActive(false);
				});
			}
			if (data.option2 > 0)
			{
				this.Option.Inst(this.OptionList).GetComponent<QiYuOption>().Init(data.optionDesc2, delegate
				{
					this.OptionAction(data.option2);
					this.OptionList.gameObject.SetActive(false);
				});
			}
			if (data.option3 > 0)
			{
				this.Option.Inst(this.OptionList).GetComponent<QiYuOption>().Init(data.optionDesc3, delegate
				{
					this.OptionAction(data.option3);
					this.OptionList.gameObject.SetActive(false);
				});
			}
		}

		// Token: 0x060044E7 RID: 17639 RVA: 0x001D7CE0 File Offset: 0x001D5EE0
		private void OptionAction(int optionId)
		{
			AllMapOptionJsonData data = AllMapOptionJsonData.DataDict[optionId];
			Avatar player = Tools.instance.getPlayer();
			if (data.value1 > 0)
			{
				player.AddTime(data.value1, 0, 0);
			}
			if (data.value2 != 0)
			{
				player.AddMoney(data.value2);
			}
			if (data.value3 != 0)
			{
				player.addEXP(data.value3);
			}
			if (data.value4 != 0)
			{
				player.xinjin += data.value4;
			}
			if (data.value5 != 0)
			{
				player.AddHp(data.value5);
			}
			if (data.value6.Count > 0)
			{
				for (int i = 0; i < data.value6.Count; i++)
				{
					player.addItem(data.value6[i], data.value7[i], Tools.CreateItemSeid(data.value6[i]), false);
				}
			}
			if (data.value8 > 0)
			{
				this.OkBtn.GetComponent<FpBtn>().mouseUpEvent.AddListener(delegate()
				{
					ResManager.inst.LoadTalk("TalkPrefab/Talk" + data.value8).Inst(null);
				});
			}
			if (data.value10 > 0)
			{
				player.wuDaoMag.AddLingGuangByJsonID(data.value10);
			}
			if (data.value9 > 0)
			{
				ResManager.inst.LoadPrefab("QiYuPanel").Inst(null).GetComponent<QiYuUIMag>().Show(data.value9);
				this.Close();
			}
			this.EventContent.text = "\u3000\u3000" + data.desc.Replace("{huanhang}", "\n");
			this.OkBtn.SetActive(true);
		}

		// Token: 0x060044E8 RID: 17640 RVA: 0x000111B3 File Offset: 0x0000F3B3
		public void Close()
		{
			Object.Destroy(base.gameObject);
		}

		// Token: 0x060044E9 RID: 17641 RVA: 0x00031468 File Offset: 0x0002F668
		private void OnDestroy()
		{
			PanelMamager.CanOpenOrClose = true;
			Tools.canClickFlag = true;
			QiYuUIMag.Inst = null;
		}

		// Token: 0x060044EA RID: 17642 RVA: 0x0003147C File Offset: 0x0002F67C
		private void Update()
		{
			if (Input.GetKeyUp(27) && this.OkBtn.activeSelf)
			{
				this.Close();
			}
		}

		// Token: 0x04003D0D RID: 15629
		[SerializeField]
		private Transform Panel;

		// Token: 0x04003D0E RID: 15630
		[SerializeField]
		private Text EventName;

		// Token: 0x04003D0F RID: 15631
		[SerializeField]
		private Text EventContent;

		// Token: 0x04003D10 RID: 15632
		[SerializeField]
		private Transform OptionList;

		// Token: 0x04003D11 RID: 15633
		[SerializeField]
		private GameObject Option;

		// Token: 0x04003D12 RID: 15634
		[SerializeField]
		private GameObject OkBtn;

		// Token: 0x04003D13 RID: 15635
		public int EventId;

		// Token: 0x04003D14 RID: 15636
		public static QiYuUIMag Inst;
	}
}
