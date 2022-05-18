using System;
using UnityEngine;

// Token: 0x0200017A RID: 378
public class LoadGameManager : MonoBehaviour
{
	// Token: 0x06000CB9 RID: 3257 RVA: 0x0000E853 File Offset: 0x0000CA53
	private void Start()
	{
		this.player = GameObject.FindGameObjectWithTag("Player").transform;
		this.loadedSlotId = PlayerPrefs.GetInt("slotLoaded_");
		this.allTriggersPresent();
		this.hasLoadedGame();
		base.GetComponent<UIController>().hideMenus();
	}

	// Token: 0x06000CBA RID: 3258 RVA: 0x0000E891 File Offset: 0x0000CA91
	private void allTriggersPresent()
	{
		this.allTriggers = Object.FindObjectsOfType<SaveGameTrigger>();
	}

	// Token: 0x06000CBB RID: 3259 RVA: 0x00098E2C File Offset: 0x0009702C
	private void hasLoadedGame()
	{
		if (this.loadedSlotId != 0)
		{
			this.saveTriggerId = PlayerPrefs.GetInt("saveTriggerId_" + this.loadedSlotId);
			for (int i = 0; i < this.allTriggers.Length; i++)
			{
				if (this.allTriggers[i].saveTriggerId == this.saveTriggerId)
				{
					this.spawnPlayerAtPoint(this.allTriggers[i].spawnPoint);
					this.saveName = this.allTriggers[i].saveName + this.loadedSlotId;
				}
			}
		}
	}

	// Token: 0x06000CBC RID: 3260 RVA: 0x0000E89E File Offset: 0x0000CA9E
	public void spawnPlayerAtPoint(Transform spawnPoint)
	{
		this.player.position = spawnPoint.position;
	}

	// Token: 0x06000CBD RID: 3261 RVA: 0x0000E8B1 File Offset: 0x0000CAB1
	public string retSaveName()
	{
		return this.saveName;
	}

	// Token: 0x040009E4 RID: 2532
	[Header("Player")]
	[Tooltip("Automatically finds the Player with tag 'Player'")]
	public Transform player;

	// Token: 0x040009E5 RID: 2533
	[Space(10f)]
	[Header("Load Variables")]
	[Tooltip("From which slot the game has been loaded")]
	public int loadedSlotId;

	// Token: 0x040009E6 RID: 2534
	[Tooltip("From which save trigger the game has been saved")]
	public int saveTriggerId;

	// Token: 0x040009E7 RID: 2535
	[Tooltip("Array of all save triggers present")]
	public SaveGameTrigger[] allTriggers;

	// Token: 0x040009E8 RID: 2536
	private string saveName;
}
