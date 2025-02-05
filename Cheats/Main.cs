﻿using Ccc;
using Harmony12;
using Hont;
using Pathea;
using Pathea.ActorNs;
using Pathea.AppearNs;
using Pathea.BlackBoardNs;
using Pathea.ConfigNs;
using Pathea.DungeonModuleNs;
using Pathea.FavorSystemNs;
using Pathea.GuildRanking;
using Pathea.HomeViewerNs;
using Pathea.ItemBoxNs;
using Pathea.ItemSystem;
using Pathea.Missions;
using Pathea.ModuleNs;
using Pathea.NpcInstanceNs;
using Pathea.OptionNs;
using Pathea.ScenarioNs;
using Pathea.Spawn;
using Pathea.UISystemNs;
using PatheaScript;
using PatheaScriptExt;
using SimpleJSON;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityModManagerNet;
using static Harmony12.AccessTools;

namespace Cheats
{
    public static partial class Main
    {
        private static bool isDebug = true;

        public static void Dbgl(string str = "", bool pref = true)
        {
            if (isDebug)
                UnityEngine.Debug.Log((pref ? "Cheats " : "") + str);
        }
        public static bool enabled;

        public static Settings settings { get; private set; }

        private static void Load(UnityModManager.ModEntry modEntry)
        {
            settings = Settings.Load<Settings>(modEntry);

            modEntry.OnToggle = OnToggle;
            modEntry.OnGUI = OnGUI;
            modEntry.OnSaveGUI = OnSaveGUI;
            modEntry.OnUpdate = OnUpdate;

            var harmony = HarmonyInstance.Create(modEntry.Info.Id);
            harmony.PatchAll(Assembly.GetExecutingAssembly());

            //Module<ScenarioModule>.Self.EndLoadEventor += OnLoadGame;

            //SceneManager.activeSceneChanged += ChangeScene;
            /*
            assetBundle = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/untitled");
            if (assetBundle != null)
            {
            }




            AssetBundle[] bundles = Resources.FindObjectsOfTypeAll(typeof(AssetBundle)) as AssetBundle[];

            string output = "";
            foreach(AssetBundle bundle in bundles)
            {
                if(bundle.name.Contains("referenced_anim_controller"))
                {
                    UnityEngine.Object[] clips = bundle.LoadAllAssets();
                    foreach(UnityEngine.Object clip in clips)
                    {
                        output += $"{clip.name}\r\n";
                        if (clip.name == "Swimming")
                        {
                            swimClip = (AnimationClip)clip;
                            break;
                        }
                    }
                    break;
                }
            }
            Dbgl(output);



            */

            //string[] names = { "Emily", "Nora", "Phyllis", "Ginger", "Sonia", };

        }
        private static BoolTrue myBoolTrue = new BoolTrue();


        private static bool noclip = false;

