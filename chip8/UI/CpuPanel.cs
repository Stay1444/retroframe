using ImGuiNET;
using RetroFrame.UI;

namespace chip8.UI;

public class CpuPanel : EditorPanel
{
    public CpuPanel(RetroFrame.RetroFrame retroFrame) : base(retroFrame)
    {
    }

    public override string Name { get; } = "CPU";
    public override void Draw()
    {
        if (Open && ImGui.Begin(Name, ref Open))
        {
            ImGui.End();
        }
    }
}