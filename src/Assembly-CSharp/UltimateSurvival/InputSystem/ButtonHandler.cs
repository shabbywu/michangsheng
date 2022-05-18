using System;
using UnityEngine;

namespace UltimateSurvival.InputSystem
{
	// Token: 0x02000925 RID: 2341
	public class ButtonHandler : MonoBehaviour
	{
		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x06003B93 RID: 15251 RVA: 0x0002B155 File Offset: 0x00029355
		// (set) Token: 0x06003B94 RID: 15252 RVA: 0x0002B15D File Offset: 0x0002935D
		public ET.ButtonState State { get; private set; }

		// Token: 0x1400003C RID: 60
		// (add) Token: 0x06003B95 RID: 15253 RVA: 0x001AEA8C File Offset: 0x001ACC8C
		// (remove) Token: 0x06003B96 RID: 15254 RVA: 0x001AEAC4 File Offset: 0x001ACCC4
		public event Action OnButtonDown;

		// Token: 0x06003B97 RID: 15255 RVA: 0x0002B166 File Offset: 0x00029366
		private void Start()
		{
			this.State = ET.ButtonState.Up;
		}

		// Token: 0x06003B98 RID: 15256 RVA: 0x0002B166 File Offset: 0x00029366
		public void SetUpState()
		{
			this.State = ET.ButtonState.Up;
		}

		// Token: 0x06003B99 RID: 15257 RVA: 0x0002B16F File Offset: 0x0002936F
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
