using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000176 RID: 374
public class LoadSlotIdentifier : MonoBehaviour
{
	// Token: 0x06000C9F RID: 3231 RVA: 0x000989A4 File Offset: 0x00096BA4
	private void Awake()
	{
		this.Init();
		UIController uicontroller = Object.FindObjectOfType<UIController>();
		if (uicontroller != null)
		{
			uicontroller.loadSlots.Add(this);
		}
	}

	// Token: 0x06000CA0 RID: 3232 RVA: 0x0000E727 File Offset: 0x0000C927
	public void Init()
	{
		this.loadSlotData();
	}

	// Token: 0x06000CA1 RID: 3233 RVA: 0x000989D4 File Offset: 0x00096BD4
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

	// Token: 0x06000CA2 RID: 3234 RVA: 0x00098ADC File Offset: 0x00096CDC
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

	// Token: 0x040009D1 RID: 2513
	[Header("Slot specific Variables")]
	[Tooltip("if true, this Slot will become the quick save slot!")]
	public bool quickSaveSlot;

	// Token: 0x040009D2 RID: 2514
	[Tooltip("Unique Slot ID used to identify while loading")]
	public int slotId;

	// Token: 0x040009D3 RID: 2515
	[Tooltip("This Slot's name field")]
	public Text saveName_text;

	// Token: 0x040009D4 RID: 2516
	[Tooltip("This Slot's percentage field")]
	public Text savePercentage_text;

	// Token: 0x040009D5 RID: 2517
	[Tooltip("Scene to load")]
	public string sceneToLoad;
}
