using UnityEngine;
using UnityEngine.UI;

namespace PaiMai;

public class CommandTips : MonoBehaviour
{
	private Text _content;

	private void Awake()
	{
		_content = ((Component)((Component)this).transform.Find("content")).GetComponent<Text>();
	}

	public void ShowTips(string msg)
	{
		((Component)this).gameObject.SetActive(true);
		_content.text = msg;
	}

	public void Hide()
	{
		((Component)this).gameObject.SetActive(false);
	}
}
