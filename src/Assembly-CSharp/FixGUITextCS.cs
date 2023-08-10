using ArabicSupport;
using UnityEngine;

public class FixGUITextCS : MonoBehaviour
{
	public string text;

	public bool tashkeel = true;

	public bool hinduNumbers = true;

	private void Start()
	{
		((Component)this).gameObject.GetComponent<GUIText>().text = ArabicFixer.Fix(text, tashkeel, hinduNumbers);
	}

	private void Update()
	{
	}
}
