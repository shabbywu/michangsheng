using UnityEngine;

public class CheckGroundUp : MonoBehaviour
{
	private MonkeyController2D player;

	public bool gornji;

	public bool donji;

	private void Awake()
	{
		player = ((Component)((Component)this).transform.parent).GetComponent<MonkeyController2D>();
	}

	private void OnTriggerEnter2D(Collider2D col)
	{
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		if ((player.state != MonkeyController2D.State.jumped && player.state != MonkeyController2D.State.climbUp && player.state != MonkeyController2D.State.wasted) || !(((Component)col).tag != "Finish"))
		{
			return;
		}
		float num = ((((Component)col).transform.childCount <= 0) ? ((Component)col).transform.position.y : ((Component)col).transform.Find("TriggerPositionDown").position.y);
		if (player.isSliding)
		{
			return;
		}
		if (((Component)player).transform.position.y < num)
		{
			((Component)((Component)this).transform.parent).GetComponent<Collider2D>().isTrigger = true;
		}
		else
		{
			if (!(((Component)player).transform.position.y >= num))
			{
				return;
			}
			if (!player.triggerCheckDownTrigger)
			{
				if (!player.triggerCheckDownBehind)
				{
					((Component)((Component)this).transform.parent).GetComponent<Collider2D>().isTrigger = true;
				}
			}
			else
			{
				_ = player.triggerCheckDownBehind;
			}
		}
	}

	private void OnTriggerExit2D(Collider2D col)
	{
		if (((Component)player).GetComponent<Collider2D>().isTrigger && !player.triggerCheckDownTrigger)
		{
			((Component)player).GetComponent<Collider2D>().isTrigger = false;
		}
	}
}