        private static ItemBox CreateTreasure(Transform point, string prefabPath, int rewardId, bool playActorAnim, float scale)
        {
            GameObject gameObject = Singleton<ResMgr>.Instance.LoadSyncByType<GameObject>(AssetType.Mission, prefabPath);
            if (gameObject == null)
            {
                return null;
            }
            GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(gameObject);

            Transform mItemBoxRoot = new GameObject("ItemBox").transform;
            mItemBoxRoot.parent = TransRoot.self.transform;
            mItemBoxRoot.localPosition = Vector3.zero;

            gameObject2.transform.parent = mItemBoxRoot;
            ItemBox component = gameObject2.GetComponent<ItemBox>();
            if (component == null)
            {
                Dbgl("item box is null!");
                return null;
            }
            Dbgl("Spawning chest!");
            component.name += "_cheat_a";
            component.path = prefabPath;
            component.SetItems(rewardId, playActorAnim, scale);
            component.gamePos = point.position;
            component.transform.position = point.position;
            component.transform.rotation = point.rotation;
            return component;
        }
        private static void OnUpdate(UnityModManager.ModEntry arg1, float arg2)
        {
            if (Input.GetKeyDown(","))
            {
                float scale = 1f;
                int rewardId = 6601;


                CreateTreasure(Player.Self.actor.transform, OtherConfig.Self.UnlimitedRewardBox[3], rewardId, false, scale);
            }
            return;
            if (Input.GetKeyDown(","))
            {
                GameObject[] gos = SceneManager.GetActiveScene().GetRootGameObjects();
                string output = "";
                foreach(GameObject go in gos)
                {
                    output += $"GO: {go.name} {go.transform.position}\r\n";
                }
                Dbgl(output);
                //lUnityEngine.Object.Instantiate
            }
            return;
            if (Input.GetKeyDown(",") && Module<Player>.Self.actor != null)
            {
                GameObject[] roots = SceneManager.GetActiveScene().GetRootGameObjects();
                List<Collider> cols = new List<Collider>();
                foreach (GameObject go in roots)
                {
                    Collider[] colliders = go.GetComponentsInChildren<Collider>();
                    cols.AddRange(colliders);
                }

                noclip = !noclip;
                Rigidbody component = Module<Player>.Self.actor.GetComponent<Rigidbody>();
                if (component != null)
                {
                    component.useGravity = !noclip;
                }
                Module<Player>.Self.actor.IgnoreCollision(cols, noclip);
            }

            return;
            if (Input.GetKeyDown(","))
            {
                Module<Player>.Self.bag.AddItem(ItemObject.CreateItem(3000319, 1), true);
            }
            return;
            if (Input.GetKeyDown("."))
            {
                HUDFPS component = Module<TestClick>.Self.gameObject.GetComponent<HUDFPS>();
                if (component != null)
                {
                    UnityEngine.Object.Destroy(component);
                }
                else
                {
                    Module<TestClick>.Self.gameObject.AddComponent<HUDFPS>();
                }

            }
            if (Input.GetKeyDown("/"))
            {
                DeviceInfo component = Module<TestClick>.Self.gameObject.GetComponent<DeviceInfo>();
                if (component != null)
                {
                    UnityEngine.Object.Destroy(component);
                }
                else
                {
                    Module<TestClick>.Self.gameObject.AddComponent<DeviceInfo>();
                }
            }
            if (Input.GetKeyDown("'"))
            {
                GraphicCtr component = Module<TestClick>.Self.GetComponent<GraphicCtr>();
                if (component)
                {
                    UnityEngine.Object.Destroy(component);
                }
                else
                {
                    Module<TestClick>.Self.gameObject.AddComponent<GraphicCtr>();
                }
            }

            return;
            if (Input.GetKeyDown(","))
            {
                Module<Player>.Self.bag.ChangeMoney(100000, true, 0);
            }
            if (Input.GetKeyDown("."))
            {
                Module<Player>.Self.bag.AddItem(ItemObject.CreateItem(1001300, 1), true);
                Module<Player>.Self.bag.AddItem(ItemObject.CreateItem(1001301, 1), true);
                Module<Player>.Self.bag.AddItem(ItemObject.CreateItem(3044001, 1), true);
                Module<Player>.Self.bag.AddItem(ItemObject.CreateItem(3044002, 1), true);
                Module<Player>.Self.bag.AddItem(ItemObject.CreateItem(3044003, 1), true);
                Module<Player>.Self.bag.AddItem(ItemObject.CreateItem(3044004, 1), true);
                Module<Player>.Self.bag.AddItem(ItemObject.CreateItem(4000328, 1), true);
                //Module<Player>.Self.bag.AddItem(ItemObject.CreateItem(5000028, 1), true);
                //Module<Player>.Self.bag.AddItem(ItemObject.CreateItem(5000029, 1), true);
                //Module<Player>.Self.bag.AddItem(ItemObject.CreateItem(5000131, 1), true);
                //Module<Player>.Self.bag.AddItem(ItemObject.CreateItem(5010004, 1), true);
                Module<Player>.Self.bag.AddItem(ItemObject.CreateItem(6000002, 1), true);
                Module<Player>.Self.bag.AddItem(ItemObject.CreateItem(6000019, 1), true);
                Module<Player>.Self.bag.AddItem(ItemObject.CreateItem(6000020, 1), true);
                Module<Player>.Self.bag.AddItem(ItemObject.CreateItem(6000021, 1), true);
                Module<Player>.Self.bag.AddItem(ItemObject.CreateItem(6000024, 1), true);
                Module<Player>.Self.bag.AddItem(ItemObject.CreateItem(6000025, 1), true);
                Module<Player>.Self.bag.AddItem(ItemObject.CreateItem(6000026, 1), true);
                Module<Player>.Self.bag.AddItem(ItemObject.CreateItem(6000027, 1), true);
                Module<Player>.Self.bag.AddItem(ItemObject.CreateItem(6000028, 1), true);
                Module<Player>.Self.bag.AddItem(ItemObject.CreateItem(6000029, 1), true);
                Module<Player>.Self.bag.AddItem(ItemObject.CreateItem(6000030, 1), true);
                Module<Player>.Self.bag.AddItem(ItemObject.CreateItem(7000008, 1), true);
                Module<Player>.Self.bag.AddItem(ItemObject.CreateItem(7000023, 1), true);
                Module<Player>.Self.bag.AddItem(ItemObject.CreateItem(7010001, 1), true);
                Module<Player>.Self.bag.AddItem(ItemObject.CreateItem(7010002, 1), true);
                Module<Player>.Self.bag.AddItem(ItemObject.CreateItem(7010003, 1), true);
                Module<Player>.Self.bag.AddItem(ItemObject.CreateItem(7010005, 1), true);
                Module<Player>.Self.bag.AddItem(ItemObject.CreateItem(7010006, 1), true);
                Module<Player>.Self.bag.AddItem(ItemObject.CreateItem(7010007, 1), true);
                Module<Player>.Self.bag.AddItem(ItemObject.CreateItem(7010008, 1), true);
                Module<Player>.Self.bag.AddItem(ItemObject.CreateItem(7010009, 1), true);
                Module<Player>.Self.bag.AddItem(ItemObject.CreateItem(7010010, 1), true);
                Module<Player>.Self.bag.AddItem(ItemObject.CreateItem(8000010, 1), true);
                Module<Player>.Self.bag.AddItem(ItemObject.CreateItem(8000028, 1), true);
                Module<Player>.Self.bag.AddItem(ItemObject.CreateItem(8010001, 1), true);
                Module<Player>.Self.bag.AddItem(ItemObject.CreateItem(8010002, 1), true);
                Module<Player>.Self.bag.AddItem(ItemObject.CreateItem(8010003, 1), true);
                Module<Player>.Self.bag.AddItem(ItemObject.CreateItem(8010004, 1), true);
                Module<Player>.Self.bag.AddItem(ItemObject.CreateItem(8020008, 1), true);
                Module<Player>.Self.bag.AddItem(ItemObject.CreateItem(8020009, 1), true);
            }
        }


