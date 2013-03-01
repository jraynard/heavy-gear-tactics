#region File Description
//-----------------------------------------------------------------------------
// HeavyGearManager.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
// Modified By
// Justin Raynard
//-----------------------------------------------------------------------------
#endregion

#region Using directives
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using HeavyGear.GameLogic;
using HeavyGear.GameScreens;
using HeavyGear.Graphics;
using HeavyGear.Helpers;
using HeavyGear.Sounds;
using HeavyGear.Shaders;
using Texture = HeavyGear.Graphics.Texture;
using HeavyGear.Properties;
#endregion

namespace HeavyGear
{
    public enum PacketType
    {
        Normal,
        Deploy,
        Menu,
        Attack,
        Target,
        StartTurn,
        StartGame
    }

    /// <summary>
    /// This is the main entry class of game. Handles all game screens,
    /// which in turn handle all game logic.
    /// </summary>
    public class HeavyGearManager : BaseGame
    {
        #region Variables
        /// <summary>
        /// Game screens stack. We can easily add and remove game screens
        /// and they follow the game logic automatically. Very cool.
        /// </summary>
        private static Stack<IGameScreen> gameScreens = new Stack<IGameScreen>();

        //private static bool gameOver;
        private static Camera2D camera = new Camera2D();

        /// <summary>
        /// displays events to the user
        /// </summary>
        private static string[] messageLog = new string[]
            { 
                "",
                "",
                "",
                ""
            };

        /// <summary>
        /// Players for the game, stores unit lists and such
        /// </summary>
        //private static Player[] players; //= new Player[4]; //new Player[]{new Player(PlayerIndex.One), new Player(PlayerIndex.Two)};

        public static bool[] PlayerReady = new bool[4];
        private static Player activePlayer;
        private static Player localPlayer;
        private static Player[] players;

        private static int playerWon;
        private static bool gameOver;
        public static bool NextPlayer = false;

        private static bool deployMode = true;
        private static bool startGame = false;

        /// <summary>
        /// List of available maps
        /// </summary>
        private static Map[] maps;
        private static string[] mapFileNames;
        /// <summary>
        /// Current map we are playing on
        /// </summary>
        private static Map map;
        private static int currentTurn = 0;

        //Network variables
        private static NetworkSession networkSession;

        private static PacketWriter packetWriter = new PacketWriter();
        private static PacketReader packetReader = new PacketReader();

        private static string errorMessage;

        // How often should we send network packets?
        private const int framesBetweenPackets = 6;

        // How recently did we send the last network packet?
        private static int framesSinceLastSend;
        
        #endregion

        #region Properties
        public static Player LocalPlayer
        {
            get
            {
                return localPlayer;
            }
        }
        public static bool GameOver
        {
            get
            {
                return gameOver;
            }
        }
        public static int PlayerWon
        {
            get
            {
                return playerWon;
            }
        }
        public static bool DeployMode
        {
            get
            {
                return deployMode;
            }
        }
        public static int CurrentTurn
        {
            get
            {
                return currentTurn;
            }
        }
        public static string ErrorMessage
        {
            get
            {
                return errorMessage;
            }
            set
            {
                errorMessage = value;
            }
        }
        public static NetworkSession NetworkSession
        {
            get
            {
                return networkSession;
            }
        }
        public static int NumberOfPlayers
        {
            get
            {
                if (networkSession == null)
                    return players.Length;
                else
                    return networkSession.AllGamers.Count;
            }
        }
        public static string[] MessageLog
        {
            get
            {
                return messageLog;
            }
        }
        public static Camera2D Camera
        {
            get
            {
                return camera;
            }
        }

        public static Player ActivePlayer
        {
            get
            {
                return activePlayer;
            }
        }
        public static bool StartGame
        {
            get
            {
                return startGame;
            }
        }
        
        /// <summary>
        /// In menu
        /// </summary>
        /// <returns>Bool</returns>
        public static bool InMenu
        {
            get
            {
                return gameScreens.Count > 0 &&
                    (gameScreens.Peek().GetType() != typeof(GameScreen));
            }
        }

        /// <summary>
        /// In game?
        /// </summary>
        public static bool InGame
        {
            get
            {
                return gameScreens.Count > 0 &&
                    (gameScreens.Peek().GetType() == typeof(GameScreen));
            }
        }

