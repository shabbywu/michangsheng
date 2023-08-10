using GUIPackage;
using UnityEngine;

public class jieyingNextBuff : MonoBehaviour
{
	private void Start()
	{
	}

	private void OnHover(bool isOver)
	{
		if (isOver)
		{
			string index = string.Concat(Tools.instance.getPlayer().buffmag.getBuffBySeid(212)[0][2]);
			string key = jsonData.instance.BuffSeidJsonData[5][index]["value1"][0].ToString();
			string desstr = Tools.instance.Code64ToString(jsonData.instance.BuffJsonData[key]["descr"].str);
			Singleton.inventory.Tooltip.GetComponentInChildren<UILabel>().text = Tools.getDesc(desstr, 1).Replace("[FF00FF]", "[ff8300]");
			Singleton.inventory.showTooltip = true;
		}
		else
		{
			Singleton.inventory.showTooltip = false;
		}
	}

	private void Update()
	{
	}
}
