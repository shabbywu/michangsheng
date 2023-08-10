using KBEngine;
using UnityEngine;
using UnityEngine.UI;

public class LingGuangCell : MonoBehaviour
{
	public JSONObject Info;

	public Text Desc;

	public Text ShengYuTime;

	private void Start()
	{
	}

	public void init(string desc)
	{
		Desc.text = desc;
	}

	public void Click()
	{
		Event.fireOut("ClickLingGuangCell", Info);
	}

	private void Update()
	{
	}
}
