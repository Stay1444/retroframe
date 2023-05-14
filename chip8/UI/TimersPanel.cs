using ImGuiNET;
using RetroFrame.UI;

namespace chip8.UI;

public class TimersPanel : EditorPanel
{
    public TimersPanel(RetroFrame.RetroFrame retroFrame) : base(retroFrame)
    {
    }

    public override string Name { get; } = "Timers";
    
    public override void Draw()
    {
        if (Open && ImGui.Begin(Name, ref Open))
        {
            ImGui.End();
        }
    }
}