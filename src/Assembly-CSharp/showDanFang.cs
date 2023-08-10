using UnityEngine;

public class showDanFang : MonoBehaviour
{
	public LianDanDanFang lianDanDanFang;

	private void Start()
	{
		lianDanDanFang.InitDanFang();
	}

	public void Open()
	{
		lianDanDanFang.InitDanFang();
	}

	private void Update()
	{
	}
}
