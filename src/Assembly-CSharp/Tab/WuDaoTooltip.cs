using System;
using JSONClass;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Tab
{
	// Token: 0x020006F4 RID: 1780
	public class WuDaoTooltip : UIBase, IESCClose
	{
		// Token: 0x06003930 RID: 14640 RVA: 0x00186924 File Offset: 0x00184B24
		public WuDaoTooltip(GameObject go)
		{
			this._go = go;
			this._icon = base.Get<Image>("Bg/Up/Bg/Mask/Icon");
			this._name = base.Get<Text>("Bg/Up/Bg/Name");
			this._xiaoGuo = base.Get<Text>("Bg/Up/XiaoGuo");
			this._cost = base.Get<Text>("Bg/Center/Cost");
			this._tiaoJian = base.Get<Text>("Bg/Center/TiaoJian");
			this._desc = base.Get<Text>("Bg/Down/Desc");
			this._bgRect = base.Get<RectTransform>("Bg");
			this._upRect = base.Get<RectTransform>("Bg/Up");
			this._centerRect = base.Get<RectTransform>("Bg/Center");
			this._downRect = base.Get<RectTransform>("Bg/Down");
			this._btn = base.Get<FpBtn>("Bg/Down/Line/Btn");
			base.Get<Button>("Mask").onClick.AddListener(new UnityAction(this.Close));
		}

		// Token: 0x06003931 RID: 14641 RVA: 0x00186A1C File Offset: 0x00184C1C
		public void Show(Sprite sprite, int wudaoId, UnityAction action)
		{
			ESCCloseManager.Inst.RegisterClose(this);
			WuDaoJson wuDaoJson = WuDaoJson.DataDict[wudaoId];
			this._icon.sprite = sprite;
			this._name.text = wuDaoJson.name;
			this._xiaoGuo.text = wuDaoJson.xiaoguo;
			this._cost.text = "<color=#ffb143>【需求点数】</color>" + wuDaoJson.Cast;
			string text = "";
			for (int i = 0; i < wuDaoJson.Type.Count; i++)
			{
				text += WuDaoAllTypeJson.DataDict[wuDaoJson.Type[i]].name;
				if (i < wuDaoJson.Type.Count - 1)
				{
					text += ",";
				}
			}
			string text2 = WuDaoJinJieJson.DataDict[wuDaoJson.Lv].Text;
			this._tiaoJian.text = "<color=#ffb143>【领悟条件】</color>对" + text + "之道的感悟达到" + text2;
			this._desc.text = "\u00a0\u00a0\u00a0\u00a0\u00a0" + wuDaoJson.desc;
			this._btn.mouseUpEvent.RemoveAllListeners();
			this._btn.mouseUpEvent.AddListener(action);
			this._go.SetActive(true);
			this.UpdateSize();
		}

		// Token: 0x06003932 RID: 14642 RVA: 0x00186B68 File Offset: 0x00184D68
		private void UpdateSize()
		{
			LayoutRebuilder.ForceRebuildLayoutImmediate(this._downRect);
			LayoutRebuilder.ForceRebuildLayoutImmediate(this._centerRect);
			LayoutRebuilder.ForceRebuildLayoutImmediate(this._upRect);
			LayoutRebuilder.ForceRebuildLayoutImmediate(this._bgRect);
		}

		// Token: 0x06003933 RID: 14643 RVA: 0x00186B96 File Offset: 0x00184D96
		public void Close()
		{
			this._go.SetActive(false);
			ESCCloseManager.Inst.UnRegisterClose(this);
		}

		// Token: 0x06003934 RID: 14644 RVA: 0x00186BAF File Offset: 0x00184DAF
		public bool TryEscClose()
		{
			this.Close();
			return true;
		}

		// Token: 0x04003138 RID: 12600
		private Image _icon;

		// Token: 0x04003139 RID: 12601
		private Text _name;

		// Token: 0x0400313A RID: 12602
		private Text _xiaoGuo;

		// Token: 0x0400313B RID: 12603
		private Text _cost;

		// Token: 0x0400313C RID: 12604
		private Text _tiaoJian;

		// Token: 0x0400313D RID: 12605
		private Text _desc;

		// Token: 0x0400313E RID: 12606
		private RectTransform _bgRect;

		// Token: 0x0400313F RID: 12607
		private RectTransform _upRect;

		// Token: 0x04003140 RID: 12608
		private RectTransform _centerRect;

		// Token: 0x04003141 RID: 12609
		private RectTransform _downRect;

		// Token: 0x04003142 RID: 12610
		private FpBtn _btn;
	}
}
