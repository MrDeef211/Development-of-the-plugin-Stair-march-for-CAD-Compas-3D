namespace UI
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            HeightLabel = new Label();
            LengthLabel = new Label();
            StepAmountLabel = new Label();
            StepHeightLabel = new Label();
            HeightTextBox = new TextBox();
            LengthTextBox = new TextBox();
            StepAmountTextBox = new TextBox();
            StepHeightTextBox = new TextBox();
            WidthTextBox = new TextBox();
            StepProjectionLengthTextBox = new TextBox();
            StepProtjectionHeightTextBox = new TextBox();
            PlatformLengthDownTextBox = new TextBox();
            PlatformLengthUpTextBox = new TextBox();
            PlatformHeightTextBox = new TextBox();
            WidthLabel = new Label();
            StepProjectionLengthLabel = new Label();
            StepProtjectionHeightLabel = new Label();
            PlatformLengthDownLabel = new Label();
            PlatformLengthUpLabel = new Label();
            PlatformHeightLabel = new Label();
            HeightLimitsLabel = new Label();
            LengthLimitsLabel = new Label();
            StepAmountLimitsLabel = new Label();
            StepHeightLimitsLabel = new Label();
            WidthLimitsLabel = new Label();
            StepProjectionLengthLimitsLabel = new Label();
            StepProtjectionHeightLimitsLabel = new Label();
            PlatformLengthDownLimitsLabel = new Label();
            PlatformLengthUpLimitsLabel = new Label();
            PlatformHeightLimitsLabel = new Label();
            BuildButton = new Button();
            ErrorPanel = new Panel();
            ErrorTextBox = new TextBox();
            ErrorLabel = new Label();
            MainModelPictureBox = new PictureBox();
            MiniModelPictureBox = new PictureBox();
            PicturesSplitContainer = new SplitContainer();
            ErrorPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)MainModelPictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)MiniModelPictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PicturesSplitContainer).BeginInit();
            PicturesSplitContainer.Panel1.SuspendLayout();
            PicturesSplitContainer.Panel2.SuspendLayout();
            PicturesSplitContainer.SuspendLayout();
            SuspendLayout();
            // 
            // HeightLabel
            // 
            HeightLabel.AutoSize = true;
            HeightLabel.Font = new Font("Verdana", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            HeightLabel.Location = new Point(6, 8);
            HeightLabel.Name = "HeightLabel";
            HeightLabel.Size = new Size(145, 18);
            HeightLabel.TabIndex = 0;
            HeightLabel.Text = "Высота марша H";
            // 
            // LengthLabel
            // 
            LengthLabel.AutoSize = true;
            LengthLabel.Font = new Font("Verdana", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            LengthLabel.Location = new Point(6, 37);
            LengthLabel.Name = "LengthLabel";
            LengthLabel.Size = new Size(148, 18);
            LengthLabel.TabIndex = 1;
            LengthLabel.Text = "Длина пролёта L";
            // 
            // StepAmountLabel
            // 
            StepAmountLabel.AutoSize = true;
            StepAmountLabel.Font = new Font("Verdana", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            StepAmountLabel.Location = new Point(6, 66);
            StepAmountLabel.Name = "StepAmountLabel";
            StepAmountLabel.Size = new Size(157, 18);
            StepAmountLabel.TabIndex = 2;
            StepAmountLabel.Text = "Число ступеней N";
            // 
            // StepHeightLabel
            // 
            StepHeightLabel.AutoSize = true;
            StepHeightLabel.Font = new Font("Verdana", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            StepHeightLabel.Location = new Point(6, 95);
            StepHeightLabel.Name = "StepHeightLabel";
            StepHeightLabel.Size = new Size(164, 18);
            StepHeightLabel.TabIndex = 3;
            StepHeightLabel.Text = "Высота ступени h1";
            // 
            // HeightTextBox
            // 
            HeightTextBox.BackColor = SystemColors.Window;
            HeightTextBox.Location = new Point(261, 8);
            HeightTextBox.Name = "HeightTextBox";
            HeightTextBox.Size = new Size(100, 23);
            HeightTextBox.TabIndex = 4;
            HeightTextBox.Text = "2";
            HeightTextBox.KeyUp += ParameterEntered;
            // 
            // LengthTextBox
            // 
            LengthTextBox.BackColor = SystemColors.Window;
            LengthTextBox.Location = new Point(261, 37);
            LengthTextBox.Name = "LengthTextBox";
            LengthTextBox.Size = new Size(100, 23);
            LengthTextBox.TabIndex = 5;
            LengthTextBox.Text = "3";
            LengthTextBox.KeyUp += ParameterEntered;
            // 
            // StepAmountTextBox
            // 
            StepAmountTextBox.BackColor = SystemColors.Window;
            StepAmountTextBox.Location = new Point(261, 66);
            StepAmountTextBox.Name = "StepAmountTextBox";
            StepAmountTextBox.Size = new Size(100, 23);
            StepAmountTextBox.TabIndex = 6;
            StepAmountTextBox.Text = "10";
            StepAmountTextBox.KeyUp += ParameterEntered;
            // 
            // StepHeightTextBox
            // 
            StepHeightTextBox.Location = new Point(261, 95);
            StepHeightTextBox.Name = "StepHeightTextBox";
            StepHeightTextBox.Size = new Size(100, 23);
            StepHeightTextBox.TabIndex = 7;
            StepHeightTextBox.Text = "200";
            StepHeightTextBox.KeyUp += ParameterEntered;
            // 
            // WidthTextBox
            // 
            WidthTextBox.BackColor = SystemColors.Window;
            WidthTextBox.Location = new Point(261, 124);
            WidthTextBox.Name = "WidthTextBox";
            WidthTextBox.Size = new Size(100, 23);
            WidthTextBox.TabIndex = 8;
            WidthTextBox.Text = "2";
            WidthTextBox.KeyUp += ParameterEntered;
            // 
            // StepProjectionLengthTextBox
            // 
            StepProjectionLengthTextBox.Location = new Point(261, 153);
            StepProjectionLengthTextBox.Name = "StepProjectionLengthTextBox";
            StepProjectionLengthTextBox.Size = new Size(100, 23);
            StepProjectionLengthTextBox.TabIndex = 9;
            StepProjectionLengthTextBox.Text = "10";
            StepProjectionLengthTextBox.KeyUp += ParameterEntered;
            // 
            // StepProtjectionHeightTextBox
            // 
            StepProtjectionHeightTextBox.Location = new Point(261, 182);
            StepProtjectionHeightTextBox.Name = "StepProtjectionHeightTextBox";
            StepProtjectionHeightTextBox.Size = new Size(100, 23);
            StepProtjectionHeightTextBox.TabIndex = 10;
            StepProtjectionHeightTextBox.Text = "5";
            StepProtjectionHeightTextBox.KeyUp += ParameterEntered;
            // 
            // PlatformLengthDownTextBox
            // 
            PlatformLengthDownTextBox.Location = new Point(261, 211);
            PlatformLengthDownTextBox.Name = "PlatformLengthDownTextBox";
            PlatformLengthDownTextBox.Size = new Size(100, 23);
            PlatformLengthDownTextBox.TabIndex = 11;
            PlatformLengthDownTextBox.Text = "2";
            PlatformLengthDownTextBox.KeyUp += ParameterEntered;
            // 
            // PlatformLengthUpTextBox
            // 
            PlatformLengthUpTextBox.Location = new Point(261, 240);
            PlatformLengthUpTextBox.Name = "PlatformLengthUpTextBox";
            PlatformLengthUpTextBox.Size = new Size(100, 23);
            PlatformLengthUpTextBox.TabIndex = 12;
            PlatformLengthUpTextBox.Text = "2";
            PlatformLengthUpTextBox.KeyUp += ParameterEntered;
            // 
            // PlatformHeightTextBox
            // 
            PlatformHeightTextBox.Location = new Point(261, 269);
            PlatformHeightTextBox.Name = "PlatformHeightTextBox";
            PlatformHeightTextBox.Size = new Size(100, 23);
            PlatformHeightTextBox.TabIndex = 13;
            PlatformHeightTextBox.Text = "200";
            PlatformHeightTextBox.KeyUp += ParameterEntered;
            // 
            // WidthLabel
            // 
            WidthLabel.AutoSize = true;
            WidthLabel.Font = new Font("Verdana", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            WidthLabel.Location = new Point(6, 124);
            WidthLabel.Name = "WidthLabel";
            WidthLabel.Size = new Size(150, 18);
            WidthLabel.TabIndex = 14;
            WidthLabel.Text = "Ширина марша B";
            // 
            // StepProjectionLengthLabel
            // 
            StepProjectionLengthLabel.AutoSize = true;
            StepProjectionLengthLabel.Font = new Font("Verdana", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            StepProjectionLengthLabel.Location = new Point(6, 153);
            StepProjectionLengthLabel.Name = "StepProjectionLengthLabel";
            StepProjectionLengthLabel.Size = new Size(162, 18);
            StepProjectionLengthLabel.TabIndex = 15;
            StepProjectionLengthLabel.Text = "Глубина выступа t";
            // 
            // StepProtjectionHeightLabel
            // 
            StepProtjectionHeightLabel.AutoSize = true;
            StepProtjectionHeightLabel.Font = new Font("Verdana", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            StepProtjectionHeightLabel.Location = new Point(6, 182);
            StepProtjectionHeightLabel.Name = "StepProtjectionHeightLabel";
            StepProtjectionHeightLabel.Size = new Size(170, 18);
            StepProtjectionHeightLabel.TabIndex = 16;
            StepProtjectionHeightLabel.Text = "Толщина выступа s";
            // 
            // PlatformLengthDownLabel
            // 
            PlatformLengthDownLabel.AutoSize = true;
            PlatformLengthDownLabel.Font = new Font("Verdana", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            PlatformLengthDownLabel.Location = new Point(6, 211);
            PlatformLengthDownLabel.Name = "PlatformLengthDownLabel";
            PlatformLengthDownLabel.Size = new Size(249, 18);
            PlatformLengthDownLabel.TabIndex = 17;
            PlatformLengthDownLabel.Text = "Длина нижней платформы l1";
            // 
            // PlatformLengthUpLabel
            // 
            PlatformLengthUpLabel.AutoSize = true;
            PlatformLengthUpLabel.Font = new Font("Verdana", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            PlatformLengthUpLabel.Location = new Point(6, 240);
            PlatformLengthUpLabel.Name = "PlatformLengthUpLabel";
            PlatformLengthUpLabel.Size = new Size(255, 18);
            PlatformLengthUpLabel.TabIndex = 18;
            PlatformLengthUpLabel.Text = "Длина верхней платформы l2";
            // 
            // PlatformHeightLabel
            // 
            PlatformHeightLabel.AutoSize = true;
            PlatformHeightLabel.Font = new Font("Verdana", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            PlatformHeightLabel.Location = new Point(6, 269);
            PlatformHeightLabel.Name = "PlatformHeightLabel";
            PlatformHeightLabel.Size = new Size(165, 18);
            PlatformHeightLabel.TabIndex = 19;
            PlatformHeightLabel.Text = "Толщина марша W";
            // 
            // HeightLimitsLabel
            // 
            HeightLimitsLabel.AutoSize = true;
            HeightLimitsLabel.Font = new Font("Verdana", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            HeightLimitsLabel.Location = new Point(367, 13);
            HeightLimitsLabel.Name = "HeightLimitsLabel";
            HeightLimitsLabel.Size = new Size(125, 18);
            HeightLimitsLabel.TabIndex = 20;
            HeightLimitsLabel.Text = "500 - 8000 мм";
            // 
            // LengthLimitsLabel
            // 
            LengthLimitsLabel.AutoSize = true;
            LengthLimitsLabel.Font = new Font("Verdana", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            LengthLimitsLabel.Location = new Point(367, 42);
            LengthLimitsLabel.Name = "LengthLimitsLabel";
            LengthLimitsLabel.Size = new Size(135, 18);
            LengthLimitsLabel.TabIndex = 21;
            LengthLimitsLabel.Text = "500 - 12000 мм";
            // 
            // StepAmountLimitsLabel
            // 
            StepAmountLimitsLabel.AutoSize = true;
            StepAmountLimitsLabel.Font = new Font("Verdana", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            StepAmountLimitsLabel.Location = new Point(367, 71);
            StepAmountLimitsLabel.Name = "StepAmountLimitsLabel";
            StepAmountLimitsLabel.Size = new Size(83, 18);
            StepAmountLimitsLabel.TabIndex = 22;
            StepAmountLimitsLabel.Text = "1 - 60 шт";
            // 
            // StepHeightLimitsLabel
            // 
            StepHeightLimitsLabel.AutoSize = true;
            StepHeightLimitsLabel.Font = new Font("Verdana", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            StepHeightLimitsLabel.Location = new Point(367, 100);
            StepHeightLimitsLabel.Name = "StepHeightLimitsLabel";
            StepHeightLimitsLabel.Size = new Size(115, 18);
            StepHeightLimitsLabel.TabIndex = 23;
            StepHeightLimitsLabel.Text = "120 - 250 мм";
            // 
            // WidthLimitsLabel
            // 
            WidthLimitsLabel.AutoSize = true;
            WidthLimitsLabel.Font = new Font("Verdana", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            WidthLimitsLabel.Location = new Point(367, 129);
            WidthLimitsLabel.Name = "WidthLimitsLabel";
            WidthLimitsLabel.Size = new Size(125, 18);
            WidthLimitsLabel.TabIndex = 24;
            WidthLimitsLabel.Text = "800 - 2500 мм";
            // 
            // StepProjectionLengthLimitsLabel
            // 
            StepProjectionLengthLimitsLabel.AutoSize = true;
            StepProjectionLengthLimitsLabel.Font = new Font("Verdana", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            StepProjectionLengthLimitsLabel.Location = new Point(367, 158);
            StepProjectionLengthLimitsLabel.Name = "StepProjectionLengthLimitsLabel";
            StepProjectionLengthLimitsLabel.Size = new Size(86, 18);
            StepProjectionLengthLimitsLabel.TabIndex = 25;
            StepProjectionLengthLimitsLabel.Text = "0 - h1 / 2";
            // 
            // StepProtjectionHeightLimitsLabel
            // 
            StepProtjectionHeightLimitsLabel.AutoSize = true;
            StepProtjectionHeightLimitsLabel.Font = new Font("Verdana", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            StepProtjectionHeightLimitsLabel.Location = new Point(367, 187);
            StepProtjectionHeightLimitsLabel.Name = "StepProtjectionHeightLimitsLabel";
            StepProtjectionHeightLimitsLabel.Size = new Size(86, 18);
            StepProtjectionHeightLimitsLabel.TabIndex = 26;
            StepProtjectionHeightLimitsLabel.Text = "0 - h1 / 2";
            // 
            // PlatformLengthDownLimitsLabel
            // 
            PlatformLengthDownLimitsLabel.AutoSize = true;
            PlatformLengthDownLimitsLabel.Font = new Font("Verdana", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            PlatformLengthDownLimitsLabel.Location = new Point(367, 216);
            PlatformLengthDownLimitsLabel.Name = "PlatformLengthDownLimitsLabel";
            PlatformLengthDownLimitsLabel.Size = new Size(135, 18);
            PlatformLengthDownLimitsLabel.TabIndex = 27;
            PlatformLengthDownLimitsLabel.Text = "1000 - 5000 мм";
            // 
            // PlatformLengthUpLimitsLabel
            // 
            PlatformLengthUpLimitsLabel.AutoSize = true;
            PlatformLengthUpLimitsLabel.Font = new Font("Verdana", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            PlatformLengthUpLimitsLabel.Location = new Point(367, 245);
            PlatformLengthUpLimitsLabel.Name = "PlatformLengthUpLimitsLabel";
            PlatformLengthUpLimitsLabel.Size = new Size(135, 18);
            PlatformLengthUpLimitsLabel.TabIndex = 28;
            PlatformLengthUpLimitsLabel.Text = "1000 - 5000 мм";
            // 
            // PlatformHeightLimitsLabel
            // 
            PlatformHeightLimitsLabel.AutoSize = true;
            PlatformHeightLimitsLabel.Font = new Font("Verdana", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            PlatformHeightLimitsLabel.Location = new Point(367, 274);
            PlatformHeightLimitsLabel.Name = "PlatformHeightLimitsLabel";
            PlatformHeightLimitsLabel.Size = new Size(115, 18);
            PlatformHeightLimitsLabel.TabIndex = 29;
            PlatformHeightLimitsLabel.Text = "100 - 500 мм";
            // 
            // BuildButton
            // 
            BuildButton.Font = new Font("Segoe UI Semibold", 10.25F, FontStyle.Bold);
            BuildButton.Location = new Point(261, 297);
            BuildButton.Name = "BuildButton";
            BuildButton.Size = new Size(100, 25);
            BuildButton.TabIndex = 30;
            BuildButton.Text = "Построить";
            BuildButton.UseVisualStyleBackColor = true;
            BuildButton.Click += BuildModel;
            // 
            // ErrorPanel
            // 
            ErrorPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            ErrorPanel.Controls.Add(ErrorTextBox);
            ErrorPanel.Location = new Point(6, 325);
            ErrorPanel.Name = "ErrorPanel";
            ErrorPanel.Size = new Size(1275, 134);
            ErrorPanel.TabIndex = 32;
            ErrorPanel.Tag = "";
            // 
            // ErrorTextBox
            // 
            ErrorTextBox.BackColor = SystemColors.Window;
            ErrorTextBox.Dock = DockStyle.Fill;
            ErrorTextBox.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ErrorTextBox.Location = new Point(0, 0);
            ErrorTextBox.Multiline = true;
            ErrorTextBox.Name = "ErrorTextBox";
            ErrorTextBox.ScrollBars = ScrollBars.Vertical;
            ErrorTextBox.Size = new Size(1275, 134);
            ErrorTextBox.TabIndex = 0;
            // 
            // ErrorLabel
            // 
            ErrorLabel.AutoSize = true;
            ErrorLabel.Font = new Font("Verdana", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            ErrorLabel.Location = new Point(6, 309);
            ErrorLabel.Name = "ErrorLabel";
            ErrorLabel.Size = new Size(98, 13);
            ErrorLabel.TabIndex = 34;
            ErrorLabel.Text = "Список ошибок";
            // 
            // MainModelPictureBox
            // 
            MainModelPictureBox.Dock = DockStyle.Fill;
            MainModelPictureBox.Image = (Image)resources.GetObject("MainModelPictureBox.Image");
            MainModelPictureBox.Location = new Point(0, 0);
            MainModelPictureBox.Name = "MainModelPictureBox";
            MainModelPictureBox.Size = new Size(464, 314);
            MainModelPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            MainModelPictureBox.TabIndex = 36;
            MainModelPictureBox.TabStop = false;
            // 
            // MiniModelPictureBox
            // 
            MiniModelPictureBox.BackgroundImageLayout = ImageLayout.Stretch;
            MiniModelPictureBox.Dock = DockStyle.Fill;
            MiniModelPictureBox.Image = (Image)resources.GetObject("MiniModelPictureBox.Image");
            MiniModelPictureBox.Location = new Point(0, 0);
            MiniModelPictureBox.Name = "MiniModelPictureBox";
            MiniModelPictureBox.Size = new Size(305, 314);
            MiniModelPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            MiniModelPictureBox.TabIndex = 35;
            MiniModelPictureBox.TabStop = false;
            // 
            // PicturesSplitContainer
            // 
            PicturesSplitContainer.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            PicturesSplitContainer.Location = new Point(508, 8);
            PicturesSplitContainer.Name = "PicturesSplitContainer";
            // 
            // PicturesSplitContainer.Panel1
            // 
            PicturesSplitContainer.Panel1.Controls.Add(MiniModelPictureBox);
            // 
            // PicturesSplitContainer.Panel2
            // 
            PicturesSplitContainer.Panel2.Controls.Add(MainModelPictureBox);
            PicturesSplitContainer.Size = new Size(773, 314);
            PicturesSplitContainer.SplitterDistance = 305;
            PicturesSplitContainer.TabIndex = 37;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.Silver;
            ClientSize = new Size(1284, 461);
            Controls.Add(PicturesSplitContainer);
            Controls.Add(BuildButton);
            Controls.Add(ErrorLabel);
            Controls.Add(ErrorPanel);
            Controls.Add(PlatformHeightLimitsLabel);
            Controls.Add(PlatformLengthUpLimitsLabel);
            Controls.Add(PlatformLengthDownLimitsLabel);
            Controls.Add(StepProtjectionHeightLimitsLabel);
            Controls.Add(StepProjectionLengthLimitsLabel);
            Controls.Add(WidthLimitsLabel);
            Controls.Add(StepHeightLimitsLabel);
            Controls.Add(StepAmountLimitsLabel);
            Controls.Add(LengthLimitsLabel);
            Controls.Add(HeightLimitsLabel);
            Controls.Add(PlatformHeightLabel);
            Controls.Add(PlatformLengthUpLabel);
            Controls.Add(PlatformLengthDownLabel);
            Controls.Add(StepProtjectionHeightLabel);
            Controls.Add(StepProjectionLengthLabel);
            Controls.Add(WidthLabel);
            Controls.Add(PlatformHeightTextBox);
            Controls.Add(PlatformLengthUpTextBox);
            Controls.Add(PlatformLengthDownTextBox);
            Controls.Add(StepProtjectionHeightTextBox);
            Controls.Add(StepProjectionLengthTextBox);
            Controls.Add(WidthTextBox);
            Controls.Add(StepHeightTextBox);
            Controls.Add(StepAmountTextBox);
            Controls.Add(LengthTextBox);
            Controls.Add(HeightTextBox);
            Controls.Add(StepHeightLabel);
            Controls.Add(StepAmountLabel);
            Controls.Add(LengthLabel);
            Controls.Add(HeightLabel);
            MaximizeBox = false;
            MinimumSize = new Size(600, 450);
            Name = "MainForm";
            Text = "Лестничный марш";
            ErrorPanel.ResumeLayout(false);
            ErrorPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)MainModelPictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)MiniModelPictureBox).EndInit();
            PicturesSplitContainer.Panel1.ResumeLayout(false);
            PicturesSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PicturesSplitContainer).EndInit();
            PicturesSplitContainer.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label HeightLabel;
        private Label LengthLabel;
        private Label StepAmountLabel;
        private Label StepHeightLabel;
        private TextBox HeightTextBox;
        private TextBox LengthTextBox;
        private TextBox StepAmountTextBox;
        private TextBox StepHeightTextBox;
        private TextBox WidthTextBox;
        private TextBox StepProjectionLengthTextBox;
        private TextBox StepProtjectionHeightTextBox;
        private TextBox PlatformLengthDownTextBox;
        private TextBox PlatformLengthUpTextBox;
        private TextBox PlatformHeightTextBox;
        private Label WidthLabel;
        private Label StepProjectionLengthLabel;
        private Label StepProtjectionHeightLabel;
        private Label PlatformLengthDownLabel;
        private Label PlatformLengthUpLabel;
        private Label PlatformHeightLabel;
        private Label HeightLimitsLabel;
        private Label LengthLimitsLabel;
        private Label StepAmountLimitsLabel;
        private Label StepHeightLimitsLabel;
        private Label WidthLimitsLabel;
        private Label StepProjectionLengthLimitsLabel;
        private Label StepProtjectionHeightLimitsLabel;
        private Label PlatformLengthDownLimitsLabel;
        private Label PlatformLengthUpLimitsLabel;
        private Label PlatformHeightLimitsLabel;
        private Button BuildButton;
        private Panel ErrorPanel;
        private TextBox ErrorTextBox;
        private Label ErrorLabel;
        private PictureBox MainModelPictureBox;
        private PictureBox MiniModelPictureBox;
        private SplitContainer PicturesSplitContainer;
    }
}
