using System;
using UnityEngine;

// Token: 0x020000D6 RID: 214
public class CharacterSkillDeployer : MonoBehaviour
{
	// Token: 0x06000B29 RID: 2857 RVA: 0x00044220 File Offset: 0x00042420
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

	// Token: 0x06000B2A RID: 2858 RVA: 0x00044273 File Offset: 0x00042473
	public void DeploySkill(int index)
	{
		this.indexSkill = index;
		this.DeploySkill();
	}

	// Token: 0x06000B2B RID: 2859 RVA: 0x00044284 File Offset: 0x00042484
	public void DeploySkill()
	{
		if (this.Skill.Length != 0 && this.Skill[this.indexSkill] != null && this.character != null && this.character.SP >= this.ManaCost[this.indexSkill])
		{
			Object.Instantiate<GameObject>(this.Skill[this.indexSkill], base.transform.position, base.transform.rotation).transform.forward = base.transform.forward;
			this.character.SP -= this.ManaCost[this.indexSkill];
		}
	}

	// Token: 0x06000B2C RID: 2860 RVA: 0x00044339 File Offset: 0x00042539
	public void DeployWithAttacking(int index)
	{
		this.indexSkill = index;
		this.attackingSkill = true;
	}

	// Token: 0x06000B2D RID: 2861 RVA: 0x00044349 File Offset: 0x00042549
	public void DeployWithAttacking()
	{
		this.attackingSkill = true;
	}

	// Token: 0x06000B2E RID: 2862 RVA: 0x00044352 File Offset: 0x00042552
	private void Update()
	{
		if (this.attackingSkill && this.characterAttack.Activated)
		{
			this.DeploySkill();
			this.attackingSkill = false;
			this.characterAttack.Activated = false;
		}
	}

	// Token: 0x04000746 RID: 1862
	public GameObject[] Skill;

	// Token: 0x04000747 RID: 1863
	public int[] ManaCost;

	// Token: 0x04000748 RID: 1864
	public Texture2D[] SkillIcon;

	// Token: 0x04000749 RID: 1865
	private CharacterStatus character;

	// Token: 0x0400074A RID: 1866
	private CharacterAttack characterAttack;

	// Token: 0x0400074B RID: 1867
	private bool attackingSkill;

	// Token: 0x0400074C RID: 1868
	public int indexSkill;
}
