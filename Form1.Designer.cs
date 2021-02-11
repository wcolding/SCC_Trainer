
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
            ((System.ComponentModel.ISupportInitialize)(this.posX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.posY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.posZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rotY)).BeginInit();
            this.SuspendLayout();
            // 
            // posX
            // 
            this.posX.DecimalPlaces = 4;
            this.posX.Location = new System.Drawing.Point(82, 114);
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
            this.posY.Location = new System.Drawing.Point(182, 114);
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
            this.posZ.Location = new System.Drawing.Point(282, 114);
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
            this.rotY.Location = new System.Drawing.Point(382, 114);
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
            this.xposLabel.Location = new System.Drawing.Point(82, 96);
            this.xposLabel.Name = "xposLabel";
            this.xposLabel.Size = new System.Drawing.Size(60, 15);
            this.xposLabel.TabIndex = 9;
            this.xposLabel.Text = "X Position";
            // 
            // yposLabel
            // 
            this.yposLabel.AutoSize = true;
            this.yposLabel.Location = new System.Drawing.Point(182, 96);
            this.yposLabel.Name = "yposLabel";
            this.yposLabel.Size = new System.Drawing.Size(60, 15);
            this.yposLabel.TabIndex = 10;
            this.yposLabel.Text = "Y Position";
            // 
            // zposLabel
            // 
            this.zposLabel.AutoSize = true;
            this.zposLabel.Location = new System.Drawing.Point(282, 96);
            this.zposLabel.Name = "zposLabel";
            this.zposLabel.Size = new System.Drawing.Size(60, 15);
            this.zposLabel.TabIndex = 11;
            this.zposLabel.Text = "Z Position";
            // 
            // yrotLabel
            // 
            this.yrotLabel.AutoSize = true;
            this.yrotLabel.Location = new System.Drawing.Point(382, 96);
            this.yrotLabel.Name = "yrotLabel";
            this.yrotLabel.Size = new System.Drawing.Size(62, 15);
            this.yrotLabel.TabIndex = 12;
            this.yrotLabel.Text = "Y Rotation";
            // 
            // logBox
            // 
            this.logBox.Location = new System.Drawing.Point(82, 165);
            this.logBox.Multiline = true;
            this.logBox.Name = "logBox";
            this.logBox.ReadOnly = true;
            this.logBox.Size = new System.Drawing.Size(394, 68);
            this.logBox.TabIndex = 13;
            // 
            // oldCounterAddr
            // 
            this.oldCounterAddr.AutoSize = true;
            this.oldCounterAddr.Location = new System.Drawing.Point(12, 24);
            this.oldCounterAddr.Name = "oldCounterAddr";
            this.oldCounterAddr.Size = new System.Drawing.Size(138, 15);
            this.oldCounterAddr.TabIndex = 14;
            this.oldCounterAddr.Text = "Old Scene Counter Addr:";
            // 
            // newCounterAddr
            // 
            this.newCounterAddr.AutoSize = true;
            this.newCounterAddr.Location = new System.Drawing.Point(12, 39);
            this.newCounterAddr.Name = "newCounterAddr";
            this.newCounterAddr.Size = new System.Drawing.Size(143, 15);
            this.newCounterAddr.TabIndex = 15;
            this.newCounterAddr.Text = "New Scene Counter Addr:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(596, 245);
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
            this.Name = "Form1";
            this.Text = "SCC Trainer";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.posX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.posY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.posZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rotY)).EndInit();
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
    }
}

