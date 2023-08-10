using UnityEngine;

namespace GUIPackage;

public class Money : MonoBehaviour
{
	public enum MoneyUIType
	{
		EXPlayerMoney,
		EXMonstarMoney,
		EXPlayerPayMoney,
		EXMonstarPayMoney,
		PlayerMoney
	}

	public int money;

	public GameObject Label;

	public MoneyUIType MoneyType;

	private void Update()
	{
		if (MoneyType == MoneyUIType.PlayerMoney)
		{
			Label.GetComponent<UILabel>().text = Tools.instance.getPlayer().money.ToString("#,##0");
		}
	}

	private void Start()
	{
	}

	public void Set_money(int num, bool isShowFuHao = false)
	{
		money = num;
		if (isShowFuHao && num >= 0)
		{
			Label.GetComponent<UILabel>().text = "+" + money.ToString("#,##0");
		}
		else
		{
			Label.GetComponent<UILabel>().text = money.ToString("#,##0");
		}
	}
}
