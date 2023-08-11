using UnityEngine;
using UnityEngine.UI;

public class UITeQuanItem : MonoBehaviour
{
	public Image LockImage;

	public Text TeQuanText;

	private static Color c1 = new Color(79f / 85f, 0.75686276f, 44f / 85f);

	private static Color c2 = new Color(0.4627451f, 29f / 85f, 0.2627451f);

	public void SetText(string text)
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		((Component)LockImage).gameObject.SetActive(false);
		TeQuanText.text = text;
		((Graphic)TeQuanText).color = c1;
	}

	public void SetLockText(string text)
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		((Component)LockImage).gameObject.SetActive(true);
		TeQuanText.text = text;
		((Graphic)TeQuanText).color = c2;
	}
}
