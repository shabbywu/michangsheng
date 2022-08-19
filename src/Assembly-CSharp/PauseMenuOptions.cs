using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000FB RID: 251
public class PauseMenuOptions : MonoBehaviour
{
	// Token: 0x06000BA5 RID: 2981 RVA: 0x00004095 File Offset: 0x00002295
	private void Start()
	{
	}

	// Token: 0x06000BA6 RID: 2982 RVA: 0x0004708D File Offset: 0x0004528D
	public void Init()
	{
		this.SelectedItemText.text = "Resume";
		this.SelectedItemInfoText.text = "Resumes the Game.";
		this.OptionsContainer.SetActive(false);
	}

	// Token: 0x06000BA7 RID: 2983 RVA: 0x000470BC File Offset: 0x000452BC
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

	// Token: 0x040007ED RID: 2029
	[Header("UI References")]
	public Text SelectedItemText;

	// Token: 0x040007EE RID: 2030
	public Text SelectedItemInfoText;

	// Token: 0x040007EF RID: 2031
	public GameObject OptionsContainer;
}
