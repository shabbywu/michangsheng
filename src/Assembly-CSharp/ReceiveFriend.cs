using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ReceiveFriend : MonoBehaviour
{
	public ulong friendDbid;

	public string friendName;

	private void Start()
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Expected O, but got Unknown
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Expected O, but got Unknown
		((UnityEvent)((Component)((Component)this).transform.Find("consent")).GetComponent<Button>().onClick).AddListener(new UnityAction(choiceconsent));
		((UnityEvent)((Component)((Component)this).transform.Find("reject")).GetComponent<Button>().onClick).AddListener(new UnityAction(choicereject));
	}

	public void choiceconsent()
	{
		requestReceive(1);
		Object.Destroy((Object)(object)((Component)this).gameObject);
	}

	public void choicereject()
	{
		requestReceive(0);
		Object.Destroy((Object)(object)((Component)this).gameObject);
	}

	public void requestReceive(int choice)
	{
		((Account)KBEngineApp.app.player())?.requestReceive((ushort)choice, friendDbid);
	}
}
