using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class NonBreakingSpaceTextComponent : MonoBehaviour
{
	public static readonly string NoBreakingSpace = "\u00a0";

	private Text text;

	private void Awake()
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Expected O, but got Unknown
		text = ((Component)this).GetComponent<Text>();
		((Graphic)text).RegisterDirtyVerticesCallback(new UnityAction(SetTextSpace));
	}

	private void SetTextSpace()
	{
		if (text.text.Contains(" "))
		{
			text.text = text.text.Replace(" ", NoBreakingSpace);
		}
	}
}
