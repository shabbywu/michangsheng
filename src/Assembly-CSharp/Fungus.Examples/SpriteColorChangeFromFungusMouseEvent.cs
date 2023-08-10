using UnityEngine;

namespace Fungus.Examples;

public class SpriteColorChangeFromFungusMouseEvent : MonoBehaviour
{
	private SpriteRenderer rend;

	private void Start()
	{
		rend = ((Component)this).GetComponent<SpriteRenderer>();
	}

	private void OnMouseEventFromFungus()
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		rend.color = Color.HSVToRGB(Random.value, Random.Range(0.7f, 0.9f), Random.Range(0.7f, 0.9f));
	}
}
