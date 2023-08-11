namespace Facebook;

public class EditorFacebookLoader : FB.CompiledFacebookLoader
{
	protected override IFacebook fb => (IFacebook)(object)FBComponentFactory.GetComponent<EditorFacebook>((IfNotExist)0);
}
