using Terraria.ModLoader;
using PenguinPartyPort.Common.Systems;
using Terraria;
using Microsoft.Xna.Framework;

namespace PenguinPartyPort.Common.Commands;

public class SchematicCommand : ModCommand
{
    public override CommandType Type => CommandType.Chat;

    public override string Command => "sc";

    public override string Usage => "/sc save/load <name>";

    public override string Description => "Saves the schematic to a JSON file with the specified name. Or loads the schematic from a JSON file";

    public override void Action(CommandCaller caller, string input, string[] args)
    {
        if (args.Length <= 1)
        {
            caller.Reply(Usage);
            return;
        }

        if (args[0] == "save" || args[0] == "s")
        {
            string name = args[1];
            Point pos1 = Main.LocalPlayer.GetModPlayer<Pointer>().Point1;
            Point pos2 = Main.LocalPlayer.GetModPlayer<Pointer>().Point2;
            if (pos1 == new Point(-1, -1) || pos2 == new Point(-1, -1))
            {
                caller.Reply("Please point to a valid area before saving.");
                return;
            }
            SchematicFile.Save(name, pos1, pos2);
            caller.Reply($"Schematic saved as {name}.json");
            return;
        }

        if (args[0] == "load" || args[0] == "l")
        {
            string name = args[1];
            var pointer = Main.LocalPlayer.GetModPlayer<Pointer>();
            if (pointer.Point1 == new Point(-1, -1))
            {
                caller.Reply("Please point to a valid point before loading.");
                return;
            }
            Schematic schematic = new(name);
            schematic.PasteSchematic(pointer.Point1);
            pointer.Point2 = new(pointer.Point1.X + schematic.Width - 1, pointer.Point1.Y + schematic.Height - 1);
            caller.Reply($"Loaded schematic {name}.json");
            return;
        }

        caller.Reply(Usage);
        return;
    }
}
