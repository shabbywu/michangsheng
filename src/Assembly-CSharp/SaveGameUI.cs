using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveGameUI : MonoBehaviour
{
	[Header("Quick Save Key")]
	public KeyCode key;

	[Header("Save Trigger Key")]
	public KeyCode SaveKey = (KeyCode)307;

	[Header("Save Trigger Data")]
	[Tooltip("No Need to Assign")]
	public string saveName;

	[Tooltip("No Need to Assign")]
	public float savePercentage;

	[Header("Save Slots UI")]
	[Tooltip("No Need to Assign")]
	public Text saveName_text;

	[Tooltip("No Need to Assign")]
	public Text savePercentage_text;

	[Header("Slot Specific Data ")]
	[Tooltip("ID WILL BE ASSIGNED AUTOMATICALLY")]
	public int slotId;

	[Tooltip("No Need to Assign")]
	public int saveTriggerId;

	[Tooltip("No Need to Assign")]
	public string sceneName;

	[HideInInspector]
	public Canvas[] allUI;

	public Text QsaveName_text;

	public Text QsavePercentage_text;

	public int QslotId;

	private IEnumerator Start()
	{
		yield return (object)new WaitForSeconds(0.1f);
		loadQuickSaveData();
	}

	private void Update()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		if (Input.GetKeyDown(key))
		{
			SaveData_quickSlot();
		}
	}

	public void quickSaveSlotData(Text saveName, Text savePercentage, int slotID)
	{
		QsaveName_text = saveName;
		QsavePercentage_text = savePercentage;
		QslotId = slotID;
	}

	public void openConfirmation()
	{
		((Component)this).GetComponent<Animator>().Play("confirmUI_open");
		playClickSound();
	}

	public void confirmation_yes()
	{
		saveData();
		((Component)this).GetComponent<Animator>().Play("confirmUI_close");
		playClickSound();
	}

	public void confirmation_no()
	{
		((Component)this).GetComponent<Animator>().Play("confirmUI_close");
		playClickSound();
	}

	public void closeSaveUI()
	{
		for (int i = 0; i < allUI.Length; i++)
		{
			((Component)allUI[i]).gameObject.SetActive(true);
		}
		Time.timeScale = 1f;
		((Component)this).GetComponent<Animator>().Play("saveGameUI_close");
		((MonoBehaviour)this).Invoke("disableUI", 0.2f);
		playClickSound();
	}

	private void disableUI()
	{
		((Component)this).GetComponent<UIController>().hideMenus();
	}

	private void saveData()
	{
		saveName_text.text = saveName;
		savePercentage_text.text = savePercentage + "%";
		PlayerPrefs.SetInt("slot_" + slotId, slotId);
		PlayerPrefs.SetString("slot_saveName_" + slotId, saveName);
		PlayerPrefs.SetFloat("slot_savePercentage_" + slotId, savePercentage);
		PlayerPrefs.SetInt("saveTriggerId_" + slotId, saveTriggerId);
		PlayerPrefs.SetString("slot_sceneName_" + slotId, sceneName);
	}

	private void SaveData_quickSlot()
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		Text qsaveName_text = QsaveName_text;
		Scene activeScene = SceneManager.GetActiveScene();
		qsaveName_text.text = "Quicksave : " + ((Scene)(ref activeScene)).name;
		QsavePercentage_text.text = "";
		PlayerPrefs.SetInt("QuickSaveDataIsPresent", 1);
		PlayerPrefs.SetInt("slot_" + QslotId, QslotId);
		PlayerPrefs.SetString("slot_saveName_" + QslotId, QsaveName_text.text);
		string text = "slot_sceneName_" + QslotId;
		activeScene = SceneManager.GetActiveScene();
		PlayerPrefs.SetString(text, ((Scene)(ref activeScene)).name);
		SavePositions();
		Animator component = GameObject.Find("SaveText").GetComponent<Animator>();
		if (!component.IsInTransition(0))
		{
			component.Play("SaveTextHUD");
		}
	}

	private void SavePositions()
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		Transform transform = GameObject.FindGameObjectWithTag("Player").transform;
		float x = transform.position.x;
		float y = transform.position.y;
		float z = transform.position.z;
		PlayerPrefs.SetFloat("player.position.x", x);
		PlayerPrefs.SetFloat("player.position.y", y);
		PlayerPrefs.SetFloat("player.position.z", z);
	}

	private void loadQuickSaveData()
	{
		if (PlayerPrefs.GetInt("slotLoaded_") == QslotId && QslotId != 0)
		{
			LoadnSetPosition();
		}
	}

	private void LoadnSetPosition()
	{
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		float @float = PlayerPrefs.GetFloat("player.position.x");
		float float2 = PlayerPrefs.GetFloat("player.position.y");
		float float3 = PlayerPrefs.GetFloat("player.position.z");
		Vector3 position = default(Vector3);
		((Vector3)(ref position))._002Ector(@float, float2, float3);
		GameObject.FindGameObjectWithTag("Player").transform.position = position;
	}

	public void playHoverClip()
	{
		EasyAudioUtility.instance.Play("Hover");
	}

	public void playClickSound()
	{
		EasyAudioUtility.instance.Play("Click");
	}
}
