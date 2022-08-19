using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Token: 0x020000FF RID: 255
public class SaveGameUI : MonoBehaviour
{
	// Token: 0x06000BBF RID: 3007 RVA: 0x0004768A File Offset: 0x0004588A
	private IEnumerator Start()
	{
		yield return new WaitForSeconds(0.1f);
		this.loadQuickSaveData();
		yield break;
	}

	// Token: 0x06000BC0 RID: 3008 RVA: 0x00047699 File Offset: 0x00045899
	private void Update()
	{
		if (Input.GetKeyDown(this.key))
		{
			this.SaveData_quickSlot();
		}
	}

	// Token: 0x06000BC1 RID: 3009 RVA: 0x000476AE File Offset: 0x000458AE
	public void quickSaveSlotData(Text saveName, Text savePercentage, int slotID)
	{
		this.QsaveName_text = saveName;
		this.QsavePercentage_text = savePercentage;
		this.QslotId = slotID;
	}

	// Token: 0x06000BC2 RID: 3010 RVA: 0x000476C5 File Offset: 0x000458C5
	public void openConfirmation()
	{
		base.GetComponent<Animator>().Play("confirmUI_open");
		this.playClickSound();
	}

	// Token: 0x06000BC3 RID: 3011 RVA: 0x000476DD File Offset: 0x000458DD
	public void confirmation_yes()
	{
		this.saveData();
		base.GetComponent<Animator>().Play("confirmUI_close");
		this.playClickSound();
	}

	// Token: 0x06000BC4 RID: 3012 RVA: 0x000476FB File Offset: 0x000458FB
	public void confirmation_no()
	{
		base.GetComponent<Animator>().Play("confirmUI_close");
		this.playClickSound();
	}

	// Token: 0x06000BC5 RID: 3013 RVA: 0x00047714 File Offset: 0x00045914
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

	// Token: 0x06000BC6 RID: 3014 RVA: 0x00047777 File Offset: 0x00045977
	private void disableUI()
	{
		base.GetComponent<UIController>().hideMenus();
	}

	// Token: 0x06000BC7 RID: 3015 RVA: 0x00047784 File Offset: 0x00045984
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

	// Token: 0x06000BC8 RID: 3016 RVA: 0x00047864 File Offset: 0x00045A64
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

	// Token: 0x06000BC9 RID: 3017 RVA: 0x00047944 File Offset: 0x00045B44
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

	// Token: 0x06000BCA RID: 3018 RVA: 0x000479A4 File Offset: 0x00045BA4
	private void loadQuickSaveData()
	{
		if (PlayerPrefs.GetInt("slotLoaded_") == this.QslotId && this.QslotId != 0)
		{
			this.LoadnSetPosition();
		}
	}

	// Token: 0x06000BCB RID: 3019 RVA: 0x000479C8 File Offset: 0x00045BC8
	private void LoadnSetPosition()
	{
		float @float = PlayerPrefs.GetFloat("player.position.x");
		float float2 = PlayerPrefs.GetFloat("player.position.y");
		float float3 = PlayerPrefs.GetFloat("player.position.z");
		Vector3 position;
		position..ctor(@float, float2, float3);
		GameObject.FindGameObjectWithTag("Player").transform.position = position;
	}

	// Token: 0x06000BCC RID: 3020 RVA: 0x00047A15 File Offset: 0x00045C15
	public void playHoverClip()
	{
		EasyAudioUtility.instance.Play("Hover");
	}

	// Token: 0x06000BCD RID: 3021 RVA: 0x00047A26 File Offset: 0x00045C26
	public void playClickSound()
	{
		EasyAudioUtility.instance.Play("Click");
	}

	// Token: 0x04000808 RID: 2056
	[Header("Quick Save Key")]
	public KeyCode key;

	// Token: 0x04000809 RID: 2057
	[Header("Save Trigger Key")]
	public KeyCode SaveKey = 307;

	// Token: 0x0400080A RID: 2058
	[Header("Save Trigger Data")]
	[Tooltip("No Need to Assign")]
	public string saveName;

	// Token: 0x0400080B RID: 2059
	[Tooltip("No Need to Assign")]
	public float savePercentage;

	// Token: 0x0400080C RID: 2060
	[Header("Save Slots UI")]
	[Tooltip("No Need to Assign")]
	public Text saveName_text;

	// Token: 0x0400080D RID: 2061
	[Tooltip("No Need to Assign")]
	public Text savePercentage_text;

	// Token: 0x0400080E RID: 2062
	[Header("Slot Specific Data ")]
	[Tooltip("ID WILL BE ASSIGNED AUTOMATICALLY")]
	public int slotId;

	// Token: 0x0400080F RID: 2063
	[Tooltip("No Need to Assign")]
	public int saveTriggerId;

	// Token: 0x04000810 RID: 2064
	[Tooltip("No Need to Assign")]
	public string sceneName;

	// Token: 0x04000811 RID: 2065
	[HideInInspector]
	public Canvas[] allUI;

	// Token: 0x04000812 RID: 2066
	public Text QsaveName_text;

	// Token: 0x04000813 RID: 2067
	public Text QsavePercentage_text;

	// Token: 0x04000814 RID: 2068
	public int QslotId;
}
