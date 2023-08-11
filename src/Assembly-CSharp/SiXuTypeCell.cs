using UnityEngine;
using UnityEngine.UI;

public class SiXuTypeCell : MonoBehaviour
{
	[SerializeField]
	private Text SiXuContent;

	[SerializeField]
	private Text OutTime;

	public void setContent(string content, string outTime)
	{
		SiXuContent.text = content;
		OutTime.text = outTime;
	}
}
