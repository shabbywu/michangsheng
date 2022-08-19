using System;
using DG.Tweening;
using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

namespace QiYu
{
	// Token: 0x02000722 RID: 1826
	public class QiYuUIMag : MonoBehaviour
	{
		// Token: 0x06003A48 RID: 14920 RVA: 0x00190470 File Offset: 0x0018E670
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

		// Token: 0x06003A49 RID: 14921 RVA: 0x00190518 File Offset: 0x0018E718
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

		// Token: 0x06003A4A RID: 14922 RVA: 0x00190650 File Offset: 0x0018E850
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

		// Token: 0x06003A4B RID: 14923 RVA: 0x0005C928 File Offset: 0x0005AB28
		public void Close()
		{
			Object.Destroy(base.gameObject);
		}

		// Token: 0x06003A4C RID: 14924 RVA: 0x00190858 File Offset: 0x0018EA58
		private void OnDestroy()
		{
			PanelMamager.CanOpenOrClose = true;
			Tools.canClickFlag = true;
			QiYuUIMag.Inst = null;
		}

		// Token: 0x06003A4D RID: 14925 RVA: 0x0019086C File Offset: 0x0018EA6C
		private void Update()
		{
			if (Input.GetKeyUp(27) && this.OkBtn.activeSelf)
			{
				this.Close();
			}
		}

		// Token: 0x04003275 RID: 12917
		[SerializeField]
		private Transform Panel;

		// Token: 0x04003276 RID: 12918
		[SerializeField]
		private Text EventName;

		// Token: 0x04003277 RID: 12919
		[SerializeField]
		private Text EventContent;

		// Token: 0x04003278 RID: 12920
		[SerializeField]
		private Transform OptionList;

		// Token: 0x04003279 RID: 12921
		[SerializeField]
		private GameObject Option;

		// Token: 0x0400327A RID: 12922
		[SerializeField]
		private GameObject OkBtn;

		// Token: 0x0400327B RID: 12923
		public int EventId;

		// Token: 0x0400327C RID: 12924
		public static QiYuUIMag Inst;
	}
}
