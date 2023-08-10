namespace Bag;

public interface IFilterTop
{
	void Init(object data, FilterType type, string title);

	void ClickEvent();

	void CreateChild();
}
