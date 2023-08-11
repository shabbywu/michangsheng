using JSONClass;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Tab;

public class WuDaoTooltip : UIBase, IESCClose
{
	private Image _icon;

	private Text _name;

	private Text _xiaoGuo;

	private Text _cost;

	private Text _tiaoJian;

	private Text _desc;

	private RectTransform _bgRect;

	private RectTransform _upRect;

	private RectTransform _centerRect;

	private RectTransform _downRect;

	private FpBtn _btn;

	public WuDaoTooltip(GameObject go)
	{
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Expected O, but got Unknown
		_go = go;
		_icon = Get<Image>("Bg/Up/Bg/Mask/Icon");
		_name = Get<Text>("Bg/Up/Bg/Name");
		_xiaoGuo = Get<Text>("Bg/Up/XiaoGuo");
		_cost = Get<Text>("Bg/Center/Cost");
		_tiaoJian = Get<Text>("Bg/Center/TiaoJian");
		_desc = Get<Text>("Bg/Down/Desc");
		_bgRect = Get<RectTransform>("Bg");
		_upRect = Get<RectTransform>("Bg/Up");
		_centerRect = Get<RectTransform>("Bg/Center");
		_downRect = Get<RectTransform>("Bg/Down");
		_btn = Get<FpBtn>("Bg/Down/Line/Btn");
		((UnityEvent)Get<Button>("Mask").onClick).AddListener(new UnityAction(Close));
	}

	public void Show(Sprite sprite, int wudaoId, UnityAction action)
	{
		ESCCloseManager.Inst.RegisterClose(this);
		WuDaoJson wuDaoJson = WuDaoJson.DataDict[wudaoId];
		_icon.sprite = sprite;
		_name.text = wuDaoJson.name;
		_xiaoGuo.text = wuDaoJson.xiaoguo;
		_cost.text = "<color=#ffb143>【需求点数】</color>" + wuDaoJson.Cast;
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
		_tiaoJian.text = "<color=#ffb143>【领悟条件】</color>对" + text + "之道的感悟达到" + text2;
		_desc.text = "\u00a0\u00a0\u00a0\u00a0\u00a0" + wuDaoJson.desc;
		((UnityEventBase)_btn.mouseUpEvent).RemoveAllListeners();
		_btn.mouseUpEvent.AddListener(action);
		_go.SetActive(true);
		UpdateSize();
	}

	private void UpdateSize()
	{
		LayoutRebuilder.ForceRebuildLayoutImmediate(_downRect);
		LayoutRebuilder.ForceRebuildLayoutImmediate(_centerRect);
		LayoutRebuilder.ForceRebuildLayoutImmediate(_upRect);
		LayoutRebuilder.ForceRebuildLayoutImmediate(_bgRect);
	}

	public void Close()
	{
		_go.SetActive(false);
		ESCCloseManager.Inst.UnRegisterClose(this);
	}

	public bool TryEscClose()
	{
		Close();
		return true;
	}
}
