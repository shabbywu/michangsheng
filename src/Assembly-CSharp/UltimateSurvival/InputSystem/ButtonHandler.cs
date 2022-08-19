using System;
using UnityEngine;

namespace UltimateSurvival.InputSystem
{
	// Token: 0x02000633 RID: 1587
	public class ButtonHandler : MonoBehaviour
	{
		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x06003259 RID: 12889 RVA: 0x001653FE File Offset: 0x001635FE
		// (set) Token: 0x0600325A RID: 12890 RVA: 0x00165406 File Offset: 0x00163606
		public ET.ButtonState State { get; private set; }

		// Token: 0x1400003C RID: 60
		// (add) Token: 0x0600325B RID: 12891 RVA: 0x00165410 File Offset: 0x00163610
		// (remove) Token: 0x0600325C RID: 12892 RVA: 0x00165448 File Offset: 0x00163648
		public event Action OnButtonDown;

		// Token: 0x0600325D RID: 12893 RVA: 0x0016547D File Offset: 0x0016367D
		private void Start()
		{
			this.State = ET.ButtonState.Up;
		}

		// Token: 0x0600325E RID: 12894 RVA: 0x0016547D File Offset: 0x0016367D
		public void SetUpState()
		{
			this.State = ET.ButtonState.Up;
		}

		// Token: 0x0600325F RID: 12895 RVA: 0x00165486 File Offset: 0x00163686
		public void SetDownState()
		{
			this.State = ET.ButtonState.Down;
			if (this.OnButtonDown != null)
			{
				this.OnButtonDown();
			}
		}
	}
}
