using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Token: 0x0200017C RID: 380
public class SaveGameUI : MonoBehaviour
{
	// Token: 0x06000CC4 RID: 3268 RVA: 0x0000E8FD File Offset: 0x0000CAFD
	private IEnumerator Start()
	{
		yield return new WaitForSeconds(0.1f);
		this.loadQuickSaveData();
		yield break;
	}

	// Token: 0x06000CC5 RID: 3269 RVA: 0x0000E90C File Offset: 0x0000CB0C
	private void Update()
	{
		if (Input.GetKeyDown(this.key))
		{
			this.SaveData_quickSlot();
		}
	}

	// Token: 0x06000CC6 RID: 3270 RVA: 0x0000E921 File Offset: 0x0000CB21
	public void quickSaveSlotData(Text saveName, Text savePercentage, int slotID)
	{
		this.QsaveName_text = saveName;
		this.QsavePercentage_text = savePercentage;
		this.QslotId = slotID;
	}

	// Token: 0x06000CC7 RID: 3271 RVA: 0x0000E938 File Offset: 0x0000CB38
	public void openConfirmation()
	{
		base.GetComponent<Animator>().Play("confirmUI_open");
		this.playClickSound();
	}

	// Token: 0x06000CC8 RID: 3272 RVA: 0x0000E950 File Offset: 0x0000CB50
	public void confirmation_yes()
	{
		this.saveData();
		base.GetComponent<Animator>().Play("confirmUI_close");
		this.playClickSound();
	}

	// Token: 0x06000CC9 RID: 3273 RVA: 0x0000E96E File Offset: 0x0000CB6E
	public void confirmation_no()
	{
		base.GetComponent<Animator>().Play("confirmUI_close");
		this.playClickSound();
	}

	// Token: 0x06000CCA RID: 3274 RVA: 0x00098FEC File Offset: 0x000971EC
	public void closeSaveUI()
	{
		for (int i = 0; i < this.allUI.Length; i++)
		{
			this.allUI[i].gameObject.SetActive(true);
		}
		Time.timeScale = 1f;
		base.GetComponent<Animator>().Play("saveGameUI_close");
		base.Invoke("disableUI", 0.2f);
		this.playClickSound();
	}

	// Token: 0x06000CCB RID: 3275 RVA: 0x0000E986 File Offset: 0x0000CB86
	private void disableUI()
	{
		base.GetComponent<UIController>().hideMenus();
	}

	// Token: 0x06000CCC RID: 3276 RVA: 0x00099050 File Offset: 0x00097250
	private void saveData()
	{
		this.saveName_text.text = this.saveName;
		this.savePercentage_text.text = this.savePercentage + "%";
		PlayerPrefs.SetInt("slot_" + this.slotId, this.slotId);
		PlayerPrefs.SetString("slot_saveName_" + this.slotId, this.saveName);
		PlayerPrefs.SetFloat("slot_savePercentage_" + this.slotId, this.savePercentage);
		PlayerPrefs.SetInt("saveTriggerId_" + this.slotId, this.saveTriggerId);
		PlayerPrefs.SetString("slot_sceneName_" + this.slotId, this.sceneName);
	}

	// Token: 0x06000CCD RID: 3277 RVA: 0x00099130 File Offset: 0x00097330
	private void SaveData_quickSlot()
	{
		this.QsaveName_text.text = "Quicksave : " + SceneManager.GetActiveScene().name;
		this.QsavePercentage_text.text = "";
		PlayerPrefs.SetInt("QuickSaveDataIsPresent", 1);
		PlayerPrefs.SetInt("slot_" + this.QslotId, this.QslotId);
		PlayerPrefs.SetString("slot_saveName_" + this.QslotId, this.QsaveName_text.text);
		PlayerPrefs.SetString("slot_sceneName_" + this.QslotId, SceneManager.GetActiveScene().name);
		this.SavePositions();
		Animator component = GameObject.Find("SaveText").GetComponent<Animator>();
		if (!component.IsInTransition(0))
		{
			component.Play("SaveTextHUD");
		}
	}

	// Token: 0x06000CCE RID: 3278 RVA: 0x00099210 File Offset: 0x00097410
	private void SavePositions()
	{
		Transform transform = GameObject.FindGameObjectWithTag("Player").transform;
		float x = transform.position.x;
		float y = transform.position.y;
		float z = transform.position.z;
		PlayerPrefs.SetFloat("player.position.x", x);
		PlayerPrefs.SetFloat("player.position.y", y);
		PlayerPrefs.SetFloat("player.position.z", z);
	}

	// Token: 0x06000CCF RID: 3279 RVA: 0x0000E993 File Offset: 0x0000CB93
	private void loadQuickSaveData()
	{
		if (PlayerPrefs.GetInt("slotLoaded_") == this.QslotId && this.QslotId != 0)
		{
			this.LoadnSetPosition();
		}
	}

	// Token: 0x06000CD0 RID: 3280 RVA: 0x00099270 File Offset: 0x00097470
	private void LoadnSetPosition()
	{
		float @float = PlayerPrefs.GetFloat("player.position.x");
		float float2 = PlayerPrefs.GetFloat("player.position.y");
		float float3 = PlayerPrefs.GetFloat("player.position.z");
		Vector3 position;
		position..ctor(@float, float2, float3);
		GameObject.FindGameObjectWithTag("Player").transform.position = position;
	}

	// Token: 0x06000CD1 RID: 3281 RVA: 0x0000E9B5 File Offset: 0x0000CBB5
	public void playHoverClip()
	{
		EasyAudioUtility.instance.Play("Hover");
	}

	// Token: 0x06000CD2 RID: 3282 RVA: 0x0000E9C6 File Offset: 0x0000CBC6
	public void playClickSound()
	{
		EasyAudioUtility.instance.Play("Click");
	}

	// Token: 0x040009F4 RID: 2548
	[Header("Quick Save Key")]
	public KeyCode key;

	// Token: 0x040009F5 RID: 2549
	[Header("Save Trigger Key")]
	public KeyCode SaveKey = 307;

	// Token: 0x040009F6 RID: 2550
	[Header("Save Trigger Data")]
	[Tooltip("No Need to Assign")]
	public string saveName;

	// Token: 0x040009F7 RID: 2551
	[Tooltip("No Need to Assign")]
	public float savePercentage;

	// Token: 0x040009F8 RID: 2552
	[Header("Save Slots UI")]
	[Tooltip("No Need to Assign")]
	public Text saveName_text;

	// Token: 0x040009F9 RID: 2553
	[Tooltip("No Need to Assign")]
	public Text savePercentage_text;

	// Token: 0x040009FA RID: 2554
	[Header("Slot Specific Data ")]
	[Tooltip("ID WILL BE ASSIGNED AUTOMATICALLY")]
	public int slotId;

	// Token: 0x040009FB RID: 2555
	[Tooltip("No Need to Assign")]
	public int saveTriggerId;

	// Token: 0x040009FC RID: 2556
	[Tooltip("No Need to Assign")]
	public string sceneName;

	// Token: 0x040009FD RID: 2557
	[HideInInspector]
	public Canvas[] allUI;

	// Token: 0x040009FE RID: 2558
	public Text QsaveName_text;

	// Token: 0x040009FF RID: 2559
	public Text QsavePercentage_text;

	// Token: 0x04000A00 RID: 2560
	public int QslotId;
}
