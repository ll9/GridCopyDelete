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
                var person = JsonConvert.DeserializeObject<Person>(File.ReadAllText(SettingsPath));

                personBindingSource.DataSource = person;
            }
            else
            {
                personBindingSource.DataSource = new Person(1, null, Gender.None);
            }
        }


        private void SaveButton_Click(object sender, EventArgs e)
        {
            var person = personBindingSource.DataSource as Person;
            var str = JsonConvert.SerializeObject(person);

            File.WriteAllText(SettingsPath, str);
        }
    }
}
