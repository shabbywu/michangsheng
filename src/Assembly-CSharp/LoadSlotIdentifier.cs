using UnityEngine;
using UnityEngine.UI;

public class LoadSlotIdentifier : MonoBehaviour
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

	[Tooltip("Scene to load")]
	public string sceneToLoad;

	private void Awake()
	{
		Init();
		UIController uIController = Object.FindObjectOfType<UIController>();
		if ((Object)(object)uIController != (Object)null)
		{
			uIController.loadSlots.Add(this);
		}
	}

	public void Init()
	{
		loadSlotData();
	}

	private void loadSlotData()
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		quickSaveSlot = ((PlayerPrefs.GetInt("quickSaveSlot") == slotId) ? true : false);
		if (quickSaveSlot)
		{
			Color red = Color.red;
			red.a = 0.25f;
			((Graphic)((Component)this).GetComponent<Image>()).color = red;
		}
		if (PlayerPrefs.GetInt("slot_" + slotId) == slotId)
		{
			saveName_text.text = PlayerPrefs.GetString("slot_saveName_" + slotId);
			sceneToLoad = PlayerPrefs.GetString("slot_sceneName_" + slotId);
			if (!quickSaveSlot)
			{
				savePercentage_text.text = PlayerPrefs.GetFloat("slot_savePercentage_" + slotId) + "%";
			}
			else
			{
				savePercentage_text.text = "";
			}
		}
	}

	public void LoadSceneSaved()
	{
		if (sceneToLoad != "")
		{
			Time.timeScale = 1f;
			GameObject val = GameObject.FindGameObjectWithTag("Player");
			if (Object.op_Implicit((Object)(object)val))
			{
				Object.Destroy((Object)(object)val);
			}
			PlayerPrefs.SetInt("slotLoaded_", slotId);
			PlayerPrefs.SetString("sceneToLoad", sceneToLoad);
			Object.FindObjectOfType<Fader>().FadeIntoLevel("LoadingScreen");
		}
	}
}
