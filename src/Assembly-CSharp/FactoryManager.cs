using System;
using UnityEngine;

// Token: 0x020003EF RID: 1007
public class FactoryManager : MonoBehaviour
{
	// Token: 0x06001B57 RID: 6999 RVA: 0x000170C5 File Offset: 0x000152C5
	private void Awake()
	{
		FactoryManager.inst = this;
	}

	// Token: 0x06001B58 RID: 7000 RVA: 0x000170CD File Offset: 0x000152CD
	private void Start()
	{
		this.npcFactory = new NPCFactory();
		this.createNewPlayerFactory = new CreateNewPlayerFactory();
		this.loadPlayerDateFactory = new LoadPlayerDateFactory();
		this.SaveLoadFactory = new SaveLoadFactory();
	}

	// Token: 0x04001710 RID: 5904
	public static FactoryManager inst;

	// Token: 0x04001711 RID: 5905
	public NPCFactory npcFactory;

	// Token: 0x04001712 RID: 5906
	public CreateNewPlayerFactory createNewPlayerFactory;

	// Token: 0x04001713 RID: 5907
	public LoadPlayerDateFactory loadPlayerDateFactory;

	// Token: 0x04001714 RID: 5908
	public SaveLoadFactory SaveLoadFactory;
}
