using UnityEngine;

public class ShowNameToggle : MonoBehaviour
{
	public UIToggle toggle;

	private void Awake()
	{
	}

	private void Start()
	{
		Tools.instance.getPlayer();
		Tools.instance.getPlayer().showSkillName = 0;
	}

	public void chenge()
	{
		if (toggle.value)
		{
			Tools.instance.getPlayer().showSkillName = 0;
		}
		else
		{
			Tools.instance.getPlayer().showSkillName = 1;
		}
	}
}
