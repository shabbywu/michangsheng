using UnityEngine;

public class FightFlashBlack : MonoBehaviour
{
	public SpriteRenderer SR;

	private void Start()
	{
	}

	public void Flash()
	{
	}

	public void Hide()
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		SR.color = new Color(1f, 1f, 1f, 0f);
	}
}
