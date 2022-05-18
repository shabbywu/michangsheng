using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000235 RID: 565
public class MenuBox : ModalBox
{
	// Token: 0x06001163 RID: 4451 RVA: 0x00010DCC File Offset: 0x0000EFCC
	public static bool hasMenu()
	{
		return GameObject.Find("Menu Box(Clone)") != null;
	}

	// Token: 0x06001164 RID: 4452 RVA: 0x00010DDE File Offset: 0x0000EFDE
	public static MenuBox Show(IEnumerable<string> options, IEnumerable<UnityAction> actions, string title = "")
	{
		if (options.Count<string>() != actions.Count<UnityAction>())
		{
			throw new Exception("MenuBox.Show must be called with an equal number of options and actions.");
		}
		MenuBox component = Object.Instantiate<GameObject>(Resources.Load<GameObject>(MenuBox.PrefabResourceName)).GetComponent<MenuBox>();
		component.SetText(null, title);
		component.SetUpButtons(options, actions);
		return component;
	}

	// Token: 0x06001165 RID: 4453 RVA: 0x000ABAD4 File Offset: 0x000A9CD4
	private void SetUpButtons(IEnumerable<string> options, IEnumerable<UnityAction> actions)
	{
		this.bgButton.onClick.RemoveAllListeners();
		if (options.Count<string>() <= 1)
		{
			this.bgButton.onClick.AddListener(actions.ElementAt(0));
			this.bgButton.onClick.AddListener(delegate()
			{
				this.Close();
			});
		}
		for (int i = 0; i < options.Count<string>(); i++)
		{
			this.CreateButton(this.Button.gameObject, options.ElementAt(i), actions.ElementAt(i));
		}
		if (options.Count<string>() <= 1)
		{
			this.Panel.gameObject.SetActive(false);
		}
		else
		{
			this.Panel.gameObject.SetActive(true);
		}
		Object.Destroy(this.Button.gameObject);
	}

	// Token: 0x06001166 RID: 4454 RVA: 0x000ABB9C File Offset: 0x000A9D9C
	private GameObject CreateButton(GameObject buttonToClone, string label, UnityAction action)
	{
		GameObject gameObject = Object.Instantiate<GameObject>(buttonToClone);
		gameObject.GetComponentInChildren<Text>().text = label;
		gameObject.GetComponent<Button>().onClick.AddListener(action);
		gameObject.GetComponent<Button>().onClick.AddListener(delegate()
		{
			this.Close();
		});
		gameObject.transform.SetParent(buttonToClone.transform.parent, false);
		return gameObject;
	}

	// Token: 0x04000E0F RID: 3599
	[Tooltip("Set this to the name of the prefab that should be loaded when a menu box is shown.")]
	public static string PrefabResourceName = "Menu Box";

	// Token: 0x04000E10 RID: 3600
	public Button bgButton;
}