        private static void OnLoadGame()
        {
            if (!Module<FarmBuildingModeModule>.Self.IsBuildingActive(FarmBuildingEnum.Factory))
            {

                Module<FarmBuildingModeModule>.Self.SetBuildingActive(FarmBuildingEnum.Factory, true);

            }
            //DumpStuff();
        }



        private static void Teleport()
        {
            Module<ScenarioModule>.Self.TransferToScenario("PlayerHome");
        }

        private static AssetBundle assetBundle = null;
        private static Dictionary<int, Texture2D> customTextures = new Dictionary<int, Texture2D>();
        private static Dictionary<string, Texture> customClothes = new Dictionary<string, Texture>();
        private static int[] textureActors = new int[] { 4000035, 4000141, 4000006, 4000093, 4000033, 4000003 };


        private static void ChangeScene(Scene oldScene, Scene newScene)
        {
            Dbgl("new scene: " + newScene.name);

            FieldRef<Actor, SkinnedMeshRenderer> rendererByRef =
                FieldRefAccess<Actor, SkinnedMeshRenderer>("skinnedMeshRenderer");
            SkinnedMeshRenderer skinnedMeshRenderer = null;

            foreach(KeyValuePair<int, Texture2D> kvp in customTextures)
            {
                Actor a = Module<ActorMgr>.Self.Get(kvp.Key);
                Dbgl($"trying to change texture for: {a.ActorName}");
                if (a != null && a.InActiveScene && kvp.Value != null)
                {
                    Dbgl($"changing texture for: {a.ActorName}");
                    skinnedMeshRenderer = rendererByRef(a);
                    if (skinnedMeshRenderer != null)
                        skinnedMeshRenderer.material.mainTexture = kvp.Value;
                }
            }
        }




