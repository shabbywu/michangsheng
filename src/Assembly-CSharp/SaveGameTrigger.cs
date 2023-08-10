using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveGameTrigger : MonoBehaviour
{
	private Animator anim;

	[Header("All other UI")]
	[Tooltip("No Need to Assign")]
	public Canvas[] allUI;

	[Header("References to UI")]
	[Tooltip("No Need to Assign")]
	public GameObject saveUI;

	[Tooltip("No Need to Assign")]
	public Text saveName_Txt;

	[Tooltip("No Need to Assign")]
	public Text savePercentage_Txt;

	[Header("Edit these in Inspector According to your level")]
	[Tooltip("Save Name which will be displayed in respective Save Slots")]
	public string saveName;

	[Tooltip("Game Completion percentage displayed in respective Save Slots")]
	public float savePercentage;

	[Tooltip("Scene to Load")]
	public string sceneName;

	[Tooltip("Player will spawn from this point when level loads")]
	public Transform spawnPoint;

	[Tooltip("Unique trigger ID to see is this the last save point")]
	public int saveTriggerId;

	[Tooltip("Debug to spawn player at this Trigger's spawnPoint")]
	public bool debugSpawn;

	private void Start()
	{
		saveUI = ((Component)Object.FindObjectOfType<SaveGameUI>()).gameObject;
		anim = saveUI.GetComponent<Animator>();
	}

	private void OnTriggerStay(Collider col)
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		if (((Component)col).tag == "Player" && Input.GetKeyDown(saveUI.GetComponent<SaveGameUI>().SaveKey))
		{
			allUI = Object.FindObjectsOfType<Canvas>();
			saveUI.GetComponent<SaveGameUI>().allUI = allUI;
			for (int i = 0; i < allUI.Length; i++)
			{
				((Component)allUI[i]).gameObject.SetActive(false);
			}
			Time.timeScale = 1E-07f;
			saveUI.SetActive(true);
			anim.Play("saveGameUI_open");
			Scene activeScene = SceneManager.GetActiveScene();
			sceneName = ((Scene)(ref activeScene)).name;
			sendCurrentSavePointData();
		}
	}

	private void sendCurrentSavePointData()
	{
		saveUI.GetComponent<SaveGameUI>().saveName = saveName;
		saveUI.GetComponent<SaveGameUI>().savePercentage = savePercentage;
		saveUI.GetComponent<SaveGameUI>().saveTriggerId = saveTriggerId;
		saveUI.GetComponent<SaveGameUI>().sceneName = sceneName;
	}

	private void Update()
	{
		if (debugSpawn)
		{
			Object.FindObjectOfType<LoadGameManager>().spawnPlayerAtPoint(spawnPoint);
			debugSpawn = false;
		}
	}
}
