using UnityEngine;

public class FinishEvent : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D col)
	{
		if (((Component)col).tag == "Monkey")
		{
			((Behaviour)((Component)this).GetComponent<Collider2D>()).enabled = false;
			GameObject.FindGameObjectWithTag("Monkey").GetComponent<MonkeyController2D>().Finish();
		}
	}
}
