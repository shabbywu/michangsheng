using System;
using UnityEngine;

namespace Fungus.Examples
{
	// Token: 0x02001465 RID: 5221
	public class ForceCursorVisible : MonoBehaviour
	{
		// Token: 0x06007DDB RID: 32219 RVA: 0x00055126 File Offset: 0x00053326
		private void Update()
		{
			Cursor.visible = !this.CursorLocked;
			Cursor.lockState = (this.CursorLocked ? 1 : 0);
		}

		// Token: 0x04006B4E RID: 27470
		public bool CursorLocked = true;
	}
}
