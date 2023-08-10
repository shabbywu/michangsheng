using UnityEngine;

public class ItemPickup : MonoBehaviour
{
	public bool DestroyWhenPickup = true;

	public AudioClip SoundPickup;

	public int IndexItem;

	public int Num = 1;

	private void Start()
	{
	}

	private void OnTriggerStay(Collider other)
	{
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		if (!Object.op_Implicit((Object)(object)((Component)other).gameObject.GetComponent<CharacterInventory>()))
		{
			return;
		}
		((Component)other).gameObject.GetComponent<CharacterInventory>().AddItem(IndexItem, Num);
		if (Object.op_Implicit((Object)(object)SoundPickup))
		{
			AudioSource.PlayClipAtPoint(SoundPickup, ((Component)Camera.main).gameObject.transform.position);
		}
		if (DestroyWhenPickup)
		{
			if (Object.op_Implicit((Object)(object)((Component)this).gameObject.transform.parent))
			{
				Object.Destroy((Object)(object)((Component)((Component)this).gameObject.transform.parent).gameObject);
			}
			else
			{
				Object.Destroy((Object)(object)((Component)this).gameObject);
			}
		}
	}
}
