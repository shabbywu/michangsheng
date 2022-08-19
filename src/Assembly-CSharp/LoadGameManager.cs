using System;
using UnityEngine;

// Token: 0x020000FD RID: 253
public class LoadGameManager : MonoBehaviour
{
	// Token: 0x06000BB4 RID: 2996 RVA: 0x0004741F File Offset: 0x0004561F
	private void Start()
	{
		this.player = GameObject.FindGameObjectWithTag("Player").transform;
		this.loadedSlotId = PlayerPrefs.GetInt("slotLoaded_");
		this.allTriggersPresent();
		this.hasLoadedGame();
		base.GetComponent<UIController>().hideMenus();
	}

	// Token: 0x06000BB5 RID: 2997 RVA: 0x0004745D File Offset: 0x0004565D
	private void allTriggersPresent()
	{
		this.allTriggers = Object.FindObjectsOfType<SaveGameTrigger>();
	}

	// Token: 0x06000BB6 RID: 2998 RVA: 0x0004746C File Offset: 0x0004566C
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

	// Token: 0x06000BB7 RID: 2999 RVA: 0x000474FF File Offset: 0x000456FF
	public void spawnPlayerAtPoint(Transform spawnPoint)
	{
		this.player.position = spawnPoint.position;
	}

	// Token: 0x06000BB8 RID: 3000 RVA: 0x00047512 File Offset: 0x00045712
	public string retSaveName()
	{
		return this.saveName;
	}

	// Token: 0x040007F8 RID: 2040
	[Header("Player")]
	[Tooltip("Automatically finds the Player with tag 'Player'")]
	public Transform player;

	// Token: 0x040007F9 RID: 2041
	[Space(10f)]
	[Header("Load Variables")]
	[Tooltip("From which slot the game has been loaded")]
	public int loadedSlotId;

	// Token: 0x040007FA RID: 2042
	[Tooltip("From which save trigger the game has been saved")]
	public int saveTriggerId;

	// Token: 0x040007FB RID: 2043
	[Tooltip("Array of all save triggers present")]
	public SaveGameTrigger[] allTriggers;

	// Token: 0x040007FC RID: 2044
	private string saveName;
}
