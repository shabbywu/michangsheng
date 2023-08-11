using UnityEngine;

public class EnemyDie : MonoBehaviour
{
	public GameObject[] ItemDropAfterDead;

	public int score = 1;

	private bool isShuttingDown;

	private void OnDestroy()
	{
		if (!isShuttingDown)
		{
			DropItem();
			GameManager gameManager = (GameManager)(object)Object.FindObjectOfType(typeof(GameManager));
			if (Object.op_Implicit((Object)(object)gameManager))
			{
				gameManager.Score += score;
			}
		}
	}

	private void OnApplicationQuit()
	{
		isShuttingDown = true;
	}

	private void DropItem()
	{
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		if (ItemDropAfterDead.Length == 0)
		{
			return;
		}
		int num = Random.Range(0, ItemDropAfterDead.Length);
		if ((Object)(object)ItemDropAfterDead[num] != (Object)null)
		{
			GameObject val = Object.Instantiate<GameObject>(ItemDropAfterDead[num], ((Component)this).gameObject.transform.position + Vector3.up * 2f, ((Component)this).gameObject.transform.rotation);
			if (Object.op_Implicit((Object)(object)val.GetComponent<Rigidbody>()))
			{
				val.GetComponent<Rigidbody>().AddForce((-((Component)this).transform.forward + Vector3.up) * 100f);
				val.GetComponent<Rigidbody>().AddTorque((-((Component)this).transform.forward + Vector3.up) * 100f);
			}
			Object.Destroy((Object)(object)val, 5f);
		}
	}
}
