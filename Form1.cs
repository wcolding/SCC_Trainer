using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Configuration;
using System.Collections.Specialized;

namespace SCC_Trainer
{
    public partial class Form1 : Form
    {
        ConvictionGame conviction;

        private bool hooked = false;

        private int controllerIndex = 1;
        private float movementSpeed = .5f;
        private float movementMultiplier = 1f;
        private byte triggerThreshold = 20;
        private XInput.State xInputState;
        private string mapName;

        private bool set_warp_pressed;
        private bool recall_warp_pressed;
        private Transform warpPoint;

        public Form1()
        {
            InitializeComponent();

            Program.log = this.logBox;

            gameVersionToggle.SelectedIndex = Properties.Settings.Default.GameVersion;
            controllerIndex = Properties.Settings.Default.XInputPort;
            DeactivateForm();

            controllerPortBox.Value = controllerIndex + 1;

            warpPoint = new Transform();
            set_warp_pressed = false;
            recall_warp_pressed = false;

            backgroundWorker1.DoWork += new DoWorkEventHandler(ReadGameState);
            backgroundWorker2.DoWork += new DoWorkEventHandler(CheckIfGameIsStillOpen);
        }

        private void GetTransform()
        {
            posX.Value = (decimal)conviction.player.transform.PosX;
            posY.Value = (decimal)conviction.player.transform.PosY;
            posZ.Value = (decimal)conviction.player.transform.PosZ;
            rotY.Value = conviction.player.transform.RotY;
            velX.Value = (decimal)conviction.player.transform.VelX;
            velY.Value = (decimal)conviction.player.transform.VelY;
            velZ.Value = (decimal)conviction.player.transform.VelZ;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void posX_ValueChanged(object sender, EventArgs e)
        {
            if (hooked)
                conviction.player.transform.PosX = (float)posX.Value;
        }

        private void posY_ValueChanged(object sender, EventArgs e)
        {
            if (hooked)
                conviction.player.transform.PosY = (float)posY.Value;
        }

        private void posZ_ValueChanged(object sender, EventArgs e)
        {
            if (hooked)
                conviction.player.transform.PosZ = (float)posZ.Value;
        }

        private void rotY_ValueChanged(object sender, EventArgs e)
        {
            if (hooked)
                conviction.player.transform.RotY = (ushort)rotY.Value;
        }

        private void velX_ValueChanged(object sender, EventArgs e)
        {
            if (hooked)
                conviction.player.transform.VelX = (float)velX.Value;
        }

        private void velY_ValueChanged(object sender, EventArgs e)
        {
            if (hooked)
                conviction.player.transform.VelY = (float)velY.Value;
        }

        private void velZ_ValueChanged(object sender, EventArgs e)
        {
            if (hooked)
                conviction.player.transform.VelZ = (float)velZ.Value;
        }

        private void CheckIfGameIsStillOpen(object sender, DoWorkEventArgs e)
        {
            while (hooked)
            {
                if (!Memory.GameIsRunning)
                {
                    hooked = false;
                    Memory.Close(); 
                    
                    Invoke(new Action(() =>
                    {
                        DeactivateForm();
                        Program.Log("Game was closed.");
                    }));
                    return;
                }
            }
        }

        private void ReadGameState(object sender, DoWorkEventArgs e)
        {
            while (hooked)
            {
                Invoke(new Action(() =>
                {
                    conviction.CheckIfReloaded();
                    oldCounterAddr.Text = String.Format("Old Scene Counter Addr: {0:X8}", conviction.oldSceneCounterAddr);
                    newCounterAddr.Text = String.Format("New Scene Counter Addr: {0:X8}", conviction.sceneCounterAddr);
                }));

                mapName = conviction.MapName;

                if (mapName.ToLower() != "menu")
                {
                    Invoke(new Action(() =>
                    {
                        curMapLabel.Text = "Current Map: " + mapName;
                        GetTransform();
                        numEnemies.Value = (decimal)conviction.EnemiesLeft;
                        sceneCounterLabel.Text = String.Format("Scene Counter Addr: {0}", conviction.SceneCounter);
                    }));

                    XInput.XInputGetState(controllerIndex, ref xInputState);

                    movementMultiplier = 1f;

                    if (xInputState.gamepad.bLeftTrigger > triggerThreshold)
                        movementMultiplier *= .5f;
                    if (xInputState.gamepad.bRightTrigger > triggerThreshold)
                        movementMultiplier *= 2f;

                    if (XInput.isButtonDown(XInput.Button.LB, xInputState))
                        conviction.player.transform.PosY -= movementSpeed * movementMultiplier;
                    if (XInput.isButtonDown(XInput.Button.RB, xInputState))
                        conviction.player.transform.PosY += movementSpeed * movementMultiplier;

                    if (XInput.isButtonDown(XInput.Button.DPad_Left, xInputState))
                        conviction.player.transform.PosX -= movementSpeed * movementMultiplier;
                    if (XInput.isButtonDown(XInput.Button.DPad_Right, xInputState))
                        conviction.player.transform.PosX += movementSpeed * movementMultiplier;
                    if (XInput.isButtonDown(XInput.Button.DPad_Up, xInputState))
                        conviction.player.transform.PosZ -= movementSpeed * movementMultiplier;
                    if (XInput.isButtonDown(XInput.Button.DPad_Down, xInputState))
                        conviction.player.transform.PosZ += movementSpeed * movementMultiplier;

                    if (XInput.isButtonDown(XInput.Button.A, xInputState))
                    {
                        if (!set_warp_pressed)
                        {
                            set_warp_pressed = true;
                            SetWarpPoint();
                        }
                    }
                    else
                    {
                        if (set_warp_pressed)
                            set_warp_pressed = false;
                    }

                    if (XInput.isButtonDown(XInput.Button.B, xInputState))
                    {
                        if (!recall_warp_pressed)
                        {
                            recall_warp_pressed = true;
                            RecallWarpPoint();
                        }
                    }
                    else
                    {
                        if (recall_warp_pressed)
                            recall_warp_pressed = false;
                    }
                }
            }
        }

        private void SetWarpPoint()
        {
            if (hooked)
            {
                warpPoint.PosX = conviction.player.transform.PosX;
                warpPoint.PosY = conviction.player.transform.PosY;
                warpPoint.PosZ = conviction.player.transform.PosZ;
                warpPoint.RotY = conviction.player.transform.RotY;
                Log("Warp point set ({0},{1},{2})", warpPoint.PosX, warpPoint.PosY, warpPoint.PosZ);
            }
        }

        private void RecallWarpPoint()
        {
            if (hooked)
            {
                conviction.player.transform.PosX = warpPoint.PosX;
                conviction.player.transform.PosY = warpPoint.PosY;
                conviction.player.transform.PosZ = warpPoint.PosZ;
                conviction.player.transform.RotY = warpPoint.RotY;
                Log("Warp point recalled ({0},{1},{2})", warpPoint.PosX, warpPoint.PosY, warpPoint.PosZ);
            }
        }

        private void hookButton_Click(object sender, EventArgs e)
        {
            Memory.HookProgram("Conviction", ProcessMode.x86);
            if (Memory.GameIsRunning)
            {
                hooked = true;
                ActivateForm();
                conviction = new ConvictionGame((SCCVersion)gameVersionToggle.SelectedIndex);
                backgroundWorker1.RunWorkerAsync();
                backgroundWorker2.RunWorkerAsync();
            }
            else
            {
                Memory.Close();
                Log("Unable to open game.");
            }
        }

        private void DeactivateForm()
        {
            posX.Enabled = false;
            posY.Enabled = false;
            posZ.Enabled = false;
            rotY.Enabled = false;
            velX.Enabled = false;
            velY.Enabled = false;
            velZ.Enabled = false;

            xposLabel.Enabled = false;
            yposLabel.Enabled = false;
            zposLabel.Enabled = false;
            yrotLabel.Enabled = false;
            xvelLabel.Enabled = false;
            yvelLabel.Enabled = false;
            zvelLabel.Enabled = false;

            curMapLabel.Enabled = false;
            sceneCounterLabel.Enabled = false;
            oldCounterAddr.Enabled = false;
            newCounterAddr.Enabled = false;

            setButton.Enabled = false;
            recallButton.Enabled = false;

            numEnemies.Enabled = false;
            numEnemiesLabel.Enabled = false;

            gameVersionToggle.Enabled = true;
            hookButton.Enabled = true;
        }

        private void ActivateForm()
        {
            posX.Enabled = true;
            posY.Enabled = true;
            posZ.Enabled = true;
            rotY.Enabled = true;
            velX.Enabled = true;
            velY.Enabled = true;
            velZ.Enabled = true;

            xposLabel.Enabled = true;
            yposLabel.Enabled = true;
            zposLabel.Enabled = true;
            yrotLabel.Enabled = true;
            xvelLabel.Enabled = true;
            yvelLabel.Enabled = true;
            zvelLabel.Enabled = true;

            curMapLabel.Enabled = true;
            sceneCounterLabel.Enabled = true;
            oldCounterAddr.Enabled = true;
            newCounterAddr.Enabled = true;

            setButton.Enabled = true;
            recallButton.Enabled = true;

            numEnemies.Enabled = true;
            numEnemiesLabel.Enabled = true;

            gameVersionToggle.Enabled = false;
            hookButton.Enabled = false;
        }        

        private void Log(string s, params object?[] args)
        {
            Invoke(new Action(() =>
            {
                Program.Log(s, args);
            }));
        }

        private void controllerPortBox_ValueChanged(object sender, EventArgs e)
        {
            controllerIndex = (int)controllerPortBox.Value - 1;
            Properties.Settings.Default.XInputPort = controllerIndex;
            Properties.Settings.Default.Save();
        }

        private void gameVersionToggle_SelectedIndexChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.GameVersion = gameVersionToggle.SelectedIndex;
            Properties.Settings.Default.Save();
        }

        private void setButton_Click(object sender, EventArgs e)
        {
            SetWarpPoint();
        }

        private void recallButton_Click(object sender, EventArgs e)
        {
            RecallWarpPoint();
        }

        private void numEnemies_ValueChanged(object sender, EventArgs e)
        {
            if (hooked)
                conviction.EnemiesLeft = (int)numEnemies.Value;
        }
    }
}
