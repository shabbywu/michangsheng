using System.Collections;
using UnityEngine;

public class UnistiteljEvents : MonoBehaviour
{
	public Transform terrainPool;

	private Transform enemyPool;

	private Transform environmentPool;

	private Transform coinsPool;

	private Transform specialPool;

	private int start;

	private int brojac;

	private void Start()
	{
		enemyPool = GameObject.Find("__EnemiesPool").transform;
		environmentPool = GameObject.Find("__EnvironmentPool").transform;
		coinsPool = GameObject.Find("__CoinsPool").transform;
		specialPool = GameObject.Find("__SpecialPool").transform;
	}

	private void OnTriggerEnter2D(Collider2D col)
	{
		if (start > 0)
		{
			if (((Object)col).name.Equals("__GranicaDesno"))
			{
				brojac++;
				((MonoBehaviour)this).StartCoroutine(vratiNazadUPool(col));
			}
		}
		else if (((Object)col).name.Equals("__GranicaDesno"))
		{
			brojac++;
			LevelFactory.instance.Reposition();
			start++;
			((Behaviour)((Component)col).GetComponent<Collider2D>()).enabled = false;
			TrenutnoSeKoristi();
		}
	}

	private IEnumerator vratiNazadUPool(Collider2D col)
	{
		LevelPrefabProperties component = ((Component)((Component)col).transform.parent).GetComponent<LevelPrefabProperties>();
		((Behaviour)((Component)col).GetComponent<Collider2D>()).enabled = false;
		component.slobodanTeren = 1;
		for (int i = 0; i < enemyPool.childCount; i++)
		{
			EntityProperties component2 = ((Component)enemyPool.GetChild(i)).GetComponent<EntityProperties>();
			if (component2.slobodanEntitet)
			{
				continue;
			}
			if (!component2.trenutnoJeAktivan)
			{
				component2.trenutnoJeAktivan = true;
				continue;
			}
			if (component2.instanciran)
			{
				Object.Destroy((Object)(object)((Component)component2).gameObject);
				continue;
			}
			if (component2.Type == 18)
			{
				((Component)((Component)component2).transform.GetChild(0).GetChild(0)).GetComponent<BarrelExplode>().ObnoviBure();
			}
			component2.slobodanEntitet = true;
		}
		yield return (object)new WaitForSeconds(0.02f);
		for (int j = 0; j < environmentPool.childCount; j++)
		{
			EntityProperties component2 = ((Component)environmentPool.GetChild(j)).GetComponent<EntityProperties>();
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
		yield return (object)new WaitForSeconds(0.02f);
		for (int k = 0; k < coinsPool.childCount; k++)
		{
			EntityProperties component2 = ((Component)coinsPool.GetChild(k)).GetComponent<EntityProperties>();
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
		yield return (object)new WaitForSeconds(0.02f);
		for (int l = 0; l < specialPool.childCount; l++)
		{
			EntityProperties component2 = ((Component)specialPool.GetChild(l)).GetComponent<EntityProperties>();
			if (component2.slobodanEntitet)
			{
				continue;
			}
			if (!component2.trenutnoJeAktivan)
			{
				component2.trenutnoJeAktivan = true;
				continue;
			}
			if (component2.Type == 2)
			{
				((Component)((Component)component2).transform.GetChild(0)).GetComponent<BarrelExplode>().ObnoviBure();
			}
			component2.slobodanEntitet = true;
		}
		yield return (object)new WaitForSeconds(0.02f);
		if (!LevelFactory.trebaFinish)
		{
			LevelFactory.instance.Reposition();
		}
	}

	private void TrenutnoSeKoristi()
	{
		for (int i = 0; i < enemyPool.childCount; i++)
		{
			EntityProperties component = ((Component)enemyPool.GetChild(i)).GetComponent<EntityProperties>();
			if (!component.trenutnoJeAktivan)
			{
				component.trenutnoJeAktivan = true;
			}
		}
		for (int j = 0; j < environmentPool.childCount; j++)
		{
			EntityProperties component = ((Component)environmentPool.GetChild(j)).GetComponent<EntityProperties>();
			if (!component.trenutnoJeAktivan)
			{
				component.trenutnoJeAktivan = true;
			}
		}
		for (int k = 0; k < coinsPool.childCount; k++)
		{
			EntityProperties component = ((Component)coinsPool.GetChild(k)).GetComponent<EntityProperties>();
			if (!component.trenutnoJeAktivan)
			{
				component.trenutnoJeAktivan = true;
			}
		}
		for (int l = 0; l < specialPool.childCount; l++)
		{
			EntityProperties component = ((Component)specialPool.GetChild(l)).GetComponent<EntityProperties>();
			if (!component.trenutnoJeAktivan)
			{
				component.trenutnoJeAktivan = true;
			}
		}
	}
}
