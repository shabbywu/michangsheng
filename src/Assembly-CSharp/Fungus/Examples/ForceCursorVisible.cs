using System;
using UnityEngine;

namespace Fungus.Examples
{
	// Token: 0x02000FAD RID: 4013
	public class ForceCursorVisible : MonoBehaviour
	{
		// Token: 0x06006FE1 RID: 28641 RVA: 0x002A86EC File Offset: 0x002A68EC
		private void Update()
		{
			Cursor.visible = !this.CursorLocked;
			Cursor.lockState = (this.CursorLocked ? 1 : 0);
		}

		// Token: 0x04005C56 RID: 23638
		public bool CursorLocked = true;
	}
}
