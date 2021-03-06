﻿using JsonSettings.Models;
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

        public Form1()
        {
            InitializeComponent();
            LoadSettings();
            InitContextMenu();
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
                var people = JsonConvert.DeserializeObject<List<Person>>(File.ReadAllText(SettingsPath));

                personBindingSource.DataSource = people;
            }
            else
            {
                personBindingSource.DataSource = new List<Person>();
            }
        }


        private void SaveButton_Click(object sender, EventArgs e)
        {
            var people = personBindingSource.DataSource as List<Person>;
            var str = JsonConvert.SerializeObject(people);

            File.WriteAllText(SettingsPath, str);
        }


        private void personDataGridView_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                DeleteSelectedGridItem(this, e);

        }
    }
}
