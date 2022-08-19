using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000511 RID: 1297
public class UnistiteljEvents : MonoBehaviour
{
	// Token: 0x060029BB RID: 10683 RVA: 0x0013ED44 File Offset: 0x0013CF44
	private void Start()
	{
		this.enemyPool = GameObject.Find("__EnemiesPool").transform;
		this.environmentPool = GameObject.Find("__EnvironmentPool").transform;
		this.coinsPool = GameObject.Find("__CoinsPool").transform;
		this.specialPool = GameObject.Find("__SpecialPool").transform;
	}

	// Token: 0x060029BC RID: 10684 RVA: 0x0013EDA8 File Offset: 0x0013CFA8
	private void OnTriggerEnter2D(Collider2D col)
	{
		if (this.start > 0)
		{
			if (col.name.Equals("__GranicaDesno"))
			{
				this.brojac++;
				base.StartCoroutine(this.vratiNazadUPool(col));
				return;
			}
		}
		else if (col.name.Equals("__GranicaDesno"))
		{
			this.brojac++;
			LevelFactory.instance.Reposition();
			this.start++;
			col.GetComponent<Collider2D>().enabled = false;
			this.TrenutnoSeKoristi();
		}
	}

	// Token: 0x060029BD RID: 10685 RVA: 0x0013EE37 File Offset: 0x0013D037
	private IEnumerator vratiNazadUPool(Collider2D col)
	{
		LevelPrefabProperties component = col.transform.parent.GetComponent<LevelPrefabProperties>();
		col.GetComponent<Collider2D>().enabled = false;
		component.slobodanTeren = 1;
		for (int i = 0; i < this.enemyPool.childCount; i++)
		{
			EntityProperties component2 = this.enemyPool.GetChild(i).GetComponent<EntityProperties>();
			if (!component2.slobodanEntitet)
			{
				if (!component2.trenutnoJeAktivan)
				{
					component2.trenutnoJeAktivan = true;
				}
				else if (component2.instanciran)
				{
					Object.Destroy(component2.gameObject);
				}
				else
				{
					if (component2.Type == 18)
					{
						component2.transform.GetChild(0).GetChild(0).GetComponent<BarrelExplode>().ObnoviBure();
					}
					component2.slobodanEntitet = true;
				}
			}
		}
		yield return new WaitForSeconds(0.02f);
		for (int j = 0; j < this.environmentPool.childCount; j++)
		{
			EntityProperties component2 = this.environmentPool.GetChild(j).GetComponent<EntityProperties>();
			if (!component2.slobodanEntitet)
			{
				if (!component2.trenutnoJeAktivan)
				{
					component2.trenutnoJeAktivan = true;
				}
				else
				{
					component2.slobodanEntitet = true;
				}
			}
		}
		yield return new WaitForSeconds(0.02f);
		for (int k = 0; k < this.coinsPool.childCount; k++)
		{
			EntityProperties component2 = this.coinsPool.GetChild(k).GetComponent<EntityProperties>();
			if (!component2.slobodanEntitet)
			{
				if (!component2.trenutnoJeAktivan)
				{
					component2.trenutnoJeAktivan = true;
				}
				else
				{
					component2.slobodanEntitet = true;
				}
			}
		}
		yield return new WaitForSeconds(0.02f);
		for (int l = 0; l < this.specialPool.childCount; l++)
		{
			EntityProperties component2 = this.specialPool.GetChild(l).GetComponent<EntityProperties>();
			if (!component2.slobodanEntitet)
			{
				if (!component2.trenutnoJeAktivan)
				{
					component2.trenutnoJeAktivan = true;
				}
				else
				{
					if (component2.Type == 2)
					{
						component2.transform.GetChild(0).GetComponent<BarrelExplode>().ObnoviBure();
					}
					component2.slobodanEntitet = true;
				}
			}
		}
		yield return new WaitForSeconds(0.02f);
		if (!LevelFactory.trebaFinish)
		{
			LevelFactory.instance.Reposition();
		}
		yield break;
	}

	// Token: 0x060029BE RID: 10686 RVA: 0x0013EE50 File Offset: 0x0013D050
	private void TrenutnoSeKoristi()
	{
		for (int i = 0; i < this.enemyPool.childCount; i++)
		{
			EntityProperties component = this.enemyPool.GetChild(i).GetComponent<EntityProperties>();
			if (!component.trenutnoJeAktivan)
			{
				component.trenutnoJeAktivan = true;
			}
		}
		for (int j = 0; j < this.environmentPool.childCount; j++)
		{
			EntityProperties component = this.environmentPool.GetChild(j).GetComponent<EntityProperties>();
			if (!component.trenutnoJeAktivan)
			{
				component.trenutnoJeAktivan = true;
			}
		}
		for (int k = 0; k < this.coinsPool.childCount; k++)
		{
			EntityProperties component = this.coinsPool.GetChild(k).GetComponent<EntityProperties>();
			if (!component.trenutnoJeAktivan)
			{
				component.trenutnoJeAktivan = true;
			}
		}
		for (int l = 0; l < this.specialPool.childCount; l++)
		{
			EntityProperties component = this.specialPool.GetChild(l).GetComponent<EntityProperties>();
			if (!component.trenutnoJeAktivan)
			{
				component.trenutnoJeAktivan = true;
			}
		}
	}

	// Token: 0x04002610 RID: 9744
	public Transform terrainPool;

	// Token: 0x04002611 RID: 9745
	private Transform enemyPool;

	// Token: 0x04002612 RID: 9746
	private Transform environmentPool;

	// Token: 0x04002613 RID: 9747
	private Transform coinsPool;

	// Token: 0x04002614 RID: 9748
	private Transform specialPool;

	// Token: 0x04002615 RID: 9749
	private int start;

	// Token: 0x04002616 RID: 9750
	private int brojac;
}
