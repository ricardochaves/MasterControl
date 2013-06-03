using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AuctionWrapper;

namespace RBCAH
{
    public partial class AhForm : Form
    {
        private Font _font;
        public AhForm()
        {
            InitializeComponent();
            Icon = Resources.ProgramIcon;
            InitializeAhList();
            ahItemsList.RowsDefaultCellStyle.Font = _font;
            ahItemsList.RowsDefaultCellStyle.ForeColor = Resources.BodyColor;
            ahItemsList.RowsDefaultCellStyle.BackColor = Resources.PrimaryBackColor;
            ahItemsList.AlternatingRowsDefaultCellStyle.BackColor = Resources.SecondaryBackColor;
            ahItemsLabel.Font = Resources.HeaderFont;
            ahItemsLabel.ForeColor = Resources.HeaderColor;
            var fontName = Resources.NormalTextFont.Name;
            var fontSize = Resources.NormalTextFont.Size;
            _font = new Font(fontName, fontSize - 2);
            var buttonFont = new Font(_font, FontStyle.Bold);
            importExportLabel.Font = Resources.HeaderFont;
            importExportLabel.ForeColor = Resources.HeaderColor;
            importButton.Font = buttonFont;
            importButton.ForeColor = Color.White;
            importButton.BackgroundImage = Resources.BlankButton;
            exportButton.Font = buttonFont;
            exportButton.ForeColor = Color.White;
            exportButton.BackgroundImage = Resources.BlankButton;
            saveButton.Font = buttonFont;
            saveButton.ForeColor = Color.White;
            saveButton.BackgroundImage = Resources.BlankButton;
            ahItemsList.BackgroundColor = Resources.SecondaryBackColor;
            clearButton.BackgroundImage = Resources.BlankButton;
            clearButton.Font = buttonFont;
            clearButton.ForeColor = Color.White;
            BackgroundImage = Resources.BackgroundTile;
        }

        private void InitializeAhList()
        {
            ahItemsList.AutoGenerateColumns = false;
            var nameColumn = new DataGridViewTextBoxColumn
                                 {
                                     DataPropertyName = "Name",
                                     HeaderText = "Name",
                                     Name = "Name",
                                     AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                                     Width = 172,
                                     SortMode = DataGridViewColumnSortMode.Automatic,
                                 };

            var minPriceColumn = new DataGridViewTextBoxColumn
                                     {
                                         DataPropertyName = "MinPriceTotal",
                                         Name = "MinPriceTotal",
                                         HeaderText = "Min Price",
                                         AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                                         Width = 80,
                                         SortMode = DataGridViewColumnSortMode.Automatic
                                     };

            var maxPriceColumn = new DataGridViewTextBoxColumn
                                     {
                                         DataPropertyName = "MaxPriceTotal",
                                         HeaderText = "Fallback Price",
                                         Name = "MaxPriceTotal",
                                         AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                                         Width = 80,
                                         SortMode = DataGridViewColumnSortMode.Automatic
                                     };
            var durationColumn = new DataGridViewComboBoxColumn
                                     {
                                         DataPropertyName = "DurationString",
                                         HeaderText = "Duration",
                                         Name = "Duration",
                                         DataSource = new []{"12h", "24h", "48h"},
                                         AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                                         Width = 70,
                                         SortMode = DataGridViewColumnSortMode.Automatic,
                                         FlatStyle = FlatStyle.Popup
                                     };
            var stacksColumn = new DataGridViewTextBoxColumn
                                   {
                                       DataPropertyName = "Stacks",
                                       HeaderText = "Stacks",
                                       Name = "Stacks",
                                       AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                                       Width = 45,
                                       SortMode = DataGridViewColumnSortMode.Automatic
                                   };

            var sellAllColumn = new DataGridViewCheckBoxColumn
                                    {
                                        DataPropertyName = "SellAll",
                                        HeaderText = "All",
                                        Name = "SellAll",
                                        AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                                        Width = 25,
                                        SortMode = DataGridViewColumnSortMode.Automatic,
                                        FlatStyle = FlatStyle.Flat,
                                    };

            var stackSizeColumn = new DataGridViewTextBoxColumn
                                      {
                                          DataPropertyName = "StackSize",
                                          HeaderText = "Size",
                                          Name = "StackSize",
                                          AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                                          Width = 35,
                                          SortMode = DataGridViewColumnSortMode.Automatic
                                      };

            var manualColumn = new DataGridViewCheckBoxColumn
                                   {
                                       DataPropertyName = "Manual",
                                       HeaderText = "Manual",
                                       Name = "Manual",
                                       AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                                       Width = 45,
                                       SortMode = DataGridViewColumnSortMode.Automatic,
                                       FlatStyle = FlatStyle.Flat
                                   };

            var deleteColumn = new DataGridViewButtonColumn
                                   {
                                       HeaderText = "",
                                       Name = "Delete",
                                       Text = "DEL",
                                       AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                                       Width = 35,
                                       UseColumnTextForButtonValue = true,
                                   };
            
            ahItemsList.AllowUserToResizeRows = false;
            ahItemsList.AllowUserToResizeColumns = false;

            ahItemsList.Columns.Add(nameColumn);
            ahItemsList.Columns.Add(minPriceColumn);
            ahItemsList.Columns.Add(maxPriceColumn);
            ahItemsList.Columns.Add(durationColumn);
            ahItemsList.Columns.Add(stacksColumn);
            ahItemsList.Columns.Add(sellAllColumn);
            ahItemsList.Columns.Add(stackSizeColumn);
            ahItemsList.Columns.Add(manualColumn);
            ahItemsList.Columns.Add(deleteColumn);
            //ahItemsList.Columns.Add(cancelColumn);

            ahItemsList.DataSource = Salesman.Instance.BindingAhItems;
            
        }

