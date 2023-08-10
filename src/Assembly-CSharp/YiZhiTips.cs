using GUIPackage;
using UnityEngine;

public class YiZhiTips : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
	}

	private void OnHover(bool isOver)
	{
		if (isOver)
		{
			string text = "意志";
			string desstr = "【意志】受【心境】与【神道】感悟影响，【心境】与【神道】感悟越高，【意志】上限越高\n当【意志】低于50时，玩家受到的心魔伤害将提升20%\n当【意志】归0时，结婴失败，结婴者将身死道消";
			Singleton.inventory.Tooltip.GetComponentInChildren<UILabel>().text = "[ff8300]" + text + ":[-] " + Tools.getDesc(desstr, 1).Replace("[FF00FF]", "[ff8300]");
			Singleton.inventory.showTooltip = true;
		}
		else
		{
			Singleton.inventory.showTooltip = false;
		}
	}
}
