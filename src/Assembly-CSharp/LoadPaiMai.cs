using UnityEngine;

public class LoadPaiMai : MonoBehaviour
{
	private void Awake()
	{
		ResManager.inst.LoadPrefab("PaiMai/PaiMaiPanel").Inst();
	}
}
