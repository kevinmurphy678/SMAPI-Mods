using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using StardewModdingAPI;
using StardewModdingAPI.Inheritance;
using StardewModdingAPI.Inheritance.Menus;
using StardewValley.Menus;

namespace StardewTestMod
{
    public class TestMod : Mod
    {
        public override string Name
        {
            get { return "Test Mod"; }
        }

        public override string Authour
        {
            get { return "Zoryn Aaron"; }
        }

        public override string Version
        {
            get { return "0.0.1Test"; }
        }

        public override string Description
        {
            get { return "A Test Mod"; }
        }

        public override void Entry()
        {
            Console.WriteLine("Test Mod Has Loaded");
            Program.LogError("Test Mod can call to Program.cs in the API");
            Program.LogColour(ConsoleColor.Magenta, "Test Mod is just a tiny DLL file in AppData/Roaming/StardewValley/Mods");
            
            //Subscribe to an event from the modding API
            Events.KeyPressed += Events_KeyPressed;
            Events.UpdateTick += Events_UpdateTick;
            Events.Initialize += Events_Initialize;
        }

        private FieldInfo cmg;
        private bool gotGame;
        private SBobberBar sb;

        void Events_Initialize()
        {
            cmg = SGame.StaticFields.First(x => x.Name == "activeClickableMenu");
        }

        void Events_UpdateTick()
        {
            if (cmg != null && cmg.GetValue(null) != null)
            {
                if (cmg.GetValue(null).GetType() == typeof(BobberBar))
                {
                    if (!gotGame)
                    {
                        gotGame = true;
                        BobberBar b = (BobberBar)cmg.GetValue(null);
                        sb = SBobberBar.ConstructFromBaseClass(b);
                    }
                    else
                    {
                        sb.bobberPosition = Extensions.Random.Next(0, 750);
                        sb.treasure = true;
                        sb.distanceFromCatching = 0.5f;
                    }
                }
                else
                {
                    gotGame = false;
                    sb = null;
                }
            }
        }

        void Events_KeyPressed(Keys key)
        {
            Console.WriteLine("TestMod sees that the following key was pressed: " + key);
        }
    }
}