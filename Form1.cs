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

        private int controllerIndex = 1;
        private float movementSpeed = .5f;
        private float movementMultiplier = 1f;
        private byte triggerThreshold = 20;
        private XInput.State xInputState;
        private string mapName;

        public Form1()
        {
            InitializeComponent();
            Memory.HookProgram("Conviction", ProcessMode.x86);
            if (Memory.handle != null)
            {
                backgroundWorker1.DoWork += new DoWorkEventHandler(ReadGameState);
                conviction = new ConvictionGame();
                backgroundWorker1.RunWorkerAsync();
            }
        }

        private void GetPosition()
        {
            posX.Value = (decimal)conviction.player.position.X;
            posY.Value = (decimal)conviction.player.position.Y;
            posZ.Value = (decimal)conviction.player.position.Z;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void posX_ValueChanged(object sender, EventArgs e)
        {
            conviction.player.position.X = (float)posX.Value;
        }

        private void posY_ValueChanged(object sender, EventArgs e)
        {
            conviction.player.position.Y = (float)posY.Value;
        }

        private void posZ_ValueChanged(object sender, EventArgs e)
        {
            conviction.player.position.Z = (float)posZ.Value;
        }

        private void ReadGameState(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                mapName = conviction.MapName;

                if (mapName.ToLower() != "menu")
                {
                    conviction.CheckIfReloaded();

                    Invoke(new Action(() =>
                    {
                        curMapLabel.Text = "Current Map: " + mapName;
                        GetPosition();
                    }));

                    XInput.XInputGetState(controllerIndex, ref xInputState);

                    movementMultiplier = 1f;

                    if (xInputState.gamepad.bLeftTrigger > triggerThreshold)
                        movementMultiplier *= .5f;
                    if (xInputState.gamepad.bRightTrigger > triggerThreshold)
                        movementMultiplier *= 2f;

                    if (XInput.isButtonDown(XInput.Button.LB, xInputState))
                        conviction.player.position.Y -= movementSpeed * movementMultiplier;
                    if (XInput.isButtonDown(XInput.Button.RB, xInputState))
                        conviction.player.position.Y += movementSpeed * movementMultiplier;

                    if (XInput.isButtonDown(XInput.Button.DPad_Left, xInputState))
                        conviction.player.position.X -= movementSpeed * movementMultiplier;
                    if (XInput.isButtonDown(XInput.Button.DPad_Right, xInputState))
                        conviction.player.position.X += movementSpeed * movementMultiplier;
                    if (XInput.isButtonDown(XInput.Button.DPad_Up, xInputState))
                        conviction.player.position.Z -= movementSpeed * movementMultiplier;
                    if (XInput.isButtonDown(XInput.Button.DPad_Down, xInputState))
                        conviction.player.position.Z += movementSpeed * movementMultiplier;
                }
            }
        }

        private void InitButtom_Click(object sender, EventArgs e)
        {
            if (conviction != null)
                conviction.Initialize();
        }
    }
}
