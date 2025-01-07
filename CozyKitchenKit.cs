using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using ExtremelySimpleLogger;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MLEM.Data;
using MLEM.Data.Content;
using MLEM.Textures;
using MLEM.Ui;
using MLEM.Ui.Elements;
using TinyLife;
using TinyLife.Actions;
using TinyLife.Emotions;
using TinyLife.Mods;
using TinyLife.Objects;
using TinyLife.Utilities;
using TinyLife.World;
using TinyLife.Tools;
using Action = TinyLife.Actions.Action;
using MLEM.Misc;

namespace CozyKitchenKit;

public class CozyKitchenKit : Mod
{

    // the logger that we can use to log info about this mod
    public static Logger Logger { get; private set; }

    // visual data about this mod
    public override string Name => "Cozy Kitchen Kit";
    public override string Description => "Live your cottagecore dreams! - By Gindew v1.3";
    public override string IssueTrackerUrl => "https://x.com/RedGindew";
    public override string TestedVersionRange => "[0.45.0]";
    private Dictionary<Point, TextureRegion> uiTextures;
    public override TextureRegion Icon => this.uiTextures[new Point(0, 0)];

    public List<string> budgetOvenDes;


    public override void Initialize(Logger logger, RawContentManager content, RuntimeTexturePacker texturePacker, ModInfo info)
    {
        CozyKitchenKit.Logger = logger;
        texturePacker.Add(new UniformTextureAtlas(content.Load<Texture2D>("UITex"), 8, 8), r => this.uiTextures = r, 1, true);
    }

    public override void AddGameContent(GameImpl game, ModInfo info)
    {
        FurnitureType.Register(new FurnitureType.TypeSettings("CozyKitchenKit.CozyCounter", new Point(1, 1), ObjectCategory.Counter, 80, new ColorScheme[] { ColorScheme.SimpleWood, ColorScheme.SimpleWood, ColorScheme.White })
        {
            Icon = this.Icon,
            Tab = (FurnitureTool.Tab.Kitchen),
            DefaultRotation = MLEM.Maths.Direction2.Right,
            ConstructedType = typeof(CornerFurniture.Counter),
            Colors = new ColorSettings(ColorScheme.SimpleWood, ColorScheme.SimpleWood, ColorScheme.White) { Defaults = new int[] { 1, 0, 0 }, PreviewName = "CozyKitchenKit.CozyCounter" },
            ObjectSpots = ObjectSpot.CounterSpots(false).ToArray()
        });
        FurnitureType.Register(new FurnitureType.TypeSettings("CozyKitchenKit.CozyFridge", new Point(1, 1), ObjectCategory.Fridge, 800, new ColorScheme[] { ColorScheme.MutedPastels, ColorScheme.White })
        {
            Icon = this.Icon,
            Tab = (FurnitureTool.Tab.Kitchen),
            Colors = new ColorSettings(ColorScheme.MutedPastels, ColorScheme.White){ Defaults = new int[] { 3, 0 }},
            DefaultRotation = MLEM.Maths.Direction2.Right
        });
                FurnitureType.Register(new FurnitureType.TypeSettings("CozyKitchenKit.CozyOven", new Point(1, 1), ObjectCategory.Stove | ObjectCategory.Oven, 600)
        {
            Icon = this.Icon,
            Reliability = 5,
            Tab = (FurnitureTool.Tab.Kitchen),
            ActionSpots = new[] { new ActionSpot(Vector2.Zero, MLEM.Maths.Direction2.Up) },
            Colors = new ColorSettings(ColorScheme.MutedPastels, ColorScheme.White){ Defaults = new int[] { 3, 0 }, Map = new int[] {0, 0, 1, 1} },
            ObjectSpots = ObjectSpot.CounterSpots(true),
            DefaultRotation = MLEM.Maths.Direction2.Right,
            ElectricityRating = 2
        });
        FurnitureType.Register(new FurnitureType.TypeSettings("CozyKitchenKit.CozyShelf", new Point(1, 1), ObjectCategory.NonColliding | ObjectCategory.WallHanging, 800, new ColorScheme[] { ColorScheme.SimpleWood, ColorScheme.White })
        {
            Icon = this.Icon,
            Tab = (FurnitureTool.Tab.Kitchen),
            Colors = new ColorSettings(ColorScheme.SimpleWood, ColorScheme.White){ Defaults = new int[] { 1, 0 }, PreviewName = "CozyKitchenKit.CozyShelf" },
            DefaultRotation = MLEM.Maths.Direction2.Right
        });
        FurnitureType.Register(new FurnitureType.TypeSettings("CozyKitchenKit.CozySink", new Point(1, 1), ObjectCategory.CounterObject | ObjectCategory.DisallowedOnGround | ObjectCategory.Sink, 350, new ColorScheme[] { ColorScheme.MutedPastels, ColorScheme.White })
        {
            Icon = this.Icon,
            Tab = (FurnitureTool.Tab.Kitchen),
            Colors = new ColorSettings(ColorScheme.MutedPastels, ColorScheme.White){Defaults = new int[] { 3, 0 } },
            Reliability = 5,
            ActionSpots = new[] { new ActionSpot(Vector2.Zero, MLEM.Maths.Direction2.Up) },
            ObjectSpots = ObjectSpot.CounterSpots(true),
            DefaultRotation = MLEM.Maths.Direction2.Right,
            ElectricityRating = 2
        });
        FurnitureType.Register(new FurnitureType.TypeSettings("CozyKitchenKit.CozyCabinet", new Point(1, 1), ObjectCategory.Nothing, 800, new ColorScheme[] { ColorScheme.SimpleWood, ColorScheme.White })
        {
            Icon = this.Icon,
            Tab = (FurnitureTool.Tab.Kitchen),
            Colors = new ColorSettings(ColorScheme.SimpleWood, ColorScheme.White){ Defaults = new int[] { 1, 0 }, PreviewName = "CozyKitchenKit.CozyCabinet" },
            DefaultRotation = MLEM.Maths.Direction2.Right
        });
    }

    public override IEnumerable<string> GetCustomFurnitureTextures(ModInfo info)
    {
        yield return "CozyCounter";
        yield return "CozyFridge";
        yield return "CozyOven";
        yield return "CozyShelf";
        yield return "CozyCabinet";
        yield return "CozySink";
    }
}