        private void ahItemsList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (ahItemsList.Columns[e.ColumnIndex].Name == "MinPriceTotal" || ahItemsList.Columns[e.ColumnIndex].Name == "MaxPriceTotal")
            {
                if (e.Value != null)
                {
                    try
                    {
                        e.Value = Money.ToString((int) e.Value);
                    }
                    catch (Exception)
                    {
                        e.FormattingApplied = false;
                        throw;
                    }
                }
            }
            if (ahItemsList.Columns[e.ColumnIndex].Name == "Stacks" || ahItemsList.Columns[e.ColumnIndex].Name == "StackSize")
            {
                if (e.Value != null)
                {
                    e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }
            if (ahItemsList.Columns[e.ColumnIndex].Name == "Stacks")
            {
                if (e.Value != null)
                {
                    var sellAll = (bool) ahItemsList.Rows[e.RowIndex].Cells["SellAll"].Value;
                    if (sellAll)
                    {
                        e.Value = "All";
                        e.FormattingApplied = true;
                    }
                }
            }
        }

        private void ahItemsList_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            if (ahItemsList.Columns[e.ColumnIndex].Name == "Duration")
            {
                if (e.Value != null)
                {
                    switch (e.Value.ToString())
                    {
                        case "12h":
                            e.Value = AhItem.Durations.HalfDay;
                            break;
                        case "24h":
                            e.Value = AhItem.Durations.OneDay;
                            break;
                        case "48h":
                            e.Value = AhItem.Durations.TwoDays;
                            break;
                        default:
                            e.Value = AhItem.Durations.HalfDay;
                            break;
                    }
                    e.ParsingApplied = true; 
                }
            }
            if (ahItemsList.Columns[e.ColumnIndex].Name == "MinPriceTotal" || ahItemsList.Columns[e.ColumnIndex].Name == "MaxPriceTotal")
            {
                if (e.Value != null)
                {
                    try
                    {
                        e.Value = new Money(e.Value.ToString()).TotalCopper;
                        e.ParsingApplied = true;
                    }
                    catch (Exception)
                    {
                        e.ParsingApplied = false;
                        throw;
                    }
                }
            }
        }

