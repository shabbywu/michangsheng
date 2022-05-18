using System;
using UnityEngine;

// Token: 0x0200014D RID: 333
public class CharacterSkillDeployer : MonoBehaviour
{
	// Token: 0x06000C18 RID: 3096 RVA: 0x00096090 File Offset: 0x00094290
	private void Start()
	{
		if (base.gameObject.GetComponent<CharacterStatus>())
		{
			this.character = base.gameObject.GetComponent<CharacterStatus>();
		}
		if (base.gameObject.GetComponent<CharacterAttack>())
		{
			this.characterAttack = base.gameObject.GetComponent<CharacterAttack>();
		}
	}

	// Token: 0x06000C19 RID: 3097 RVA: 0x0000E224 File Offset: 0x0000C424
	public void DeploySkill(int index)
	{
		this.indexSkill = index;
		this.DeploySkill();
	}

	// Token: 0x06000C1A RID: 3098 RVA: 0x000960E4 File Offset: 0x000942E4
	public void DeploySkill()
	{
		if (this.Skill.Length != 0 && this.Skill[this.indexSkill] != null && this.character != null && this.character.SP >= this.ManaCost[this.indexSkill])
		{
			Object.Instantiate<GameObject>(this.Skill[this.indexSkill], base.transform.position, base.transform.rotation).transform.forward = base.transform.forward;
			this.character.SP -= this.ManaCost[this.indexSkill];
		}
	}

	// Token: 0x06000C1B RID: 3099 RVA: 0x0000E233 File Offset: 0x0000C433
	public void DeployWithAttacking(int index)
	{
		this.indexSkill = index;
		this.attackingSkill = true;
	}

	// Token: 0x06000C1C RID: 3100 RVA: 0x0000E243 File Offset: 0x0000C443
	public void DeployWithAttacking()
	{
		this.attackingSkill = true;
	}

	// Token: 0x06000C1D RID: 3101 RVA: 0x0000E24C File Offset: 0x0000C44C
	private void Update()
	{
		if (this.attackingSkill && this.characterAttack.Activated)
		{
			this.DeploySkill();
			this.attackingSkill = false;
			this.characterAttack.Activated = false;
		}
	}

	// Token: 0x04000921 RID: 2337
	public GameObject[] Skill;

	// Token: 0x04000922 RID: 2338
	public int[] ManaCost;

	// Token: 0x04000923 RID: 2339
	public Texture2D[] SkillIcon;

	// Token: 0x04000924 RID: 2340
	private CharacterStatus character;

	// Token: 0x04000925 RID: 2341
	private CharacterAttack characterAttack;

	// Token: 0x04000926 RID: 2342
	private bool attackingSkill;

	// Token: 0x04000927 RID: 2343
	public int indexSkill;
}
