using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MenuBox : ModalBox
{
	[Tooltip("Set this to the name of the prefab that should be loaded when a menu box is shown.")]
	public static string PrefabResourceName = "Menu Box";

	public Button bgButton;

	public static bool hasMenu()
	{
		return (Object)(object)GameObject.Find("Menu Box(Clone)") != (Object)null;
	}

	public static MenuBox Show(IEnumerable<string> options, IEnumerable<UnityAction> actions, string title = "")
	{
		if (options.Count() != actions.Count())
		{
			throw new Exception("MenuBox.Show must be called with an equal number of options and actions.");
		}
		MenuBox component = Object.Instantiate<GameObject>(Resources.Load<GameObject>(PrefabResourceName)).GetComponent<MenuBox>();
		component.SetText(null, title);
		component.SetUpButtons(options, actions);
		return component;
	}

	private void SetUpButtons(IEnumerable<string> options, IEnumerable<UnityAction> actions)
	{
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Expected O, but got Unknown
		((UnityEventBase)bgButton.onClick).RemoveAllListeners();
		if (options.Count() <= 1)
		{
			((UnityEvent)bgButton.onClick).AddListener(actions.ElementAt(0));
			((UnityEvent)bgButton.onClick).AddListener((UnityAction)delegate
			{
				Close();
			});
		}
		for (int i = 0; i < options.Count(); i++)
		{
			CreateButton(((Component)Button).gameObject, options.ElementAt(i), actions.ElementAt(i));
		}
		if (options.Count() <= 1)
		{
			((Component)Panel).gameObject.SetActive(false);
		}
		else
		{
			((Component)Panel).gameObject.SetActive(true);
		}
		Object.Destroy((Object)(object)((Component)Button).gameObject);
	}

	private GameObject CreateButton(GameObject buttonToClone, string label, UnityAction action)
	{
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Expected O, but got Unknown
		GameObject obj = Object.Instantiate<GameObject>(buttonToClone);
		obj.GetComponentInChildren<Text>().text = label;
		((UnityEvent)obj.GetComponent<Button>().onClick).AddListener(action);
		((UnityEvent)obj.GetComponent<Button>().onClick).AddListener((UnityAction)delegate
		{
			Close();
		});
		obj.transform.SetParent(buttonToClone.transform.parent, false);
		return obj;
	}
}
