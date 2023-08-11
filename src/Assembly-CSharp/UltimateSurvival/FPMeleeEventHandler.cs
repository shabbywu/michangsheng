using UnityEngine;

namespace UltimateSurvival;

public class FPMeleeEventHandler : MonoBehaviour
{
	public Message Hit = new Message();

	public Message Woosh = new Message();

	public void On_Hit()
	{
		Hit.Send();
	}

	public void On_Woosh()
	{
		Woosh.Send();
	}
}
