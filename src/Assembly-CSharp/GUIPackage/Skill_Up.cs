using UnityEngine;

namespace GUIPackage;

public class Skill_Up : MonoBehaviour
{
	private void OnPress()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Singleton.skillUI2.SkillUP(((Component)((Component)this).transform.parent).GetComponent<SkillStaticCell>().skillID);
		}
	}
}
