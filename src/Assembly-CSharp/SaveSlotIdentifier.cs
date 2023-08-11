using UnityEngine;
using UnityEngine.UI;

public class SaveSlotIdentifier : MonoBehaviour
{
	[Header("Slot specific Variables")]
	[Tooltip("if true, this Slot will become the quick save slot!")]
	public bool quickSaveSlot;

	[Tooltip("Unique Slot ID used to identify while loading")]
	public int slotId;

	[Tooltip("This Slot's name field")]
	public Text saveName_text;

	[Tooltip("This Slot's percentage field")]
	public Text savePercentage_text;

	[Tooltip("Parent UI Class to send slot specific data")]
	public SaveGameUI saveGameUI;

	[Tooltip("Unique save trigger id ")]
	public int saveTriggerId;

	private void Start()
	{
		loadSlotData();
	}

	public void sendSlotData()
	{
		saveGameUI.saveName_text = saveName_text;
		saveGameUI.savePercentage_text = savePercentage_text;
		saveGameUI.slotId = slotId;
	}

	private void loadSlotData()
	{
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		if (PlayerPrefs.GetInt("slot_" + slotId) == slotId)
		{
			saveName_text.text = PlayerPrefs.GetString("slot_saveName_" + slotId);
			savePercentage_text.text = PlayerPrefs.GetFloat("slot_savePercentage_" + slotId) + "%";
		}
		quickSaveSlot = ((PlayerPrefs.GetInt("quickSaveSlot") == slotId) ? true : false);
		if (quickSaveSlot)
		{
			Color red = Color.red;
			red.a = 0.25f;
			((Graphic)((Component)this).GetComponent<Image>()).color = red;
			((Behaviour)((Component)this).GetComponentInChildren<Button>()).enabled = false;
			savePercentage_text.text = "";
			saveGameUI.quickSaveSlotData(saveName_text, savePercentage_text, slotId);
		}
		if (slotId == 1)
		{
			MainMenu_KeyboardController mainMenu_KeyboardController = Object.FindObjectOfType<MainMenu_KeyboardController>();
			if (Object.op_Implicit((Object)(object)mainMenu_KeyboardController))
			{
				mainMenu_KeyboardController.SetNextSelectedGameobject(((Component)saveName_text).gameObject);
			}
		}
	}
}
