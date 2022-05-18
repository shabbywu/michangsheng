using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x0200128D RID: 4749
	[CommandInfo("Sprite", "Set Mouse Cursor", "Sets the mouse cursor sprite.", 0)]
	[AddComponentMenu("")]
	public class SetMouseCursor : Command
	{
		// Token: 0x06007321 RID: 29473 RVA: 0x0004E7EF File Offset: 0x0004C9EF
		public static void ResetMouseCursor()
		{
			Cursor.SetCursor(SetMouseCursor.activeCursorTexture, SetMouseCursor.activeHotspot, 0);
		}

		// Token: 0x06007322 RID: 29474 RVA: 0x0004E801 File Offset: 0x0004CA01
		public override void OnEnter()
		{
			Cursor.SetCursor(this.cursorTexture, this.hotSpot, 0);
			SetMouseCursor.activeCursorTexture = this.cursorTexture;
			SetMouseCursor.activeHotspot = this.hotSpot;
			this.Continue();
		}

		// Token: 0x06007323 RID: 29475 RVA: 0x0004E831 File Offset: 0x0004CA31
		public override string GetSummary()
		{
			if (this.cursorTexture == null)
			{
				return "Error: No cursor sprite selected";
			}
			return this.cursorTexture.name;
		}

		// Token: 0x06007324 RID: 29476 RVA: 0x0004C5E0 File Offset: 0x0004A7E0
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x04006520 RID: 25888
		[Tooltip("Texture to use for cursor. Will use default mouse cursor if no sprite is specified")]
		[SerializeField]
		protected Texture2D cursorTexture;

		// Token: 0x04006521 RID: 25889
		[Tooltip("The offset from the top left of the texture to use as the target point")]
		[SerializeField]
		protected Vector2 hotSpot;

		// Token: 0x04006522 RID: 25890
		protected static Texture2D activeCursorTexture;

		// Token: 0x04006523 RID: 25891
		protected static Vector2 activeHotspot;
	}
}
