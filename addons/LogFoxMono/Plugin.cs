#if TOOLS
using Godot;

[Tool]
public partial class Plugin : EditorPlugin
{
	public override void _EnterTree()
	{
		AddAutoloadSingleton("LogFoxAutoload", "res://addons/LogFoxMono/Autoload.cs");
	}
	public override void _ExitTree()
	{
		RemoveAutoloadSingleton("LogFoxAutoload");
	}
}

#endif