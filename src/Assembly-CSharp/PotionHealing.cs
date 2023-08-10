using UnityEngine;

public class PotionHealing : MonoBehaviour
{
	public int HPheal = 10;

	private void Start()
	{
		if (Object.op_Implicit((Object)(object)((Component)this).gameObject.transform.parent) && Object.op_Implicit((Object)(object)((Component)((Component)this).gameObject.transform.parent).gameObject.GetComponent<CharacterStatus>()))
		{
			((Component)((Component)this).gameObject.transform.parent).gameObject.GetComponent<CharacterStatus>().HP += HPheal;
		}
		Object.Destroy((Object)(object)((Component)this).gameObject, 3f);
	}
}
