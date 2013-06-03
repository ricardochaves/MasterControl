namespace RBCAH
{
    partial class SalesmanForm
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
            this.waitCycleBox = new System.Windows.Forms.GroupBox();
            this.maxWaitTimeLabel = new System.Windows.Forms.Label();
            this.maxWaitTimeValue = new FlatNumericUpDown();
            this.minWaitTimeLabel = new System.Windows.Forms.Label();
            this.minWaitTimeValue = new FlatNumericUpDown();
            this.doneActionValue = new System.Windows.Forms.ComboBox();
            this.doneActionLabel = new System.Windows.Forms.Label();
            this.loadProfileBox = new System.Windows.Forms.GroupBox();
            this.profilePathValue = new System.Windows.Forms.TextBox();
            this.botbaseValue = new System.Windows.Forms.TextBox();
            this.profilePathButton = new System.Windows.Forms.Button();
            this.botbaseLabel = new System.Windows.Forms.Label();
            this.profilePathLabel = new System.Windows.Forms.Label();
            this.ahItemsButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.undercutSettingsBox = new System.Windows.Forms.GroupBox();
            this.stacksizesValue = new System.Windows.Forms.ComboBox();
            this.stacksizesLabel = new System.Windows.Forms.Label();
            this.minPriceBehaviorValue = new System.Windows.Forms.ComboBox();
            this.minPriceBehaviorLabel = new System.Windows.Forms.Label();
            this.undercutAmountFixedValue = new System.Windows.Forms.TextBox();
            this.undercutAmountPercentageSymbolLabel = new System.Windows.Forms.Label();
            this.undercutAmountPercentageValue = new FlatNumericUpDown();
            this.undercutAmountLabel = new System.Windows.Forms.Label();
            this.undercutTypeValue = new System.Windows.Forms.ComboBox();
            this.undercutTypeLabel = new System.Windows.Forms.Label();
            this.botHavenLogo = new System.Windows.Forms.PictureBox();
            this.waitCycleBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.maxWaitTimeValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minWaitTimeValue)).BeginInit();
            this.loadProfileBox.SuspendLayout();
            this.undercutSettingsBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.undercutAmountPercentageValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.botHavenLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // waitCycleBox
            // 
            this.waitCycleBox.AutoSize = true;
            this.waitCycleBox.BackColor = System.Drawing.Color.Transparent;
            this.waitCycleBox.Controls.Add(this.maxWaitTimeLabel);
            this.waitCycleBox.Controls.Add(this.maxWaitTimeValue);
            this.waitCycleBox.Controls.Add(this.minWaitTimeLabel);
            this.waitCycleBox.Controls.Add(this.minWaitTimeValue);
            this.waitCycleBox.Location = new System.Drawing.Point(12, 110);
            this.waitCycleBox.Name = "waitCycleBox";
            this.waitCycleBox.Size = new System.Drawing.Size(125, 110);
            this.waitCycleBox.TabIndex = 2;
            this.waitCycleBox.TabStop = false;
            this.waitCycleBox.Visible = false;
            // 
            // maxWaitTimeLabel
            // 
            this.maxWaitTimeLabel.AutoSize = true;
            this.maxWaitTimeLabel.Location = new System.Drawing.Point(6, 47);
            this.maxWaitTimeLabel.Name = "maxWaitTimeLabel";
            this.maxWaitTimeLabel.Size = new System.Drawing.Size(67, 13);
            this.maxWaitTimeLabel.TabIndex = 5;
            this.maxWaitTimeLabel.Text = "Max Minutes";
            // 
            // maxWaitTimeValue
            // 
            this.maxWaitTimeValue.Location = new System.Drawing.Point(76, 45);
            this.maxWaitTimeValue.Name = "maxWaitTimeValue";
            this.maxWaitTimeValue.Size = new System.Drawing.Size(43, 20);
            this.maxWaitTimeValue.TabIndex = 4;
            // 
            // minWaitTimeLabel
            // 
            this.minWaitTimeLabel.AutoSize = true;
            this.minWaitTimeLabel.Location = new System.Drawing.Point(6, 21);
            this.minWaitTimeLabel.Name = "minWaitTimeLabel";
            this.minWaitTimeLabel.Size = new System.Drawing.Size(64, 13);
            this.minWaitTimeLabel.TabIndex = 3;
            this.minWaitTimeLabel.Text = "Min Minutes";
            // 
            // minWaitTimeValue
            // 
            this.minWaitTimeValue.Location = new System.Drawing.Point(76, 19);
            this.minWaitTimeValue.Name = "minWaitTimeValue";
            this.minWaitTimeValue.Size = new System.Drawing.Size(43, 20);
            this.minWaitTimeValue.TabIndex = 0;
            // 
            // doneActionValue
            // 
            this.doneActionValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.doneActionValue.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.doneActionValue.FormattingEnabled = true;
            this.doneActionValue.Items.AddRange(new object[] {
            "Wait and repeat",
            "Load a profile",
            "Shutdown WoW+HB"});
            this.doneActionValue.Location = new System.Drawing.Point(19, 83);
            this.doneActionValue.Name = "doneActionValue";
            this.doneActionValue.Size = new System.Drawing.Size(107, 21);
            this.doneActionValue.TabIndex = 3;
            this.doneActionValue.SelectedIndexChanged += new System.EventHandler(this.doneActionValue_SelectedIndexChanged);
            // 
            // doneActionLabel
            // 
            this.doneActionLabel.AutoSize = true;
            this.doneActionLabel.BackColor = System.Drawing.Color.Transparent;
            this.doneActionLabel.Location = new System.Drawing.Point(12, 64);
            this.doneActionLabel.Name = "doneActionLabel";
            this.doneActionLabel.Size = new System.Drawing.Size(93, 13);
            this.doneActionLabel.TabIndex = 4;
            this.doneActionLabel.Text = "Action when done";
            // 
            // loadProfileBox
            // 
            this.loadProfileBox.AutoSize = true;
            this.loadProfileBox.BackColor = System.Drawing.Color.Transparent;
            this.loadProfileBox.Controls.Add(this.profilePathValue);
            this.loadProfileBox.Controls.Add(this.botbaseValue);
            this.loadProfileBox.Controls.Add(this.profilePathButton);
            this.loadProfileBox.Controls.Add(this.botbaseLabel);
            this.loadProfileBox.Controls.Add(this.profilePathLabel);
            this.loadProfileBox.Location = new System.Drawing.Point(12, 110);
            this.loadProfileBox.Name = "loadProfileBox";
            this.loadProfileBox.Size = new System.Drawing.Size(125, 110);
            this.loadProfileBox.TabIndex = 6;
            this.loadProfileBox.TabStop = false;
            this.loadProfileBox.Visible = false;
            // 
            // profilePathValue
            // 
            this.profilePathValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.profilePathValue.Location = new System.Drawing.Point(10, 32);
            this.profilePathValue.Name = "profilePathValue";
            this.profilePathValue.Size = new System.Drawing.Size(104, 20);
            this.profilePathValue.TabIndex = 8;
            // 
            // botbaseValue
            // 
            this.botbaseValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.botbaseValue.Location = new System.Drawing.Point(10, 71);
            this.botbaseValue.Name = "botbaseValue";
            this.botbaseValue.Size = new System.Drawing.Size(104, 20);
            this.botbaseValue.TabIndex = 7;
            // 
            // profilePathButton
            // 
            this.profilePathButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.profilePathButton.Location = new System.Drawing.Point(74, 10);
            this.profilePathButton.Margin = new System.Windows.Forms.Padding(0);
            this.profilePathButton.Name = "profilePathButton";
            this.profilePathButton.Size = new System.Drawing.Size(40, 19);
            this.profilePathButton.TabIndex = 6;
            this.profilePathButton.Text = "Select";
            this.profilePathButton.UseVisualStyleBackColor = true;
            this.profilePathButton.Click += new System.EventHandler(this.profilePathButton_Click);
            // 
            // botbaseLabel
            // 
            this.botbaseLabel.AutoSize = true;
            this.botbaseLabel.Location = new System.Drawing.Point(7, 55);
            this.botbaseLabel.Name = "botbaseLabel";
            this.botbaseLabel.Size = new System.Drawing.Size(49, 13);
            this.botbaseLabel.TabIndex = 5;
            this.botbaseLabel.Text = "Bot base";
            // 
            // profilePathLabel
            // 
            this.profilePathLabel.AutoSize = true;
            this.profilePathLabel.Location = new System.Drawing.Point(7, 12);
            this.profilePathLabel.Name = "profilePathLabel";
            this.profilePathLabel.Size = new System.Drawing.Size(61, 13);
            this.profilePathLabel.TabIndex = 3;
            this.profilePathLabel.Text = "Profile Path";
            // 
            // ahItemsButton
            // 
            this.ahItemsButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ahItemsButton.FlatAppearance.BorderSize = 0;
            this.ahItemsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ahItemsButton.Location = new System.Drawing.Point(130, 238);
            this.ahItemsButton.Name = "ahItemsButton";
            this.ahItemsButton.Size = new System.Drawing.Size(121, 37);
            this.ahItemsButton.TabIndex = 8;
            this.ahItemsButton.Text = "Manage Auction Items";
            this.ahItemsButton.UseVisualStyleBackColor = true;
            this.ahItemsButton.Click += new System.EventHandler(this.ahItemsButton_Click);
            this.ahItemsButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.saveButton_MouseDown);
            this.ahItemsButton.MouseEnter += new System.EventHandler(this.saveButton_MouseEnter);
            this.ahItemsButton.MouseLeave += new System.EventHandler(this.saveButton_MouseLeave);
            this.ahItemsButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.saveButton_MouseUp);
            // 
            // saveButton
            // 
            this.saveButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.saveButton.FlatAppearance.BorderSize = 0;
            this.saveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.saveButton.Location = new System.Drawing.Point(19, 238);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(105, 23);
            this.saveButton.TabIndex = 9;
            this.saveButton.Text = "Save && Close";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            this.saveButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.saveButton_MouseDown);
            this.saveButton.MouseEnter += new System.EventHandler(this.saveButton_MouseEnter);
            this.saveButton.MouseLeave += new System.EventHandler(this.saveButton_MouseLeave);
            this.saveButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.saveButton_MouseUp);
            // 
            // undercutSettingsBox
            // 
            this.undercutSettingsBox.BackColor = System.Drawing.Color.Transparent;
            this.undercutSettingsBox.Controls.Add(this.stacksizesValue);
            this.undercutSettingsBox.Controls.Add(this.stacksizesLabel);
            this.undercutSettingsBox.Controls.Add(this.minPriceBehaviorValue);
            this.undercutSettingsBox.Controls.Add(this.minPriceBehaviorLabel);
            this.undercutSettingsBox.Controls.Add(this.undercutAmountFixedValue);
            this.undercutSettingsBox.Controls.Add(this.undercutAmountPercentageSymbolLabel);
            this.undercutSettingsBox.Controls.Add(this.undercutAmountPercentageValue);
            this.undercutSettingsBox.Controls.Add(this.undercutAmountLabel);
            this.undercutSettingsBox.Controls.Add(this.undercutTypeValue);
            this.undercutSettingsBox.Controls.Add(this.undercutTypeLabel);
            this.undercutSettingsBox.Location = new System.Drawing.Point(143, 12);
            this.undercutSettingsBox.Name = "undercutSettingsBox";
            this.undercutSettingsBox.Size = new System.Drawing.Size(151, 193);
            this.undercutSettingsBox.TabIndex = 10;
            this.undercutSettingsBox.TabStop = false;
            this.undercutSettingsBox.Text = "Undercut Settings";
            // 
            // stacksizesValue
            // 
            this.stacksizesValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.stacksizesValue.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.stacksizesValue.FormattingEnabled = true;
            this.stacksizesValue.Items.AddRange(new object[] {
            "Same",
            "Same or bigger",
            "Same or smaller",
            "Any"});
            this.stacksizesValue.Location = new System.Drawing.Point(9, 118);
            this.stacksizesValue.Name = "stacksizesValue";
            this.stacksizesValue.Size = new System.Drawing.Size(98, 21);
            this.stacksizesValue.TabIndex = 28;
            // 
            // stacksizesLabel
            // 
            this.stacksizesLabel.AutoSize = true;
            this.stacksizesLabel.Location = new System.Drawing.Point(6, 101);
            this.stacksizesLabel.Name = "stacksizesLabel";
            this.stacksizesLabel.Size = new System.Drawing.Size(61, 13);
            this.stacksizesLabel.TabIndex = 26;
            this.stacksizesLabel.Text = "Stacksizes:";
            // 
            // minPriceBehaviorValue
            // 
            this.minPriceBehaviorValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.minPriceBehaviorValue.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.minPriceBehaviorValue.FormattingEnabled = true;
            this.minPriceBehaviorValue.Items.AddRange(new object[] {
            "Skip posting",
            "Ignore cheaper",
            "Use fallback",
            "Undercut",
            "Use minimum"});
            this.minPriceBehaviorValue.Location = new System.Drawing.Point(9, 159);
            this.minPriceBehaviorValue.Name = "minPriceBehaviorValue";
            this.minPriceBehaviorValue.Size = new System.Drawing.Size(94, 21);
            this.minPriceBehaviorValue.TabIndex = 24;
            // 
            // minPriceBehaviorLabel
            // 
            this.minPriceBehaviorLabel.AutoSize = true;
            this.minPriceBehaviorLabel.Location = new System.Drawing.Point(6, 142);
            this.minPriceBehaviorLabel.Name = "minPriceBehaviorLabel";
            this.minPriceBehaviorLabel.Size = new System.Drawing.Size(120, 13);
            this.minPriceBehaviorLabel.TabIndex = 23;
            this.minPriceBehaviorLabel.Text = "Minimum Price Behavior";
            // 
            // undercutAmountFixedValue
            // 
            this.undercutAmountFixedValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.undercutAmountFixedValue.Location = new System.Drawing.Point(10, 78);
            this.undercutAmountFixedValue.Name = "undercutAmountFixedValue";
            this.undercutAmountFixedValue.Size = new System.Drawing.Size(66, 20);
            this.undercutAmountFixedValue.TabIndex = 22;
            this.undercutAmountFixedValue.Visible = false;
            // 
            // undercutAmountPercentageSymbolLabel
            // 
            this.undercutAmountPercentageSymbolLabel.AutoSize = true;
            this.undercutAmountPercentageSymbolLabel.Location = new System.Drawing.Point(60, 80);
            this.undercutAmountPercentageSymbolLabel.Name = "undercutAmountPercentageSymbolLabel";
            this.undercutAmountPercentageSymbolLabel.Size = new System.Drawing.Size(15, 13);
            this.undercutAmountPercentageSymbolLabel.TabIndex = 21;
            this.undercutAmountPercentageSymbolLabel.Text = "%";
            this.undercutAmountPercentageSymbolLabel.Visible = false;
            // 
            // undercutAmountPercentageValue
            // 
            this.undercutAmountPercentageValue.Location = new System.Drawing.Point(10, 78);
            this.undercutAmountPercentageValue.Name = "undercutAmountPercentageValue";
            this.undercutAmountPercentageValue.Size = new System.Drawing.Size(50, 20);
            this.undercutAmountPercentageValue.TabIndex = 14;
            this.undercutAmountPercentageValue.Visible = false;
            // 
            // undercutAmountLabel
            // 
            this.undercutAmountLabel.AutoSize = true;
            this.undercutAmountLabel.Location = new System.Drawing.Point(7, 61);
            this.undercutAmountLabel.Name = "undercutAmountLabel";
            this.undercutAmountLabel.Size = new System.Drawing.Size(46, 13);
            this.undercutAmountLabel.TabIndex = 13;
            this.undercutAmountLabel.Text = "Amount:";
            // 
            // undercutTypeValue
            // 
            this.undercutTypeValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.undercutTypeValue.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.undercutTypeValue.FormattingEnabled = true;
            this.undercutTypeValue.Items.AddRange(new object[] {
            "Percentage",
            "Fixed Amount"});
            this.undercutTypeValue.Location = new System.Drawing.Point(9, 33);
            this.undercutTypeValue.Name = "undercutTypeValue";
            this.undercutTypeValue.Size = new System.Drawing.Size(98, 21);
            this.undercutTypeValue.TabIndex = 12;
            this.undercutTypeValue.SelectedIndexChanged += new System.EventHandler(this.undercutTypeValue_SelectedIndexChanged);
            // 
            // undercutTypeLabel
            // 
            this.undercutTypeLabel.AutoSize = true;
            this.undercutTypeLabel.Location = new System.Drawing.Point(6, 16);
            this.undercutTypeLabel.Name = "undercutTypeLabel";
            this.undercutTypeLabel.Size = new System.Drawing.Size(34, 13);
            this.undercutTypeLabel.TabIndex = 11;
            this.undercutTypeLabel.Text = "Type:";
            // 
            // botHavenLogo
            // 
            this.botHavenLogo.BackColor = System.Drawing.Color.Transparent;
            this.botHavenLogo.Location = new System.Drawing.Point(12, 12);
            this.botHavenLogo.Name = "botHavenLogo";
            this.botHavenLogo.Size = new System.Drawing.Size(125, 40);
            this.botHavenLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.botHavenLogo.TabIndex = 13;
            this.botHavenLogo.TabStop = false;
            this.botHavenLogo.Click += new System.EventHandler(this.botHavenLogo_Click);
            // 
            // SalesmanForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(309, 296);
            this.Controls.Add(this.botHavenLogo);
            this.Controls.Add(this.undercutSettingsBox);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.ahItemsButton);
            this.Controls.Add(this.loadProfileBox);
            this.Controls.Add(this.doneActionLabel);
            this.Controls.Add(this.doneActionValue);
            this.Controls.Add(this.waitCycleBox);
            this.Name = "SalesmanForm";
            this.Text = "Salesman Configuration";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SalesmanForm_FormClosing);
            this.waitCycleBox.ResumeLayout(false);
            this.waitCycleBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.maxWaitTimeValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minWaitTimeValue)).EndInit();
            this.loadProfileBox.ResumeLayout(false);
            this.loadProfileBox.PerformLayout();
            this.undercutSettingsBox.ResumeLayout(false);
            this.undercutSettingsBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.undercutAmountPercentageValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.botHavenLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox waitCycleBox;
        private System.Windows.Forms.Label maxWaitTimeLabel;
        private FlatNumericUpDown maxWaitTimeValue;
        private System.Windows.Forms.Label minWaitTimeLabel;
        private FlatNumericUpDown minWaitTimeValue;
        private System.Windows.Forms.ComboBox doneActionValue;
        private System.Windows.Forms.Label doneActionLabel;
        private System.Windows.Forms.GroupBox loadProfileBox;
        private System.Windows.Forms.TextBox profilePathValue;
        private System.Windows.Forms.TextBox botbaseValue;
        private System.Windows.Forms.Button profilePathButton;
        private System.Windows.Forms.Label botbaseLabel;
        private System.Windows.Forms.Label profilePathLabel;
        private System.Windows.Forms.Button ahItemsButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.GroupBox undercutSettingsBox;
        private System.Windows.Forms.ComboBox undercutTypeValue;
        private System.Windows.Forms.Label undercutTypeLabel;
        private System.Windows.Forms.Label undercutAmountLabel;
        private System.Windows.Forms.TextBox undercutAmountFixedValue;
        private System.Windows.Forms.Label undercutAmountPercentageSymbolLabel;
        private FlatNumericUpDown undercutAmountPercentageValue;
        private System.Windows.Forms.ComboBox minPriceBehaviorValue;
        private System.Windows.Forms.Label minPriceBehaviorLabel;
        private System.Windows.Forms.ComboBox stacksizesValue;
        private System.Windows.Forms.Label stacksizesLabel;
        private System.Windows.Forms.PictureBox botHavenLogo;
    }
}