        /// <summary>
        /// ShowMouseCursor
        /// </summary>
        /// <returns>Bool</returns>
        public static bool ShowMouseCursor
        {
            get
            {
                // Only if not in Game, not in splash screen!
                return gameScreens.Count > 0 &&
                    //gameScreens.Peek().GetType() != typeof(GameScreen) &&
                    gameScreens.Peek().GetType() != typeof(SplashScreen);
            }
        }

        /// <summary>
        /// Players for the game
        /// </summary>
        /// <returns>Player</returns>
        public static Player Player(int index)
        {
            if (networkSession == null)
                return players[index];
            else
                return networkSession.AllGamers[index].Tag as Player;
        }

        /// <summary>
        /// Gets the current Map
        /// </summary>
        /// <returns>Map</returns>
        public static Map Map
        {
            get
            {
                return map;
            }
            set
            {
                map = value;
            }
        }

        public static Map[] Maps
        {
            get
            {
                return maps;
            }
        }
        public static string[] MapFileNames
        {
            get
            {
                return mapFileNames;
            }
        }

        #endregion

        #region Constructor
        /// <summary>
        /// Create game
        /// </summary>
        public HeavyGearManager()
            : base("HeavyGear")
        {
#if DEBUG
            this.Components.Add(new GamerServicesComponent(this));
#endif
            //TODO: Start playing the menu music

            // Create main menu at our main entry point
            gameScreens.Push(new MainMenu());

            //But start with splash screen
            gameScreens.Push(new SplashScreen());

            //Init any static screens here

            //Init map list
            mapFileNames = Directory.GetFiles(BaseGame.Content.RootDirectory + "/content/maps/", "*.xml");
            maps = new Map[mapFileNames.Length];

            for (int i = 0; i < mapFileNames.Length; i++)
                maps[i] = new Map(mapFileNames[i]);
        }

        /// <summary>
        /// Create game for unit tests, not used for anything else.
        /// </summary>
        public HeavyGearManager(string unitTestName)
            : base(unitTestName)
        {
            // Don't add game screens here
        }

        /// <summary>
        /// Load stuff
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();

        }
        #endregion

        #region Camera
        /// <summary>
        /// Reset the camera to the center of the tile grid
        /// and reset the position of the animted sprite
        /// </summary>
        private void ResetToInitialPositions()
        {
            //set the initial position to the center of the
            //tile field
            camera.Position = new Vector2(Map.Width/2, Map.Height/2);

            CameraChanged();
        }

        /// <summary>
        /// This function is called when the camera's values have changed
        /// and is used to update the properties of the tiles and animated sprite
        /// </summary>
        public void CameraChanged()
        {
            map.VisibilityChanged = true;
            //changes have been accounted for, reset the changed value so that this
            //function is not called unnecessarily
            camera.ResetChanged();
        }
        #endregion

        #region Add game screen
        /// <summary>
        /// Add game screen
        /// </summary>
        /// <param name="gameScreen">Game screen</param>
        public static void AddGameScreen(IGameScreen gameScreen)
        {
            // Play sound for screen click
            Sound.Play(Sound.Sounds.ScreenClick);

            // Add the game screen
            gameScreens.Push(gameScreen);
        }
        #endregion

        #region Update
        /// <summary>
        /// Update
        /// </summary>
        protected override void Update(GameTime gameTime)
        {
            // Update game engine
            base.Update(gameTime);

            if (networkSession != null)
            {
                UpdateNetworkSession(gameTime);
            }

            if (InGame)
            {
                
                if (networkSession == null)
                {
                    foreach (Player player in players)
                        player.UpdateLocal();
                }

                if (camera.IsChanged)
                    CameraChanged();

                //check to see if one player has won the game
                int playerCount = 0;
                for (int i = 0; i < NumberOfPlayers; i++)
                {
                    Player player = Player(i);
                    foreach (Unit unit in player.Units)
                    {
                        if (unit.IsAlive || unit.InAnimation)
                        {
                            playerCount++;
                            playerWon = player.PlayerIndex;
                            break;
                        }
                    }
                }

                if (playerCount < 2)
                    gameOver = true;
            }
        }
        #endregion

        #region Network Methods
        

