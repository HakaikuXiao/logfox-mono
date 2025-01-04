using Godot;


public partial class InGameLogView : RichTextLabel
{

	public override void _Ready()
	{
		BbcodeEnabled = true;
		SetAnchorsAndOffsetsPreset(LayoutPreset.FullRect);
		Visible = false;
		InputMap.AddAction("toggle_log_view");
		InputMap.ActionAddEvent("toggle_log_view", new InputEventKey() { Keycode = Key.F3 });
		GetTree().Root.GetNode<Autoload>("LogFoxAutoload").Connect("NewLog", new Callable(this, "AddLog"));
		LogFox.Info("_Ready");
	}
	public override void _Input(InputEvent @event)
	{
		if (Input.IsActionJustPressed("toggle_log_view"))
		{
			Visible = !Visible;
		}
	}
	private void AddLog(string msg)
	{
		AppendText(msg + "\n");
	}
}