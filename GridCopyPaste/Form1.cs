using JsonSettings.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JsonSettings
{
    public partial class Form1 : Form
    {
        private static string SettingsPath => Application.StartupPath + "\\" + "settings.json";
        public List<Person> People { get; set; }
        public List<HashSet<string>> AutoSuggest { get; set; }

        public Form1()
        {
            InitializeComponent();
            LoadSettings();
            InitContextMenu();
            InitAutoSuggestions();
        }

        private void InitAutoSuggestions()
        {
            throw new NotImplementedException();
        }

        private void InitContextMenu()
        {
            ContextMenuStrip mnu = new ContextMenuStrip();
            ToolStripMenuItem mnuCopy = new ToolStripMenuItem("Copy");
            ToolStripMenuItem mnuDel = new ToolStripMenuItem("Delete");
            ToolStripMenuItem mnuPaste = new ToolStripMenuItem("Paste");
            //Assign event handlers
            mnuDel.Click += DeleteSelectedGridItem;
            mnuCopy.Click += (sender, e) =>
            {
                var conent = personDataGridView.GetClipboardContent();
                Clipboard.SetDataObject(personDataGridView.GetClipboardContent());
            };
            mnuPaste.Click += (sender, e) =>
            {
                MessageBox.Show("Should Paste Here");
            };
            //Add to main context menu
            mnu.Items.AddRange(new ToolStripItem[] { mnuCopy, mnuPaste, mnuDel });
            //Assign to datagridview
            personDataGridView.ContextMenuStrip = mnu;
        }

        private void DeleteSelectedGridItem(object sender, EventArgs e)
        {
            foreach (DataGridViewCell cell in personDataGridView.SelectedCells)
            {
                cell.Value = null;
            }
        }

        private void LoadSettings()
        {

            if (File.Exists(SettingsPath))
            {
                People = JsonConvert.DeserializeObject<List<Person>>(File.ReadAllText(SettingsPath));

                personBindingSource.DataSource = People;
            }
            else
            {
                personBindingSource.DataSource = new List<Person>();
            }
        }


        private void SaveButton_Click(object sender, EventArgs e)
        {
            var str = JsonConvert.SerializeObject(People);

            File.WriteAllText(SettingsPath, str);
        }


        private void personDataGridView_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                DeleteSelectedGridItem(this, e);

        }

        private void personDataGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (true)
            {
                TextBox prodCode = e.Control as TextBox;
                if (prodCode != null)
                {
                    prodCode.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    prodCode.AutoCompleteCustomSource = new AutoCompleteStringCollection() { "Hans", "Jens" };
                    prodCode.AutoCompleteSource = AutoCompleteSource.CustomSource;
                }
            }
        }
    }
}
