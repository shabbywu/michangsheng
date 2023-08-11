using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class USelectBox : MonoBehaviour, IESCClose
{
	private static USelectBox inst;

	private static string prefabPath = "USelectBox";

	private static bool needShow;

	private static string showText = "";

	private static UnityAction OKAction;

	private static UnityAction CloseAction;

	public Text ShowText;

	public Button CloseBtn;

	public Button OKBtn;

	public static bool IsShow;

	private void Awake()
	{
		if ((Object)(object)inst != (Object)null)
		{
			Object.Destroy((Object)(object)((Component)this).gameObject);
		}
		inst = this;
		Object.DontDestroyOnLoad((Object)(object)((Component)this).gameObject);
	}

	private void Update()
	{
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Expected O, but got Unknown
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Expected O, but got Unknown
		if (needShow)
		{
			IsShow = true;
			ShowText.text = showText;
			((UnityEventBase)OKBtn.onClick).RemoveAllListeners();
			if (OKAction != null)
			{
				((UnityEvent)OKBtn.onClick).AddListener(OKAction);
			}
			((UnityEvent)OKBtn.onClick).AddListener(new UnityAction(Close));
			((UnityEventBase)CloseBtn.onClick).RemoveAllListeners();
			if (CloseAction != null)
			{
				((UnityEvent)CloseBtn.onClick).AddListener(CloseAction);
			}
			((UnityEvent)CloseBtn.onClick).AddListener(new UnityAction(Close));
			((Component)((Component)this).transform.GetChild(0)).gameObject.SetActive(true);
			needShow = false;
			ESCCloseManager.Inst.RegisterClose(this);
		}
	}

	private void Close()
	{
		IsShow = false;
		((Component)((Component)this).transform.GetChild(0)).gameObject.SetActive(false);
		ESCCloseManager.Inst.UnRegisterClose(this);
	}

	public static void Show(string text, UnityAction onOK = null, UnityAction onClose = null)
	{
		if ((Object)(object)inst == (Object)null)
		{
			Object.Instantiate<GameObject>(Resources.Load<GameObject>(prefabPath));
		}
		showText = text;
		OKAction = onOK;
		CloseAction = onClose;
		needShow = true;
	}

	public bool TryEscClose()
	{
		((UnityEvent)CloseBtn.onClick).Invoke();
		return true;
	}
}
