using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using System.IO.Pipes;
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
        private StreamReader inputStream;
        private StreamWriter outputStream;

        private NamedPipeServerStream toApp = new NamedPipeServerStream("toApp", PipeDirection.In);
        private NamedPipeServerStream fromApp = new NamedPipeServerStream("fromApp", PipeDirection.Out);

        private byte[] inputBuffer = new byte[API.PipeBufferSize];
        private byte[] outputBuffer = new byte[API.PipeBufferSize];

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
            backgroundWorker3.DoWork += new DoWorkEventHandler(WaitforPipeConnections);
            backgroundWorker4.DoWork += new DoWorkEventHandler(ReadPipeThread);
        }

        private void GetTransform()
        {
            posX.Value = (decimal)conviction.p1.Transform.PosX;
            posY.Value = (decimal)conviction.p1.Transform.PosY;
            posZ.Value = (decimal)conviction.p1.Transform.PosZ;
            rotY.Value = conviction.p1.Transform.RotY;
            velX.Value = (decimal)conviction.p1.Transform.VelX;
            velY.Value = (decimal)conviction.p1.Transform.VelY;
            velZ.Value = (decimal)conviction.p1.Transform.VelZ;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void posX_ValueChanged(object sender, EventArgs e)
        {
            if (hooked)
                conviction.p1.Transform.PosX = (float)posX.Value;
        }

        private void posY_ValueChanged(object sender, EventArgs e)
        {
            if (hooked)
                conviction.p1.Transform.PosY = (float)posY.Value;
        }

        private void posZ_ValueChanged(object sender, EventArgs e)
        {
            if (hooked)
                conviction.p1.Transform.PosZ = (float)posZ.Value;
        }

        private void rotY_ValueChanged(object sender, EventArgs e)
        {
            if (hooked)
                conviction.p1.Transform.RotY = (ushort)rotY.Value;
        }

        private void velX_ValueChanged(object sender, EventArgs e)
        {
            if (hooked)
                conviction.p1.Transform.VelX = (float)velX.Value;
        }

        private void velY_ValueChanged(object sender, EventArgs e)
        {
            if (hooked)
                conviction.p1.Transform.VelY = (float)velY.Value;
        }

        private void velZ_ValueChanged(object sender, EventArgs e)
        {
            if (hooked)
                conviction.p1.Transform.VelZ = (float)velZ.Value;
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
                //Invoke(new Action(() =>
                //{
                //    conviction.CheckIfReloaded();
                //    oldCounterAddr.Text = String.Format("Old Scene Counter Addr: {0:X8}", conviction.oldSceneCounterAddr);
                //    newCounterAddr.Text = String.Format("Scene Counter Addr: {0:X8}", conviction.p1.SceneCounter);
                //}));

                mapName = conviction.MapName;

                if (mapName.ToLower() != "menu")
                {
                    Invoke(new Action(() =>
                    {
                        curMapLabel.Text = "Current Map: " + mapName;
                        GetTransform();
                        numEnemies.Value = (decimal)conviction.EnemiesLeft;
                        sceneCounterLabel.Text = String.Format("Scene Counter Addr: {0}", conviction.p1.SceneCounter);
                    }));

                    XInput.XInputGetState(controllerIndex, ref xInputState);

                    movementMultiplier = 1f;

                    if (xInputState.gamepad.bLeftTrigger > triggerThreshold)
                        movementMultiplier *= .5f;
                    if (xInputState.gamepad.bRightTrigger > triggerThreshold)
                        movementMultiplier *= 2f;

                    if (XInput.isButtonDown(XInput.Button.LB, xInputState))
                        conviction.p1.Transform.PosY -= movementSpeed * movementMultiplier;
                    if (XInput.isButtonDown(XInput.Button.RB, xInputState))
                        conviction.p1.Transform.PosY += movementSpeed * movementMultiplier;

                    if (XInput.isButtonDown(XInput.Button.DPad_Left, xInputState))
                        conviction.p1.Transform.PosX -= movementSpeed * movementMultiplier;
                    if (XInput.isButtonDown(XInput.Button.DPad_Right, xInputState))
                        conviction.p1.Transform.PosX += movementSpeed * movementMultiplier;
                    if (XInput.isButtonDown(XInput.Button.DPad_Up, xInputState))
                        conviction.p1.Transform.PosZ -= movementSpeed * movementMultiplier;
                    if (XInput.isButtonDown(XInput.Button.DPad_Down, xInputState))
                        conviction.p1.Transform.PosZ += movementSpeed * movementMultiplier;

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
                warpPoint.PosX = conviction.p1.Transform.PosX;
                warpPoint.PosY = conviction.p1.Transform.PosY;
                warpPoint.PosZ = conviction.p1.Transform.PosZ;
                warpPoint.RotY = conviction.p1.Transform.RotY;
                Log("Warp point set ({0},{1},{2})", warpPoint.PosX, warpPoint.PosY, warpPoint.PosZ);
            }
        }

        private void RecallWarpPoint()
        {
            if (hooked)
            {
                conviction.p1.Transform.PosX = warpPoint.PosX;
                conviction.p1.Transform.PosY = warpPoint.PosY;
                conviction.p1.Transform.PosZ = warpPoint.PosZ;
                conviction.p1.Transform.RotY = warpPoint.RotY;
                Log("Warp point recalled ({0},{1},{2})", warpPoint.PosX, warpPoint.PosY, warpPoint.PosZ);
            }
        }

        private void hookButton_Click(object sender, EventArgs e)
        {
            inputStream  = new StreamReader(toApp);
            outputStream = new StreamWriter(fromApp);

            Memory.HookProgram("Conviction", ProcessMode.x86);
            if (Memory.GameIsRunning)
            {
                hooked = true;
                ActivateForm();
                conviction = new ConvictionGame((SCCVersion)gameVersionToggle.SelectedIndex);
                backgroundWorker1.RunWorkerAsync();
                backgroundWorker2.RunWorkerAsync();
                backgroundWorker3.RunWorkerAsync();
            }
            else
            {
                Memory.Close();
                Log("Unable to open game.");
            }
        }

        private void WaitforPipeConnections(object sender, DoWorkEventArgs e)
        {
            toApp.WaitForConnection();
            fromApp.WaitForConnection();
            backgroundWorker4.RunWorkerAsync();
        }

        private void ReadPipeThread(object sender, DoWorkEventArgs e)
        {
            while (hooked)
            {
                ZeroInputBuffer();
                inputStream.BaseStream.Read(inputBuffer, 0, API.PipeBufferSize);

                MessageType type = (MessageType)inputBuffer[0];

                switch (type)
                {
                    case MessageType.Success:
                        Log("DLL says hi");
                        break;
                    case MessageType.Esam:
                        int newEsam = BitConverter.ToInt32(inputBuffer, 1);
                        Log("ESam address changed to 0x{0:X8}", newEsam);
                        conviction.p1.Address = (ulong)newEsam;
                        break;
                    default:
                        break;
                }
            }
        }

        private void ZeroInputBuffer()
        {
            for (int i = 0; i < API.PipeBufferSize; i++)
                inputBuffer[i] = 0;
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

        private void Log(string s, params object[] args)
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
