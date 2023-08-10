using UnityEngine;

public class CoinVisibility : MonoBehaviour
{
	private void OnBecameInvisible()
	{
		Debug.Log((object)"SAKRIO, OPAAAAA");
		((Component)this).GetComponent<Renderer>().enabled = false;
	}

	private void OnBecameVisible()
	{
		if (MonkeyController2D.canRespawnThings)
		{
			Debug.Log((object)"SAD SE VIDI, RNZAAAA");
			((Component)this).GetComponent<Renderer>().enabled = true;
		}
	}
}
