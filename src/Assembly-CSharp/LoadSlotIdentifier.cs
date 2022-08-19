using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000FA RID: 250
public class LoadSlotIdentifier : MonoBehaviour
{
	// Token: 0x06000BA0 RID: 2976 RVA: 0x00046EDC File Offset: 0x000450DC
	private void Awake()
	{
		this.Init();
		UIController uicontroller = Object.FindObjectOfType<UIController>();
		if (uicontroller != null)
		{
			uicontroller.loadSlots.Add(this);
		}
	}

	// Token: 0x06000BA1 RID: 2977 RVA: 0x00046F0A File Offset: 0x0004510A
	public void Init()
	{
		this.loadSlotData();
	}

	// Token: 0x06000BA2 RID: 2978 RVA: 0x00046F14 File Offset: 0x00045114
	private void loadSlotData()
	{
		this.quickSaveSlot = (PlayerPrefs.GetInt("quickSaveSlot") == this.slotId);
		if (this.quickSaveSlot)
		{
			Color red = Color.red;
			red.a = 0.25f;
			base.GetComponent<Image>().color = red;
		}
		if (PlayerPrefs.GetInt("slot_" + this.slotId) == this.slotId)
		{
			this.saveName_text.text = PlayerPrefs.GetString("slot_saveName_" + this.slotId);
			this.sceneToLoad = PlayerPrefs.GetString("slot_sceneName_" + this.slotId);
			if (!this.quickSaveSlot)
			{
				this.savePercentage_text.text = PlayerPrefs.GetFloat("slot_savePercentage_" + this.slotId) + "%";
				return;
			}
			this.savePercentage_text.text = "";
		}
	}

	// Token: 0x06000BA3 RID: 2979 RVA: 0x0004701C File Offset: 0x0004521C
	public void LoadSceneSaved()
	{
		if (this.sceneToLoad != "")
		{
			Time.timeScale = 1f;
			GameObject gameObject = GameObject.FindGameObjectWithTag("Player");
			if (gameObject)
			{
				Object.Destroy(gameObject);
			}
			PlayerPrefs.SetInt("slotLoaded_", this.slotId);
			PlayerPrefs.SetString("sceneToLoad", this.sceneToLoad);
			Object.FindObjectOfType<Fader>().FadeIntoLevel("LoadingScreen");
		}
	}

	// Token: 0x040007E8 RID: 2024
	[Header("Slot specific Variables")]
	[Tooltip("if true, this Slot will become the quick save slot!")]
	public bool quickSaveSlot;

	// Token: 0x040007E9 RID: 2025
	[Tooltip("Unique Slot ID used to identify while loading")]
	public int slotId;

	// Token: 0x040007EA RID: 2026
	[Tooltip("This Slot's name field")]
	public Text saveName_text;

	// Token: 0x040007EB RID: 2027
	[Tooltip("This Slot's percentage field")]
	public Text savePercentage_text;

	// Token: 0x040007EC RID: 2028
	[Tooltip("Scene to load")]
	public string sceneToLoad;
}