        private void ahItemsList_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (ahItemsList.CurrentCell.OwningColumn.Name == "Name")
            {
                var te = e.Control as TextBox;
                te.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                Console.WriteLine(te.AutoCompleteCustomSource.Count);
                if (te.AutoCompleteCustomSource.Count == 0)
                    te.AutoCompleteCustomSource.AddRange(Salesman.Instance.UMJ.ItemNames);
                te.AutoCompleteSource = AutoCompleteSource.CustomSource;
            }
            var otherTextboxColumns = new[] {"MinPriceTotal", "MaxPriceTotal", "Stacks", "Size"};
            if (otherTextboxColumns.Contains(ahItemsList.CurrentCell.OwningColumn.Name))
            {
                var te = e.Control as TextBox;
                te.AutoCompleteMode = AutoCompleteMode.None;
            }
        }

        private void ahItemsList_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            var name = ahItemsList.CurrentCell.OwningColumn.Name;
            if (ahItemsList.IsCurrentCellDirty && (name == "SellAll" || name == "Duration" || name == "Cancel"))
            {
                ahItemsList.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void ahItemsList_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            e.Row.Cells["Name"].Value = "Insert item name or ID";
        }

        private void importButton_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Import Auctions List";
            openFileDialog.CheckPathExists = true;
            openFileDialog.CheckFileExists = true;
            openFileDialog.DefaultExt = "sal";
            openFileDialog.Filter = "Salesman Files (*.sal, *.cfg)|*.sal;*.cfg";
            openFileDialog.RestoreDirectory = true;
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                if (Path.GetExtension(openFileDialog.FileName).Equals(".cfg"))
                {
                    var file = File.ReadAllLines(openFileDialog.FileName).ToList();
                    for (int i = file.Count-1; i >= 0; i--)
                    {
                        if (file[i].Equals("</Salesman>"))
                            file.RemoveAt(i);
                        else if (file[i].Contains("<MaxCycle>"))
                            file.RemoveAt(i);
                        else if (file[i].Contains("<MinCycle>"))
                            file.RemoveAt(i);
                        else if (file[i].Contains("<CycleDelay>"))
                            file.RemoveAt(i);
                        else if (file[i].Contains("<GetMail>"))
                            file.RemoveAt(i);
                        else if (file[i].Contains("<UseGBank>"))
                            file.RemoveAt(i);
                        else if (file[i].Contains("<UsePBank>"))
                            file.RemoveAt(i);
                        else if (file[i].Contains("<CodItems />"))
                            file.RemoveAt(i);
                        else if (file[i].Equals("  </AhItems>"))
                            file[i] = "</ArrayOfAhItem>";
                        else if (file[i].Contains("    <AhItems"))
                            file[i] = file[i].Replace("    <AhItems", "  <AhItem");
                        else if (file[i].Equals("  <AhItems>"))
                            file[i] = "<ArrayOfAhItem xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\">";
                        else if (file[i].Equals("<Salesman xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">"))
                            file.RemoveAt(i);
                    }
                    var newFileName = openFileDialog.FileName.Substring(0, openFileDialog.FileName.Length - 4) + ".sal";
                    File.WriteAllLines(newFileName, file);
                    openFileDialog.FileName = newFileName;
                }
                Salesman.Instance.AhItems = ObjectXMLSerializer<List<AhItem>>.Load(openFileDialog.FileName);
                Salesman.Instance.BindingAhItems.Clear();
                Salesman.Instance.AhItems.ForEach(Salesman.Instance.BindingAhItems.Add);
                Salesman.Save();
            }
        }

        private void exportButton_Click(object sender, EventArgs e)
        {
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Export Auctions List";
            saveFileDialog.CheckPathExists = true;
            saveFileDialog.DefaultExt = "sal";
            saveFileDialog.Filter = "Salesman Auction List (*.sal)|*.sal";
            saveFileDialog.RestoreDirectory = true;
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                ObjectXMLSerializer<List<AhItem>>.Save(Salesman.Instance.AhItems, saveFileDialog.FileName);
            }

        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            SaveSettings();
            Close();
        }

        private void SaveSettings()
        {
            Salesman.Instance.AhItems = Salesman.Instance.BindingAhItems.ToList();
            Salesman.Save();
        }

        private void ahItemsList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == ahItemsList.Columns["Delete"].Index)
            {
                var message = "Do you really want to delete this item?\n\nName: {0}\nStack Size: {1}\nStack Count: {2}";
                var name = ahItemsList.Rows[e.RowIndex].Cells["Name"].Value;
                var size = ahItemsList.Rows[e.RowIndex].Cells["StackSize"].Value;
                var count = ahItemsList.Rows[e.RowIndex].Cells["Stacks"].Value;
                message = string.Format(message, name, size, count);
                if (MessageBox.Show(this, message, "Confirm: Delete item?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    ahItemsList.Rows.RemoveAt(e.RowIndex);
                }
            }
        }
        private void AhItemsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveSettings();
        }

        private void presetsGemsButton_MouseDown(object sender, MouseEventArgs e)
        {
            var button = (Button)sender;
            button.BackgroundImage = Resources.BlankButtonClicked;
        }

        private void presetsGemsButton_MouseEnter(object sender, EventArgs e)
        {
            var button = (Button)sender;
            button.BackgroundImage = Resources.BlankButtonMouseover;
        }

        private void presetsGemsButton_MouseLeave(object sender, EventArgs e)
        {
            var button = (Button)sender;
            button.BackgroundImage = Resources.BlankButton;
        }

        private void presetsGemsButton_MouseUp(object sender, MouseEventArgs e)
        {
            var button = (Button)sender;
            button.BackgroundImage = Resources.BlankButtonMouseover;
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "Are you sure you want to remove all items from the list?\nUnless you exported the items first, this action is IRREVERSIBLE.\n\nContinue?", "Confirm clear items", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                Salesman.Instance.BindingAhItems.Clear();
            }
        }
    }
}
