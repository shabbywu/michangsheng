using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(UIWidget))]
[AddComponentMenu("NGUI/Examples/Set Color on Selection")]
public class SetColorOnSelection : MonoBehaviour
{
	private UIWidget mWidget;

	public void SetSpriteBySelection()
	{
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		if (!((Object)(object)UIPopupList.current == (Object)null))
		{
			if ((Object)(object)mWidget == (Object)null)
			{
				mWidget = ((Component)this).GetComponent<UIWidget>();
			}
			switch (UIPopupList.current.value)
			{
			case "White":
				mWidget.color = Color.white;
				break;
			case "Red":
				mWidget.color = Color.red;
				break;
			case "Green":
				mWidget.color = Color.green;
				break;
			case "Blue":
				mWidget.color = Color.blue;
				break;
			case "Yellow":
				mWidget.color = Color.yellow;
				break;
			case "Cyan":
				mWidget.color = Color.cyan;
				break;
			case "Magenta":
				mWidget.color = Color.magenta;
				break;
			}
		}
	}
}
