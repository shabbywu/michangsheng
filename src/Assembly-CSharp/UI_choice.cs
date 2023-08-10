using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UI_choice : MonoBehaviour
{
	private UnityAction ok;

	private UnityAction cancel;

	public Button OK;

	public Button Cancel;

	public Text desc;

	private void Start()
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Expected O, but got Unknown
		cancel = (UnityAction)Delegate.Combine((Delegate?)(object)cancel, (Delegate?)new UnityAction(removeSelf));
		((UnityEvent)Cancel.onClick).AddListener(cancel);
		((UnityEvent)OK.onClick).AddListener(cancel);
	}

	public void removeSelf()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		((Component)this).transform.localScale = Vector3.zero;
		Object.Destroy((Object)(object)((Component)this).gameObject, 0.1f);
	}

	public static UI_choice CreatUI_choice()
	{
		Object obj = Resources.Load("uiPrefab/CanvasChoice");
		return Object.Instantiate<GameObject>((GameObject)(object)((obj is GameObject) ? obj : null)).GetComponent<UI_choice>();
	}

	public void OKAddListener(UnityAction unityAction)
	{
		((UnityEvent)OK.onClick).AddListener(unityAction);
	}

	public void setText(string str)
	{
		desc.text = str;
	}

	private void Update()
	{
	}
}
