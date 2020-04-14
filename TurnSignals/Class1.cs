using NativeUI;
using GTA;
using GTA.Native;
using GTA.Math;
using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Media;
using System.Windows.Forms;

public class TurnSignals : Script
{
    UIMenu mainMenu = new UIMenu("Turn~o~Signals", "Script");
    UIMenuCheckboxItem scriptTurn = new UIMenuCheckboxItem("TurnScript", true, "Checked - ~g~ON\n~w~UnChecked - ~r~OFF");
    UIMenuCheckboxItem Passanger = new UIMenuCheckboxItem("Spawn With Passanger", true, "Checked - ~g~ON\n~w~UnChecked - ~r~OFF");
    UIMenuCheckboxItem godModeCheck = new UIMenuCheckboxItem("God", false, "Checked - ~g~ON\n~w~UnChecked - ~r~OFF");
    UIMenuCheckboxItem open_closecar_checkbox = new UIMenuCheckboxItem("Close Car", false);
    UIMenuCheckboxItem startEngine = new UIMenuCheckboxItem("Start Engine", false);
    UIMenuCheckboxItem remoteStart = new UIMenuCheckboxItem("AutoStart", false, "AutoStart car when you near him");
    UIMenuCheckboxItem showCarInfo = new UIMenuCheckboxItem("Show Car Info", false, "Show info about car ex.\nSpeed, Gear, RPM.");
    UIMenuCheckboxItem RemotePilot = new UIMenuCheckboxItem("RemotePilot", false, "Press ~g~] ~w~for Start Engine\nYou must be ~r~15 meters~w~ from car");
    UIMenuSliderItem carInfoPositionY = new UIMenuSliderItem("Info PositionY: 20");
    UIMenuSliderItem carInfoPositionX = new UIMenuSliderItem("Info PositionX: 1130");
    UIMenuItem bati = new UIMenuItem("Taipan");
    UIMenuItem Tezeract = new UIMenuItem("Tezeract");
    UIMenuItem PoliceBuffalo = new UIMenuItem("Police Buffalo");
    UIMenuItem fixCar = new UIMenuItem("Fix Car");
    UIMenuItem orSeparator = new UIMenuItem("Or");
    UIMenuItem separator = new UIMenuItem("--------------");
    UIMenuSliderItem wantedLevel = new UIMenuSliderItem("Wanted Level: 0", "Set Wanted Level", true);
    UIText speedText = new UIText("Speed: ", new Point(1130, 20), 0.5f);
    UIText gearText = new UIText("Gear ", new Point(1130, 40), 0.5f);
    UIText rpmText = new UIText("0 RPM", new Point(1130, 60), 0.5f);
    private Ped playerPed = Game.Player.Character;
    private Player player = Game.Player;
    private MenuPool _menuPool;
    int defaultEnginePower;
    bool leftBlinker = false;
    bool rightBlinker = false;
    bool engineOn = true;
    bool hornTwoTimes = true;
    bool leftDoorOpened = false;
    bool rightDoorOpened = false;
    bool hoodOpened = false;
    bool trunkOpened = false;