        private static void UpdateNetworkSession(GameTime gameTime)
        {
            // Is it time to send outgoing network packets?
            bool sendPacketThisFrame = false;

            framesSinceLastSend++;

            if (framesSinceLastSend >= framesBetweenPackets)
            {
                sendPacketThisFrame = true;
                framesSinceLastSend = 0;
            }

            // Update our locally controlled tanks, sending
            // their latest state at periodic intervals.
            foreach (LocalNetworkGamer gamer in networkSession.LocalGamers)
            {
                if (InGame)
                    UpdateLocalGamer(gamer, gameTime, sendPacketThisFrame);
            }

            // Pump the underlying session object.
            try
            {
                networkSession.Update();
            }
            catch (Exception e)
            {
                errorMessage = e.Message;
                networkSession.Dispose();
                networkSession = null;
            }

            // Make sure the session has not ended.
            if (networkSession == null)
                return;

            // Read any packets telling us the state of remotely controlled tanks.
            foreach (LocalNetworkGamer gamer in networkSession.LocalGamers)
            {
                ReadIncomingPackets(gamer, gameTime);
            }

            if (InGame)
            {
                // Apply prediction and smoothing to the remotely controlled tanks.
                foreach (NetworkGamer gamer in networkSession.RemoteGamers)
                {
                    Player player = gamer.Tag as Player;

                    player.UpdateRemote(framesBetweenPackets);
                }
            }

        }

        /// <summary>
        /// Helper for updating a locally controlled gamer.
        /// </summary>
        private static void UpdateLocalGamer(LocalNetworkGamer gamer, GameTime gameTime,
                                                       bool sendPacketThisFrame)
        {
            // Look up what tank is associated with this local player.
            Player player = gamer.Tag as Player;

            // Update the tank.
            player.UpdateLocal();

            // Periodically send our state to everyone in the session.
            if (sendPacketThisFrame)
            {
                if (deployMode)
                    player.WriteDeployPacket(packetWriter, gameTime);
                else
                    player.WriteNormalPacket(packetWriter, gameTime);

                gamer.SendData(packetWriter, SendDataOptions.InOrder);
            }
        }

        public static void SendMenuPacket(int playerIndex)
        {
            LocalNetworkGamer gamer = networkSession.LocalGamers[0];
            WriteMenuPacket(packetWriter, playerIndex);
            gamer.SendData(packetWriter, SendDataOptions.InOrder);
        }
        private static void SendStartTurnPacket(int playerIndex)
        {
            LocalNetworkGamer gamer = networkSession.LocalGamers[0];
            packetWriter.Write((int)PacketType.StartTurn);
            packetWriter.Write(playerIndex);
            gamer.SendData(packetWriter, SendDataOptions.ReliableInOrder);
        }
        public static void SendStartGamePacket()
        {
            LocalNetworkGamer gamer = networkSession.LocalGamers[0];
            packetWriter.Write((int)PacketType.StartGame);
            gamer.SendData(packetWriter, SendDataOptions.ReliableInOrder);
        }
        private static void ReadStartTurnPacket(PacketReader reader)
        {
            int playerIndex = packetReader.ReadInt32();
            // automatically end deploy mode since the host will send the first start turn packet at the end of deploy mode
            if (deployMode)
                deployMode = false;
            activePlayer = networkSession.AllGamers[playerIndex].Tag as Player;
            if (networkSession.AllGamers.IndexOf(networkSession.LocalGamers[0]) == playerIndex)
            {
                foreach (Unit unit in activePlayer.Units)
                {
                    if (unit.IsAlive)
                        unit.StartTurn();
                }
                NextPlayer = true;    
            }
            
        }
        
        private static void WriteMenuPacket(PacketWriter writer, int playerIndex)
        {
            /*
            byte bitField = 0;

            if (PlayerReady[0]) bitField |= 1;
            if (PlayerReady[1]) bitField |= 2;
            if (PlayerReady[2]) bitField |= 4;
            if (PlayerReady[3]) bitField |= 8;
             */
            
            string mapName = "";

            if (map != null)
                mapName = map.Name;

            writer.Write((byte)PacketType.Menu);
            writer.Write(mapName);
            writer.Write(playerIndex);
            //packetWriter.Write(PlayerReady[0]);
            //packetWriter.Write(PlayerReady[1]);
            //packetWriter.Write(PlayerReady[2]);
            //packetWriter.Write(PlayerReady[3]);
        }

        private static void ReadMenuPackets(PacketReader reader)
        {
            //byte bitField = packetReader.ReadByte();
            string mapName = reader.ReadString();
            int playerIndex = reader.ReadInt32();
            //byte bitField = packetReader.ReadByte();
            /*
            PlayerReady[0] = Convert.ToBoolean(GetBits(bitField, 0, 1));
            PlayerReady[1] = Convert.ToBoolean(GetBits(bitField, 1, 1));
            PlayerReady[2] = Convert.ToBoolean(GetBits(bitField, 2, 1));
            PlayerReady[3] = Convert.ToBoolean(GetBits(bitField, 3, 1));
            */
            if (playerIndex >= 0)
                PlayerReady[playerIndex] = !PlayerReady[playerIndex];
            if (!string.IsNullOrEmpty(mapName))
            {
                for (int i = 0; i < maps.Length; i++)
                {
                    if (maps[i].Name == mapName)
                    {
                        map = new Map(mapFileNames[i]);
                        break;
                    }
                }
            }
        }

