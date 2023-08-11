using GUIPackage;
using UnityEngine;

public class JinMaiTips : MonoBehaviour
{
	private void OnHover(bool isOver)
	{
		if (isOver)
		{
			string text = "经脉";
			string desstr = "【经脉】受【体道】与【气道】感悟影响，【体道】与【气道】感悟越高，【经脉】上限越高\n当【经脉】血量低于50时，每回合抽牌数-2\n当【经脉】归0时，结婴失败，结婴者将修为大退，但下次结婴时，【经脉】上限+5";
			Singleton.inventory.Tooltip.GetComponentInChildren<UILabel>().text = "[ff8300]" + text + ":[-] " + Tools.getDesc(desstr, 1).Replace("[FF00FF]", "[ff8300]");
			Singleton.inventory.showTooltip = true;
		}
		else
		{
			Singleton.inventory.showTooltip = false;
		}
	}
}
