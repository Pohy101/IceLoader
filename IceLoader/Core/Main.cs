using MelonLoader;
using IceModSystem;
using System.Collections.Generic;

namespace IceLoader
{
    public class Main : MelonMod
    {
        public static List<VRmod> Addons = new List<VRmod>();

        public override void OnApplicationStart()
        {
            Addons.Add(new IceMod.ServerCheck());
        }

        public override void OnUpdate()
        {
            foreach (var item in Addons)
                item.OnUpdate();
        }

        public override void OnGUI()
        {
            foreach (var item in Addons)
                item.OnGUI();
        }

        public override void OnFixedUpdate()
        {
            foreach (var item in Addons)
                item.OnFixedUpdate();
        }

        public override void OnLateUpdate()
        {
            foreach (var item in Addons)
                item.OnLateUpdate();
        }

        public override void VRChat_OnUiManagerInit()
        {
            foreach (var item in Addons)
                item.OnStart();
        }

        public override void OnApplicationQuit()
        {
            foreach (var item in Addons)
                item.OnQuit();
        }
    }
}
