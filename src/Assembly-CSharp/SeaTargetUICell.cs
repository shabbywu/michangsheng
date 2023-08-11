using UnityEngine;
using UnityEngine.UI;

public class SeaTargetUICell : MonoBehaviour
{
	public int eventId;

	public Button jiaohu;

	public Text Title;

	private void Start()
	{
	}

	public void click()
	{
		bool flag = true;
		foreach (SeaAvatarObjBase monstar in EndlessSeaMag.Inst.MonstarList)
		{
			if (monstar._EventId == eventId)
			{
				monstar.EventShiJian();
				flag = false;
				break;
			}
		}
		if (flag)
		{
			int num = (int)jsonData.instance.EndlessSeaNPCData[string.Concat(eventId)][(object)"EventList"];
			Object.Instantiate<GameObject>(Resources.Load<GameObject>("talkPrefab/TalkPrefab/talk" + num));
		}
	}

	private void Update()
	{
	}
}
