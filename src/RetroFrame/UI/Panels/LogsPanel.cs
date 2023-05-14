using System.Numerics;
using ImGuiNET;

namespace RetroFrame.UI.Panels;

public class LogsPanel : EditorPanel
{
    public LogsPanel(RetroFrame retroFrame) : base(retroFrame)
    {
    }
    
    public override string Name { get; } = "Logs";


    private string? _name = null;
    
    private long _unseenLogs = 0;
    private int _lastFrameCount = 0;
    public override void Draw()
    {
        _name ??= _unseenLogs > 0 ? $"Logs ({_unseenLogs})" : "Logs";

        ImGui.PushStyleVar(ImGuiStyleVar.WindowPadding, new Vector2(0,0));

        if (Open && ImGui.Begin($"{_name}###logs", ref Open, _unseenLogs > 0 ? ImGuiWindowFlags.UnsavedDocument : ImGuiWindowFlags.None))
        {
            if (_unseenLogs != 0)
            {
                _unseenLogs = 0;
                _name = null;
            }
            _lastFrameCount = RetroFrame.VirtualLogger.Logs.Count;
            
            ImGui.BeginTable("logs", 3, ImGuiTableFlags.Resizable);

            ImGui.TableSetupColumn("Timestamp", ImGuiTableColumnFlags.None, 0.1f);
            ImGui.TableSetupColumn("Log Level", ImGuiTableColumnFlags.None, 0.1f);
            ImGui.TableSetupColumn("Message", ImGuiTableColumnFlags.WidthStretch);

            ImGui.TableHeadersRow();

            for (int i = 0; i < RetroFrame.VirtualLogger.Logs.Count; i++)
            {
                ImGui.TableNextRow();
                ImGui.TableNextColumn();
                ImGui.Text(RetroFrame.VirtualLogger.Logs[i].DateTimeOffset.ToString("HH:mm:ss.fff"));
                ImGui.TableNextColumn();
                ImGui.Text(RetroFrame.VirtualLogger.Logs[i].LogLevel.ToString());
                ImGui.TableNextColumn();
                ImGui.Text(RetroFrame.VirtualLogger.Logs[i].Message);

                if (i % 2 == 0)
                {
                    ImGui.TableSetBgColor(ImGuiTableBgTarget.RowBg0, ImGui.ColorConvertFloat4ToU32(new Vector4(1f, 1f, 1f, 0.03f)));
                }
            }

            ImGui.EndTable();

            ImGui.End();
        }
        else
        {
            _unseenLogs = RetroFrame.VirtualLogger.Logs.Count - _lastFrameCount;
            _name = null;
        }
        
        ImGui.PopStyleVar();
    }
}