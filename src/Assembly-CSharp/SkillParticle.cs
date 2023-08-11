using UnityEngine;
using YSGame;

public class SkillParticle : MonoBehaviour
{
	public float RemoveTime = 1f;

	public float hitTime = 0.1f;

	private void Start()
	{
		((MonoBehaviour)this).Invoke("playHit", hitTime);
		((MonoBehaviour)this).Invoke("removeThis", RemoveTime);
	}

	public void removeThis()
	{
		Object.Destroy((Object)(object)((Component)this).gameObject);
	}

	public void playHit()
	{
		YSFuncList.Ints.Continue();
	}

	private void Update()
	{
	}
}
