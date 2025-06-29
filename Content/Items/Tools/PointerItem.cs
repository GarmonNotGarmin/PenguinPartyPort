using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using PenguinPartyPort.Common.Systems;

namespace PenguinPartyPort.Content.Items.Tools;

public class PointerItem : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 30;
        Item.height = 48;
        Item.useTime = 10;
        Item.useAnimation = 10;
        Item.useStyle = ItemUseStyleID.HoldUp;
        Item.value = 10000;
        Item.rare = ItemRarityID.Master;
        Item.UseSound = SoundID.Item1;
        Item.holdStyle = ItemHoldStyleID.HoldLamp;
        Item.useTurn = false;
        Item.channel = true;
    }

    public override bool CanUseItem(Player player) => false;

    private bool wasPressingLeftClick = false;
    private bool wasPressingRightClick = false;
    private bool wasPressingMiddleClick = false;

    public override void HoldItem(Player player)
    {
        Point mousePoint = Main.MouseWorld.ToTileCoordinates();
        var pointer = player.GetModPlayer<Pointer>();

        pointer.MousePoint = mousePoint;

        if (wasPressingLeftClick && !Main.mouseLeft)
        {
            if (pointer.Point1 != mousePoint)
                pointer.Point2 = mousePoint;
            if (pointer.Point2 != pointer.Point1)
                Main.NewText($"Point 2 set to tile: {mousePoint.X}, {mousePoint.Y}", 0, 255, 0);
            if (pointer.CheckAndSwapPoints())
                Main.NewText($"Reset points to Point1: {pointer.Point1.X}, {pointer.Point1.Y} and Point2: {pointer.Point2.X}, {pointer.Point2.Y}", 255, 255, 255);
        }

        if (Main.mouseLeft)
        {
            if (!wasPressingLeftClick && pointer.Point1 != pointer.MousePoint)
            {
                pointer.UpdatePointer(mousePoint, mousePoint);
                Main.NewText($"Point 1 set to tile: {mousePoint.X}, {mousePoint.Y}", 255, 255, 0);
            }
            else if (pointer.Point1 != mousePoint)
                pointer.Point2 = mousePoint;
        }

        if (Main.mouseRight && !wasPressingRightClick && pointer.MousePoint != pointer.Point1 && pointer.MousePoint != pointer.Point2)
        {
            pointer.Point2 = pointer.MousePoint;
            Main.NewText($"Point 2 set to tile: {pointer.Point1.X}, {pointer.Point2.Y}", 0, 255, 0);
            if (pointer.CheckAndSwapPoints())
                Main.NewText($"Reset points to Point1: {pointer.Point1.X}, {pointer.Point1.Y} and Point2: {pointer.Point2.X}, {pointer.Point2.Y}", 255, 255, 255);
        }

        if (Main.mouseMiddle && !wasPressingMiddleClick)
        {
            pointer.UpdatePointer(new(-1, -1), new(-1, -1));
            Main.NewText($"Reset points!", 255, 255, 255);
        }
        wasPressingLeftClick = Main.mouseLeft;
        wasPressingRightClick = Main.mouseRight;
        wasPressingMiddleClick = Main.mouseMiddle;
    }
}
