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

        public Form1()
        {
            InitializeComponent();
            LoadSettings();
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
    }
}
