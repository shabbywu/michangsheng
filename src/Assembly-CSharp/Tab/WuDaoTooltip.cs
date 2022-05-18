using System;
using JSONClass;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Tab
{
	// Token: 0x02000A35 RID: 2613
	public class WuDaoTooltip : UIBase, IESCClose
	{
		// Token: 0x0600439B RID: 17307 RVA: 0x001CE1FC File Offset: 0x001CC3FC
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

		// Token: 0x0600439C RID: 17308 RVA: 0x001CE2F4 File Offset: 0x001CC4F4
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

		// Token: 0x0600439D RID: 17309 RVA: 0x00030591 File Offset: 0x0002E791
		private void UpdateSize()
		{
			LayoutRebuilder.ForceRebuildLayoutImmediate(this._downRect);
			LayoutRebuilder.ForceRebuildLayoutImmediate(this._centerRect);
			LayoutRebuilder.ForceRebuildLayoutImmediate(this._upRect);
			LayoutRebuilder.ForceRebuildLayoutImmediate(this._bgRect);
		}

		// Token: 0x0600439E RID: 17310 RVA: 0x000305BF File Offset: 0x0002E7BF
		public void Close()
		{
			this._go.SetActive(false);
			ESCCloseManager.Inst.UnRegisterClose(this);
		}

		// Token: 0x0600439F RID: 17311 RVA: 0x000305D8 File Offset: 0x0002E7D8
		public bool TryEscClose()
		{
			this.Close();
			return true;
		}

		// Token: 0x04003B9D RID: 15261
		private Image _icon;

		// Token: 0x04003B9E RID: 15262
		private Text _name;

		// Token: 0x04003B9F RID: 15263
		private Text _xiaoGuo;

		// Token: 0x04003BA0 RID: 15264
		private Text _cost;

		// Token: 0x04003BA1 RID: 15265
		private Text _tiaoJian;

		// Token: 0x04003BA2 RID: 15266
		private Text _desc;

		// Token: 0x04003BA3 RID: 15267
		private RectTransform _bgRect;

		// Token: 0x04003BA4 RID: 15268
		private RectTransform _upRect;

		// Token: 0x04003BA5 RID: 15269
		private RectTransform _centerRect;

		// Token: 0x04003BA6 RID: 15270
		private RectTransform _downRect;

		// Token: 0x04003BA7 RID: 15271
		private FpBtn _btn;
	}
}
