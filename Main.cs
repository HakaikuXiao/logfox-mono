using Godot;

public partial class Main : Node
{
	public override void _Ready()
	{
		LogFox.Info("_Ready");
	}
	public override void _ExitTree()
	{
		LogFox.Info("_ExitTree");
	}

}
