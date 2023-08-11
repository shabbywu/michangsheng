using UnityEngine;

public class CoinMagnet : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D col)
	{
		if (((Component)col).tag == "Novcic")
		{
			((Component)col).gameObject.SetActive(false);
		}
	}
}
