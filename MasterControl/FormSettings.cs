using System;
using System.Windows.Forms;

namespace MasterControl
{
    public partial class FormSettings : Form
    {

        public FormSettings()
        {
            InitializeComponent();
        }

        private void FormSettings_Load(object sender, EventArgs e)
        {
            pgSettings.SelectedObject = MasterControlSettings.Instance;
        }

        private void pgSettings_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            if (pgSettings.SelectedObject != null && pgSettings.SelectedObject is MasterControlSettings)
                ((MasterControlSettings)pgSettings.SelectedObject).Save();
        }
    }
}
