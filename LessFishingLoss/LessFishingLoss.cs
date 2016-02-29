using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using StardewModdingAPI;
using StardewModdingAPI.Inheritance;
using StardewModdingAPI.Inheritance.Menus;
using StardewValley.Menus;

namespace LessFishingLoss
{
    public class LessFishingLoss : Mod
    {
        public override string Name
        {
            get { return "Less Fishing Loss"; }
        }

        public override string Authour
        {
            get { return "Zoryn Aaron"; }
        }

        public override string Version
        {
            get { return "1.0"; }
        }

        public override string Description
        {
            get { return "Causes the amount of loss from not being over a fish to be less."; }
        }

        private FieldInfo cmg;
        private bool gotGame;
        private SBobberBar sb;

        public override void Entry()
        {
            Events.UpdateTick += Events_UpdateTick;
            Events.Initialize += Events_Initialize;
        }

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
                        if (!sb.bobberInBar)
                        {
                            //player is failing
                            sb.distanceFromCatching += 2 / 1000f;
                        }
                    }
                }
                else
                {
                    gotGame = false;
                    sb = null;
                }
            }
        }
    }
}
