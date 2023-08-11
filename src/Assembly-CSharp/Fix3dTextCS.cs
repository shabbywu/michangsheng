using ArabicSupport;
using UnityEngine;

public class Fix3dTextCS : MonoBehaviour
{
	public string text;

	public bool tashkeel = true;

	public bool hinduNumbers = true;

	private void Start()
	{
		((Component)this).gameObject.GetComponent<TextMesh>().text = ArabicFixer.Fix(text, tashkeel, hinduNumbers);
	}

	private void Update()
	{
	}
}