        //[HarmonyPatch(typeof(ActorEquip), "ApplyCloth")]
        static class ActorEquip_Patch
        {
            static bool Prefix(ref SkinnedMeshRenderer __result, ref ActorEquip __instance, string[] ___nudeAppearUnits,
                string[] ___equipAppearUnits, ref AppearData ___appearData, ref string ___tattooPath,
                GameObject clothRoot = null, bool showHat = true)
            {
                List<AppearUnit> list = new List<AppearUnit>();
                AppearUnit tattooTarget = null;
                for (int i = 0; i < 5; i++)
                {
                    if (___nudeAppearUnits.Length > i && ___nudeAppearUnits[i] != null)
                    {
                        AppearUnit appearUnit = null;
                        if (i < ___equipAppearUnits.Length && !string.IsNullOrEmpty(___equipAppearUnits[i]) &&
                            ((showHat && Singleton<OptionsMgr>.Self.ShowHat) || i != 1))
                        {
                            AppearUnit appearUnit2 =
                                Singleton<ResMgr>.Instance.LoadSyncByType<AppearUnit>(AssetType.Appear,
                                    ___equipAppearUnits[i]);
                            if (appearUnit2 != null)
                            {
                                Dbgl("ActorEquip path: " + ___equipAppearUnits[i]);
                                foreach (string key in customClothes.Keys)
                                {
                                    if (___equipAppearUnits[i].EndsWith(key))
                                    {
                                        appearUnit2.Smr.material.SetTexture("_MainTex", customClothes[key]);
                                    }

                                }

                                appearUnit = appearUnit2;
                            }
                        }
                        else
                        {
                            appearUnit =
                                Singleton<ResMgr>.Instance.LoadSyncByType<AppearUnit>(AssetType.Appear,
                                    ___nudeAppearUnits[i]);
                        }

                        if (appearUnit != null)
                        {
                            list.Add(appearUnit);
                            if (i == 0)
                            {
                                tattooTarget = appearUnit;
                            }
                        }
                    }
                }

                if (clothRoot == null)
                {
                    clothRoot = __instance.ClothRoot;
                }

                AppearTarget.Instance.SetRoot(clothRoot);
                SkinnedMeshRenderer result =
                    AppearTarget.Instance.BuildMesh(list, ___appearData, tattooTarget, ___tattooPath);

                MethodInfo dynMethod = __instance.GetType()
                    .GetMethod("ApplyDyboneConfigs", BindingFlags.NonPublic | BindingFlags.Instance);
                dynMethod.Invoke(__instance, new object[] {AppearTarget.Instance.BoneDic});

                __result = result;
                return false;
            }
        }


        private static void OnSaveGUI(UnityModManager.ModEntry modEntry)
        {
            settings.Save(modEntry);
        }

        private static string command;

