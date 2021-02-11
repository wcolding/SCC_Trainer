using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

            gameVersionToggle.SelectedIndex = 0;
            DeactivateForm();

            warpPoint = new Transform();
            set_warp_pressed = false;
            recall_warp_pressed = false;
        }

        private void GetTransform()
        {
            posX.Value = (decimal)conviction.player.transform.PosX;
            posY.Value = (decimal)conviction.player.transform.PosY;
            posZ.Value = (decimal)conviction.player.transform.PosZ;
            rotY.Value = conviction.player.transform.RotY;
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

        private void ReadGameState(object sender, DoWorkEventArgs e)
        {
            while (hooked)
            {
                if (Memory.handle == null)
                {
                    hooked = false;
                    DeactivateForm();
                    return;
                }

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
                Log("Warp point set!");
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
                Log("Warp point recalled!");
            }
        }

        private void hookButton_Click(object sender, EventArgs e)
        {
            Memory.HookProgram("Conviction", ProcessMode.x86);
            if (Memory.handle != null)
            {
                hooked = true;
                ActivateForm();
                backgroundWorker1.DoWork += new DoWorkEventHandler(ReadGameState);
                conviction = new ConvictionGame((SCCVersion)gameVersionToggle.SelectedIndex);
                backgroundWorker1.RunWorkerAsync();
            }
        }

        private void DeactivateForm()
        {
            curMapLabel.Enabled = false;
            posX.Enabled = false;
            posY.Enabled = false;
            posZ.Enabled = false;

            xposLabel.Enabled = false;
            yposLabel.Enabled = false;
            zposLabel.Enabled = false;
            yrotLabel.Enabled = false;

            gameVersionToggle.Enabled = true;
            hookButton.Enabled = true;
        }

        private void ActivateForm()
        {
            curMapLabel.Enabled = true;
            posX.Enabled = true;
            posY.Enabled = true;
            posZ.Enabled = true;

            xposLabel.Enabled = true;
            yposLabel.Enabled = true;
            zposLabel.Enabled = true;
            yrotLabel.Enabled = true;

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
    }
}
