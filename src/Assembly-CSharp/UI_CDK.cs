using KBEngine;
using UnityEngine;
using UnityEngine.UI;

public class UI_CDK : MonoBehaviour
{
	public GameObject backgroud;

	public InputField CDK;

	private int stopTime;

	private int backtype;

	private void Awake()
	{
		backgroud.SetActive(false);
	}

	public void open()
	{
		backtype = 1;
		backgroud.SetActive(true);
	}

	public void close()
	{
		backtype = 0;
		backgroud.SetActive(false);
	}

	public void useCDK()
	{
		string text = CDK.text;
		Account account = (Account)KBEngineApp.app.player();
		if (account != null && stopTime == 0)
		{
			stopTime = 1;
			account.useCDK(text);
			((MonoBehaviour)this).Invoke("resetTime", 1f);
		}
	}

	public void resetTime()
	{
		stopTime = 0;
	}

	public void buttondwon()
	{
		if (backtype == 0)
		{
			open();
		}
		else
		{
			close();
		}
	}
}
