using System;
using System.Collections.Generic;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000EB0 RID: 3760
	[EventHandlerInfo("Scene", "Save Point Loaded", "Execute this block when a saved point is loaded. Use the 'new_game' key to handle game start.")]
	public class SavePointLoaded : EventHandler
	{
		// Token: 0x06006A50 RID: 27216 RVA: 0x00292DA0 File Offset: 0x00290FA0
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

		// Token: 0x06006A51 RID: 27217 RVA: 0x00292DE0 File Offset: 0x00290FE0
		public static void NotifyEventHandlers(string _savePointKey)
		{
			SavePointLoaded[] array = Object.FindObjectsOfType<SavePointLoaded>();
			for (int i = 0; i < array.Length; i++)
			{
				array[i].OnSavePointLoaded(_savePointKey);
			}
		}

		// Token: 0x040059E6 RID: 23014
		[Tooltip("Block will execute if the Save Key of the loaded save point matches this save key.")]
		[SerializeField]
		protected List<string> savePointKeys = new List<string>();
	}
}
