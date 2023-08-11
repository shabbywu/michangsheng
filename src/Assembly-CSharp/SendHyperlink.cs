using UnityEngine;
using YSGame.TuJian;

public class SendHyperlink : MonoBehaviour
{
	public void Send(string link)
	{
		TuJianManager.Inst.OnHyperlink(link);
	}
}
