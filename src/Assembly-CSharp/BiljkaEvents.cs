using UnityEngine;

public class BiljkaEvents : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D col)
	{
		((Component)col).tag.Equals("Monkey");
	}
}
