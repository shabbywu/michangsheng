using System.Collections.Generic;
using UnityEngine;

namespace YSGame;

public class SkillEffectPerfab : MonoBehaviour
{
	[SerializeField]
	public List<skillEffctType> EffectsList = new List<skillEffctType>();

	public static SkillEffectPerfab inst;

	private void Start()
	{
		inst = this;
		Object.DontDestroyOnLoad((Object)(object)((Component)this).gameObject);
	}
}
