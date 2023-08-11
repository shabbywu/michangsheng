using UnityEngine;

public class setChuanWenDisable : MonoBehaviour
{
	private void Start()
	{
		if (PlayerPrefs.GetInt("NowPlayerFileAvatar") >= 100)
		{
			((Behaviour)((Component)this).GetComponent<UIToggle>()).enabled = false;
			((Component)this).GetComponent<UIButtonColor>().SetState(UIButtonColor.State.Disabled, instant: true);
			((Behaviour)((Component)this).GetComponent<UIPlayAnimation>()).enabled = false;
			((Collider)((Component)this).GetComponent<BoxCollider>()).enabled = false;
		}
	}

	public void setenabel()
	{
		((Behaviour)((Component)this).GetComponent<UIButtonColor>()).enabled = false;
	}

	private void Update()
	{
	}
}
