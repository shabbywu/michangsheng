using UnityEngine;

public class LoadGameManager : MonoBehaviour
{
	[Header("Player")]
	[Tooltip("Automatically finds the Player with tag 'Player'")]
	public Transform player;

	[Space(10f)]
	[Header("Load Variables")]
	[Tooltip("From which slot the game has been loaded")]
	public int loadedSlotId;

	[Tooltip("From which save trigger the game has been saved")]
	public int saveTriggerId;

	[Tooltip("Array of all save triggers present")]
	public SaveGameTrigger[] allTriggers;

	private string saveName;

	private void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player").transform;
		loadedSlotId = PlayerPrefs.GetInt("slotLoaded_");
		allTriggersPresent();
		hasLoadedGame();
		((Component)this).GetComponent<UIController>().hideMenus();
	}

	private void allTriggersPresent()
	{
		allTriggers = Object.FindObjectsOfType<SaveGameTrigger>();
	}

	private void hasLoadedGame()
	{
		if (loadedSlotId == 0)
		{
			return;
		}
		saveTriggerId = PlayerPrefs.GetInt("saveTriggerId_" + loadedSlotId);
		for (int i = 0; i < allTriggers.Length; i++)
		{
			if (allTriggers[i].saveTriggerId == saveTriggerId)
			{
				spawnPlayerAtPoint(allTriggers[i].spawnPoint);
				saveName = allTriggers[i].saveName + loadedSlotId;
			}
		}
	}

	public void spawnPlayerAtPoint(Transform spawnPoint)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		player.position = spawnPoint.position;
	}

	public string retSaveName()
	{
		return saveName;
	}
}
