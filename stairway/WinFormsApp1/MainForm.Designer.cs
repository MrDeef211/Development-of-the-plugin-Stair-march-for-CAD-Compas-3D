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
            StepProjectionHeightTextBox = new TextBox();
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
            FloorsCountTextBox = new TextBox();
            FloorsCountLabel = new Label();
            IsMultiFlightLabel = new Label();
            IsMultiFlightСheckBox = new CheckBox();
            FloorsCountLimitLabel = new Label();
            LimitsPanel = new Panel();
            InputsPanel = new Panel();
            MiniPicturePanel = new Panel();
            MainPicturePanel = new Panel();
            BorderPanel = new Panel();
            TopPanel = new Panel();
            PicturesSplitContainer = new SplitContainer();
            ErrorPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)MainModelPictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)MiniModelPictureBox).BeginInit();
            LimitsPanel.SuspendLayout();
            InputsPanel.SuspendLayout();
            MiniPicturePanel.SuspendLayout();
            MainPicturePanel.SuspendLayout();
            TopPanel.SuspendLayout();
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
            HeightLabel.Location = new Point(3, 10);
            HeightLabel.Name = "HeightLabel";
            HeightLabel.Size = new Size(145, 18);
            HeightLabel.TabIndex = 0;
            HeightLabel.Text = "Высота марша H";
            // 
            // LengthLabel
            // 
            LengthLabel.AutoSize = true;
            LengthLabel.Font = new Font("Verdana", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            LengthLabel.Location = new Point(3, 39);
            LengthLabel.Name = "LengthLabel";
            LengthLabel.Size = new Size(148, 18);
            LengthLabel.TabIndex = 1;
            LengthLabel.Text = "Длина пролёта L";
            // 
            // StepAmountLabel
            // 
            StepAmountLabel.AutoSize = true;
            StepAmountLabel.Font = new Font("Verdana", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            StepAmountLabel.Location = new Point(3, 68);
            StepAmountLabel.Name = "StepAmountLabel";
            StepAmountLabel.Size = new Size(157, 18);
            StepAmountLabel.TabIndex = 2;
            StepAmountLabel.Text = "Число ступеней N";
            // 
            // StepHeightLabel
            // 
            StepHeightLabel.AutoSize = true;
            StepHeightLabel.Font = new Font("Verdana", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            StepHeightLabel.Location = new Point(3, 97);
            StepHeightLabel.Name = "StepHeightLabel";
            StepHeightLabel.Size = new Size(164, 18);
            StepHeightLabel.TabIndex = 3;
            StepHeightLabel.Text = "Высота ступени h1";
            // 
            // HeightTextBox
            // 
            HeightTextBox.BackColor = SystemColors.Window;
            HeightTextBox.Location = new Point(257, 10);
            HeightTextBox.Name = "HeightTextBox";
            HeightTextBox.Size = new Size(100, 23);
            HeightTextBox.TabIndex = 4;
            HeightTextBox.Text = "2";
            HeightTextBox.KeyUp += ParameterEntered;
            // 
            // LengthTextBox
            // 
            LengthTextBox.BackColor = SystemColors.Window;
            LengthTextBox.Location = new Point(257, 39);
            LengthTextBox.Name = "LengthTextBox";
            LengthTextBox.Size = new Size(100, 23);
            LengthTextBox.TabIndex = 5;
            LengthTextBox.Text = "3";
            LengthTextBox.KeyUp += ParameterEntered;
            // 
            // StepAmountTextBox
            // 
            StepAmountTextBox.BackColor = SystemColors.Window;
            StepAmountTextBox.Location = new Point(257, 68);
            StepAmountTextBox.Name = "StepAmountTextBox";
            StepAmountTextBox.Size = new Size(100, 23);
            StepAmountTextBox.TabIndex = 6;
            StepAmountTextBox.Text = "10";
            StepAmountTextBox.KeyUp += ParameterEntered;
            // 
            // StepHeightTextBox
            // 
            StepHeightTextBox.Location = new Point(257, 97);
            StepHeightTextBox.Name = "StepHeightTextBox";
            StepHeightTextBox.Size = new Size(100, 23);
            StepHeightTextBox.TabIndex = 7;
            StepHeightTextBox.Text = "200";
            StepHeightTextBox.KeyUp += ParameterEntered;
            // 
            // WidthTextBox
            // 
            WidthTextBox.BackColor = SystemColors.Window;
            WidthTextBox.Location = new Point(257, 126);
            WidthTextBox.Name = "WidthTextBox";
            WidthTextBox.Size = new Size(100, 23);
            WidthTextBox.TabIndex = 8;
            WidthTextBox.Text = "2";
            WidthTextBox.KeyUp += ParameterEntered;
            // 
            // StepProjectionLengthTextBox
            // 
            StepProjectionLengthTextBox.Location = new Point(257, 155);
            StepProjectionLengthTextBox.Name = "StepProjectionLengthTextBox";
            StepProjectionLengthTextBox.Size = new Size(100, 23);
            StepProjectionLengthTextBox.TabIndex = 9;
            StepProjectionLengthTextBox.Text = "10";
            StepProjectionLengthTextBox.KeyUp += ParameterEntered;
            // 
            // StepProjectionHeightTextBox
            // 
            StepProjectionHeightTextBox.Location = new Point(257, 184);
            StepProjectionHeightTextBox.Name = "StepProjectionHeightTextBox";
            StepProjectionHeightTextBox.Size = new Size(100, 23);
            StepProjectionHeightTextBox.TabIndex = 10;
            StepProjectionHeightTextBox.Text = "5";
            StepProjectionHeightTextBox.KeyUp += ParameterEntered;
            // 
            // PlatformLengthDownTextBox
            // 
            PlatformLengthDownTextBox.Location = new Point(257, 213);
            PlatformLengthDownTextBox.Name = "PlatformLengthDownTextBox";
            PlatformLengthDownTextBox.Size = new Size(100, 23);
            PlatformLengthDownTextBox.TabIndex = 11;
            PlatformLengthDownTextBox.Text = "2";
            PlatformLengthDownTextBox.KeyUp += ParameterEntered;
            // 
            // PlatformLengthUpTextBox
            // 
            PlatformLengthUpTextBox.Location = new Point(257, 242);
            PlatformLengthUpTextBox.Name = "PlatformLengthUpTextBox";
            PlatformLengthUpTextBox.Size = new Size(100, 23);
            PlatformLengthUpTextBox.TabIndex = 12;
            PlatformLengthUpTextBox.Text = "2";
            PlatformLengthUpTextBox.KeyUp += ParameterEntered;
            // 
            // PlatformHeightTextBox
            // 
            PlatformHeightTextBox.Location = new Point(257, 271);
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
            WidthLabel.Location = new Point(3, 126);
            WidthLabel.Name = "WidthLabel";
            WidthLabel.Size = new Size(150, 18);
            WidthLabel.TabIndex = 14;
            WidthLabel.Text = "Ширина марша B";
            // 
            // StepProjectionLengthLabel
            // 
            StepProjectionLengthLabel.AutoSize = true;
            StepProjectionLengthLabel.Font = new Font("Verdana", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            StepProjectionLengthLabel.Location = new Point(3, 155);
            StepProjectionLengthLabel.Name = "StepProjectionLengthLabel";
            StepProjectionLengthLabel.Size = new Size(162, 18);
            StepProjectionLengthLabel.TabIndex = 15;
            StepProjectionLengthLabel.Text = "Глубина выступа t";
            // 
            // StepProtjectionHeightLabel
            // 
            StepProtjectionHeightLabel.AutoSize = true;
            StepProtjectionHeightLabel.Font = new Font("Verdana", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            StepProtjectionHeightLabel.Location = new Point(3, 184);
            StepProtjectionHeightLabel.Name = "StepProtjectionHeightLabel";
            StepProtjectionHeightLabel.Size = new Size(170, 18);
            StepProtjectionHeightLabel.TabIndex = 16;
            StepProtjectionHeightLabel.Text = "Толщина выступа s";
            // 
            // PlatformLengthDownLabel
            // 
            PlatformLengthDownLabel.AutoSize = true;
            PlatformLengthDownLabel.Font = new Font("Verdana", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            PlatformLengthDownLabel.Location = new Point(3, 213);
            PlatformLengthDownLabel.Name = "PlatformLengthDownLabel";
            PlatformLengthDownLabel.Size = new Size(249, 18);
            PlatformLengthDownLabel.TabIndex = 17;
            PlatformLengthDownLabel.Text = "Длина нижней платформы l1";
            // 
            // PlatformLengthUpLabel
            // 
            PlatformLengthUpLabel.AutoSize = true;
            PlatformLengthUpLabel.Font = new Font("Verdana", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            PlatformLengthUpLabel.Location = new Point(3, 242);
            PlatformLengthUpLabel.Name = "PlatformLengthUpLabel";
            PlatformLengthUpLabel.Size = new Size(255, 18);
            PlatformLengthUpLabel.TabIndex = 18;
            PlatformLengthUpLabel.Text = "Длина верхней платформы l2";
            // 
            // PlatformHeightLabel
            // 
            PlatformHeightLabel.AutoSize = true;
            PlatformHeightLabel.Font = new Font("Verdana", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            PlatformHeightLabel.Location = new Point(3, 271);
            PlatformHeightLabel.Name = "PlatformHeightLabel";
            PlatformHeightLabel.Size = new Size(165, 18);
            PlatformHeightLabel.TabIndex = 19;
            PlatformHeightLabel.Text = "Толщина марша W";
            // 
            // HeightLimitsLabel
            // 
            HeightLimitsLabel.AutoSize = true;
            HeightLimitsLabel.Font = new Font("Verdana", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            HeightLimitsLabel.Location = new Point(0, 7);
            HeightLimitsLabel.Name = "HeightLimitsLabel";
            HeightLimitsLabel.Size = new Size(125, 18);
            HeightLimitsLabel.TabIndex = 20;
            HeightLimitsLabel.Text = "500 - 8000 мм";
            // 
            // LengthLimitsLabel
            // 
            LengthLimitsLabel.AutoSize = true;
            LengthLimitsLabel.Font = new Font("Verdana", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            LengthLimitsLabel.Location = new Point(0, 36);
            LengthLimitsLabel.Name = "LengthLimitsLabel";
            LengthLimitsLabel.Size = new Size(135, 18);
            LengthLimitsLabel.TabIndex = 21;
            LengthLimitsLabel.Text = "500 - 12000 мм";
            // 
            // StepAmountLimitsLabel
            // 
            StepAmountLimitsLabel.AutoSize = true;
            StepAmountLimitsLabel.Font = new Font("Verdana", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            StepAmountLimitsLabel.Location = new Point(0, 65);
            StepAmountLimitsLabel.Name = "StepAmountLimitsLabel";
            StepAmountLimitsLabel.Size = new Size(83, 18);
            StepAmountLimitsLabel.TabIndex = 22;
            StepAmountLimitsLabel.Text = "1 - 60 шт";
            // 
            // StepHeightLimitsLabel
            // 
            StepHeightLimitsLabel.AutoSize = true;
            StepHeightLimitsLabel.Font = new Font("Verdana", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            StepHeightLimitsLabel.Location = new Point(0, 94);
            StepHeightLimitsLabel.Name = "StepHeightLimitsLabel";
            StepHeightLimitsLabel.Size = new Size(115, 18);
            StepHeightLimitsLabel.TabIndex = 23;
            StepHeightLimitsLabel.Text = "120 - 250 мм";
            // 
            // WidthLimitsLabel
            // 
            WidthLimitsLabel.AutoSize = true;
            WidthLimitsLabel.Font = new Font("Verdana", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            WidthLimitsLabel.Location = new Point(0, 123);
            WidthLimitsLabel.Name = "WidthLimitsLabel";
            WidthLimitsLabel.Size = new Size(125, 18);
            WidthLimitsLabel.TabIndex = 24;
            WidthLimitsLabel.Text = "800 - 2500 мм";
            // 
            // StepProjectionLengthLimitsLabel
            // 
            StepProjectionLengthLimitsLabel.AutoSize = true;
            StepProjectionLengthLimitsLabel.Font = new Font("Verdana", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            StepProjectionLengthLimitsLabel.Location = new Point(0, 152);
            StepProjectionLengthLimitsLabel.Name = "StepProjectionLengthLimitsLabel";
            StepProjectionLengthLimitsLabel.Size = new Size(86, 18);
            StepProjectionLengthLimitsLabel.TabIndex = 25;
            StepProjectionLengthLimitsLabel.Text = "0 - h1 / 2";
            // 
            // StepProtjectionHeightLimitsLabel
            // 
            StepProtjectionHeightLimitsLabel.AutoSize = true;
            StepProtjectionHeightLimitsLabel.Font = new Font("Verdana", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            StepProtjectionHeightLimitsLabel.Location = new Point(0, 181);
            StepProtjectionHeightLimitsLabel.Name = "StepProtjectionHeightLimitsLabel";
            StepProtjectionHeightLimitsLabel.Size = new Size(86, 18);
            StepProtjectionHeightLimitsLabel.TabIndex = 26;
            StepProtjectionHeightLimitsLabel.Text = "0 - h1 / 2";
            // 
            // PlatformLengthDownLimitsLabel
            // 
            PlatformLengthDownLimitsLabel.AutoSize = true;
            PlatformLengthDownLimitsLabel.Font = new Font("Verdana", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            PlatformLengthDownLimitsLabel.Location = new Point(0, 210);
            PlatformLengthDownLimitsLabel.Name = "PlatformLengthDownLimitsLabel";
            PlatformLengthDownLimitsLabel.Size = new Size(135, 18);
            PlatformLengthDownLimitsLabel.TabIndex = 27;
            PlatformLengthDownLimitsLabel.Text = "1000 - 5000 мм";
            // 
            // PlatformLengthUpLimitsLabel
            // 
            PlatformLengthUpLimitsLabel.AutoSize = true;
            PlatformLengthUpLimitsLabel.Font = new Font("Verdana", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            PlatformLengthUpLimitsLabel.Location = new Point(0, 239);
            PlatformLengthUpLimitsLabel.Name = "PlatformLengthUpLimitsLabel";
            PlatformLengthUpLimitsLabel.Size = new Size(135, 18);
            PlatformLengthUpLimitsLabel.TabIndex = 28;
            PlatformLengthUpLimitsLabel.Text = "1000 - 5000 мм";
            // 
            // PlatformHeightLimitsLabel
            // 
            PlatformHeightLimitsLabel.AutoSize = true;
            PlatformHeightLimitsLabel.Font = new Font("Verdana", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            PlatformHeightLimitsLabel.Location = new Point(0, 268);
            PlatformHeightLimitsLabel.Name = "PlatformHeightLimitsLabel";
            PlatformHeightLimitsLabel.Size = new Size(115, 18);
            PlatformHeightLimitsLabel.TabIndex = 29;
            PlatformHeightLimitsLabel.Text = "100 - 500 мм";
            // 
            // BuildButton
            // 
            BuildButton.Font = new Font("Segoe UI Semibold", 10.25F, FontStyle.Bold);
            BuildButton.Location = new Point(257, 358);
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
            ErrorPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ErrorPanel.Controls.Add(ErrorTextBox);
            ErrorPanel.Location = new Point(0, 389);
            ErrorPanel.Name = "ErrorPanel";
            ErrorPanel.Size = new Size(1284, 373);
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
            ErrorTextBox.Size = new Size(1284, 373);
            ErrorTextBox.TabIndex = 0;
            // 
            // ErrorLabel
            // 
            ErrorLabel.AutoSize = true;
            ErrorLabel.Font = new Font("Verdana", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            ErrorLabel.Location = new Point(3, 373);
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
            MainModelPictureBox.Size = new Size(471, 383);
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
            MiniModelPictureBox.Size = new Size(302, 383);
            MiniModelPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            MiniModelPictureBox.TabIndex = 35;
            MiniModelPictureBox.TabStop = false;
            // 
            // FloorsCountTextBox
            // 
            FloorsCountTextBox.Location = new Point(257, 329);
            FloorsCountTextBox.Name = "FloorsCountTextBox";
            FloorsCountTextBox.Size = new Size(100, 23);
            FloorsCountTextBox.TabIndex = 40;
            FloorsCountTextBox.Text = "200";
            FloorsCountTextBox.KeyUp += ParameterEntered;
            // 
            // FloorsCountLabel
            // 
            FloorsCountLabel.AutoSize = true;
            FloorsCountLabel.Font = new Font("Verdana", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            FloorsCountLabel.Location = new Point(3, 329);
            FloorsCountLabel.Name = "FloorsCountLabel";
            FloorsCountLabel.Size = new Size(170, 18);
            FloorsCountLabel.TabIndex = 42;
            FloorsCountLabel.Text = "Количество этажей";
            // 
            // IsMultiFlightLabel
            // 
            IsMultiFlightLabel.AutoSize = true;
            IsMultiFlightLabel.Font = new Font("Verdana", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            IsMultiFlightLabel.Location = new Point(3, 300);
            IsMultiFlightLabel.Name = "IsMultiFlightLabel";
            IsMultiFlightLabel.Size = new Size(135, 18);
            IsMultiFlightLabel.TabIndex = 43;
            IsMultiFlightLabel.Text = "Строить пролёт";
            // 
            // IsMultiFlightСheckBox
            // 
            IsMultiFlightСheckBox.AutoSize = true;
            IsMultiFlightСheckBox.Location = new Point(260, 304);
            IsMultiFlightСheckBox.Name = "IsMultiFlightСheckBox";
            IsMultiFlightСheckBox.Size = new Size(15, 14);
            IsMultiFlightСheckBox.TabIndex = 44;
            IsMultiFlightСheckBox.UseVisualStyleBackColor = true;
            IsMultiFlightСheckBox.MouseClick += IsMultiFlightСheckBox_MouseClick;
            // 
            // FloorsCountLimitLabel
            // 
            FloorsCountLimitLabel.AutoSize = true;
            FloorsCountLimitLabel.Font = new Font("Verdana", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            FloorsCountLimitLabel.Location = new Point(0, 326);
            FloorsCountLimitLabel.Name = "FloorsCountLimitLabel";
            FloorsCountLimitLabel.Size = new Size(83, 18);
            FloorsCountLimitLabel.TabIndex = 45;
            FloorsCountLimitLabel.Text = "0 - 25 шт";
            // 
            // LimitsPanel
            // 
            LimitsPanel.Controls.Add(FloorsCountLimitLabel);
            LimitsPanel.Controls.Add(HeightLimitsLabel);
            LimitsPanel.Controls.Add(LengthLimitsLabel);
            LimitsPanel.Controls.Add(StepAmountLimitsLabel);
            LimitsPanel.Controls.Add(StepHeightLimitsLabel);
            LimitsPanel.Controls.Add(WidthLimitsLabel);
            LimitsPanel.Controls.Add(StepProjectionLengthLimitsLabel);
            LimitsPanel.Controls.Add(StepProtjectionHeightLimitsLabel);
            LimitsPanel.Controls.Add(PlatformLengthDownLimitsLabel);
            LimitsPanel.Controls.Add(PlatformLengthUpLimitsLabel);
            LimitsPanel.Controls.Add(PlatformHeightLimitsLabel);
            LimitsPanel.Location = new Point(363, 3);
            LimitsPanel.Name = "LimitsPanel";
            LimitsPanel.Size = new Size(138, 349);
            LimitsPanel.TabIndex = 46;
            // 
            // InputsPanel
            // 
            InputsPanel.AutoSize = true;
            InputsPanel.Controls.Add(IsMultiFlightСheckBox);
            InputsPanel.Controls.Add(FloorsCountLabel);
            InputsPanel.Controls.Add(HeightTextBox);
            InputsPanel.Controls.Add(LengthTextBox);
            InputsPanel.Controls.Add(StepAmountTextBox);
            InputsPanel.Controls.Add(StepHeightTextBox);
            InputsPanel.Controls.Add(WidthTextBox);
            InputsPanel.Controls.Add(StepProjectionLengthTextBox);
            InputsPanel.Controls.Add(StepProjectionHeightTextBox);
            InputsPanel.Controls.Add(PlatformLengthDownTextBox);
            InputsPanel.Controls.Add(PlatformLengthUpTextBox);
            InputsPanel.Controls.Add(PlatformHeightTextBox);
            InputsPanel.Controls.Add(FloorsCountTextBox);
            InputsPanel.Controls.Add(IsMultiFlightLabel);
            InputsPanel.Controls.Add(PlatformHeightLabel);
            InputsPanel.Controls.Add(PlatformLengthUpLabel);
            InputsPanel.Controls.Add(BuildButton);
            InputsPanel.Controls.Add(PlatformLengthDownLabel);
            InputsPanel.Controls.Add(StepProtjectionHeightLabel);
            InputsPanel.Controls.Add(StepProjectionLengthLabel);
            InputsPanel.Controls.Add(WidthLabel);
            InputsPanel.Controls.Add(StepHeightLabel);
            InputsPanel.Controls.Add(StepAmountLabel);
            InputsPanel.Controls.Add(LengthLabel);
            InputsPanel.Controls.Add(HeightLabel);
            InputsPanel.Dock = DockStyle.Left;
            InputsPanel.Location = new Point(0, 0);
            InputsPanel.Name = "InputsPanel";
            InputsPanel.Size = new Size(360, 386);
            InputsPanel.TabIndex = 48;
            // 
            // MiniPicturePanel
            // 
            MiniPicturePanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            MiniPicturePanel.Controls.Add(MiniModelPictureBox);
            MiniPicturePanel.Dock = DockStyle.Fill;
            MiniPicturePanel.Location = new Point(0, 0);
            MiniPicturePanel.Name = "MiniPicturePanel";
            MiniPicturePanel.Size = new Size(302, 383);
            MiniPicturePanel.TabIndex = 49;
            // 
            // MainPicturePanel
            // 
            MainPicturePanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            MainPicturePanel.Controls.Add(MainModelPictureBox);
            MainPicturePanel.Dock = DockStyle.Fill;
            MainPicturePanel.Location = new Point(0, 0);
            MainPicturePanel.Name = "MainPicturePanel";
            MainPicturePanel.Size = new Size(471, 383);
            MainPicturePanel.TabIndex = 50;
            // 
            // BorderPanel
            // 
            BorderPanel.BackgroundImage = (Image)resources.GetObject("BorderPanel.BackgroundImage");
            BorderPanel.Dock = DockStyle.Right;
            BorderPanel.Location = new Point(1260, 0);
            BorderPanel.Name = "BorderPanel";
            BorderPanel.Size = new Size(24, 386);
            BorderPanel.TabIndex = 1;
            // 
            // TopPanel
            // 
            TopPanel.Controls.Add(PicturesSplitContainer);
            TopPanel.Controls.Add(InputsPanel);
            TopPanel.Controls.Add(BorderPanel);
            TopPanel.Controls.Add(LimitsPanel);
            TopPanel.Dock = DockStyle.Top;
            TopPanel.Location = new Point(0, 0);
            TopPanel.Name = "TopPanel";
            TopPanel.Size = new Size(1284, 386);
            TopPanel.TabIndex = 51;
            // 
            // PicturesSplitContainer
            // 
            PicturesSplitContainer.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            PicturesSplitContainer.Location = new Point(504, 0);
            PicturesSplitContainer.Name = "PicturesSplitContainer";
            // 
            // PicturesSplitContainer.Panel1
            // 
            PicturesSplitContainer.Panel1.Controls.Add(MiniPicturePanel);
            // 
            // PicturesSplitContainer.Panel2
            // 
            PicturesSplitContainer.Panel2.Controls.Add(MainPicturePanel);
            PicturesSplitContainer.Size = new Size(777, 383);
            PicturesSplitContainer.SplitterDistance = 302;
            PicturesSplitContainer.TabIndex = 49;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.Silver;
            ClientSize = new Size(1284, 762);
            Controls.Add(ErrorLabel);
            Controls.Add(ErrorPanel);
            Controls.Add(TopPanel);
            MaximizeBox = false;
            MinimumSize = new Size(400, 450);
            Name = "MainForm";
            Text = "Лестничный марш";
            FormClosing += MainForm_FormClosing;
            ResizeEnd += MainForm_ResizeEnd;
            Resize += MainForm_Resize;
            ErrorPanel.ResumeLayout(false);
            ErrorPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)MainModelPictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)MiniModelPictureBox).EndInit();
            LimitsPanel.ResumeLayout(false);
            LimitsPanel.PerformLayout();
            InputsPanel.ResumeLayout(false);
            InputsPanel.PerformLayout();
            MiniPicturePanel.ResumeLayout(false);
            MainPicturePanel.ResumeLayout(false);
            TopPanel.ResumeLayout(false);
            TopPanel.PerformLayout();
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
        private TextBox StepProjectionHeightTextBox;
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
        private Label label1;
        private TextBox FloorsCountTextBox;
        private Label FloorsCountLabel;
        private TextBox textBox1;
        private Label IsMultiFlightLabel;
        private CheckBox IsMultiFlightСheckBox;
        private Label FloorsCountLimitLabel;
        private Panel LimitsPanel;
        private Panel TopPanel;
        private Panel InputsPanel;
        private Panel MainPicturePanel;
        private Panel MainPicturePanel2;
        private Panel MiniPicturePanel;
        private SplitContainer PicturesSplitContainer;
        private Panel BorderPanel;
    }
}
