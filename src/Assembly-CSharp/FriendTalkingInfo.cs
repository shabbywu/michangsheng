using KBEngine;
using UnityEngine;
using UnityEngine.UI;

public class FriendTalkingInfo : MonoBehaviour
{
	public ulong friendDbid;

	public string friendName;

	public GameObject inputInfo;

	private void Start()
	{
		inputInfo = ((Component)((Component)this).transform.Find("Scroll View").Find("InputField")).gameObject;
	}

	public void sendMsg()
	{
		Account account = (Account)KBEngineApp.app.player();
		string text = ((Component)inputInfo.transform.Find("Text")).GetComponent<Text>().text;
		if (account != null && text != "")
		{
			account.sendMsg(friendDbid, text);
		}
	}
}
