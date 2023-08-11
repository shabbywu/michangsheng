using UnityEngine;

public class DestroySpearGorilla : MonoBehaviour
{
	public Transform gorilla;

	public void DestroyGorilla()
	{
		((Component)gorilla).GetComponent<KillTheBaboon>().DestoyEnemy();
	}
}
