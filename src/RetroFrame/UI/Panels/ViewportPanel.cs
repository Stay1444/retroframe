using ImGuiNET;

namespace RetroFrame.UI.Panels;

public class ViewportPanel : EditorPanel
{
    public ViewportPanel(RetroFrame retroFrame) : base(retroFrame)
    {
        
    }

    public override string Name { get; } = "Viewport";

    public override void Draw()
    {
        if (Open && ImGui.Begin("Viewport", ref Open))
        {
            ImGui.Text("Hello World");
            ImGui.End();
        }
    }
}