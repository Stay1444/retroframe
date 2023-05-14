using ImGuiNET;
using Microsoft.Xna.Framework;
using RetroFrame.UI;
using Vector2 = System.Numerics.Vector2;

namespace chip8.UI;

public class RegistersPanel : EditorPanel
{
    public RegistersPanel(RetroFrame.RetroFrame retroFrame) : base(retroFrame)
    {
    }

    public override string Name { get; } = "Registers";

    private byte[] registers = new byte[16];
    ushort ir = 0;
    private double _timer = 0;
    public override void Draw(GameTime gameTime)
    {
        ImGui.PushStyleVar(ImGuiStyleVar.WindowPadding, new Vector2(0,0));
        bool reset = false;
        _timer += gameTime.ElapsedGameTime.TotalMilliseconds;
        if (Open && ImGui.Begin(Name, ref Open))
        {
            
            ImGui.BeginTable("variable_registers", 3, ImGuiTableFlags.Resizable | ImGuiTableFlags.Borders);
            
            ImGui.TableSetupColumn("Name", ImGuiTableColumnFlags.None);
            ImGui.TableSetupColumn("Value (DEC)", ImGuiTableColumnFlags.WidthStretch);
            ImGui.TableSetupColumn("Value (HEX)", ImGuiTableColumnFlags.WidthStretch);
            
            ImGui.TableHeadersRow();
            RenderTableRegisterUShort("Index Register", ref ir);
            
            if (_timer > 1)
            {
                ir += 1;
                reset = true;
            }
            
            for (var i = 0; i < registers.Length; i++)
            {
                RenderTableRegisterByte($"V{i:X1}", ref registers[i]);
                if (_timer > 1)
                {
                    registers[i] += 1;
                    reset = true;
                }
            } 
            
            ImGui.EndTable();
            
            ImGui.End();
        }

        if (reset) _timer = 0;    
        ImGui.PopStyleVar();
    }

    private void RenderTableRegisterByte(string name, ref byte value)
    {
        ImGui.TableNextRow();
        ImGui.TableNextColumn();
        
        ImGui.Text(name);
        
        ImGui.TableNextColumn();
        
        var columnWidth = ImGui.GetColumnWidth();
        ImGui.SetNextItemWidth(columnWidth);
        int vint = value;
        if (ImGui.InputInt($"###{name}int", ref vint, 0, 0, ImGuiInputTextFlags.EnterReturnsTrue))
        {
            value = (byte)vint;
            RetroFrame.VirtualLogger.Info($"Changed value of {name} to {value}");
        }

        ImGui.TableNextColumn();

        ImGui.Text($"0x{value:X2}");
    }

    private void RenderTableRegisterUShort(string name, ref ushort value)
    {
        ImGui.TableNextRow();
        ImGui.TableNextColumn();
        
        ImGui.Text(name);
        
        ImGui.TableNextColumn();
        
        var columnWidth = ImGui.GetColumnWidth();
        ImGui.SetNextItemWidth(columnWidth);
        int vint = value;
        if (ImGui.InputInt($"###{name}int", ref vint, 0, 0, ImGuiInputTextFlags.EnterReturnsTrue))
        {
            value = (ushort)vint;
            RetroFrame.VirtualLogger.Info($"Changed value of {name} to {value}");
        }

        ImGui.TableNextColumn();

        ImGui.Text($"0x{value:X4}");
    }
}