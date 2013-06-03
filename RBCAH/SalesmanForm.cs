using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using AuctionWrapper;

namespace RBCAH
{
    public partial class SalesmanForm : Form
    {
        public SalesmanForm()
        {
            InitializeComponent();
            Icon = Resources.ProgramIcon;
            FillDefaultValues();
            var font = new Font("Tahoma", 8.0f);
            
            botHavenLogo.Image = Resources.Logo;
            BackgroundImage = Resources.BackgroundTile;
            doneActionLabel.Font = font;
            doneActionLabel.ForeColor = Resources.BodyColor;
            profilePathLabel.Font = font;
            profilePathLabel.ForeColor = Resources.BodyColor;
            profilePathValue.Font = font;
            profilePathValue.ForeColor = Resources.BodyColor;
            profilePathValue.BackColor = Resources.SecondaryBackColor;
            botbaseLabel.Font = font;
            botbaseLabel.ForeColor = Resources.BodyColor;
            botbaseValue.Font = font;
            botbaseValue.ForeColor = Resources.BodyColor;
            botbaseValue.BackColor = Resources.SecondaryBackColor;
            doneActionValue.Font = font;
            doneActionValue.ForeColor = Resources.BodyColor;
            doneActionValue.BackColor = Resources.SecondaryBackColor;
            minWaitTimeLabel.Font = font;
            minWaitTimeLabel.ForeColor = Resources.BodyColor;
            minWaitTimeValue.Font = font;
            minWaitTimeValue.ForeColor = Resources.BodyColor;
            minWaitTimeValue.BackColor = Resources.SecondaryBackColor;
            maxWaitTimeLabel.Font = font;
            maxWaitTimeLabel.ForeColor = Resources.BodyColor;
            maxWaitTimeValue.Font = font;
            maxWaitTimeValue.ForeColor = Resources.BodyColor;
            maxWaitTimeValue.BackColor = Resources.SecondaryBackColor;
            undercutSettingsBox.Font = font;
            undercutSettingsBox.ForeColor = Resources.HeaderColor;
            undercutTypeLabel.Font = font;
            undercutTypeLabel.ForeColor = Resources.BodyColor;
            undercutTypeValue.Font = font;
            undercutTypeValue.ForeColor = Resources.BodyColor;
            undercutTypeValue.BackColor = Resources.SecondaryBackColor;
            undercutAmountLabel.Font = font;
            undercutAmountLabel.ForeColor = Resources.BodyColor;
            undercutAmountFixedValue.Font = font;
            undercutAmountFixedValue.ForeColor = Resources.BodyColor;
            undercutAmountFixedValue.BackColor = Resources.SecondaryBackColor;
            undercutAmountPercentageSymbolLabel.Font = font;
            undercutAmountPercentageSymbolLabel.ForeColor = Resources.BodyColor;
            undercutAmountPercentageValue.Font = font;
            undercutAmountPercentageValue.ForeColor = Resources.BodyColor;
            undercutAmountPercentageValue.BackColor = Resources.SecondaryBackColor;
            stacksizesLabel.Font = font;
            stacksizesLabel.ForeColor = Resources.BodyColor;
            stacksizesValue.Font = font;
            stacksizesValue.ForeColor = Resources.BodyColor;
            stacksizesValue.BackColor = Resources.SecondaryBackColor;
            minPriceBehaviorLabel.Font = font;
            minPriceBehaviorLabel.ForeColor = Resources.BodyColor;
            minPriceBehaviorValue.Font = font;
            minPriceBehaviorValue.ForeColor = Resources.BodyColor;
            minPriceBehaviorValue.BackColor = Resources.SecondaryBackColor;
            var buttonFont = new Font(font, FontStyle.Bold);
            saveButton.Font = buttonFont;
            saveButton.ForeColor = Color.White;
            saveButton.BackgroundImage = Resources.BlankButton;
            ahItemsButton.Font = buttonFont;
            ahItemsButton.ForeColor = Color.White;
            ahItemsButton.BackgroundImage = Resources.BlankButton;
        }

        private void doneActionValue_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (doneActionValue.SelectedIndex)
            {
                case 0:
                    loadProfileBox.Visible = false;
                    waitCycleBox.Visible = true;
                    break;
                case 1:
                    waitCycleBox.Visible = false;
                    loadProfileBox.Visible = true;
                    break;
                case 2:
                    waitCycleBox.Visible = false;
                    loadProfileBox.Visible = false;
                    break;
            }
        }

