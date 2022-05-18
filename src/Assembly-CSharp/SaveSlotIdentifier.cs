using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200017E RID: 382
public class SaveSlotIdentifier : MonoBehaviour
{
	// Token: 0x06000CDA RID: 3290 RVA: 0x0000EA01 File Offset: 0x0000CC01
	private void Start()
	{
		this.loadSlotData();
	}

	// Token: 0x06000CDB RID: 3291 RVA: 0x0000EA09 File Offset: 0x0000CC09
	public void sendSlotData()
	{
		this.saveGameUI.saveName_text = this.saveName_text;
		this.saveGameUI.savePercentage_text = this.savePercentage_text;
		this.saveGameUI.slotId = this.slotId;
	}

	// Token: 0x06000CDC RID: 3292 RVA: 0x00099314 File Offset: 0x00097514
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

	// Token: 0x04000A04 RID: 2564
	[Header("Slot specific Variables")]
	[Tooltip("if true, this Slot will become the quick save slot!")]
	public bool quickSaveSlot;

	// Token: 0x04000A05 RID: 2565
	[Tooltip("Unique Slot ID used to identify while loading")]
	public int slotId;

	// Token: 0x04000A06 RID: 2566
	[Tooltip("This Slot's name field")]
	public Text saveName_text;

	// Token: 0x04000A07 RID: 2567
	[Tooltip("This Slot's percentage field")]
	public Text savePercentage_text;

	// Token: 0x04000A08 RID: 2568
	[Tooltip("Parent UI Class to send slot specific data")]
	public SaveGameUI saveGameUI;

	// Token: 0x04000A09 RID: 2569
	[Tooltip("Unique save trigger id ")]
	public int saveTriggerId;
}
