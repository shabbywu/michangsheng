using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIShuangXiuResultPanel : MonoBehaviour
{
	public Text ShowText;

	public FpBtn OKBtn;

	private UnityAction OkAction;

	public Transform panel;

	private void Awake()
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		((Component)this).transform.SetParent(((Component)NewUICanvas.Inst).gameObject.transform);
		((Component)this).transform.localPosition = Vector3.zero;
		((Component)this).transform.localScale = Vector3.one;
		((Component)this).transform.SetAsLastSibling();
		Tools.canClickFlag = false;
		PanelMamager.CanOpenOrClose = false;
		panel.localScale = new Vector3(0.3f, 0.3f, 0.3f);
		ShortcutExtensions.DOScale(panel, Vector3.one, 0.5f);
	}

	private void Update()
	{
		if (Input.GetKeyUp((KeyCode)27))
		{
			Close();
		}
	}

	public void Close()
	{
		Object.Destroy((Object)(object)((Component)this).gameObject);
		Tools.canClickFlag = true;
		PanelMamager.CanOpenOrClose = true;
		if (OkAction != null)
		{
			OkAction.Invoke();
		}
	}

	public void Show(string str, UnityAction onOk = null)
	{
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Expected O, but got Unknown
		ShowText.text = str;
		OkAction = onOk;
		((UnityEventBase)OKBtn.mouseUpEvent).RemoveAllListeners();
		OKBtn.mouseUpEvent.AddListener(new UnityAction(Close));
	}
}
