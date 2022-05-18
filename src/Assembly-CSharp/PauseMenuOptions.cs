using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000177 RID: 375
public class PauseMenuOptions : MonoBehaviour
{
	// Token: 0x06000CA4 RID: 3236 RVA: 0x000042DD File Offset: 0x000024DD
	private void Start()
	{
	}

	// Token: 0x06000CA5 RID: 3237 RVA: 0x0000E72F File Offset: 0x0000C92F
	public void Init()
	{
		this.SelectedItemText.text = "Resume";
		this.SelectedItemInfoText.text = "Resumes the Game.";
		this.OptionsContainer.SetActive(false);
	}

	// Token: 0x06000CA6 RID: 3238 RVA: 0x00098B50 File Offset: 0x00096D50
	public void OnHoverTextChange(string name)
	{
		this.SelectedItemText.text = name;
		if (name == "Resume")
		{
			this.SelectedItemInfoText.text = "Resumes the Game.";
			this.OptionsContainer.SetActive(false);
			return;
		}
		if (name == "Options")
		{
			this.SelectedItemInfoText.text = "Change graphics Options.";
			this.OptionsContainer.SetActive(true);
			return;
		}
		if (name == "Main Menu")
		{
			this.SelectedItemInfoText.text = "Go back to Main Menu.";
			this.OptionsContainer.SetActive(false);
			return;
		}
		if (!(name == "Load Game"))
		{
			return;
		}
		this.SelectedItemInfoText.text = "Load previously Saved Game.";
		this.OptionsContainer.SetActive(false);
	}

	// Token: 0x040009D6 RID: 2518
	[Header("UI References")]
	public Text SelectedItemText;

	// Token: 0x040009D7 RID: 2519
	public Text SelectedItemInfoText;

	// Token: 0x040009D8 RID: 2520
	public GameObject OptionsContainer;
}
