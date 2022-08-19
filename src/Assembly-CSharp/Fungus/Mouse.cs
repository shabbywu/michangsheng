using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000EA7 RID: 3751
	[EventHandlerInfo("MonoBehaviour", "Mouse", "The block will execute when the desired OnMouse* message for the monobehaviour is received")]
	[AddComponentMenu("")]
	public class Mouse : EventHandler
	{
		// Token: 0x06006A23 RID: 27171 RVA: 0x00292ACF File Offset: 0x00290CCF
		private void OnMouseDown()
		{
			this.HandleTriggering(Mouse.MouseMessageFlags.OnMouseDown);
		}

		// Token: 0x06006A24 RID: 27172 RVA: 0x00292AD8 File Offset: 0x00290CD8
		private void OnMouseDrag()
		{
			this.HandleTriggering(Mouse.MouseMessageFlags.OnMouseDrag);
		}

		// Token: 0x06006A25 RID: 27173 RVA: 0x00292AE1 File Offset: 0x00290CE1
		private void OnMouseEnter()
		{
			this.HandleTriggering(Mouse.MouseMessageFlags.OnMouseEnter);
		}

		// Token: 0x06006A26 RID: 27174 RVA: 0x00292AEA File Offset: 0x00290CEA
		private void OnMouseExit()
		{
			this.HandleTriggering(Mouse.MouseMessageFlags.OnMouseExit);
		}

		// Token: 0x06006A27 RID: 27175 RVA: 0x00292AF3 File Offset: 0x00290CF3
		private void OnMouseOver()
		{
			this.HandleTriggering(Mouse.MouseMessageFlags.OnMouseOver);
		}

		// Token: 0x06006A28 RID: 27176 RVA: 0x00292AFD File Offset: 0x00290CFD
		private void OnMouseUp()
		{
			this.HandleTriggering(Mouse.MouseMessageFlags.OnMouseUp);
		}

		// Token: 0x06006A29 RID: 27177 RVA: 0x00292B07 File Offset: 0x00290D07
		private void OnMouseUpAsButton()
		{
			this.HandleTriggering(Mouse.MouseMessageFlags.OnMouseUpAsButton);
		}

		// Token: 0x06006A2A RID: 27178 RVA: 0x00292B11 File Offset: 0x00290D11
		private void HandleTriggering(Mouse.MouseMessageFlags from)
		{
			if ((from & this.FireOn) != (Mouse.MouseMessageFlags)0)
			{
				this.ExecuteBlock();
			}
		}

		// Token: 0x040059DD RID: 23005
		[Tooltip("Which of the Mouse messages to trigger on.")]
		[SerializeField]
		[EnumFlag]
		protected Mouse.MouseMessageFlags FireOn = Mouse.MouseMessageFlags.OnMouseUpAsButton;

		// Token: 0x020016F6 RID: 5878
		[Flags]
		public enum MouseMessageFlags
		{
			// Token: 0x04007483 RID: 29827
			OnMouseDown = 1,
			// Token: 0x04007484 RID: 29828
			OnMouseDrag = 2,
			// Token: 0x04007485 RID: 29829
			OnMouseEnter = 4,
			// Token: 0x04007486 RID: 29830
			OnMouseExit = 8,
			// Token: 0x04007487 RID: 29831
			OnMouseOver = 16,
			// Token: 0x04007488 RID: 29832
			OnMouseUp = 32,
			// Token: 0x04007489 RID: 29833
			OnMouseUpAsButton = 64
		}
	}
}
