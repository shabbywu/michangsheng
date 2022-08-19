using System;
using UnityEngine;

// Token: 0x020000E0 RID: 224
public class EnemyBossDie : MonoBehaviour
{
	// Token: 0x06000B57 RID: 2903 RVA: 0x0004510C File Offset: 0x0004330C
	private void OnDestroy()
	{
		if (!this.isShuttingDown)
		{
			this.DropItem();
			GameManager gameManager = (GameManager)Object.FindObjectOfType(typeof(GameManager));
			if (gameManager)
			{
				gameManager.Score += this.score;
			}
		}
	}

	// Token: 0x06000B58 RID: 2904 RVA: 0x00045157 File Offset: 0x00043357
	private void OnApplicationQuit()
	{
		this.isShuttingDown = true;
	}

	// Token: 0x06000B59 RID: 2905 RVA: 0x00045160 File Offset: 0x00043360
	private void DropItem()
	{
		if (this.ItemDropAfterDead.Length != 0)
		{
			int num = Random.Range(0, this.ItemDropAfterDead.Length);
			if (this.ItemDropAfterDead[num] != null)
			{
				GameObject gameObject = Object.Instantiate<GameObject>(this.ItemDropAfterDead[num], base.gameObject.transform.position + Vector3.up * 2f, base.gameObject.transform.rotation);
				if (gameObject.GetComponent<Rigidbody>())
				{
					gameObject.GetComponent<Rigidbody>().AddForce((-base.transform.forward + Vector3.up) * 100f);
					gameObject.GetComponent<Rigidbody>().AddTorque((-base.transform.forward + Vector3.up) * 100f);
				}
				Object.Destroy(gameObject, 5f);
			}
		}
	}

	// Token: 0x04000787 RID: 1927
	public GameObject[] ItemDropAfterDead;

	// Token: 0x04000788 RID: 1928
	public int score = 1;

	// Token: 0x04000789 RID: 1929
	private bool isShuttingDown;
}