        public static void SendAttackPacket(LocalNetworkGamer gamer, int attackingUnitIndex)
        {
            Player player = gamer.Tag as Player;

            packetWriter.Write((byte)PacketType.Attack);

            packetWriter.Write(messageLog[0]);
            packetWriter.Write(messageLog[1]);
            packetWriter.Write(messageLog[2]);
            packetWriter.Write(messageLog[3]);

            player.WriteAttackPacket(packetWriter, attackingUnitIndex);

            gamer.SendData(packetWriter, SendDataOptions.ReliableInOrder);
        }

        public static void SendTargetPacket(LocalNetworkGamer gamer, int targetedUnitIndex)
        {
            Player player = gamer.Tag as Player;

            packetWriter.Write((byte)PacketType.Target);

            packetWriter.Write(messageLog[0]);
            packetWriter.Write(messageLog[1]);
            packetWriter.Write(messageLog[2]);
            packetWriter.Write(messageLog[3]);

            player.WriteTargetPacket(packetWriter, targetedUnitIndex);

            gamer.SendData(packetWriter, SendDataOptions.ReliableInOrder);
        }

        

        private static void ReadNormalPackets(PacketReader reader, NetworkGamer sender, GameTime gameTime)
        {
            // Look up the player associated with whoever sent this packet.
            Player player = sender.Tag as Player;

            // Estimate how long this packet took to arrive.
            TimeSpan latency = networkSession.SimulatedLatency +
                               TimeSpan.FromTicks(sender.RoundtripTime.Ticks / 2);

            player.ReadNormalPacket(reader, gameTime, latency);
        }

        private static void ReadDeployPackets(PacketReader reader, NetworkGamer sender, GameTime gameTime)
        {
            // Look up the player associated with whoever sent this packet.
            Player player = sender.Tag as Player;

            // Estimate how long this packet took to arrive.
            TimeSpan latency = networkSession.SimulatedLatency +
                               TimeSpan.FromTicks(sender.RoundtripTime.Ticks / 2);

            player.ReadDeployPacket(reader, gameTime, latency);
        }

        private static void ReadAttackPackets(PacketReader reader, NetworkGamer sender, GameTime gameTime)
        {
            //Message Log packets
            messageLog[0] = reader.ReadString();
            messageLog[1] = reader.ReadString();
            messageLog[2] = reader.ReadString();
            messageLog[3] = reader.ReadString();

            // Look up the player associated with whoever sent this packet.
            Player player = sender.Tag as Player;

            DamageType damage = DamageType.None;
            int infDamage = 0;

            int attackingUnitIndex = (int)reader.ReadByte();
            UnitType targetUnitType = (UnitType)reader.ReadByte();
            int targetPlayerIndex = (int)reader.ReadByte();
            int targetUnitIndex = (int)reader.ReadByte();
            if (targetUnitType == UnitType.Infantry)
                infDamage = (int)reader.ReadByte();
            else
                damage = (DamageType)reader.ReadByte();

            WeaponType weaponType = (WeaponType)reader.ReadByte();

            Unit attackingUnit = player.Units[attackingUnitIndex];
            Unit targetUnit = Player(targetPlayerIndex).Units[targetUnitIndex];

            attackingUnit.TargetedUnit = targetUnit;
            //TODO: Add count and weapon type
            attackingUnit.AddProjectiles(weaponType, 0);

            int localPlayerIndex = (networkSession.LocalGamers[0].Tag as Player).PlayerIndex;

            if (localPlayerIndex == targetPlayerIndex)
            {
                if (targetUnitType == UnitType.Infantry)
                    HeavyGearManager.Player(targetPlayerIndex).ApplyInfantryDamage(targetUnitIndex, infDamage);
                else
                    HeavyGearManager.Player(targetPlayerIndex).ApplyDamage(targetUnitIndex, damage);
            }
            else if (infDamage >= 0 && damage >= 0)
            {
                targetUnit.StartAnimation(AnimationType.Hit);
            }
        }

