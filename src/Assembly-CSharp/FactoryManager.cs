using System;
using UnityEngine;

// Token: 0x020002B3 RID: 691
public class FactoryManager : MonoBehaviour
{
	// Token: 0x06001862 RID: 6242 RVA: 0x000AB4FE File Offset: 0x000A96FE
	private void Awake()
	{
		FactoryManager.inst = this;
	}

	// Token: 0x06001863 RID: 6243 RVA: 0x000AB506 File Offset: 0x000A9706
	private void Start()
	{
		this.npcFactory = new NPCFactory();
		this.createNewPlayerFactory = new CreateNewPlayerFactory();
		this.loadPlayerDateFactory = new LoadPlayerDateFactory();
		this.SaveLoadFactory = new SaveLoadFactory();
	}

	// Token: 0x04001369 RID: 4969
	public static FactoryManager inst;

	// Token: 0x0400136A RID: 4970
	public NPCFactory npcFactory;

	// Token: 0x0400136B RID: 4971
	public CreateNewPlayerFactory createNewPlayerFactory;

	// Token: 0x0400136C RID: 4972
	public LoadPlayerDateFactory loadPlayerDateFactory;

	// Token: 0x0400136D RID: 4973
	public SaveLoadFactory SaveLoadFactory;
}
