using ImGuiNET;
using RetroFrame.UI;

namespace chip8.UI;

public class StackPanel : EditorPanel
{
    public StackPanel(RetroFrame.RetroFrame retroFrame) : base(retroFrame)
    {
    }

    public override string Name { get; } = "Stack";
    public override void Draw()
    {
        if (Open && ImGui.Begin(Name, ref Open))
        {

            
            ImGui.End();
        }
    }
}