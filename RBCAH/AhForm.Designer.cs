namespace RBCAH
{
    partial class AhForm
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
            this.clearButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.importExportLabel = new System.Windows.Forms.Label();
            this.exportButton = new System.Windows.Forms.Button();
            this.importButton = new System.Windows.Forms.Button();
            this.ahItemsLabel = new System.Windows.Forms.Label();
            this.ahItemsList = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.ahItemsList)).BeginInit();
            this.SuspendLayout();
            // 
            // clearButton
            // 
            this.clearButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.clearButton.FlatAppearance.BorderSize = 0;
            this.clearButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.clearButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.clearButton.Location = new System.Drawing.Point(655, 647);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(78, 23);
            this.clearButton.TabIndex = 40;
            this.clearButton.Text = "Clear List";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            this.clearButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.presetsGemsButton_MouseDown);
            this.clearButton.MouseEnter += new System.EventHandler(this.presetsGemsButton_MouseEnter);
            this.clearButton.MouseLeave += new System.EventHandler(this.presetsGemsButton_MouseLeave);
            this.clearButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.presetsGemsButton_MouseUp);
            // 
            // saveButton
            // 
            this.saveButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.saveButton.FlatAppearance.BorderSize = 0;
            this.saveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.saveButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.saveButton.Location = new System.Drawing.Point(773, 647);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(96, 23);
            this.saveButton.TabIndex = 39;
            this.saveButton.Text = "Save && Close";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            this.saveButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.presetsGemsButton_MouseDown);
            this.saveButton.MouseEnter += new System.EventHandler(this.presetsGemsButton_MouseEnter);
            this.saveButton.MouseLeave += new System.EventHandler(this.presetsGemsButton_MouseLeave);
            this.saveButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.presetsGemsButton_MouseUp);
            // 
            // importExportLabel
            // 
            this.importExportLabel.AutoSize = true;
            this.importExportLabel.BackColor = System.Drawing.Color.Transparent;
            this.importExportLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F);
            this.importExportLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.importExportLabel.Location = new System.Drawing.Point(694, 378);
            this.importExportLabel.Name = "importExportLabel";
            this.importExportLabel.Size = new System.Drawing.Size(206, 33);
            this.importExportLabel.TabIndex = 38;
            this.importExportLabel.Text = "Import / Export";
            // 
            // exportButton
            // 
            this.exportButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.exportButton.FlatAppearance.BorderSize = 0;
            this.exportButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.exportButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.exportButton.Location = new System.Drawing.Point(787, 427);
            this.exportButton.Name = "exportButton";
            this.exportButton.Size = new System.Drawing.Size(75, 23);
            this.exportButton.TabIndex = 37;
            this.exportButton.Text = "Export";
            this.exportButton.UseVisualStyleBackColor = true;
            this.exportButton.Click += new System.EventHandler(this.exportButton_Click);
            this.exportButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.presetsGemsButton_MouseDown);
            this.exportButton.MouseEnter += new System.EventHandler(this.presetsGemsButton_MouseEnter);
            this.exportButton.MouseLeave += new System.EventHandler(this.presetsGemsButton_MouseLeave);
            this.exportButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.presetsGemsButton_MouseUp);
            // 
            // importButton
            // 
            this.importButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.importButton.FlatAppearance.BorderSize = 0;
            this.importButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.importButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.importButton.Location = new System.Drawing.Point(658, 427);
            this.importButton.Name = "importButton";
            this.importButton.Size = new System.Drawing.Size(75, 23);
            this.importButton.TabIndex = 36;
            this.importButton.Text = "Import";
            this.importButton.UseVisualStyleBackColor = true;
            this.importButton.Click += new System.EventHandler(this.importButton_Click);
            this.importButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.presetsGemsButton_MouseDown);
            this.importButton.MouseEnter += new System.EventHandler(this.presetsGemsButton_MouseEnter);
            this.importButton.MouseLeave += new System.EventHandler(this.presetsGemsButton_MouseLeave);
            this.importButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.presetsGemsButton_MouseUp);
            // 
            // ahItemsLabel
            // 
            this.ahItemsLabel.AutoSize = true;
            this.ahItemsLabel.BackColor = System.Drawing.Color.Transparent;
            this.ahItemsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.ahItemsLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ahItemsLabel.Location = new System.Drawing.Point(34, 21);
            this.ahItemsLabel.Name = "ahItemsLabel";
            this.ahItemsLabel.Size = new System.Drawing.Size(404, 29);
            this.ahItemsLabel.TabIndex = 35;
            this.ahItemsLabel.Text = "List of items to sell on Auction House";
            // 
            // ahItemsList
            // 
            this.ahItemsList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ahItemsList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.ahItemsList.Location = new System.Drawing.Point(39, 53);
            this.ahItemsList.Name = "ahItemsList";
            this.ahItemsList.RowHeadersVisible = false;
            this.ahItemsList.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ahItemsList.ShowEditingIcon = false;
            this.ahItemsList.Size = new System.Drawing.Size(610, 618);
            this.ahItemsList.TabIndex = 34;
            this.ahItemsList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ahItemsList_CellClick);
            this.ahItemsList.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.ahItemsList_CellFormatting);
            this.ahItemsList.CellParsing += new System.Windows.Forms.DataGridViewCellParsingEventHandler(this.ahItemsList_CellParsing);
            this.ahItemsList.CurrentCellDirtyStateChanged += new System.EventHandler(this.ahItemsList_CurrentCellDirtyStateChanged);
            this.ahItemsList.DefaultValuesNeeded += new System.Windows.Forms.DataGridViewRowEventHandler(this.ahItemsList_DefaultValuesNeeded);
            this.ahItemsList.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.ahItemsList_EditingControlShowing);
            // 
            // AhForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(926, 693);
            this.Controls.Add(this.clearButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.importExportLabel);
            this.Controls.Add(this.exportButton);
            this.Controls.Add(this.importButton);
            this.Controls.Add(this.ahItemsLabel);
            this.Controls.Add(this.ahItemsList);
            this.Name = "AhForm";
            this.Text = "AhForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AhItemsForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.ahItemsList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Label importExportLabel;
        private System.Windows.Forms.Button exportButton;
        private System.Windows.Forms.Button importButton;
        private System.Windows.Forms.Label ahItemsLabel;
        private System.Windows.Forms.DataGridView ahItemsList;
    }
}