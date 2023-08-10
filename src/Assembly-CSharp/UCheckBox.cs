using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UCheckBox : MonoBehaviour
{
	private static UCheckBox inst;

	private static string prefabPath = "UCheckBox";

	private static bool needShow;

	private static string showText = "";

	private static UnityAction OKAction;

	public Text ShowText;

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
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Expected O, but got Unknown
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
			((Component)((Component)this).transform.GetChild(0)).gameObject.SetActive(true);
			needShow = false;
		}
	}

	private void Close()
	{
		IsShow = false;
		((Component)((Component)this).transform.GetChild(0)).gameObject.SetActive(false);
	}

	public static void Show(string text, UnityAction onOK = null)
	{
		if ((Object)(object)inst == (Object)null)
		{
			Object.Instantiate<GameObject>(Resources.Load<GameObject>(prefabPath));
		}
		showText = text;
		OKAction = onOK;
		needShow = true;
	}
}
