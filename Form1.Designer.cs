
namespace SCC_Trainer
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.posX = new System.Windows.Forms.NumericUpDown();
            this.posY = new System.Windows.Forms.NumericUpDown();
            this.posZ = new System.Windows.Forms.NumericUpDown();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.curMapLabel = new System.Windows.Forms.Label();
            this.gameVersionToggle = new System.Windows.Forms.ComboBox();
            this.hookButton = new System.Windows.Forms.Button();
            this.rotY = new System.Windows.Forms.NumericUpDown();
            this.xposLabel = new System.Windows.Forms.Label();
            this.yposLabel = new System.Windows.Forms.Label();
            this.zposLabel = new System.Windows.Forms.Label();
            this.yrotLabel = new System.Windows.Forms.Label();
            this.logBox = new System.Windows.Forms.TextBox();
            this.oldCounterAddr = new System.Windows.Forms.Label();
            this.newCounterAddr = new System.Windows.Forms.Label();
            this.controllerPortBox = new System.Windows.Forms.NumericUpDown();
            this.controllerLabel = new System.Windows.Forms.Label();
            this.sceneCounterLabel = new System.Windows.Forms.Label();
            this.zvelLabel = new System.Windows.Forms.Label();
            this.yvelLabel = new System.Windows.Forms.Label();
            this.xvelLabel = new System.Windows.Forms.Label();
            this.velZ = new System.Windows.Forms.NumericUpDown();
            this.velY = new System.Windows.Forms.NumericUpDown();
            this.velX = new System.Windows.Forms.NumericUpDown();
            this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            this.setButton = new System.Windows.Forms.Button();
            this.recallButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.posX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.posY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.posZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rotY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.controllerPortBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.velZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.velY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.velX)).BeginInit();
            this.SuspendLayout();
            // 
            // posX
            // 
            this.posX.DecimalPlaces = 4;
            this.posX.Location = new System.Drawing.Point(82, 93);
            this.posX.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.posX.Minimum = new decimal(new int[] {
            2147483647,
            0,
            0,
            -2147483648});
            this.posX.Name = "posX";
            this.posX.Size = new System.Drawing.Size(94, 23);
            this.posX.TabIndex = 1;
            this.posX.ValueChanged += new System.EventHandler(this.posX_ValueChanged);
            // 
            // posY
            // 
            this.posY.DecimalPlaces = 4;
            this.posY.Location = new System.Drawing.Point(182, 93);
            this.posY.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.posY.Minimum = new decimal(new int[] {
            2147483647,
            0,
            0,
            -2147483648});
            this.posY.Name = "posY";
            this.posY.Size = new System.Drawing.Size(94, 23);
            this.posY.TabIndex = 2;
            this.posY.ValueChanged += new System.EventHandler(this.posY_ValueChanged);
            // 
            // posZ
            // 
            this.posZ.DecimalPlaces = 4;
            this.posZ.Location = new System.Drawing.Point(282, 93);
            this.posZ.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.posZ.Minimum = new decimal(new int[] {
            2147483647,
            0,
            0,
            -2147483648});
            this.posZ.Name = "posZ";
            this.posZ.Size = new System.Drawing.Size(94, 23);
            this.posZ.TabIndex = 3;
            this.posZ.ValueChanged += new System.EventHandler(this.posZ_ValueChanged);
            // 
            // curMapLabel
            // 
            this.curMapLabel.AutoSize = true;
            this.curMapLabel.Location = new System.Drawing.Point(12, 9);
            this.curMapLabel.Name = "curMapLabel";
            this.curMapLabel.Size = new System.Drawing.Size(77, 15);
            this.curMapLabel.TabIndex = 5;
            this.curMapLabel.Text = "Current Map:";
            // 
            // gameVersionToggle
            // 
            this.gameVersionToggle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.gameVersionToggle.FormattingEnabled = true;
            this.gameVersionToggle.Items.AddRange(new object[] {
            "Steam",
            "Uplay"});
            this.gameVersionToggle.Location = new System.Drawing.Point(502, 12);
            this.gameVersionToggle.Name = "gameVersionToggle";
            this.gameVersionToggle.Size = new System.Drawing.Size(82, 23);
            this.gameVersionToggle.TabIndex = 6;
            this.gameVersionToggle.SelectedIndexChanged += new System.EventHandler(this.gameVersionToggle_SelectedIndexChanged);
            // 
            // hookButton
            // 
            this.hookButton.Location = new System.Drawing.Point(502, 43);
            this.hookButton.Name = "hookButton";
            this.hookButton.Size = new System.Drawing.Size(82, 25);
            this.hookButton.TabIndex = 7;
            this.hookButton.Text = "Hook Game";
            this.hookButton.UseVisualStyleBackColor = true;
            this.hookButton.Click += new System.EventHandler(this.hookButton_Click);
            // 
            // rotY
            // 
            this.rotY.Location = new System.Drawing.Point(382, 93);
            this.rotY.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.rotY.Name = "rotY";
            this.rotY.Size = new System.Drawing.Size(94, 23);
            this.rotY.TabIndex = 8;
            this.rotY.ValueChanged += new System.EventHandler(this.rotY_ValueChanged);
            // 
            // xposLabel
            // 
            this.xposLabel.AutoSize = true;
            this.xposLabel.Location = new System.Drawing.Point(82, 75);
            this.xposLabel.Name = "xposLabel";
            this.xposLabel.Size = new System.Drawing.Size(60, 15);
            this.xposLabel.TabIndex = 9;
            this.xposLabel.Text = "X Position";
            // 
            // yposLabel
            // 
            this.yposLabel.AutoSize = true;
            this.yposLabel.Location = new System.Drawing.Point(182, 75);
            this.yposLabel.Name = "yposLabel";
            this.yposLabel.Size = new System.Drawing.Size(60, 15);
            this.yposLabel.TabIndex = 10;
            this.yposLabel.Text = "Y Position";
            // 
            // zposLabel
            // 
            this.zposLabel.AutoSize = true;
            this.zposLabel.Location = new System.Drawing.Point(282, 75);
            this.zposLabel.Name = "zposLabel";
            this.zposLabel.Size = new System.Drawing.Size(60, 15);
            this.zposLabel.TabIndex = 11;
            this.zposLabel.Text = "Z Position";
            // 
            // yrotLabel
            // 
            this.yrotLabel.AutoSize = true;
            this.yrotLabel.Location = new System.Drawing.Point(382, 75);
            this.yrotLabel.Name = "yrotLabel";
            this.yrotLabel.Size = new System.Drawing.Size(62, 15);
            this.yrotLabel.TabIndex = 12;
            this.yrotLabel.Text = "Y Rotation";
            // 
            // logBox
            // 
            this.logBox.Location = new System.Drawing.Point(82, 175);
            this.logBox.Multiline = true;
            this.logBox.Name = "logBox";
            this.logBox.ReadOnly = true;
            this.logBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.logBox.Size = new System.Drawing.Size(394, 119);
            this.logBox.TabIndex = 13;
            // 
            // oldCounterAddr
            // 
            this.oldCounterAddr.AutoSize = true;
            this.oldCounterAddr.Location = new System.Drawing.Point(12, 39);
            this.oldCounterAddr.Name = "oldCounterAddr";
            this.oldCounterAddr.Size = new System.Drawing.Size(138, 15);
            this.oldCounterAddr.TabIndex = 14;
            this.oldCounterAddr.Text = "Old Scene Counter Addr:";
            // 
            // newCounterAddr
            // 
            this.newCounterAddr.AutoSize = true;
            this.newCounterAddr.Location = new System.Drawing.Point(12, 53);
            this.newCounterAddr.Name = "newCounterAddr";
            this.newCounterAddr.Size = new System.Drawing.Size(143, 15);
            this.newCounterAddr.TabIndex = 15;
            this.newCounterAddr.Text = "New Scene Counter Addr:";
            // 
            // controllerPortBox
            // 
            this.controllerPortBox.Location = new System.Drawing.Point(448, 12);
            this.controllerPortBox.Maximum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.controllerPortBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.controllerPortBox.Name = "controllerPortBox";
            this.controllerPortBox.Size = new System.Drawing.Size(48, 23);
            this.controllerPortBox.TabIndex = 16;
            this.controllerPortBox.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.controllerPortBox.ValueChanged += new System.EventHandler(this.controllerPortBox_ValueChanged);
            // 
            // controllerLabel
            // 
            this.controllerLabel.AutoSize = true;
            this.controllerLabel.Location = new System.Drawing.Point(374, 14);
            this.controllerLabel.Name = "controllerLabel";
            this.controllerLabel.Size = new System.Drawing.Size(70, 15);
            this.controllerLabel.TabIndex = 17;
            this.controllerLabel.Text = "XInput Port:";
            this.controllerLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // sceneCounterLabel
            // 
            this.sceneCounterLabel.AutoSize = true;
            this.sceneCounterLabel.Location = new System.Drawing.Point(12, 24);
            this.sceneCounterLabel.Name = "sceneCounterLabel";
            this.sceneCounterLabel.Size = new System.Drawing.Size(85, 15);
            this.sceneCounterLabel.TabIndex = 18;
            this.sceneCounterLabel.Text = "Scene counter:";
            // 
            // zvelLabel
            // 
            this.zvelLabel.AutoSize = true;
            this.zvelLabel.Location = new System.Drawing.Point(282, 128);
            this.zvelLabel.Name = "zvelLabel";
            this.zvelLabel.Size = new System.Drawing.Size(58, 15);
            this.zvelLabel.TabIndex = 24;
            this.zvelLabel.Text = "Z Velocity";
            // 
            // yvelLabel
            // 
            this.yvelLabel.AutoSize = true;
            this.yvelLabel.Location = new System.Drawing.Point(182, 128);
            this.yvelLabel.Name = "yvelLabel";
            this.yvelLabel.Size = new System.Drawing.Size(58, 15);
            this.yvelLabel.TabIndex = 23;
            this.yvelLabel.Text = "Y Velocity";
            // 
            // xvelLabel
            // 
            this.xvelLabel.AutoSize = true;
            this.xvelLabel.Location = new System.Drawing.Point(82, 128);
            this.xvelLabel.Name = "xvelLabel";
            this.xvelLabel.Size = new System.Drawing.Size(58, 15);
            this.xvelLabel.TabIndex = 22;
            this.xvelLabel.Text = "X Velocity";
            // 
            // velZ
            // 
            this.velZ.DecimalPlaces = 4;
            this.velZ.Location = new System.Drawing.Point(282, 146);
            this.velZ.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.velZ.Minimum = new decimal(new int[] {
            2147483647,
            0,
            0,
            -2147483648});
            this.velZ.Name = "velZ";
            this.velZ.Size = new System.Drawing.Size(94, 23);
            this.velZ.TabIndex = 21;
            this.velZ.ValueChanged += new System.EventHandler(this.velZ_ValueChanged);
            // 
            // velY
            // 
            this.velY.DecimalPlaces = 4;
            this.velY.Location = new System.Drawing.Point(182, 146);
            this.velY.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.velY.Minimum = new decimal(new int[] {
            2147483647,
            0,
            0,
            -2147483648});
            this.velY.Name = "velY";
            this.velY.Size = new System.Drawing.Size(94, 23);
            this.velY.TabIndex = 20;
            this.velY.ValueChanged += new System.EventHandler(this.velY_ValueChanged);
            // 
            // velX
            // 
            this.velX.DecimalPlaces = 4;
            this.velX.Location = new System.Drawing.Point(82, 146);
            this.velX.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.velX.Minimum = new decimal(new int[] {
            2147483647,
            0,
            0,
            -2147483648});
            this.velX.Name = "velX";
            this.velX.Size = new System.Drawing.Size(94, 23);
            this.velX.TabIndex = 19;
            this.velX.ValueChanged += new System.EventHandler(this.velX_ValueChanged);
            // 
            // setButton
            // 
            this.setButton.Location = new System.Drawing.Point(502, 90);
            this.setButton.Name = "setButton";
            this.setButton.Size = new System.Drawing.Size(82, 25);
            this.setButton.TabIndex = 25;
            this.setButton.Text = "Set Warp";
            this.setButton.UseVisualStyleBackColor = true;
            this.setButton.Click += new System.EventHandler(this.setButton_Click);
            // 
            // recallButton
            // 
            this.recallButton.Location = new System.Drawing.Point(502, 123);
            this.recallButton.Name = "recallButton";
            this.recallButton.Size = new System.Drawing.Size(82, 25);
            this.recallButton.TabIndex = 26;
            this.recallButton.Text = "Recall Warp";
            this.recallButton.UseVisualStyleBackColor = true;
            this.recallButton.Click += new System.EventHandler(this.recallButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(596, 306);
            this.Controls.Add(this.recallButton);
            this.Controls.Add(this.setButton);
            this.Controls.Add(this.zvelLabel);
            this.Controls.Add(this.yvelLabel);
            this.Controls.Add(this.xvelLabel);
            this.Controls.Add(this.velZ);
            this.Controls.Add(this.velY);
            this.Controls.Add(this.velX);
            this.Controls.Add(this.sceneCounterLabel);
            this.Controls.Add(this.controllerLabel);
            this.Controls.Add(this.controllerPortBox);
            this.Controls.Add(this.newCounterAddr);
            this.Controls.Add(this.oldCounterAddr);
            this.Controls.Add(this.logBox);
            this.Controls.Add(this.yrotLabel);
            this.Controls.Add(this.zposLabel);
            this.Controls.Add(this.yposLabel);
            this.Controls.Add(this.xposLabel);
            this.Controls.Add(this.rotY);
            this.Controls.Add(this.hookButton);
            this.Controls.Add(this.gameVersionToggle);
            this.Controls.Add(this.curMapLabel);
            this.Controls.Add(this.posZ);
            this.Controls.Add(this.posY);
            this.Controls.Add(this.posX);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Splinter Cell Conviction Tool";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.posX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.posY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.posZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rotY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.controllerPortBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.velZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.velY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.velX)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.NumericUpDown posX;
        private System.Windows.Forms.NumericUpDown posY;
        private System.Windows.Forms.NumericUpDown posZ;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label curMapLabel;
        private System.Windows.Forms.ComboBox gameVersionToggle;
        private System.Windows.Forms.Button hookButton;
        private System.Windows.Forms.NumericUpDown rotY;
        private System.Windows.Forms.Label xposLabel;
        private System.Windows.Forms.Label yposLabel;
        private System.Windows.Forms.Label zposLabel;
        private System.Windows.Forms.Label yrotLabel;
        private System.Windows.Forms.TextBox logBox;
        private System.Windows.Forms.Label oldCounterAddr;
        private System.Windows.Forms.Label newCounterAddr;
        private System.Windows.Forms.NumericUpDown controllerPortBox;
        private System.Windows.Forms.Label controllerLabel;
        private System.Windows.Forms.Label sceneCounterLabel;
        private System.Windows.Forms.Label zvelLabel;
        private System.Windows.Forms.Label yvelLabel;
        private System.Windows.Forms.Label xvelLabel;
        private System.Windows.Forms.NumericUpDown velZ;
        private System.Windows.Forms.NumericUpDown velY;
        private System.Windows.Forms.NumericUpDown velX;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
        private System.Windows.Forms.Button setButton;
        private System.Windows.Forms.Button recallButton;
    }
}