    private void OnKeyDown(object sender, KeyEventArgs e) // Code inside this is what happens when a key gets pushed down or held down
    {
        if (scriptTurn.Checked == true)
        {
            if (playerPed.IsInVehicle() == true)
            {
                if (e.KeyCode == Keys.NumPad1)
                {
                    if (leftBlinker == false)
                    {
                        if (rightBlinker == true)
                        {
                            playerPed.CurrentVehicle.RightIndicatorLightOn = false;
                            rightBlinker = false;
                            playerPed.CurrentVehicle.LeftIndicatorLightOn = true;
                            leftBlinker = true;
                        }
                        else
                        {
                            playerPed.CurrentVehicle.LeftIndicatorLightOn = true;
                            leftBlinker = true;
                        }
                    }
                    else
                    {
                        playerPed.CurrentVehicle.LeftIndicatorLightOn = false;
                        leftBlinker = false;
                    }
                }

                if (e.KeyCode == Keys.NumPad3)
                {
                    if (rightBlinker == false)
                    {
                        if (leftBlinker == true)
                        {
                            playerPed.CurrentVehicle.LeftIndicatorLightOn = false;
                            leftBlinker = false;
                            playerPed.CurrentVehicle.RightIndicatorLightOn = true;
                            rightBlinker = true;
                        }
                        else
                        {
                            playerPed.CurrentVehicle.RightIndicatorLightOn = true;
                            rightBlinker = true;
                        }

                    }
                    else
                    {
                        playerPed.CurrentVehicle.RightIndicatorLightOn = false;
                        rightBlinker = false;
                    }
                }

                if (e.KeyCode == Keys.NumPad2)
                {
                    if (leftBlinker == false && rightBlinker == false)
                    {
                        playerPed.CurrentVehicle.LeftIndicatorLightOn = true;
                        leftBlinker = true;
                        playerPed.CurrentVehicle.RightIndicatorLightOn = true;
                        rightBlinker = true;
                    }
                    else
                    {
                        playerPed.CurrentVehicle.LeftIndicatorLightOn = false;
                        leftBlinker = false;
                        playerPed.CurrentVehicle.RightIndicatorLightOn = false;
                        rightBlinker = false;
                    }
                }

                if (e.KeyCode == Keys.NumPad5)
                {
                    if (playerPed.CurrentVehicle.LightsOn == true)
                    {
                        playerPed.CurrentVehicle.LightsOn = false;
                    }
                    else
                    {
                        playerPed.CurrentVehicle.LightsOn = true;
                    }
                }

                if (e.KeyCode == Keys.NumPad6)
                {
                    if (engineOn == true)
                    {
                        playerPed.CurrentVehicle.EngineRunning = false;
                        engineOn = false;
                    }
                    else
                    {
                        playerPed.CurrentVehicle.EngineRunning = true;
                        engineOn = true;
                    }
                }
            }
            else
            {
                if (e.KeyCode == Keys.NumPad4)
                {
                    if (leftDoorOpened == false)
                    {
                        playerPed.LastVehicle.OpenDoor(VehicleDoor.FrontLeftDoor, false, false);
                        leftDoorOpened = true;
                    }
                    else
                    {
                        playerPed.LastVehicle.CloseDoor(VehicleDoor.FrontLeftDoor, false);
                        leftDoorOpened = false;
                    }
                }

                if (e.KeyCode == Keys.NumPad6)
                {
                    if (rightDoorOpened == false)
                    {
                        playerPed.LastVehicle.OpenDoor(VehicleDoor.FrontRightDoor, false, false);
                        rightDoorOpened = true;
                    }
                    else
                    {
                        playerPed.LastVehicle.CloseDoor(VehicleDoor.FrontRightDoor, false);
                        rightDoorOpened = false;
                    }
                }

                if (e.KeyCode == Keys.NumPad2)
                {
                    if (trunkOpened == false)
                    {
                        playerPed.LastVehicle.OpenDoor(VehicleDoor.Trunk, false, false);
                        trunkOpened = true;
                    }
                    else
                    {
                        playerPed.LastVehicle.CloseDoor(VehicleDoor.Trunk, false);
                        trunkOpened = false;
                    }
                }

                if (e.KeyCode == Keys.NumPad8)
                {
                    if (hoodOpened == false)
                    {
                        playerPed.LastVehicle.OpenDoor(VehicleDoor.Hood, false, false);
                        hoodOpened = true;
                    }
                    else
                    {
                        playerPed.LastVehicle.CloseDoor(VehicleDoor.Hood, false);
                        hoodOpened = false;
                    }
                }

                if (e.KeyCode == Keys.NumPad5)
                {
                    if (playerPed.LastVehicle.HasSiren)
                    {
                        if (playerPed.LastVehicle.SirenActive)
                        {
                            playerPed.LastVehicle.SirenActive = false;
                        }
                        else
                        {
                            playerPed.LastVehicle.SirenActive = true;
                            playerPed.LastVehicle.IsSirenSilent = true;
                        }
                    }
                }

                if (e.KeyCode == Keys.OemCloseBrackets)
                {
                    Entity carLast = playerPed.LastVehicle;

                    if (RemotePilot.Checked == true)
                    {
                        if (playerPed.IsInVehicle() == false)
                        {
                            if (playerPed.IsNearEntity(carLast, new Vector3(15, 15, 15))) //If player it's 15 meters from car
                            {
                                if (!playerPed.IsGettingIntoAVehicle)
                                {
                                    if (playerPed.LastVehicle.EngineRunning == false)
                                    {
                                        playerPed.LastVehicle.EngineRunning = true;
                                        playerPed.LastVehicle.InteriorLightOn = true;
                                        if (World.CurrentDayTime.Hours >= 20 && World.CurrentDayTime.Hours > 6)
                                        {
                                            playerPed.LastVehicle.LightsOn = true;
                                            playerPed.LastVehicle.BrakeLightsOn = true;
                                        }
                                        if (hornTwoTimes == true)
                                        {
                                            playerPed.LastVehicle.HandbrakeOn = true;
                                            playerPed.LastVehicle.CurrentRPM = 100;
                                            Wait(600);
                                            playerPed.LastVehicle.HandbrakeOn = false;
                                            playerPed.LastVehicle.CurrentRPM = 0;
                                            Wait(700);
                                            playerPed.LastVehicle.HandbrakeOn = true;
                                            playerPed.LastVehicle.CurrentRPM = 100;
                                            Wait(400);
                                            playerPed.LastVehicle.HandbrakeOn = false;
                                            playerPed.LastVehicle.CurrentRPM = 0;
                                            hornTwoTimes = false;
                                        }
                                    }
                                    else
                                    {
                                        hornTwoTimes = true;
                                        playerPed.LastVehicle.EngineRunning = false;
                                        playerPed.LastVehicle.InteriorLightOn = false;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        startEngine.Enabled = true;
                    }
                }
            }
        }

        if (e.KeyCode == Keys.F10)
        {
            mainMenu.Visible = !mainMenu.Visible;
        }

        if (e.KeyCode == Keys.F11)
        {
            //World.CurrentDayTime = TimeSpan.FromHours(23);
        }
    }

    public void ScriptSett(UIMenu menu)
    {
        var scriptsettsubmenu = _menuPool.AddSubMenu(menu, "Script Settings");
        for (int i = 0; i < 0; i++) ;

        scriptsettsubmenu.AddItem(scriptTurn);
    }

    public void Other(UIMenu menu)
    {
        var othersubmenu = _menuPool.AddSubMenu(menu, "Other");
        for (int i = 0; i < 1; i++) ;

        wantedLevel.Maximum = 5; //Because max Wanted level it's "5"
        wantedLevel.Multiplier = 1; //move slider to every 1 position (Default "5")
        wantedLevel.Value = player.WantedLevel;
        wantedLevel.Text = "Wanted Level: " + player.WantedLevel;
        wantedLevel.SetLeftBadge(UIMenuItem.BadgeStyle.Star);
        othersubmenu.AddItem(godModeCheck);
        othersubmenu.AddItem(wantedLevel);

        othersubmenu.OnSliderChange += (sender, item, index) =>
        {
            player.WantedLevel = item.Value;
            item.Text = "Wanted Level: " + item.Value;
        };
    }

    public void carMenu(UIMenu menu)
    {
        var carmenusubmenu = _menuPool.AddSubMenu(menu, "Your Car Menu");
        for (int i = 0; i < 0; i++) ;

        carmenusubmenu.AddItem(remoteStart);
        carmenusubmenu.AddItem(orSeparator);
        carmenusubmenu.AddItem(RemotePilot);
        carmenusubmenu.AddItem(separator);
        carmenusubmenu.AddItem(open_closecar_checkbox);
        carmenusubmenu.AddItem(startEngine);

        carmenusubmenu.OnCheckboxChange += (sender, item, index) =>
        {
            if (item == open_closecar_checkbox)
            {
                if (playerPed.IsInVehicle() == true)
                {
                    UI.Notify("You can't close car when you are in car!");
                    open_closecar_checkbox.Checked = false;
                }
                else
                {
                    if (open_closecar_checkbox.Checked)
                    {
                        player.LastVehicle.LockStatus = VehicleLockStatus.Locked;                        
                    }
                    else
                    {
                        player.LastVehicle.LockStatus = VehicleLockStatus.Unlocked;
                    }

                }
            }

            if (item == startEngine)
            {
                if (startEngine.Checked)
                {
                    playerPed.LastVehicle.EngineRunning = true;
                }
                else
                {
                    playerPed.LastVehicle.EngineRunning = false;
                }
            }

            if (item == remoteStart)
            {
                if (RemotePilot.Checked == true)
                {
                    remoteStart.Checked = false;
                }
            }

            if (item == RemotePilot)
            {
                if (remoteStart.Checked == true)
                {
                    remoteStart.Checked = false;
                }
            }
        };
    }

    public void VehicleSpawn(UIMenu menu)
    {
        var vehiclespawnsubmenu = _menuPool.AddSubMenu(menu, "Vehicle Spawn");
        for (int i = 0; i < 1; i++) ;

        vehiclespawnsubmenu.AddItem(showCarInfo); //Add Checkbox to menu
        vehiclespawnsubmenu.AddItem(carInfoPositionX); //Add Slider to menu
        vehiclespawnsubmenu.AddItem(carInfoPositionY); //Add Slider to menu
        vehiclespawnsubmenu.AddItem(Passanger); //Add Checkbox to menu
        vehiclespawnsubmenu.AddItem(fixCar); //Add Button to menu
        vehiclespawnsubmenu.AddItem(separator); //Add separator to menu
        vehiclespawnsubmenu.AddItem(bati); //Add Button to menu
        vehiclespawnsubmenu.AddItem(Tezeract); //Add Button to menu
        vehiclespawnsubmenu.AddItem(PoliceBuffalo); //Add Button to menu

        carInfoPositionX.Maximum = Game.ScreenResolution.Width;
        carInfoPositionY.Maximum = Game.ScreenResolution.Height;
        carInfoPositionX.Value = 1130;
        carInfoPositionY.Value = 20;

        vehiclespawnsubmenu.OnCheckboxChange += (sender, item, index) =>
        {
            if (item == showCarInfo)
            {
                if (showCarInfo.Checked)
                {
                    carInfoPositionX.Enabled = true;
                    carInfoPositionY.Enabled = true;
                    carInfoPositionX.Multiplier = 10;
                    carInfoPositionY.Multiplier = 10;
                }
                else
                {
                    carInfoPositionX.Enabled = false;
                    carInfoPositionY.Enabled = false;
                    carInfoPositionX.Multiplier = 0;
                    carInfoPositionY.Multiplier = 0;
                }
            }
        };

        vehiclespawnsubmenu.OnSliderChange += (sender, item, index) =>
        {
            if (item == carInfoPositionX)
            {
                speedText.Position = new Point(carInfoPositionX.Value, carInfoPositionY.Value);
                gearText.Position = new Point(carInfoPositionX.Value, carInfoPositionY.Value + 20);
                rpmText.Position = new Point(carInfoPositionX.Value, carInfoPositionY.Value + 40);
                carInfoPositionX.Text = "Info PositionX: " + carInfoPositionX.Value;
            }

            if (item == carInfoPositionY)
            {
                speedText.Position = new Point(carInfoPositionX.Value, carInfoPositionY.Value);
                gearText.Position = new Point(carInfoPositionX.Value, carInfoPositionY.Value + 20);
                rpmText.Position = new Point(carInfoPositionX.Value, carInfoPositionY.Value + 40);
                carInfoPositionY.Text = "Info PositionY: " + carInfoPositionY.Value;
            }
        };

        vehiclespawnsubmenu.OnItemSelect += (sender, item, index) =>
        {
            if (item == fixCar)
            {
                if (playerPed.IsInVehicle() == true)
                {
                    playerPed.CurrentVehicle.NumberPlate = "TheWolf";
                    playerPed.CurrentVehicle.Repair();
                    playerPed.CurrentVehicle.DirtLevel = 0;
                }
            }

            if (item == bati)
            {
                if (playerPed.IsInVehicle() == true)
                {
                    playerPed.CurrentVehicle.Delete();
                }

                Vehicle car = World.CreateVehicle("TAIPAN", Game.Player.Character.Position);
                car.SecondaryColor = VehicleColor.MatteWhite;
                car.PrimaryColor = VehicleColor.MatteRed;
                playerPed.SetIntoVehicle(car, VehicleSeat.Driver);
                if (Passanger.Checked)
                {
                    car.CreatePedOnSeat(VehicleSeat.Passenger, PedHash.Beach01AFM);
                }
                car.SetMod(VehicleMod.Exhaust, 1, false);
                car.CanTiresBurst = false;
            }

            if (item == Tezeract)
            {
                if (playerPed.IsInVehicle() == true)
                {
                    playerPed.CurrentVehicle.Delete();
                }

                Vehicle car = World.CreateVehicle("TEZERACT", Game.Player.Character.Position);
                playerPed.SetIntoVehicle(car, VehicleSeat.Driver);
                if (Passanger.Checked)
                {
                    car.CreatePedOnSeat(VehicleSeat.Passenger, PedHash.Beach01AFM);
                }
            }

            if (item == PoliceBuffalo)
            {
                if (playerPed.IsInVehicle() == true)
                {
                    playerPed.CurrentVehicle.Delete();
                }

                Vehicle car = World.CreateVehicle("police2", Game.Player.Character.Position);
                playerPed.SetIntoVehicle(car, VehicleSeat.Driver);
                if (Passanger.Checked)
                {
                    car.CreatePedOnSeat(VehicleSeat.Passenger, PedHash.FemaleAgent);
                }
                car.InstallModKit();
            }
        };
    }

    public void GodMode()
    {
        if (godModeCheck.Checked == true)
        {
            playerPed.Health = playerPed.MaxHealth;
        }
    }

    public TurnSignals()
    {
        UI.Notify("Script ScriptTemplate ~r~Loaded~w~!");
        carInfoPositionX.Enabled = false;
        carInfoPositionY.Enabled = false;
        carInfoPositionX.Multiplier = 0;
        carInfoPositionY.Multiplier = 0;

        speedText.Color = Color.White;
        speedText.Font = GTA.Font.Monospace;

        gearText.Color = Color.White;
        gearText.Font = GTA.Font.Monospace;

        rpmText.Color = Color.White;
        rpmText.Font = GTA.Font.Monospace;

        _menuPool = new MenuPool();
        _menuPool.Add(mainMenu);
        ScriptSett(mainMenu);
        VehicleSpawn(mainMenu);
        carMenu(mainMenu);
        Other(mainMenu);
        _menuPool.RefreshIndex();
        Tick += OnTick;
        KeyDown += OnKeyDown;
        KeyUp += OnKeyUp;
        StartInfo();
    }

    private void OnTick(object sender, EventArgs e)
    {
        _menuPool.ProcessMenus();
        CheckEngineState();
        GodMode();
        BrakeLightOnPark();
        CarInfo();
        AutoStart();
        CheckAutoStart();
        alarmWhenPlayerTryOpenClosedDoors();
    }

    private void OnKeyUp(object sender, KeyEventArgs e)
    {

    }

    public void alarmWhenPlayerTryOpenClosedDoors()
    {
        if (playerPed.LastVehicle.LockStatus == VehicleLockStatus.Locked)
        {
            open_closecar_checkbox.Checked = true;
        }
        else
        {
            open_closecar_checkbox.Checked = false;
        }
    }

    private void CheckEngineState()
    {
        if (playerPed.IsInVehicle() == true)
        {
            if (engineOn == true)
            {
                //nothing
            }
            else
            {
                playerPed.CurrentVehicle.EngineRunning = false;
            }
        }
    }

    public void BrakeLightOnPark()
    {
        if (playerPed.IsInVehicle() == true)
        {
            if (playerPed.CurrentVehicle.Speed == 0)
            {
                playerPed.CurrentVehicle.BrakeLightsOn = true;
            }

            if (playerPed.CurrentVehicle.Speed * 3600 / 1000 < 40)
            {
                playerPed.CurrentVehicle.EnginePowerMultiplier = 0.1f;
                playerPed.CurrentVehicle.EngineTorqueMultiplier = 0.5f;
            }
            else
            {
                playerPed.CurrentVehicle.EnginePowerMultiplier = 1.5f;
                playerPed.CurrentVehicle.EngineTorqueMultiplier = 1.5f;
            }
        }
    }


    private void StartInfo()
    {
        UI.ShowSubtitle("Click ~g~F10 ~w~for open ~r~Menu~w~.", 3000);
    }

    private void CarInfo()
    {
        if (playerPed.IsInVehicle() == true)
        {
            if (showCarInfo.Checked == true)
            {
                    float speedKph = playerPed.CurrentVehicle.Speed * 3600 / 1000;
                    string speedConv = speedKph.ToString("0");

                    float gear = playerPed.CurrentVehicle.CurrentGear;
                    string gearConv = gear.ToString();

                    float engineHealth = playerPed.CurrentVehicle.EngineHealth / 10;
                    int engineHealthInt = Convert.ToInt32(engineHealth);
                    string engineHealthConv = engineHealthInt.ToString();

                    speedText.Caption = speedConv + " KM";
                    gearText.Caption = "Gear " + gearConv;
                    rpmText.Caption = "Engine Health " + engineHealthConv;

                    if (engineHealth >= 100)
                    {
                        rpmText.Color = Color.White;
                    }

                    if (engineHealth < 50)
                    {
                        rpmText.Color = Color.Yellow;
                    }

                    if (engineHealth < 25)
                    {
                        rpmText.Color = Color.Red;
                    }

                    speedText.Enabled = true;
                    speedText.Draw();
                    gearText.Enabled = true;
                    gearText.Draw();
                    rpmText.Enabled = true;
                    rpmText.Draw();
            }
        }
    }

    private void AutoStart()
    {
        Entity carLast = playerPed.LastVehicle;

        if (remoteStart.Checked)
        {
            startEngine.Enabled = false;
            if (playerPed.IsInVehicle() == false)
            {
                if (playerPed.IsNearEntity(carLast, new Vector3(5, 5, 5))) //If player it's 5 meters from car
                {
                    if (!playerPed.IsGettingIntoAVehicle)
                    {
                        playerPed.LastVehicle.EngineRunning = true;
                        playerPed.LastVehicle.InteriorLightOn = true;
                        if (World.CurrentDayTime.Hours >= 20 && World.CurrentDayTime.Hours > 6)
                        {
                            playerPed.LastVehicle.LightsOn = true;
                            playerPed.LastVehicle.BrakeLightsOn = true;
                        }
                        if (hornTwoTimes == true)
                        {
                            playerPed.LastVehicle.HandbrakeOn = true;
                            playerPed.LastVehicle.CurrentRPM = 100;
                            Wait(600);
                            playerPed.LastVehicle.HandbrakeOn = false;
                            playerPed.LastVehicle.CurrentRPM = 0;
                            Wait(700);
                            playerPed.LastVehicle.HandbrakeOn = true;
                            playerPed.LastVehicle.CurrentRPM = 100;
                            Wait(400);
                            playerPed.LastVehicle.HandbrakeOn = false;
                            playerPed.LastVehicle.CurrentRPM = 0;
                            hornTwoTimes = false;
                        }
                    }
                }
                else
                {
                    hornTwoTimes = true;
                    playerPed.LastVehicle.EngineRunning = false;
                    playerPed.LastVehicle.InteriorLightOn = false;
                }
            }
        }
        else
        {
            startEngine.Enabled = true;
        }
    }

    public void CheckAutoStart()
    {

        if (remoteStart.Checked == true)
        {
            startEngine.Enabled = false;
        }
        else
        {
            startEngine.Enabled = true;
        }

        if (playerPed.IsInVehicle() == true)
        {
            startEngine.Description = "In car use ~g~NumPad 6";
            startEngine.Enabled = false;
        }
        else
        {
            startEngine.Description = null;
            startEngine.Enabled = true;
            if (playerPed.LastVehicle.EngineRunning == true)
            {
                startEngine.Checked = true;
            }
            else
            {
                startEngine.Checked = false;
            }
        }
    }
}