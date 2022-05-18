using System;
using System.Collections.Generic;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001339 RID: 4921
	[EventHandlerInfo("Scene", "Save Point Loaded", "Execute this block when a saved point is loaded. Use the 'new_game' key to handle game start.")]
	public class SavePointLoaded : EventHandler
	{
		// Token: 0x0600778D RID: 30605 RVA: 0x002B5200 File Offset: 0x002B3400
		protected void OnSavePointLoaded(string _savePointKey)
		{
			for (int i = 0; i < this.savePointKeys.Count; i++)
			{
				if (string.Compare(this.savePointKeys[i], _savePointKey, true) == 0)
				{
					this.ExecuteBlock();
					return;
				}
			}
		}

		// Token: 0x0600778E RID: 30606 RVA: 0x002B5240 File Offset: 0x002B3440
		public static void NotifyEventHandlers(string _savePointKey)
		{
			SavePointLoaded[] array = Object.FindObjectsOfType<SavePointLoaded>();
			for (int i = 0; i < array.Length; i++)
			{
				array[i].OnSavePointLoaded(_savePointKey);
			}
		}

		// Token: 0x0400682D RID: 26669
		[Tooltip("Block will execute if the Save Key of the loaded save point matches this save key.")]
		[SerializeField]
		protected List<string> savePointKeys = new List<string>();
	}
}
