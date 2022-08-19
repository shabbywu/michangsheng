using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Token: 0x020000FE RID: 254
public class SaveGameTrigger : MonoBehaviour
{
	// Token: 0x06000BBA RID: 3002 RVA: 0x0004751A File Offset: 0x0004571A
	private void Start()
	{
		this.saveUI = Object.FindObjectOfType<SaveGameUI>().gameObject;
		this.anim = this.saveUI.GetComponent<Animator>();
	}

	// Token: 0x06000BBB RID: 3003 RVA: 0x00047540 File Offset: 0x00045740
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

	// Token: 0x06000BBC RID: 3004 RVA: 0x00047604 File Offset: 0x00045804
	private void sendCurrentSavePointData()
	{
		this.saveUI.GetComponent<SaveGameUI>().saveName = this.saveName;
		this.saveUI.GetComponent<SaveGameUI>().savePercentage = this.savePercentage;
		this.saveUI.GetComponent<SaveGameUI>().saveTriggerId = this.saveTriggerId;
		this.saveUI.GetComponent<SaveGameUI>().sceneName = this.sceneName;
	}

	// Token: 0x06000BBD RID: 3005 RVA: 0x00047669 File Offset: 0x00045869
	private void Update()
	{
		if (this.debugSpawn)
		{
			Object.FindObjectOfType<LoadGameManager>().spawnPlayerAtPoint(this.spawnPoint);
			this.debugSpawn = false;
		}
	}

	// Token: 0x040007FD RID: 2045
	private Animator anim;

	// Token: 0x040007FE RID: 2046
	[Header("All other UI")]
	[Tooltip("No Need to Assign")]
	public Canvas[] allUI;

	// Token: 0x040007FF RID: 2047
	[Header("References to UI")]
	[Tooltip("No Need to Assign")]
	public GameObject saveUI;

	// Token: 0x04000800 RID: 2048
	[Tooltip("No Need to Assign")]
	public Text saveName_Txt;

	// Token: 0x04000801 RID: 2049
	[Tooltip("No Need to Assign")]
	public Text savePercentage_Txt;

	// Token: 0x04000802 RID: 2050
	[Header("Edit these in Inspector According to your level")]
	[Tooltip("Save Name which will be displayed in respective Save Slots")]
	public string saveName;

	// Token: 0x04000803 RID: 2051
	[Tooltip("Game Completion percentage displayed in respective Save Slots")]
	public float savePercentage;

	// Token: 0x04000804 RID: 2052
	[Tooltip("Scene to Load")]
	public string sceneName;

	// Token: 0x04000805 RID: 2053
	[Tooltip("Player will spawn from this point when level loads")]
	public Transform spawnPoint;

	// Token: 0x04000806 RID: 2054
	[Tooltip("Unique trigger ID to see is this the last save point")]
	public int saveTriggerId;

	// Token: 0x04000807 RID: 2055
	[Tooltip("Debug to spawn player at this Trigger's spawnPoint")]
	public bool debugSpawn;
}
