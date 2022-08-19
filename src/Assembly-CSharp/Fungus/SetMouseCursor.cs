using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000E3C RID: 3644
	[CommandInfo("Sprite", "Set Mouse Cursor", "Sets the mouse cursor sprite.", 0)]
	[AddComponentMenu("")]
	public class SetMouseCursor : Command
	{
		// Token: 0x06006693 RID: 26259 RVA: 0x00286B14 File Offset: 0x00284D14
		public static void ResetMouseCursor()
		{
			Cursor.SetCursor(SetMouseCursor.activeCursorTexture, SetMouseCursor.activeHotspot, 0);
		}

		// Token: 0x06006694 RID: 26260 RVA: 0x00286B26 File Offset: 0x00284D26
		public override void OnEnter()
		{
			Cursor.SetCursor(this.cursorTexture, this.hotSpot, 0);
			SetMouseCursor.activeCursorTexture = this.cursorTexture;
			SetMouseCursor.activeHotspot = this.hotSpot;
			this.Continue();
		}

		// Token: 0x06006695 RID: 26261 RVA: 0x00286B56 File Offset: 0x00284D56
		public override string GetSummary()
		{
			if (this.cursorTexture == null)
			{
				return "Error: No cursor sprite selected";
			}
			return this.cursorTexture.name;
		}

		// Token: 0x06006696 RID: 26262 RVA: 0x0027D3DB File Offset: 0x0027B5DB
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x040057DC RID: 22492
		[Tooltip("Texture to use for cursor. Will use default mouse cursor if no sprite is specified")]
		[SerializeField]
		protected Texture2D cursorTexture;

		// Token: 0x040057DD RID: 22493
		[Tooltip("The offset from the top left of the texture to use as the target point")]
		[SerializeField]
		protected Vector2 hotSpot;

		// Token: 0x040057DE RID: 22494
		protected static Texture2D activeCursorTexture;

		// Token: 0x040057DF RID: 22495
		protected static Vector2 activeHotspot;
	}
}
