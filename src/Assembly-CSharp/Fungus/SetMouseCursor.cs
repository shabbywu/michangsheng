using UnityEngine;

namespace Fungus;

[CommandInfo("Sprite", "Set Mouse Cursor", "Sets the mouse cursor sprite.", 0)]
[AddComponentMenu("")]
public class SetMouseCursor : Command
{
	[Tooltip("Texture to use for cursor. Will use default mouse cursor if no sprite is specified")]
	[SerializeField]
	protected Texture2D cursorTexture;

	[Tooltip("The offset from the top left of the texture to use as the target point")]
	[SerializeField]
	protected Vector2 hotSpot;

	protected static Texture2D activeCursorTexture;

	protected static Vector2 activeHotspot;

	public static void ResetMouseCursor()
	{
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		Cursor.SetCursor(activeCursorTexture, activeHotspot, (CursorMode)0);
	}

	public override void OnEnter()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		Cursor.SetCursor(cursorTexture, hotSpot, (CursorMode)0);
		activeCursorTexture = cursorTexture;
		activeHotspot = hotSpot;
		Continue();
	}

	public override string GetSummary()
	{
		if ((Object)(object)cursorTexture == (Object)null)
		{
			return "Error: No cursor sprite selected";
		}
		return ((Object)cursorTexture).name;
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)235, (byte)191, (byte)217, byte.MaxValue));
	}
}
