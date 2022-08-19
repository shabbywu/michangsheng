using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000100 RID: 256
public class SaveSlotIdentifier : MonoBehaviour
{
	// Token: 0x06000BCF RID: 3023 RVA: 0x00047A4A File Offset: 0x00045C4A
	private void Start()
	{
		this.loadSlotData();
	}

	// Token: 0x06000BD0 RID: 3024 RVA: 0x00047A52 File Offset: 0x00045C52
	public void sendSlotData()
	{
		this.saveGameUI.saveName_text = this.saveName_text;
		this.saveGameUI.savePercentage_text = this.savePercentage_text;
		this.saveGameUI.slotId = this.slotId;
	}

	// Token: 0x06000BD1 RID: 3025 RVA: 0x00047A88 File Offset: 0x00045C88
	private void loadSlotData()
	{
		if (PlayerPrefs.GetInt("slot_" + this.slotId) == this.slotId)
		{
			this.saveName_text.text = PlayerPrefs.GetString("slot_saveName_" + this.slotId);
			this.savePercentage_text.text = PlayerPrefs.GetFloat("slot_savePercentage_" + this.slotId) + "%";
		}
		this.quickSaveSlot = (PlayerPrefs.GetInt("quickSaveSlot") == this.slotId);
		if (this.quickSaveSlot)
		{
			Color red = Color.red;
			red.a = 0.25f;
			base.GetComponent<Image>().color = red;
			base.GetComponentInChildren<Button>().enabled = false;
			this.savePercentage_text.text = "";
			this.saveGameUI.quickSaveSlotData(this.saveName_text, this.savePercentage_text, this.slotId);
		}
		if (this.slotId == 1)
		{
			MainMenu_KeyboardController mainMenu_KeyboardController = Object.FindObjectOfType<MainMenu_KeyboardController>();
			if (mainMenu_KeyboardController)
			{
				mainMenu_KeyboardController.SetNextSelectedGameobject(this.saveName_text.gameObject);
			}
		}
	}

	// Token: 0x04000815 RID: 2069
	[Header("Slot specific Variables")]
	[Tooltip("if true, this Slot will become the quick save slot!")]
	public bool quickSaveSlot;

	// Token: 0x04000816 RID: 2070
	[Tooltip("Unique Slot ID used to identify while loading")]
	public int slotId;

	// Token: 0x04000817 RID: 2071
	[Tooltip("This Slot's name field")]
	public Text saveName_text;

	// Token: 0x04000818 RID: 2072
	[Tooltip("This Slot's percentage field")]
	public Text savePercentage_text;

	// Token: 0x04000819 RID: 2073
	[Tooltip("Parent UI Class to send slot specific data")]
	public SaveGameUI saveGameUI;

	// Token: 0x0400081A RID: 2074
	[Tooltip("Unique save trigger id ")]
	public int saveTriggerId;
}
