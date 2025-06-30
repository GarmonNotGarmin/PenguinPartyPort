using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using PenguinPartyPort.Content.Items.Tools;

namespace PenguinPartyPort.Common.Systems;

public class PointerDraw : ModSystem
{
    public override void PostDrawTiles()
    {
        Player player = Main.LocalPlayer;

        if (player.HeldItem.type != ModContent.ItemType<PointerItem>())
            return;

        var pointer = player.GetModPlayer<Pointer>();

        Point Point1 = pointer.Point1;
        Point Point2 = pointer.Point2;
        Point mousePoint = pointer.MousePoint;

        Texture2D texture = ModContent.Request<Texture2D>("PenguinPartyPort/Assets/PointerHighlight").Value;
        Texture2D texturePoint1 = ModContent.Request<Texture2D>("PenguinPartyPort/Assets/Point1").Value;
        Texture2D texturePoint2 = ModContent.Request<Texture2D>("PenguinPartyPort/Assets/Point2").Value;

        //the stuff inside Begin() apparently fixes the textures not drawing properly when zooming in????? whatever
        Main.spriteBatch.Begin(
            SpriteSortMode.Deferred,
            BlendState.AlphaBlend,
            SamplerState.LinearClamp,
            DepthStencilState.None,
            RasterizerState.CullCounterClockwise,
            null,
            Main.GameViewMatrix.TransformationMatrix
        );

        if (Point1 != new Point(-1, -1) && Point2 != new Point(-1, -1) && Point1 != Point2)
            DrawRectangle(Point1, Point2, texture, Color.Yellow * 0.5f);

        if (Point1 != new Point(-1, -1))
            DrawPointer(Point1, texturePoint1, Color.Red * 0.5f);
        if (Point2 != new Point(-1, -1) && Point2 != Point1)
            DrawPointer(Point2, texturePoint2, Color.Green * 0.5f);

        DrawPointer(mousePoint, texture, Color.Yellow * 0.5f);
        Main.spriteBatch.End();
    }

    private void DrawPointer(Point point, Texture2D texture, Color color)
    {
        Vector2 pos = new Vector2(point.X * 16, point.Y * 16) - Main.screenPosition;
        Main.spriteBatch.Draw(texture, pos, color);
    }

    private void DrawRectangle(Point point1, Point point2, Texture2D texture, Color color)
    {
        Point topLeft = new(Math.Min(point1.X, point2.X), Math.Min(point1.Y, point2.Y));
        Point bottomRight = new(Math.Max(point1.X, point2.X), Math.Max(point1.Y, point2.Y));

        for (int i = topLeft.X; i <= bottomRight.X; i++)
            for (int j = topLeft.Y; j <= bottomRight.Y; j++)
                Main.spriteBatch.Draw(texture, new Vector2(i * 16, j * 16) - Main.screenPosition, color);
    }
}
