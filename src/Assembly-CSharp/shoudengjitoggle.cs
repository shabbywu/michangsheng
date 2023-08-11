using KBEngine;
using UnityEngine;

public class shoudengjitoggle : MonoBehaviour
{
	public UIToggle toggle;

	private void Awake()
	{
		if (Tools.instance.getPlayer().showStaticSkillDengJi == 1)
		{
			toggle.value = true;
		}
		else
		{
			toggle.value = false;
		}
	}

	private void Start()
	{
	}

	public void chenge()
	{
		if (toggle.value)
		{
			Tools.instance.getPlayer().showStaticSkillDengJi = 1;
		}
		else
		{
			Tools.instance.getPlayer().showStaticSkillDengJi = 0;
		}
	}

	private void Update()
	{
		Avatar player = Tools.instance.getPlayer();
		if (player.showStaticSkillDengJi == 1 && !toggle.value)
		{
			toggle.value = true;
		}
		else if (player.showStaticSkillDengJi != 1 && toggle.value)
		{
			toggle.value = false;
		}
	}
}
