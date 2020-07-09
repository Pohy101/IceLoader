using MelonLoader;
using IceModSystem;
using System.Collections.Generic;
using System.IO;
using Logger;

namespace IceLoader
{
    public class Main : MelonMod
    {
        public static List<VRmod> Mods = new List<VRmod>();

        public override void OnApplicationStart()
        {
            new IceMod.ServerCheck().Init();

            foreach (var item in Mods)
                item.OnEarlierStart();
        }

        public override void OnUpdate()
        {
            foreach (var item in Mods)
                item.OnUpdate();
        }

        public override void OnGUI()
        {
            foreach (var item in Mods)
                item.OnGUI();
        }

        public override void OnFixedUpdate()
        {
            foreach (var item in Mods)
                item.OnFixedUpdate();
        }

        public override void OnLateUpdate()
        {
            foreach (var item in Mods)
                item.OnLateUpdate();
        }

        public override void VRChat_OnUiManagerInit()
        {
            foreach (var item in Mods)
                item.OnStart();
        }

        public override void OnApplicationQuit()
        {
            foreach (var item in Mods)
                item.OnQuit();
        }
    }
}
