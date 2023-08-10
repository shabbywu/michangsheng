using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
	public float Direction = 0.5f;

	public float Radius = 1f;

	public int Force = 500;

	public AudioClip[] SoundHit;

	public bool Activated;

	public GameObject FloatingText;

	private HashSet<GameObject> listObjHitted = new HashSet<GameObject>();

	public void StartDamage()
	{
		listObjHitted.Clear();
		Activated = false;
	}

	private void AddObjHitted(GameObject obj)
	{
		listObjHitted.Add(obj);
	}

	private void Update()
	{
	}

	public void AddFloatingText(Vector3 pos, string text)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		if (Object.op_Implicit((Object)(object)FloatingText))
		{
			GameObject val = Object.Instantiate<GameObject>(FloatingText, pos, ((Component)this).transform.rotation);
			if (Object.op_Implicit((Object)(object)val.GetComponent<FloatingText>()))
			{
				val.GetComponent<FloatingText>().Text = text;
			}
			Object.Destroy((Object)(object)val, 1f);
		}
	}

	public void DoDamage()
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		Activated = true;
		Collider[] array = Physics.OverlapSphere(((Component)this).transform.position, Radius);
		foreach (Collider val in array)
		{
			if (!Object.op_Implicit((Object)(object)val) || (Object)(object)((Component)val).gameObject == (Object)(object)((Component)this).gameObject || ((Component)val).gameObject.tag == ((Component)this).gameObject.tag || listObjHitted.Contains(((Component)val).gameObject))
			{
				continue;
			}
			Vector3 val2 = ((Component)val).transform.position - ((Component)this).transform.position;
			if (Vector3.Dot(((Vector3)(ref val2)).normalized, ((Component)this).transform.forward) < Direction)
			{
				continue;
			}
			OrbitGameObject component = ((Component)Camera.main).gameObject.GetComponent<OrbitGameObject>();
			if ((Object)(object)component != (Object)null && (Object)(object)component.Target == (Object)(object)((Component)val).gameObject)
			{
				ShakeCamera.Shake(0.5f, 0.5f);
			}
			Vector3 val3 = (((Component)this).transform.forward + ((Component)this).transform.up) * (float)Force;
			if (Object.op_Implicit((Object)(object)((Component)val).gameObject.GetComponent<CharacterStatus>()))
			{
				if (SoundHit.Length != 0)
				{
					int num = Random.Range(0, SoundHit.Length);
					if ((Object)(object)SoundHit[num] != (Object)null)
					{
						AudioSource.PlayClipAtPoint(SoundHit[num], ((Component)this).transform.position);
					}
				}
				int damage = ((Component)this).gameObject.GetComponent<CharacterStatus>().Damage;
				int damage2 = (int)Random.Range((float)damage / 2f, (float)damage) + 1;
				CharacterStatus component2 = ((Component)val).gameObject.GetComponent<CharacterStatus>();
				int num2 = component2.ApplayDamage(damage2, val3);
				AddFloatingText(((Component)val).transform.position + Vector3.up, num2.ToString());
				component2.AddParticle(((Component)val).transform.position + Vector3.up);
			}
			if (Object.op_Implicit((Object)(object)((Component)val).GetComponent<Rigidbody>()))
			{
				((Component)val).GetComponent<Rigidbody>().AddForce(val3);
			}
			AddObjHitted(((Component)val).gameObject);
		}
	}
}
