using System.Globalization;
using ImGuiNET;
using Microsoft.Xna.Framework;
using Vector2 = System.Numerics.Vector2;

namespace RetroFrame.UI.Panels;

public class PerformancePanel : EditorPanel
{
    public PerformancePanel(RetroFrame retroFrame) : base(retroFrame)
    {
        
    }

    public override string Name { get; } = "Performance";

    public override void Draw(GameTime gameTime)
    {
        ImGui.PushStyleVar(ImGuiStyleVar.WindowPadding, new Vector2(0,0));
        if (Open && ImGui.Begin(Name, ref Open))
        {
            ImGui.BeginTable("performance_counters", 2, ImGuiTableFlags.Resizable | ImGuiTableFlags.Borders);
            ImGui.TableSetupColumn("Name", ImGuiTableColumnFlags.None);
            ImGui.TableSetupColumn("Value", ImGuiTableColumnFlags.WidthStretch);
            
            ImGui.TableHeadersRow();

            RenderValue("Total Time", Math.Round(gameTime.TotalGameTime.TotalSeconds, 1).ToString(CultureInfo.InvariantCulture));
            RenderValue("Render Time", gameTime.ElapsedGameTime.TotalSeconds.ToString(CultureInfo.InvariantCulture));
            
            ImGui.EndTable();
            
            ImGui.End();
        }
        ImGui.PopStyleVar();
    }

    private void RenderValue(string name, string value)
    {
        ImGui.TableNextRow();
        ImGui.TableNextColumn();
        
        ImGui.Text(name);
        
        ImGui.TableNextColumn();
        
        ImGui.Text(value);
    }
}