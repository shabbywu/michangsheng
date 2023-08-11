using System.Collections.Generic;
using UnityEngine;

namespace Fungus;

[EventHandlerInfo("Scene", "Save Point Loaded", "Execute this block when a saved point is loaded. Use the 'new_game' key to handle game start.")]
public class SavePointLoaded : EventHandler
{
	[Tooltip("Block will execute if the Save Key of the loaded save point matches this save key.")]
	[SerializeField]
	protected List<string> savePointKeys = new List<string>();

	protected void OnSavePointLoaded(string _savePointKey)
	{
		for (int i = 0; i < savePointKeys.Count; i++)
		{
			if (string.Compare(savePointKeys[i], _savePointKey, ignoreCase: true) == 0)
			{
				ExecuteBlock();
				break;
			}
		}
	}

	public static void NotifyEventHandlers(string _savePointKey)
	{
		SavePointLoaded[] array = Object.FindObjectsOfType<SavePointLoaded>();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].OnSavePointLoaded(_savePointKey);
		}
	}
}
