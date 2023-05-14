using ImGuiNET;
using RetroFrame.UI;

namespace chip8.UI;

public class KeyboardPanel : EditorPanel
{
    public KeyboardPanel(RetroFrame.RetroFrame retroFrame) : base(retroFrame)
    {
    }

    public override string Name { get; } = "Keyboard";
    public override void Draw()
    {
        if (Open && ImGui.Begin(Name, ref Open))
        {
            ImGui.End();
        }
    }
}