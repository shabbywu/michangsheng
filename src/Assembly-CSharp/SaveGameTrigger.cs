using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Token: 0x0200017B RID: 379
public class SaveGameTrigger : MonoBehaviour
{
	// Token: 0x06000CBF RID: 3263 RVA: 0x0000E8B9 File Offset: 0x0000CAB9
	private void Start()
	{
		this.saveUI = Object.FindObjectOfType<SaveGameUI>().gameObject;
		this.anim = this.saveUI.GetComponent<Animator>();
	}

	// Token: 0x06000CC0 RID: 3264 RVA: 0x00098EC0 File Offset: 0x000970C0
	private void OnTriggerStay(Collider col)
	{
		if (col.tag == "Player" && Input.GetKeyDown(this.saveUI.GetComponent<SaveGameUI>().SaveKey))
		{
			this.allUI = Object.FindObjectsOfType<Canvas>();
			this.saveUI.GetComponent<SaveGameUI>().allUI = this.allUI;
			for (int i = 0; i < this.allUI.Length; i++)
			{
				this.allUI[i].gameObject.SetActive(false);
			}
			Time.timeScale = 1E-07f;
			this.saveUI.SetActive(true);
			this.anim.Play("saveGameUI_open");
			this.sceneName = SceneManager.GetActiveScene().name;
			this.sendCurrentSavePointData();
		}
	}

	// Token: 0x06000CC1 RID: 3265 RVA: 0x00098F84 File Offset: 0x00097184
	private void sendCurrentSavePointData()
	{
		this.saveUI.GetComponent<SaveGameUI>().saveName = this.saveName;
		this.saveUI.GetComponent<SaveGameUI>().savePercentage = this.savePercentage;
		this.saveUI.GetComponent<SaveGameUI>().saveTriggerId = this.saveTriggerId;
		this.saveUI.GetComponent<SaveGameUI>().sceneName = this.sceneName;
	}

	// Token: 0x06000CC2 RID: 3266 RVA: 0x0000E8DC File Offset: 0x0000CADC
	private void Update()
	{
		if (this.debugSpawn)
		{
			Object.FindObjectOfType<LoadGameManager>().spawnPlayerAtPoint(this.spawnPoint);
			this.debugSpawn = false;
		}
	}

	// Token: 0x040009E9 RID: 2537
	private Animator anim;

	// Token: 0x040009EA RID: 2538
	[Header("All other UI")]
	[Tooltip("No Need to Assign")]
	public Canvas[] allUI;

	// Token: 0x040009EB RID: 2539
	[Header("References to UI")]
	[Tooltip("No Need to Assign")]
	public GameObject saveUI;

	// Token: 0x040009EC RID: 2540
	[Tooltip("No Need to Assign")]
	public Text saveName_Txt;

	// Token: 0x040009ED RID: 2541
	[Tooltip("No Need to Assign")]
	public Text savePercentage_Txt;

	// Token: 0x040009EE RID: 2542
	[Header("Edit these in Inspector According to your level")]
	[Tooltip("Save Name which will be displayed in respective Save Slots")]
	public string saveName;

	// Token: 0x040009EF RID: 2543
	[Tooltip("Game Completion percentage displayed in respective Save Slots")]
	public float savePercentage;

	// Token: 0x040009F0 RID: 2544
	[Tooltip("Scene to Load")]
	public string sceneName;

	// Token: 0x040009F1 RID: 2545
	[Tooltip("Player will spawn from this point when level loads")]
	public Transform spawnPoint;

	// Token: 0x040009F2 RID: 2546
	[Tooltip("Unique trigger ID to see is this the last save point")]
	public int saveTriggerId;

	// Token: 0x040009F3 RID: 2547
	[Tooltip("Debug to spawn player at this Trigger's spawnPoint")]
	public bool debugSpawn;
}
