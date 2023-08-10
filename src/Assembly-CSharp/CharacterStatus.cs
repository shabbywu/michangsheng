using System.Collections;
using System.Linq;
using UnityEngine;

public class CharacterStatus : MonoBehaviour
{
	public GameObject DeadbodyModel;

	public GameObject ParticleObject;

	public string Name = "";

	public int HP = 10;

	public int SP = 10;

	public int SPmax = 10;

	public int HPmax = 10;

	public int Damage = 1;

	public int Defend = 1;

	public int HPregen = 1;

	public int SPregen = 1;

	public AudioClip[] SoundHit;

	private Vector3 velocityDamage;

	private float lastRegen;

	private void Update()
	{
		if (Time.time - lastRegen >= 1f)
		{
			lastRegen = Time.time;
			HP += HPregen;
			SP += SPregen;
		}
		if (HP > HPmax)
		{
			HP = HPmax;
		}
		if (SP > SPmax)
		{
			SP = SPmax;
		}
	}

	public int ApplayDamage(int damage, Vector3 dirdamge)
	{
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		if (HP < 0)
		{
			return 0;
		}
		if (SoundHit.Length != 0)
		{
			int num = Random.Range(0, SoundHit.Length);
			if ((Object)(object)SoundHit[num] != (Object)null)
			{
				AudioSource.PlayClipAtPoint(SoundHit[num], ((Component)this).transform.position);
			}
		}
		if (Object.op_Implicit((Object)(object)((Component)this).gameObject.GetComponent<CharacterSystem>()))
		{
			((Component)this).gameObject.GetComponent<CharacterSystem>().GotHit(1f);
		}
		int num2 = damage - Defend;
		if (num2 < 1)
		{
			num2 = 1;
		}
		HP -= num2;
		velocityDamage = dirdamge;
		if (HP <= 0)
		{
			Dead();
		}
		return num2;
	}

	public void AddParticle(Vector3 pos)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		if (Object.op_Implicit((Object)(object)ParticleObject))
		{
			Object.Destroy((Object)(object)Object.Instantiate<GameObject>(ParticleObject, pos, ((Component)this).transform.rotation), 1f);
		}
	}

	private void Dead()
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		if (Object.op_Implicit((Object)(object)DeadbodyModel))
		{
			GameObject val = Object.Instantiate<GameObject>(DeadbodyModel, ((Component)this).gameObject.transform.position, ((Component)this).gameObject.transform.rotation);
			CopyTransformsRecurse(((Component)this).gameObject.transform, val.transform);
			Object.Destroy((Object)(object)val, 10f);
		}
		Object.Destroy((Object)(object)((Component)this).gameObject);
	}

	public void CopyTransformsRecurse(Transform src, Transform dst)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		dst.position = src.position;
		dst.rotation = src.rotation;
		dst.localScale = src.localScale;
		foreach (Transform item in ((IEnumerable)dst).Cast<Transform>())
		{
			Transform val = src.Find(((Object)item).name);
			if (Object.op_Implicit((Object)(object)((Component)item).GetComponent<Rigidbody>()))
			{
				((Component)item).GetComponent<Rigidbody>().AddForce(velocityDamage / 3f);
			}
			if (Object.op_Implicit((Object)(object)val))
			{
				CopyTransformsRecurse(val, item);
			}
		}
	}
}
