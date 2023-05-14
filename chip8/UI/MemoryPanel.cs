using ImGuiNET;
using RetroFrame.UI;

namespace chip8.UI;

public class MemoryPanel : EditorPanel
{
    public MemoryPanel(RetroFrame.RetroFrame retroFrame) : base(retroFrame)
    {
    }

    public override string Name { get; } = "Memory";
    public override void Draw()
    {
        if (Open && ImGui.Begin(Name, ref Open))
        {

            
            ImGui.End();
        }
    }
}