        private static void ReadTargetPackets(PacketReader reader, NetworkGamer sender, GameTime gameTime)
        {
            //Message Log packets
            messageLog[0] = reader.ReadString();
            messageLog[1] = reader.ReadString();
            messageLog[2] = reader.ReadString();
            messageLog[3] = reader.ReadString();

            // Look up the player associated with whoever sent this packet.
            Player player = sender.Tag as Player;

            int targetedUnitIndex = (int)reader.ReadByte();
            bool destroyed = reader.ReadBoolean();

            Unit target = player.Units[targetedUnitIndex];

            if (destroyed)
                target.Destroy();
        }

        /// <summary>
        /// Helper for reading incoming network packets.
        /// </summary>
        private static void ReadIncomingPackets(LocalNetworkGamer gamer, GameTime gameTime)
        {
            // Keep reading as long as incoming packets are available.
            while (gamer.IsDataAvailable)
            {
                NetworkGamer sender;

                // Read a single packet from the network.
                gamer.ReceiveData(packetReader, out sender);

                PacketType packetType = (PacketType)packetReader.ReadByte();

                if (packetType == PacketType.Menu)
                    ReadMenuPackets(packetReader);

                if (packetType == PacketType.StartTurn)
                    ReadMenuPackets(packetReader);

                if (packetType == PacketType.StartGame)
                    startGame = true;

                // Discard packets sent by local gamers: we already know their state!
                if (sender.IsLocal)
                    continue;

                switch (packetType)
                {
                    case PacketType.Normal:
                        ReadNormalPackets(packetReader, sender, gameTime);
                        break;
                    case PacketType.Deploy:
                        ReadDeployPackets(packetReader, sender, gameTime);
                        break;
                    case PacketType.Attack:
                        ReadAttackPackets(packetReader, sender, gameTime);
                        break;
                    case PacketType.Target:
                        ReadTargetPackets(packetReader, sender, gameTime);
                        break;
                }
                
            }
        }

        public static void JoinSession(AvailableNetworkSession session)
        {
            networkSession = NetworkSession.Join(session);

#if DEBUG
            networkSession.SimulatedLatency = TimeSpan.FromMilliseconds(100);
            networkSession.SimulatedPacketLoss = 0.1f;
#endif

            HookSessionEvents();
        }

        public static void EndSession()
        {
            networkSession = null;
        }

        /// <summary>
        /// Starts hosting a new network session.
        /// </summary>
        public static void CreateSession()
        {
            //DrawMessage("Creating session...");

            try
            {
                networkSession = NetworkSession.Create(NetworkSessionType.SystemLink,
                                                       1, 4);

#if DEBUG
                //Simulate latency and packet loss for debugging
                networkSession.SimulatedLatency = TimeSpan.FromMilliseconds(100);
                networkSession.SimulatedPacketLoss = 0.1f;
#endif

                HookSessionEvents();
            }
            catch (Exception e)
            {
                errorMessage = e.Message;
            }
        }

        public static void CreateSessionLocal()
        {
            //DrawMessage("Creating session...");

            try
            {
                networkSession = NetworkSession.Create(NetworkSessionType.Local,
                                                       4, 4);

                HookSessionEvents();
            }
            catch (Exception e)
            {
                errorMessage = e.Message;
            }
        }

        /// <summary>
        /// After creating or joining a network session, we must subscribe to
        /// some events so we will be notified when the session changes state.
        /// </summary>
        public static void HookSessionEvents()
        {
            networkSession.GamerJoined += GamerJoinedEventHandler;
            networkSession.SessionEnded += SessionEndedEventHandler;
        }

        /// <summary>
        /// This event handler will be called whenever a new gamer joins the session.
        /// We use it to allocate a player object, and associate it with the new gamer.
        /// </summary>
        public static void GamerJoinedEventHandler(object sender, GamerJoinedEventArgs e)
        {
            int gamerIndex = networkSession.AllGamers.IndexOf(e.Gamer);

            e.Gamer.Tag = new Player(gamerIndex, e.Gamer.Gamertag);

            if (e.Gamer.IsLocal)
                localPlayer = e.Gamer.Tag as Player;
        }


        /// <summary>
        /// Event handler notifies us when the network session has ended.
        /// </summary>
        public static void SessionEndedEventHandler(object sender, NetworkSessionEndedEventArgs e)
        {
            errorMessage = e.EndReason.ToString();

            networkSession.Dispose();
            networkSession = null;
        }

        public static int GetBits(byte b, int offset, int count)
        {
            return (b >> offset) & ((1 << count) - 1);
        }
        #endregion

        

