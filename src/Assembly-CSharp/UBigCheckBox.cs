using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UBigCheckBox : MonoBehaviour
{
	private static UBigCheckBox inst;

	private static string prefabPath = "UBigCheckBox";

	private static bool needShow;

	private static string showText = "";

	private static UnityAction OKAction;

	public Text ShowText;

	public Button OKBtn;

	public RectTransform ContentRT;

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
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
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
			ContentRT.anchoredPosition = Vector2.zero;
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
