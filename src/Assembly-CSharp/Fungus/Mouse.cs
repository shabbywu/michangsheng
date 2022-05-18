using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001329 RID: 4905
	[EventHandlerInfo("MonoBehaviour", "Mouse", "The block will execute when the desired OnMouse* message for the monobehaviour is received")]
	[AddComponentMenu("")]
	public class Mouse : EventHandler
	{
		// Token: 0x06007759 RID: 30553 RVA: 0x000515A7 File Offset: 0x0004F7A7
		private void OnMouseDown()
		{
			this.HandleTriggering(Mouse.MouseMessageFlags.OnMouseDown);
		}

		// Token: 0x0600775A RID: 30554 RVA: 0x000515B0 File Offset: 0x0004F7B0
		private void OnMouseDrag()
		{
			this.HandleTriggering(Mouse.MouseMessageFlags.OnMouseDrag);
		}

		// Token: 0x0600775B RID: 30555 RVA: 0x000515B9 File Offset: 0x0004F7B9
		private void OnMouseEnter()
		{
			this.HandleTriggering(Mouse.MouseMessageFlags.OnMouseEnter);
		}

		// Token: 0x0600775C RID: 30556 RVA: 0x000515C2 File Offset: 0x0004F7C2
		private void OnMouseExit()
		{
			this.HandleTriggering(Mouse.MouseMessageFlags.OnMouseExit);
		}

		// Token: 0x0600775D RID: 30557 RVA: 0x000515CB File Offset: 0x0004F7CB
		private void OnMouseOver()
		{
			this.HandleTriggering(Mouse.MouseMessageFlags.OnMouseOver);
		}

		// Token: 0x0600775E RID: 30558 RVA: 0x000515D5 File Offset: 0x0004F7D5
		private void OnMouseUp()
		{
			this.HandleTriggering(Mouse.MouseMessageFlags.OnMouseUp);
		}

		// Token: 0x0600775F RID: 30559 RVA: 0x000515DF File Offset: 0x0004F7DF
		private void OnMouseUpAsButton()
		{
			this.HandleTriggering(Mouse.MouseMessageFlags.OnMouseUpAsButton);
		}

		// Token: 0x06007760 RID: 30560 RVA: 0x000515E9 File Offset: 0x0004F7E9
		private void HandleTriggering(Mouse.MouseMessageFlags from)
		{
			if ((from & this.FireOn) != (Mouse.MouseMessageFlags)0)
			{
				this.ExecuteBlock();
			}
		}

		// Token: 0x04006804 RID: 26628
		[Tooltip("Which of the Mouse messages to trigger on.")]
		[SerializeField]
		[EnumFlag]
		protected Mouse.MouseMessageFlags FireOn = Mouse.MouseMessageFlags.OnMouseUpAsButton;

		// Token: 0x0200132A RID: 4906
		[Flags]
		public enum MouseMessageFlags
		{
			// Token: 0x04006806 RID: 26630
			OnMouseDown = 1,
			// Token: 0x04006807 RID: 26631
			OnMouseDrag = 2,
			// Token: 0x04006808 RID: 26632
			OnMouseEnter = 4,
			// Token: 0x04006809 RID: 26633
			OnMouseExit = 8,
			// Token: 0x0400680A RID: 26634
			OnMouseOver = 16,
			// Token: 0x0400680B RID: 26635
			OnMouseUp = 32,
			// Token: 0x0400680C RID: 26636
			OnMouseUpAsButton = 64
		}
	}
}
