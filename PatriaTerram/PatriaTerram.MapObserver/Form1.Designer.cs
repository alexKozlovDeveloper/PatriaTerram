namespace PatriaTerram.MapObserver
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mapPictureBox = new System.Windows.Forms.PictureBox();
            this.startButton = new System.Windows.Forms.Button();
            this.sizeComboBox = new System.Windows.Forms.ComboBox();
            this.exponentiationButton = new System.Windows.Forms.Button();
            this.lowEdgeTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.highEdgeTextBox = new System.Windows.Forms.TextBox();
            this.clearLowValueButton = new System.Windows.Forms.Button();
            this.clearHighValueButton = new System.Windows.Forms.Button();
            this.paletteButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.oceanEdgeTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.mountainsTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.fertileSoilBottomEdgeTextBox = new System.Windows.Forms.TextBox();
            this.fertileSoilTopEdgeTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.woodBottomEdgeTextBox = new System.Windows.Forms.TextBox();
            this.woodTopEdgeTextBox = new System.Windows.Forms.TextBox();
            this.stoneBottomEdgeTextBox = new System.Windows.Forms.TextBox();
            this.stoneTopEdgeTextBox = new System.Windows.Forms.TextBox();
            this.lakeBottomEdgeTextBox = new System.Windows.Forms.TextBox();
            this.lakeTopEdgeTextBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.beachSizeTextBox = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.pixelSizeTextBox = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.seedTextBox = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.SmoothingSizeTextBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.mapPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // mapPictureBox
            // 
            this.mapPictureBox.Location = new System.Drawing.Point(145, 12);
            this.mapPictureBox.Name = "mapPictureBox";
            this.mapPictureBox.Size = new System.Drawing.Size(2500, 2500);
            this.mapPictureBox.TabIndex = 0;
            this.mapPictureBox.TabStop = false;
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(12, 62);
            this.startButton.Name = "startButton";
            this.startButton.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.startButton.Size = new System.Drawing.Size(127, 28);
            this.startButton.TabIndex = 1;
            this.startButton.Text = "Start Perlin Noise";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // sizeComboBox
            // 
            this.sizeComboBox.FormattingEnabled = true;
            this.sizeComboBox.Items.AddRange(new object[] {
            "2",
            "4",
            "8",
            "16",
            "32",
            "64",
            "128",
            "256",
            "512",
            "1024",
            "2048",
            "4096",
            "8192",
            "16 384",
            "32 768"});
            this.sizeComboBox.Location = new System.Drawing.Point(12, 96);
            this.sizeComboBox.Name = "sizeComboBox";
            this.sizeComboBox.Size = new System.Drawing.Size(127, 24);
            this.sizeComboBox.TabIndex = 2;
            this.sizeComboBox.Text = "128";
            // 
            // exponentiationButton
            // 
            this.exponentiationButton.Location = new System.Drawing.Point(12, 154);
            this.exponentiationButton.Name = "exponentiationButton";
            this.exponentiationButton.Size = new System.Drawing.Size(127, 47);
            this.exponentiationButton.TabIndex = 3;
            this.exponentiationButton.Text = "Exponentiation";
            this.exponentiationButton.UseVisualStyleBackColor = true;
            this.exponentiationButton.Click += new System.EventHandler(this.exponentiationButton_Click);
            // 
            // lowEdgeTextBox
            // 
            this.lowEdgeTextBox.Location = new System.Drawing.Point(11, 224);
            this.lowEdgeTextBox.Name = "lowEdgeTextBox";
            this.lowEdgeTextBox.Size = new System.Drawing.Size(67, 22);
            this.lowEdgeTextBox.TabIndex = 4;
            this.lowEdgeTextBox.Text = "80";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 204);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 17);
            this.label1.TabIndex = 5;
            this.label1.Text = "Clear low Value";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 255);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "Clear high Value";
            // 
            // highEdgeTextBox
            // 
            this.highEdgeTextBox.Location = new System.Drawing.Point(11, 275);
            this.highEdgeTextBox.Name = "highEdgeTextBox";
            this.highEdgeTextBox.Size = new System.Drawing.Size(67, 22);
            this.highEdgeTextBox.TabIndex = 7;
            this.highEdgeTextBox.Text = "200";
            // 
            // clearLowValueButton
            // 
            this.clearLowValueButton.Location = new System.Drawing.Point(84, 224);
            this.clearLowValueButton.Name = "clearLowValueButton";
            this.clearLowValueButton.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.clearLowValueButton.Size = new System.Drawing.Size(54, 28);
            this.clearLowValueButton.TabIndex = 8;
            this.clearLowValueButton.Text = "Clear";
            this.clearLowValueButton.UseVisualStyleBackColor = true;
            this.clearLowValueButton.Click += new System.EventHandler(this.clearLowValueButton_Click);
            // 
            // clearHighValueButton
            // 
            this.clearHighValueButton.Location = new System.Drawing.Point(84, 275);
            this.clearHighValueButton.Name = "clearHighValueButton";
            this.clearHighValueButton.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.clearHighValueButton.Size = new System.Drawing.Size(54, 28);
            this.clearHighValueButton.TabIndex = 9;
            this.clearHighValueButton.Text = "Clear";
            this.clearHighValueButton.UseVisualStyleBackColor = true;
            this.clearHighValueButton.Click += new System.EventHandler(this.clearHighValueButton_Click);
            // 
            // paletteButton
            // 
            this.paletteButton.Location = new System.Drawing.Point(12, 309);
            this.paletteButton.Name = "paletteButton";
            this.paletteButton.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.paletteButton.Size = new System.Drawing.Size(127, 47);
            this.paletteButton.TabIndex = 10;
            this.paletteButton.Text = "Start Palette";
            this.paletteButton.UseVisualStyleBackColor = true;
            this.paletteButton.Click += new System.EventHandler(this.paletteButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 359);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 17);
            this.label3.TabIndex = 11;
            this.label3.Text = "Ocean Edge";
            // 
            // oceanEdgeTextBox
            // 
            this.oceanEdgeTextBox.Location = new System.Drawing.Point(12, 379);
            this.oceanEdgeTextBox.Name = "oceanEdgeTextBox";
            this.oceanEdgeTextBox.Size = new System.Drawing.Size(67, 22);
            this.oceanEdgeTextBox.TabIndex = 12;
            this.oceanEdgeTextBox.Text = "120";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 404);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 17);
            this.label4.TabIndex = 13;
            this.label4.Text = "Mountains Edge";
            // 
            // mountainsTextBox
            // 
            this.mountainsTextBox.Location = new System.Drawing.Point(12, 424);
            this.mountainsTextBox.Name = "mountainsTextBox";
            this.mountainsTextBox.Size = new System.Drawing.Size(67, 22);
            this.mountainsTextBox.TabIndex = 14;
            this.mountainsTextBox.Text = "160";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 449);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 17);
            this.label5.TabIndex = 15;
            this.label5.Text = "Fertile Soil";
            // 
            // fertileSoilBottomEdgeTextBox
            // 
            this.fertileSoilBottomEdgeTextBox.Location = new System.Drawing.Point(12, 469);
            this.fertileSoilBottomEdgeTextBox.Name = "fertileSoilBottomEdgeTextBox";
            this.fertileSoilBottomEdgeTextBox.Size = new System.Drawing.Size(55, 22);
            this.fertileSoilBottomEdgeTextBox.TabIndex = 16;
            this.fertileSoilBottomEdgeTextBox.Text = "122";
            // 
            // fertileSoilTopEdgeTextBox
            // 
            this.fertileSoilTopEdgeTextBox.Location = new System.Drawing.Point(84, 469);
            this.fertileSoilTopEdgeTextBox.Name = "fertileSoilTopEdgeTextBox";
            this.fertileSoilTopEdgeTextBox.Size = new System.Drawing.Size(55, 22);
            this.fertileSoilTopEdgeTextBox.TabIndex = 17;
            this.fertileSoilTopEdgeTextBox.Text = "139";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 584);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(39, 17);
            this.label6.TabIndex = 18;
            this.label6.Text = "Lake";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 494);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(45, 17);
            this.label7.TabIndex = 19;
            this.label7.Text = "Wood";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 539);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(45, 17);
            this.label8.TabIndex = 20;
            this.label8.Text = "Stone";
            // 
            // woodBottomEdgeTextBox
            // 
            this.woodBottomEdgeTextBox.Location = new System.Drawing.Point(12, 514);
            this.woodBottomEdgeTextBox.Name = "woodBottomEdgeTextBox";
            this.woodBottomEdgeTextBox.Size = new System.Drawing.Size(55, 22);
            this.woodBottomEdgeTextBox.TabIndex = 21;
            this.woodBottomEdgeTextBox.Text = "124";
            // 
            // woodTopEdgeTextBox
            // 
            this.woodTopEdgeTextBox.Location = new System.Drawing.Point(85, 514);
            this.woodTopEdgeTextBox.Name = "woodTopEdgeTextBox";
            this.woodTopEdgeTextBox.Size = new System.Drawing.Size(55, 22);
            this.woodTopEdgeTextBox.TabIndex = 22;
            this.woodTopEdgeTextBox.Text = "160";
            // 
            // stoneBottomEdgeTextBox
            // 
            this.stoneBottomEdgeTextBox.Location = new System.Drawing.Point(12, 559);
            this.stoneBottomEdgeTextBox.Name = "stoneBottomEdgeTextBox";
            this.stoneBottomEdgeTextBox.Size = new System.Drawing.Size(55, 22);
            this.stoneBottomEdgeTextBox.TabIndex = 23;
            this.stoneBottomEdgeTextBox.Text = "165";
            // 
            // stoneTopEdgeTextBox
            // 
            this.stoneTopEdgeTextBox.Location = new System.Drawing.Point(84, 559);
            this.stoneTopEdgeTextBox.Name = "stoneTopEdgeTextBox";
            this.stoneTopEdgeTextBox.Size = new System.Drawing.Size(55, 22);
            this.stoneTopEdgeTextBox.TabIndex = 24;
            this.stoneTopEdgeTextBox.Text = "200";
            // 
            // lakeBottomEdgeTextBox
            // 
            this.lakeBottomEdgeTextBox.Location = new System.Drawing.Point(12, 604);
            this.lakeBottomEdgeTextBox.Name = "lakeBottomEdgeTextBox";
            this.lakeBottomEdgeTextBox.Size = new System.Drawing.Size(55, 22);
            this.lakeBottomEdgeTextBox.TabIndex = 25;
            this.lakeBottomEdgeTextBox.Text = "160";
            // 
            // lakeTopEdgeTextBox
            // 
            this.lakeTopEdgeTextBox.Location = new System.Drawing.Point(84, 604);
            this.lakeTopEdgeTextBox.Name = "lakeTopEdgeTextBox";
            this.lakeTopEdgeTextBox.Size = new System.Drawing.Size(55, 22);
            this.lakeTopEdgeTextBox.TabIndex = 26;
            this.lakeTopEdgeTextBox.Text = "200";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 629);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(77, 17);
            this.label9.TabIndex = 27;
            this.label9.Text = "Beach size";
            // 
            // beachSizeTextBox
            // 
            this.beachSizeTextBox.Location = new System.Drawing.Point(12, 649);
            this.beachSizeTextBox.Name = "beachSizeTextBox";
            this.beachSizeTextBox.Size = new System.Drawing.Size(55, 22);
            this.beachSizeTextBox.TabIndex = 28;
            this.beachSizeTextBox.Text = "5";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 129);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(66, 17);
            this.label10.TabIndex = 29;
            this.label10.Text = "Pixel size";
            // 
            // pixelSizeTextBox
            // 
            this.pixelSizeTextBox.Location = new System.Drawing.Point(84, 126);
            this.pixelSizeTextBox.Name = "pixelSizeTextBox";
            this.pixelSizeTextBox.Size = new System.Drawing.Size(55, 22);
            this.pixelSizeTextBox.TabIndex = 30;
            this.pixelSizeTextBox.Text = "4";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(9, 9);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(41, 17);
            this.label11.TabIndex = 31;
            this.label11.Text = "Seed";
            // 
            // seedTextBox
            // 
            this.seedTextBox.Location = new System.Drawing.Point(56, 6);
            this.seedTextBox.Name = "seedTextBox";
            this.seedTextBox.Size = new System.Drawing.Size(82, 22);
            this.seedTextBox.TabIndex = 32;
            this.seedTextBox.Text = "666";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(9, 35);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(75, 17);
            this.label12.TabIndex = 33;
            this.label12.Text = "Smoothing";
            // 
            // SmoothingSizeTextBox
            // 
            this.SmoothingSizeTextBox.Location = new System.Drawing.Point(84, 34);
            this.SmoothingSizeTextBox.Name = "SmoothingSizeTextBox";
            this.SmoothingSizeTextBox.Size = new System.Drawing.Size(55, 22);
            this.SmoothingSizeTextBox.TabIndex = 34;
            this.SmoothingSizeTextBox.Text = "1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1243, 752);
            this.Controls.Add(this.SmoothingSizeTextBox);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.seedTextBox);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.pixelSizeTextBox);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.beachSizeTextBox);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.lakeTopEdgeTextBox);
            this.Controls.Add(this.lakeBottomEdgeTextBox);
            this.Controls.Add(this.stoneTopEdgeTextBox);
            this.Controls.Add(this.stoneBottomEdgeTextBox);
            this.Controls.Add(this.woodTopEdgeTextBox);
            this.Controls.Add(this.woodBottomEdgeTextBox);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.fertileSoilTopEdgeTextBox);
            this.Controls.Add(this.fertileSoilBottomEdgeTextBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.mountainsTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.oceanEdgeTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.paletteButton);
            this.Controls.Add(this.clearHighValueButton);
            this.Controls.Add(this.clearLowValueButton);
            this.Controls.Add(this.highEdgeTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lowEdgeTextBox);
            this.Controls.Add(this.exponentiationButton);
            this.Controls.Add(this.sizeComboBox);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.mapPictureBox);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.mapPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox mapPictureBox;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.ComboBox sizeComboBox;
        private System.Windows.Forms.Button exponentiationButton;
        private System.Windows.Forms.TextBox lowEdgeTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox highEdgeTextBox;
        private System.Windows.Forms.Button clearLowValueButton;
        private System.Windows.Forms.Button clearHighValueButton;
        private System.Windows.Forms.Button paletteButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox oceanEdgeTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox mountainsTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox fertileSoilBottomEdgeTextBox;
        private System.Windows.Forms.TextBox fertileSoilTopEdgeTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox woodBottomEdgeTextBox;
        private System.Windows.Forms.TextBox woodTopEdgeTextBox;
        private System.Windows.Forms.TextBox stoneBottomEdgeTextBox;
        private System.Windows.Forms.TextBox stoneTopEdgeTextBox;
        private System.Windows.Forms.TextBox lakeBottomEdgeTextBox;
        private System.Windows.Forms.TextBox lakeTopEdgeTextBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox beachSizeTextBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox pixelSizeTextBox;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox seedTextBox;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox SmoothingSizeTextBox;
    }
}

