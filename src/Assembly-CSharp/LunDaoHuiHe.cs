using UnityEngine;
using UnityEngine.UI;

public class LunDaoHuiHe : MonoBehaviour
{
	public int totalHui;

	public int curHui;

	public int shengYuHuiHe;

	[SerializeField]
	private Text curHuiText;

	[SerializeField]
	private Text shengYuHuiHeText;

	public void Init()
	{
		totalHui = 5;
		curHui = 1;
		shengYuHuiHe = totalHui - curHui;
		upDateHuiHeText();
	}

	public void ReduceHuiHe()
	{
		shengYuHuiHe--;
		curHui++;
		if (curHui > totalHui)
		{
			LunDaoManager.inst.gameState = LunDaoManager.GameState.论道结束;
		}
		else
		{
			upDateHuiHeText();
		}
	}

	private void upDateHuiHeText()
	{
		curHuiText.text = curHui.ToCNNumber();
		shengYuHuiHeText.text = "(剩余" + shengYuHuiHe.ToCNNumber() + "回合)";
	}
}
