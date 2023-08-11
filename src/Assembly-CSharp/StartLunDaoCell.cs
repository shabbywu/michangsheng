using UnityEngine;
using UnityEngine.UI;

public class StartLunDaoCell : MonoBehaviour
{
	public Image sanJiaoImage;

	public Image wenZi;

	public bool CanClick;

	public void Click()
	{
		if (CanClick)
		{
			LunDaoManager.inst.StartGame();
		}
		else
		{
			UIPopTip.Inst.Pop("必须选择一个论题");
		}
	}
}