        private void ahItemsButton_Click(object sender, EventArgs e)
        {
            var ahItems = new AhForm();
            ahItems.Show();
        }

        private void profilePathButton_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Select Honorbuddy Profile";
            openFileDialog.CheckPathExists = true;
            openFileDialog.CheckFileExists = true;
            openFileDialog.DefaultExt = "xml";
            openFileDialog.Filter = "Honorbuddy Profile (*.xml)|*.xml";
            openFileDialog.RestoreDirectory = true;
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                profilePathValue.Text = openFileDialog.FileName;
                profilePathValue.SelectionStart = profilePathValue.Text.Length;
            }
        }

        private void undercutTypeValue_SelectedIndexChanged(object sender, EventArgs e)
        {
            undercutAmountFixedValue.Visible = undercutTypeValue.SelectedIndex == 1;
            undercutAmountPercentageValue.Visible = undercutTypeValue.SelectedIndex == 0;
            undercutAmountPercentageSymbolLabel.Visible = undercutTypeValue.SelectedIndex == 0;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            SaveSettings();
            Close();
        }

        private void SaveSettings()
        {
            Salesman.Instance.CycleMax = (int)maxWaitTimeValue.Value;
            Salesman.Instance.CycleMin = (int)minWaitTimeValue.Value;
            Salesman.Instance.DoneBehaviorNumValue = doneActionValue.SelectedIndex;
            Salesman.Instance.LoadProfileBotbase = botbaseValue.Text;
            Salesman.Instance.LoadProfilePath = profilePathValue.Text;
            Salesman.Instance.MinPriceBehaviorNumValue = minPriceBehaviorValue.SelectedIndex;
            Salesman.Instance.UndercutFixedAmount = new Money(undercutAmountFixedValue.Text);
            Salesman.Instance.UndercutPercentage = (int)undercutAmountPercentageValue.Value;
            Salesman.Instance.UndercutUsePercentage = undercutTypeValue.SelectedIndex == 0;
            Salesman.Instance.UndercutStacksizeTypeNumValue = stacksizesValue.SelectedIndex;
            Salesman.Instance.UndercutStacksizeTypeNumValue = stacksizesValue.SelectedIndex;
            Salesman.Save();
        }
        public void FillDefaultValues()
        {
            doneActionValue.SelectedIndex = Salesman.Instance.DoneBehaviorNumValue;
            maxWaitTimeValue.Value = Salesman.Instance.CycleMax;
            minWaitTimeValue.Value = Salesman.Instance.CycleMin;
            undercutTypeValue.SelectedIndex = Salesman.Instance.UndercutUsePercentage ? 0 : 1;
            undercutAmountPercentageValue.Value = Salesman.Instance.UndercutPercentage;
            undercutAmountFixedValue.Text = Salesman.Instance.UndercutFixedAmount.ToString();
            minPriceBehaviorValue.SelectedIndex = Salesman.Instance.MinPriceBehaviorNumValue;
            stacksizesValue.SelectedIndex = 0;
            profilePathValue.Text = Salesman.Instance.LoadProfilePath;
            botbaseValue.Text = Salesman.Instance.LoadProfileBotbase;
            stacksizesValue.SelectedIndex = Salesman.Instance.UndercutStacksizeTypeNumValue;
        }

        private void SalesmanForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveSettings();
        }

        private void saveButton_MouseEnter(object sender, EventArgs e)
        {
            var button = (Button)sender;
            button.BackgroundImage = Resources.BlankButtonMouseover;
        }

        private void saveButton_MouseLeave(object sender, EventArgs e)
        {
            var button = (Button)sender;
            button.BackgroundImage = Resources.BlankButton;
        }

        private void saveButton_MouseDown(object sender, MouseEventArgs e)
        {
            var button = (Button)sender;
            button.BackgroundImage = Resources.BlankButtonClicked;
        }

        private void saveButton_MouseUp(object sender, MouseEventArgs e)
        {
            var button = (Button) sender;
            button.BackgroundImage = Resources.BlankButtonMouseover;
        }

        private void botHavenLogo_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://bothaven.com/products/salesman-the-easiest-and-greatest-ah-bot");
        }
    }
}
