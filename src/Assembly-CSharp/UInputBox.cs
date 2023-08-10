using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UInputBox : MonoBehaviour
{
	public static UInputBox Inst;

	public static bool IsShow;

	private static string prefabPath = "UInputBox";

	private static bool needShow;

	private static string showText = "";

	private static UnityAction<string> OKAction;

	public Text ShowText;

	public InputField TextInputField;

	public Button CloseBtn;

	public Button OKBtn;

	private void Awake()
	{
		if ((Object)(object)Inst != (Object)null)
		{
			Object.Destroy((Object)(object)((Component)this).gameObject);
		}
		Inst = this;
		Object.DontDestroyOnLoad((Object)(object)((Component)this).gameObject);
	}

	private void Update()
	{
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Expected O, but got Unknown
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Expected O, but got Unknown
		if (!needShow)
		{
			return;
		}
		needShow = false;
		ShowText.text = showText;
		TextInputField.text = "";
		((UnityEventBase)OKBtn.onClick).RemoveAllListeners();
		if (OKAction != null)
		{
			((UnityEvent)OKBtn.onClick).AddListener((UnityAction)delegate
			{
				OKAction.Invoke(TextInputField.text);
			});
		}
		((UnityEvent)OKBtn.onClick).AddListener(new UnityAction(Close));
		((Component)((Component)this).transform.GetChild(0)).gameObject.SetActive(true);
		IsShow = true;
	}

	private void Close()
	{
		IsShow = false;
		((Component)((Component)this).transform.GetChild(0)).gameObject.SetActive(false);
	}

	public static void Show(string text, UnityAction<string> onOK = null)
	{
		if ((Object)(object)Inst == (Object)null)
		{
			Object.Instantiate<GameObject>(Resources.Load<GameObject>(prefabPath));
		}
		showText = text;
		OKAction = onOK;
		needShow = true;
	}
}
