using System;
using UnityEngine;

// Token: 0x02000611 RID: 1553
public class AvatarShowHpDamage : MonoBehaviour
{
	// Token: 0x060026A6 RID: 9894 RVA: 0x0012F144 File Offset: 0x0012D344
	public virtual void show(int _demage, int type = 0)
	{
		if (RoundManager.instance == null)
		{
			Debug.LogError("RoundManager为空，不显示伤害");
			return;
		}
		if (!RoundManager.instance.IsVirtual)
		{
			string text = "";
			int type2 = 0;
			if (type == 0)
			{
				if (_demage > 0)
				{
					type2 = 1;
					text = "-" + _demage;
					Debug.Log("伤害:" + text);
				}
				else if (_demage == 0)
				{
					text = "";
				}
				else
				{
					type2 = 2;
					text = "+" + Math.Abs(_demage);
					Debug.Log("加血:" + text);
				}
			}
			else if (type == 1)
			{
				type2 = 3;
				text = "-" + _demage;
				Debug.Log("护盾:" + text);
			}
			this.SetText(text, type2);
		}
	}

	// Token: 0x060026A7 RID: 9895 RVA: 0x0001ED1E File Offset: 0x0001CF1E
	public virtual void show(string text)
	{
		this.SetText(text, 0);
	}

	// Token: 0x060026A8 RID: 9896 RVA: 0x0012F210 File Offset: 0x0012D410
	public virtual void SetText(string _demage, int type = 0)
	{
		GameObject gameObject = Object.Instantiate<GameObject>(this.DamageTemp);
		Vector3 customOffset;
		if (this.UseCustomOffset)
		{
			customOffset = this.CustomOffset;
		}
		else
		{
			customOffset..ctor(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f);
		}
		gameObject.transform.position = base.transform.position + customOffset;
		Transform child = gameObject.transform.GetChild(type);
		child.GetComponent<TextMesh>().text = _demage;
		child.gameObject.SetActive(true);
	}

	// Token: 0x040020F6 RID: 8438
	public GameObject DamageTemp;

	// Token: 0x040020F7 RID: 8439
	public bool UseCustomOffset;

	// Token: 0x040020F8 RID: 8440
	public Vector3 CustomOffset;
}
