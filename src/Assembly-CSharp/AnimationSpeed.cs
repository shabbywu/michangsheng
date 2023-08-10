using UnityEngine;

public class AnimationSpeed : MonoBehaviour
{
	public string AniamName;

	public float speed = 1f;

	private void Start()
	{
		((Component)this).GetComponent<Animation>()[AniamName].speed = speed;
	}
}
