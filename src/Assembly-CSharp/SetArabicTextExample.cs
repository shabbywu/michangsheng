using ArabicSupport;
using UnityEngine;

public class SetArabicTextExample : MonoBehaviour
{
	public string text;

	private void Start()
	{
		((Component)this).gameObject.GetComponent<GUIText>().text = "This sentence (wrong display):\n" + text + "\n\nWill appear correctly as:\n" + ArabicFixer.Fix(text, false, false);
	}

	private void Update()
	{
	}
}
