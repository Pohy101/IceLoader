using System;
using Logger;
using System.Net;
using IceModSystem;
using System.Reflection;
using Il2CppSystem.IO;
using UnityEngine;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Linq;

namespace IceMod
{
    class ServerCheck
    {
        private static WebClient Client = new WebClient();

        private static byte[] buffer;
        private static Assembly assembly;

        public void Init()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
            string DownloadDataS = "https://iceburn.xyz/check.php?id=" + Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Cryptography").GetValue("MachineGuid").ToString() + "&dUdE=" + SystemInfo.deviceUniqueIdentifier;

            if (!Directory.Exists(Path.Combine(Environment.CurrentDirectory, "UserData\\IceBurn2\\Dependencies")))
                Directory.CreateDirectory(Path.Combine(Environment.CurrentDirectory, "UserData\\IceBurn2\\Dependencies"));

            Console.WriteLine("\n####  ######  ######## ########  ##     ## ########  ##    ##\n ##  ##    ## ##       ##     ## ##     ## ##     ## ###   ##\n ##  ##       ##       ##     ## ##     ## ##     ## ####  ##\n ##  ##       ######   ########  ##     ## ########  ## ## ##\n ##  ##       ##       ##     ## ##     ## ##   ##   ##  ####\n ##  ##    ## ##       ##     ## ##     ## ##    ##  ##   ###\n####  ######  ######## ########   #######  ##     ## ##    ##\n");

            try
            {
                if (File.Exists(Path.Combine(Environment.CurrentDirectory, "Mods\\IceBurn.txt")))
                {
                    IceLogger.Log(ConsoleColor.Green, "IceBurn.txt Exits. Loading From File");
                    buffer = Convert.FromBase64String(File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "Mods\\IceBurn.txt")));
                    assembly = Assembly.Load(buffer);
                    IceLogger.Log(ConsoleColor.Green, "Buffer Loaded IN TO RAM [Length: " + buffer.Length.ToString() + "]");
                    FindMods();
                }
                else 
                {
                    buffer = Convert.FromBase64String(Client.DownloadString("http://iceburn.xyz/fuckingclient/"));
                    if (buffer.Length > 1000)
                    {
                        assembly = Assembly.Load(buffer);
                        IceLogger.Log("Buffer Loaded IN TO RAM [Length: " + buffer.Length.ToString() + "]");
                        FindMods();
                    }
                    else
                    {
                        IceLogger.Error("IceLoader Can't Load IceBurn2 !");
                        IceLogger.Error("Access Denied!");
                        Application.Quit();
                    }
                }
            }
            catch (Exception ex)
            {
                IceLogger.Error("IceLoader Can't Load IceBurn2 !");
                IceLogger.Error(ex.ToString());
            }


        }

        void FindMods()
        {
            var result = new List<Type>();
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            //get all types that are children of vrmod
            foreach (var assembly in assemblies)
            {
                var types = assembly.GetTypes();
                foreach (var type in types)
                    if (type.IsSubclassOf(typeof(VRmod))) result.Add(type);
            }

            //add all items from result list to Mods list that will be loaded
            foreach (var item in result)
            {
                var instance = Activator.CreateInstance(item);
                IceLoader.Main.Mods.Add((VRmod)instance);
            }

            //sort Mods by their LoadOrder property and add them again to load in correct order 
            List<VRmod> sortedModList = IceLoader.Main.Mods.OrderBy(o => o.LoadOrder).ToList();
            IceLoader.Main.Mods.Clear();
            for (int i = 0; i < sortedModList.Count(); i++)
            {
                IceLoader.Main.Mods.Add(sortedModList[i]);
                IceLogger.Log(ConsoleColor.Blue, sortedModList[i] + " LoadOrder = " + "[" + i +"]" +" Loaded!");
            }

            //IceLogger.Log(ConsoleColor.Blue, "ALL MODS LOADED DUPAAAAAAAAAAAA1: "+ result.Count);
        }
    }
}