        private static void OnGUI(UnityModManager.ModEntry modEntry)
        {


            GUILayout.BeginHorizontal();
            GUILayout.Label("Run command:", new GUILayoutOption[0]);
            command = GUILayout.TextField(command, new GUILayoutOption[0]);
            if (GUILayout.Button("Run", new GUILayoutOption[0]))
            {
                typeof(CCC_Cmd).GetMethod(command, BindingFlags.Instance | BindingFlags.NonPublic).Invoke(new CCC_Cmd(), new object[0]);
            }
            GUILayout.EndHorizontal();

            UnityEngine.Event e = UnityEngine.Event.current;


            string ts = $"Pathea.FavorSystemNs.FavorManagerDebuger, {typeof(TransRoot).Assembly}";
            Type type = Type.GetType(ts);

            type.GetMethod("OnGUI", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(Activator.CreateInstance(type), new object[] { });
            

            if (GUILayout.Button("FavorDebugger", new GUILayoutOption[]{
                        GUILayout.Width(150f)
                    }))
            {
                if (TransRoot.self.GetComponent(type) == null)
                {
                    TransRoot.self.gameObject.AddComponent(type);
                }
                else
                {
                    UnityEngine.Object.Destroy(TransRoot.self.GetComponent(type));
                }
            }

            if (GUILayout.Button("ScriptDebugger", new GUILayoutOption[]{
                        GUILayout.Width(150f)
                    }))
            {
                if (TransRoot.self.GetComponent<CCScriptDebugger>() == null)
                {
                    TransRoot.self.gameObject.AddComponent<CCScriptDebugger>();
                }
                else
                {
                    UnityEngine.Object.Destroy(TransRoot.self.GetComponent<CCScriptDebugger>());
                }
            }

            if (GUILayout.Button("Starlight", new GUILayoutOption[]{
                        GUILayout.Width(150f)
                    }))
            {
                Module<ScenarioModule>.Self.TransferToScenario("StarlightIsland");
            }
            if (GUILayout.Button("Teleport", new GUILayoutOption[]{
                        GUILayout.Width(150f)
                    }))
            {
                Teleport();
            }
            if (GUILayout.Button("New Game+", new GUILayoutOption[]{
                        GUILayout.Width(150f)
                    }))
            {
                NewGamePlus();
            }


            if (GUILayout.Button("Relationship++", new GUILayoutOption[]
            {
                GUILayout.Width(150f)
            }))
            {
                FavorObject[] fa = FavorManager.Self.GetAllFavorObjects();
                foreach (FavorObject f in fa)
                {
                    if (!f.IsDebut || f.RelationshipType == FavorRelationshipType.Couple)
                    {
                        FavorManager.Self.GainFavorValue(f.ID, -1, 100);
                    }
                }
            }

            if (GUILayout.Button("Time--", new GUILayoutOption[]{
                        GUILayout.Width(150f)
                    }))
            {
                GameDateTime dt = TimeManager.Self.DateTime.AddSeconds(-60 * 60);
                GameTimeSpan t = dt - TimeManager.Self.DateTime;
                TimeManager.Self.SetDateTime(dt, true, TimeManager.JumpingType.Max);
            }
            if (GUILayout.Button("Time++", new GUILayoutOption[]{
                        GUILayout.Width(150f)
                    }))
            {
                TimeManager.Self.JumpTimeByGameTime(60 * 60);
            }
        }

        private static void NewGamePlus()
        {
            MethodInfo dynMethod = Module<GlobleBlackBoard>.Self.GetType().GetMethod("OnInit", BindingFlags.NonPublic | BindingFlags.Instance);
            dynMethod.Invoke(Module<GlobleBlackBoard>.Self, new object[] { });

            FieldRef<Story, PsScriptMgr> ScriptMgr = FieldRefAccess<Story, PsScriptMgr>("mScriptMgr");
            ScriptMgr(Module<Story>.Self) = PsScriptMgr.Create(new CCFactory());

            /*
            FieldRef<FavorManager, Dictionary<int, FavorObject>> FavorDict = FieldRefAccess<FavorManager, Dictionary<int, FavorObject>>("mFavorDict");
            Dictionary<int, FavorObject> mFavorDict = FavorDict(Module<FavorManager>.Self);
            Dictionary<int, FavorObject> tFavorDict = new Dictionary<int, FavorObject>();
            foreach(KeyValuePair<int,FavorObject> p in mFavorDict)
            {
                int id = p.Key;
                FavorObject f = p.Value;
                tFavorDict.Add(id,new FavorObject(id, f.NpcType));
            }
            FavorDict(Module<FavorManager>.Self) = tFavorDict;
            */

            MethodInfo dynMethod2 = Module<FavorManager>.Self.GetType().GetMethod("OnInit", BindingFlags.NonPublic | BindingFlags.Instance);
            dynMethod2.Invoke(Module<FavorManager>.Self, new object[] { });

            MethodInfo dynMethod3 = MissionManager.GetInstance.GetType().GetMethod("OnInit", BindingFlags.NonPublic | BindingFlags.Instance);
            dynMethod3.Invoke(MissionManager.GetInstance, new object[] { });

            MethodInfo dynMethod4 = Module<SceneItemManager>.Self.GetType().GetMethod("OnInit", BindingFlags.NonPublic | BindingFlags.Instance);
            dynMethod4.Invoke(Module<SceneItemManager>.Self, new object[] { });

            Module<TimeManager>.Self.SetDateTime(new Hont.GameDateTime(1, 1, 1, 3, 0, 0), true, TimeManager.JumpingType.System);


        }

        // Called when the mod is turned to on/off.
        static bool OnToggle(UnityModManager.ModEntry modEntry, bool value /* active or inactive */)
        {
            enabled = value;
            return true; // Permit or not.
        }
        /*
        [HarmonyPatch(typeof(PackageUIBase), "ShowSplitWindow")]
        static class PackageUIBase_BagSplit
        {
            static void Prefix(ref Action<int> Confirm, ItemObject item, PackageUIBase __instance, bool ___IsSplitMode)
            {
                if (item != null && item.Number > 1)
                {
                    Confirm = delegate (int num)
                    {
                        ItemObject newItem = ItemObject.CreateItem(item.ItemDataId, num);
                        if (Module<Player>.Self.bag.AddItemFromSplit(newItem, false))
                        {
                            item.ChangeNumber(-num);
                            __instance.FreshCurpageItem();
                            __instance.playerItemBar.FreshItem();
                        }
                    };
                }
            }
        }
        */
        //[HarmonyPatch(typeof(PackageUIBase), "BagClick", new Type[] { typeof(int) })]
        static class PackageUIBase_BagSelect
        {
            static bool Prefix(PackageUIBase __instance, int obj, bool ___IsSplitMode, int ___bagIndex)
            {
                if (obj == -1)
                    return true;

                ItemObject item = Module<Player>.Self.bag.GetItem(___bagIndex, obj);
                if (item != null && ___IsSplitMode && item.Number == 1)
                {
                    Module<Player>.Self.bag.AddItem(item, true);
                    __instance.FreshCurpageItem();
                    __instance.playerItemBar.FreshItem();
                    return false;
                }
                return true;
            }
        }
        //[HarmonyPatch(typeof(Pathea.SkillNs.Factory), "CreateCmd", new Type[] { typeof(JSONNode) })]
        static class Factory_CreateCmd_Patch
        {
            static void Prefix(ref JSONNode node)
            {
                if (node["name"] == "ClashCmd" && Module<Player>.Self.actor.IsAnimTag("Chainsaw") || Module<Player>.Self.actor.IsAnimTag("Chainsaw_Cut") || Module<Player>.Self.actor.IsAnimTag("Chainsaw_Stand"))
                {
                    float asFloat = node["dmg"].AsFloat;
                    asFloat *= 4;
                    node["dmg"] = asFloat.ToString();
                }

            }
        }

        [HarmonyPatch(typeof(Player), "Jump", new Type[] { })]
        static class Player_Jump_Patch
        {
            static void Postfix()
            {
                Dbgl("new Vector3" + Module<Player>.Self.actor.gamePos.ToString() + ",");
                return;
                Module<Player>.Self.bag.ChangeMoney(1000000, true, 0);
                Module<Player>.Self.AddExp(400000, true);
                //Object here is necessary.


                //Dbgl("OtherConfig: " + string.Format(str));




                //trialDungeonMonsterConfInfo.ActorID;
                //trialDungeonMonsterConfInfo.Faction;

                //ActorAgent aa new ActorAgent(vp, vr,);
                /*
                int id = ruleData.PickOneBossMonster();
                TrialDungeonMonsterConfInfo trialDungeonMonsterConfInfo = Module<DungeonConfCreator>.Self.RandomDungeonMonsterConfList.Find((TrialDungeonMonsterConfInfo m) => m.ID == id);
                if (trialDungeonMonsterConfInfo == null)
                {
                    Debug.LogError(string.Format("monster {0} not existed!", id));
                    return;
                }
                GameObject gameObject = spawnPoint.Spawn(new object[]
                {
                    trialDungeonMonsterConfInfo.ActorID,
                    trialDungeonMonsterConfInfo.Faction
                });
                if (gameObject != null)
                {
                    Actor component = gameObject.GetComponent<Actor>();
                    this.mCreatedActorList.Add(component);
                    Module<TrialRandomDungeonManager>.Self.AddMonster(component, trialDungeonMonsterConfInfo);
                    component.killEvent += Module<TrialRandomDungeonManager>.Self.MonsterBeKilledCBMethod;
                }
                */
                //FavorObject ff = FavorManager.Self.GetFavorObject(-1);
                //bool test = ff.IsDebut;

                if (true)
                {
                    FavorObject[] fa = FavorManager.Self.GetAllFavorObjects();

                    foreach (FavorObject f in fa)
                    {
                        if (!f.IsDebut || f.RelationshipType == FavorRelationshipType.Couple)
                        {
                            FavorManager.Self.GainFavorValue(f.ID, -1, 100);
                            if (false)
                            {
                                FavorRelationshipId relationId = FavorRelationshipId.UltimateCouple;
                                relationId = FavorRelationshipId.Friend;
                                FavorUtility.SetNpcRelation(f.ID, relationId, 1);
                                relationId = FavorRelationshipId.SoulMate;
                                FavorUtility.SetNpcRelation(f.ID, relationId, 1);
                            }
                        }
                    }
                    if (false)
                    {


                        Module<Player>.Self.bag.AddItem(ItemObject.CreateItem(7000041, 100), true);
                        Module<Player>.Self.bag.AddItem(ItemObject.CreateItem(8000019, 100), true);
                        Module<Player>.Self.bag.ChangeMoney(100000, true, 0);
                    }

                }
            }
        }
        //[HarmonyPatch(typeof(Player), "Roll", new Type[] { })]
        static class Player_Roll_Patch
        {
            static void Postfix()
            {
                //Module<FavorManager>.Self.GainFavorValue(4000141, -1, 1000);
                if (false)
                    TimeManager.Self.JumpTimeByGameTime(60 * 60);



                if (true)
                {

                }

                //relationId = FavorRelationshipId.SoulMate;
                //FavorUtility.SetNpcRelation(f.ID, relationId, 1);

            }
        }


        //[HarmonyPatch(typeof(TextMgr), "Get", new Type[] { typeof(int) })]
        static class TextMgr_Get_Patch
        {
            static void Postfix(ref String __result)
            {
                if (__result.Contains("I've have"))
                {
                    __result = "[1|4000001], I've done a bunch of work, check it out!";

                }
            }
        }

        //[HarmonyPatch(typeof(FarmUpgradeMgr), "GetUpgradeItemCost", new Type[] { typeof(FarmBuildingEnum), typeof(int) })]
        static class FarmUpgradeMgr_GetUpgradeItemCost_Patch
        {
            static void Postfix(List<IdCount> __result)
            {
                if (__result == null)
                    return;
                foreach (IdCount idc in __result)
                {
                    try
                    {
                        Module<Player>.Self.bag.AddItem(ItemObject.CreateItem(idc.id, idc.count), true);
                    }
                    catch
                    {

                    }
                }
            }
        }





        //[HarmonyPatch(typeof(SceneItemMissionBoard), "InitFromData")]
        static class SceneItemMissionBoardData_Patch
        {
            static void Prefix(ref SceneItemData data)
            {
            }
            static void Postfix(SceneItemData data)
            {
                Dbgl("SIMB InitFromData zyx");
                foreach (BoardItemSeat bis in ((SceneItemMissionBoardData)data).allBoardItemSeat)
                {
                    Dbgl(bis.modelPath+" "+bis.pos.x+" "+bis.pos.y+" "+bis.pos.z+" "+bis.missionId);
                }
            }
        }        
        
        //[HarmonyPatch(typeof(FarmUpgradeMgr), "GetUpgradeMoneyCost", new Type[] { typeof(FarmBuildingEnum), typeof(int)})]
        static class FarmUpgradeMgr_GetUpgradeMoneyCost_Patch
        {
            static void Postfix(int __result)
            {
                Module<Player>.Self.bag.ChangeMoney(__result, true, 0);
            }
        }

        //[HarmonyPatch(typeof(RequirementData), "LoadDataBase", new Type[] { })]
        static class RequirementData_LoadDataBase_Patch
        {
            static void Postfix()
            {
                List<string> data = new List<string>();
                foreach(KeyValuePair<int,RequirementData> kvp in RequirementData.refDataDic)
                {
                    string a = $"{kvp.Key} {kvp.Value.itemId}  {Module<ItemDataMgr>.Self.GetItemName(kvp.Value.itemId)} Level: {kvp.Value.level}";
                    data.Add(a);
                }
                Dbgl(string.Join("\r\n", data.ToArray()));
            }
        }

        private static void DumpStuff()
        {
            var dbgs = "";
            foreach(KeyValuePair<int,RequirementGroupData> kvp in RequirementGroupData.refDataDic)
            {
                dbgs += $"\r\n{kvp.Key}\r\n\r\n";
                foreach(int i in kvp.Value.requirementIdAry)
                {
                    int item = RequirementData.refDataDic[i].itemId;
                    dbgs += $"{item} {ItemDataMgr.Self.GetItemName(item)}\r\n";
                }
            }
            Dbgl(dbgs, false);

            /*
            List<ItemBaseConfData> lista = DbReader.Read<ItemBaseConfData>(LocalDb.cur.ReadFullTable("Props_total_table"), 20);
            string dbgstr = "";

            foreach (ItemBaseConfData ai in lista)
            {
                dbgstr += ai.ID + ": " + TextMgr.GetStr(ai.NameID, -1) + "\r\n";
            }

            Dbgl(dbgstr);

            */
            /*
            List<NpcData> lista = DbReader.Read<NpcData>(LocalDb.cur.ReadFullTable("NpcRepository"), 20);
            string dbgstr = "";

            FieldRef<NpcData, string> interactStrRef = FieldRefAccess<NpcData, string>("interactStr");
            //List<FishFeedItem> list = datas(new FishFeedItem());

            foreach (NpcData ai in lista)
            {
                dbgstr += ai.id + ": " + ai.Name +" " + interactStrRef (ai)+ "\r\n";
            }

            Dbgl(dbgstr);
            */
            /*
            while (sqliteDataReader.Read())
            {
                List<int> ints = new List<int>();
                int id = sqliteDataReader.GetInt32(sqliteDataReader.GetOrdinal("ID"));
                ints.Add(id);
                ints.Add(sqliteDataReader.GetInt32(sqliteDataReader.GetOrdinal("Type")));
                ints.Add(sqliteDataReader.GetInt32(sqliteDataReader.GetOrdinal("Quantity")));
                ints.Add(sqliteDataReader.GetInt32(sqliteDataReader.GetOrdinal("PriceBase")));
                ints.Add(sqliteDataReader.GetInt32(sqliteDataReader.GetOrdinal("ExtraRewards")));
                string name = Module<ItemDataMgr>.Self.GetItemName(id);
                list.Add(name+","+string.Join(",", ints.Select(x => x.ToString()).ToArray()));

            }
            Dbgl(string.Format(string.Join("\r\n",list.ToArray())));
            */

            //MethodInfo dynMethod = (new FishFeedItem()).GetType().GetMethod("LoadData", BindingFlags.Static | BindingFlags.NonPublic);
            //List<FishFeedItem> list = (List<FishFeedItem>)dynMethod.Invoke(new FishFeedItem(), new object[] { });

            //FieldRef<FishFeedItem, List<FishFeedItem>> datas = FieldRefAccess<FishFeedItem, List<FishFeedItem>>("datas");
            //List<FishFeedItem> list = datas(new FishFeedItem());

            //string str = DumpList(list);

            //Dbgl(string.Format(str));

            //FieldRef<FishData, List<FishData>> datas2 = FieldRefAccess<FishData, List<FishData>>("datas");
            //List<FishData> list2 = datas2(new FishData());

            //MethodInfo dynMethod2 = (new FishData()).GetType().GetMethod("LoadData", BindingFlags.Static | BindingFlags.NonPublic);
            //List<FishData> list2 = (List<FishData>)dynMethod2.Invoke(new FishData(), new object[] { });

            //string str2 = DumpList(list2);

            //Dbgl(string.Format(str2));

        }

        private static string DumpList<T>(List<T> obj)
        {
            bool first = true;
            String str = "";
            foreach (object i in obj)
            {
                FieldInfo[] fields = i.GetType().GetFields(BindingFlags.Public |
                                              BindingFlags.NonPublic |
                                              BindingFlags.Instance);
                if (first)
                {
                    string[] head = new string[fields.Length];
                    for (int j = 0; j < fields.Length; j++)
                    {
                        head[j] = fields[j].Name;
                    }
                    str += string.Join(",", head) + "\r\n";
                }
                first = false;

                string name = "";

                if (fields[0].GetValue(i) != null)
                {
                    name = Module<ItemDataMgr>.Self.GetItemName((int)fields[0].GetValue(i));
                }

                string[] row = new string[fields.Length];
                for (int j = 0; j < fields.Length; j++)
                {
                    if (fields[j].GetValue(i) == null)
                        row[j] = "";
                    else row[j] = fields[j].GetValue(i).ToString();
                }
                str += name+","+ string.Join(",", row) + "\r\n";

            }
            return str;
        }

    }
}