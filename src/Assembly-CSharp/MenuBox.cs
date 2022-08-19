using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x0200015D RID: 349
public class MenuBox : ModalBox
{
	// Token: 0x06000F34 RID: 3892 RVA: 0x0005BB83 File Offset: 0x00059D83
	public static bool hasMenu()
	{
		return GameObject.Find("Menu Box(Clone)") != null;
	}

	// Token: 0x06000F35 RID: 3893 RVA: 0x0005BB95 File Offset: 0x00059D95
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

	// Token: 0x06000F36 RID: 3894 RVA: 0x0005BBD4 File Offset: 0x00059DD4
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

	// Token: 0x06000F37 RID: 3895 RVA: 0x0005BC9C File Offset: 0x00059E9C
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

	// Token: 0x04000B69 RID: 2921
	[Tooltip("Set this to the name of the prefab that should be loaded when a menu box is shown.")]
	public static string PrefabResourceName = "Menu Box";

	// Token: 0x04000B6A RID: 2922
	public Button bgButton;
}
