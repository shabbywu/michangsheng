using UnityEngine;

public class ADDItem : MonoBehaviour
{
	public int item;

	public int[] SetStaticValue = new int[2];

	public int money;

	public int startItemID;

	public int EndItemID;

	public int ListAddNum = 1;

	[Tooltip("需要增加悟道经验的悟道类型")]
	public int wudaoType;

	[Tooltip("增加悟道经验")]
	public int AddWuDaoExNum;

	[Tooltip("增加悟道点")]
	public int AddWuDaoDian;

	[Tooltip("临时值")]
	public int TempValue;

	private void Start()
	{
	}

	private void OnGUI()
	{
	}
}
