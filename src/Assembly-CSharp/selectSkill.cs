using GUIPackage;

public class selectSkill : prepareSelect
{
	public Skill_UI obj;

	private void Start()
	{
	}

	public void setMaxPage(int max)
	{
		maxPage = max;
	}

	public override void addNowPage()
	{
		obj.nowIndex++;
		nowIndex = obj.nowIndex;
		if (nowIndex >= maxPage)
		{
			nowIndex = 0;
			obj.nowIndex = 0;
		}
	}

	public override void reduceIndex()
	{
		obj.nowIndex--;
		nowIndex--;
		if (nowIndex < 0)
		{
			nowIndex = maxPage - 1;
			obj.nowIndex = maxPage - 1;
		}
	}

	public override void resetObj()
	{
		setPageTetx();
	}

	public override void SetFirstPage()
	{
		obj.nowIndex = 0;
		base.SetFirstPage();
	}

	private void Update()
	{
	}
}