        public static void StartLocal(int numOfPlayers)
        {
            players = new Player[numOfPlayers];
            for (int i = 0; i < numOfPlayers; i++)
                players[i] = new Player(i, "Player " + (i + 1));
            activePlayer = Player(0);
        }

        /// <summary>
        /// Adds a new message to the end of the message log and moves other strings back one
        /// </summary>
        /// <param name="message"></param>
        public static void MessageLogAdd(string message)
        {
            for (int i = 1; i < 4; i++)
            {
                messageLog[i - 1] = messageLog[i];
            }
            messageLog[3] = message;
        }

        #region EndTurn

        public static void EndTurn()
        {
            if (gameOver)
                return;

            if (activePlayer.PlayerIndex == NumberOfPlayers - 1)
                currentTurn++;

            activePlayer = Player((activePlayer.PlayerIndex + 1) % NumberOfPlayers);

            if (activePlayer.Units.Count == 0)
                EndTurn();

            if (activePlayer.Units.Count > 0)
                    camera.MapPosition = activePlayer.Units[0].MapPosition;

            if (networkSession == null)
            {
                foreach (Unit unit in activePlayer.Units)
                {
                    if (unit.IsAlive)
                        unit.StartTurn();
                }
                
                NextPlayer = true;
            }
            else
            {
                SendStartTurnPacket(activePlayer.PlayerIndex);
            }
        }
        

        public static void DeployEndTurn()
        {
            activePlayer = Player((activePlayer.PlayerIndex + 1) % NumberOfPlayers);
            bool unitNeedsPlace = false;

            foreach (Unit unit in activePlayer.Units)
            {
                if (unit.MapPosition.X < 0 || unit.MapPosition.Y < 0)
                {
                    unitNeedsPlace = true;
                    break;
                }
            }

            if (!unitNeedsPlace)
            {
                activePlayer = Player(NumberOfPlayers - 1);
                //camera.MapPosition = cursorPosition = activePlayer.Army[0].MapPosition;
                deployMode = false;
                EndTurn();
            }
        }
        public static void StartNetworkGame()
        {
            activePlayer = Player(NumberOfPlayers - 1);
            EndTurn();
        }

        public static void InitDeploy()
        {
            activePlayer = localPlayer;
        }
        

        #endregion

        #region Exit/Reset
        public static void ExitGame()
        {
            gameScreens.Clear();
        }

        public static void ReturnToMenu()
        {
            gameOver = false;
            activePlayer = null;
            playerWon = 0;
            NextPlayer = false;
            deployMode = true;
            startGame = false;
            messageLog = new string[] { "", "", "", "" };
            map = null;
            currentTurn = 0;
            players = null;
            networkSession = null;

            gameScreens.Clear();
            gameScreens.Push(new MainMenu());
        }
        #endregion

        #region Render
        /// <summary>
        /// Render
        /// </summary>
        protected override void Render()
        {
            // No more game screens?
            if (gameScreens.Count == 0)
            {
                // Before quiting, stop music
                //Sound.StopMusic();

                // Then quit
                Exit();
                return;
            }

            // Handle current screen
            if (gameScreens.Peek().Render())
            {
                if (gameScreens.Count == 0)
                {
                    // Before quiting, stop music
                    //Sound.StopMusic();

                    // Then quit
                    Exit();
                    return;
                }
                
                // If this was the options screen and the resolution has changed,
                // apply the changes
                //if (gameScreens.Peek().GetType() == typeof(Options) &&
                //    (BaseGame.Width != GameSettings.Default.ResolutionWidth ||
               //     BaseGame.Height != GameSettings.Default.ResolutionHeight ||
               //     BaseGame.Fullscreen != GameSettings.Default.Fullscreen))
              //  {
             //       BaseGame.ApplyResolutionChange();
             //   }
            
                // Play sound for screen back
                Sound.Play(Sound.Sounds.ScreenBack);

                gameScreens.Pop();
            }
        }
        

        /// <summary>
        /// Post user interface rendering, in case we need it.
        /// Used for rendering the car selection 3d stuff after the UI.
        /// </summary>
        protected override void PostUIRender()
        {
            // Enable depth buffer again
            BaseGame.Device.RenderState.DepthBufferEnable = true;

            // Do menu shader after everything
            //if (HeavyGearManager.InMenu) &&
                //PostScreenMenu.Started)
                //UI.PostScreenMenuShader.Show();
        }
        #endregion
    }
}
