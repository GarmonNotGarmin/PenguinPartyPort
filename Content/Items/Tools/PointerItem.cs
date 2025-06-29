using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using PenguinPartyPort.Common.Systems;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using System.IO.Pipelines;

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

    public override void HoldItem(Player player)
    {
        Point mousePoint = Main.MouseWorld.ToTileCoordinates();
        var pointer = player.GetModPlayer<Pointer>();

        if (wasPressingLeftClick && Main.mouseLeftRelease)
        {
            wasPressingLeftClick = false;
            if (pointer.Point1 != mousePoint)
                pointer.Point2 = mousePoint;
            if (pointer.Point2 != pointer.Point1)
                Main.NewText($"Point 2 set to tile: {mousePoint.X}, {mousePoint.Y}", 255, 255, 0);
            bool isSwapped = false;
            if (pointer.Point1.X > pointer.Point2.X)
            {
                (pointer.Point1.X, pointer.Point2.X) = (pointer.Point2.X, pointer.Point1.X);
                isSwapped = true;
            }
            if (pointer.Point1.Y > pointer.Point2.Y)
            {
                (pointer.Point1.Y, pointer.Point2.Y) = (pointer.Point2.Y, pointer.Point1.Y);
                isSwapped = true;
            }
            if (isSwapped)
                Main.NewText($"Reset points to Point1: {pointer.Point1.X}, {pointer.Point1.Y} and Point2: {pointer.Point2.X}, {pointer.Point2.Y}", 255, 255, 0);
        }

        if (Main.mouseLeft)
        {
            if (!wasPressingLeftClick)
            {
                pointer.UpdatePointer(mousePoint, mousePoint);
                wasPressingLeftClick = true;
                Main.NewText($"Point 1 set to tile: {mousePoint.X}, {mousePoint.Y}", 255, 255, 0);
            }
            else if (pointer.Point1 != mousePoint)
                pointer.Point2 = mousePoint;
        }
    }
}
