using UnityEngine;
using UnityEngine.UI;

public class PauseMenuOptions : MonoBehaviour
{
	[Header("UI References")]
	public Text SelectedItemText;

	public Text SelectedItemInfoText;

	public GameObject OptionsContainer;

	private void Start()
	{
	}

	public void Init()
	{
		SelectedItemText.text = "Resume";
		SelectedItemInfoText.text = "Resumes the Game.";
		OptionsContainer.SetActive(false);
	}

	public void OnHoverTextChange(string name)
	{
		SelectedItemText.text = name;
		switch (name)
		{
		case "Resume":
			SelectedItemInfoText.text = "Resumes the Game.";
			OptionsContainer.SetActive(false);
			break;
		case "Options":
			SelectedItemInfoText.text = "Change graphics Options.";
			OptionsContainer.SetActive(true);
			break;
		case "Main Menu":
			SelectedItemInfoText.text = "Go back to Main Menu.";
			OptionsContainer.SetActive(false);
			break;
		case "Load Game":
			SelectedItemInfoText.text = "Load previously Saved Game.";
			OptionsContainer.SetActive(false);
			break;
		}
	}
}
