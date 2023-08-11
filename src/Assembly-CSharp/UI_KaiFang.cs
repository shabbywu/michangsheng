using KBEngine;
using UnityEngine;

public class UI_KaiFang : MonoBehaviour
{
	public int castMoney = 10;

	public UIBiGuan biguan;

	public string ScenName = "";

	public UILabel castMonsy;

	public UILabel hasMoney;

	private void Start()
	{
	}

	public void ClickConfim()
	{
		Avatar player = Tools.instance.getPlayer();
		if ((int)player.money < GetUseMoney())
		{
			UIPopTip.Inst.Pop("金币不足");
			return;
		}
		string scenName = ScenName;
		int addyear = int.Parse(biguan.getInputYear.value);
		int addMonth = int.Parse(biguan.getInputMonth.value);
		player.zulinContorl.KZAddTime(scenName, 0, addMonth, addyear);
		player.money -= (ulong)GetUseMoney();
		biguan.close();
	}

	private void Update()
	{
		updateCastMoney();
	}

	public int GetUseMoney()
	{
		int num = int.Parse(biguan.getInputYear.value);
		int num2 = int.Parse(biguan.getInputMonth.value);
		return castMoney * (12 * num + num2);
	}

	public void updateCastMoney()
	{
		if (Tools.instance.getPlayer() != null)
		{
			int useMoney = GetUseMoney();
			string text = string.Concat(useMoney);
			castMonsy.text = (((ulong)useMoney > Tools.instance.getPlayer().money) ? ("[FF0000]" + text) : ("[62C4CB]" + text));
		}
	}
}
