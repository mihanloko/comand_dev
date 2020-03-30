﻿using System;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class PriemWorkPrikaz : Form
    {
        public PriemWorkPrikaz()
        {
            InitializeComponent();
        }

        public PriemWorkPrikaz(decimal tabNumber)
        {
            InitializeComponent();
            initData(tabNumber);
        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DelWorkPrikaz deleteWork = new DelWorkPrikaz();
            deleteWork.Show();
        }

        private void initData(decimal tabNumber)
        {
            Model1 model = new Model1();
            var org = model.OUR_ORG.FirstOrDefault(c => c.PK_OUR_ORG == 1);
            if (org == null) 
                return;
            string numberDoc = "12345678";
            var prikaz = model.PRIKAZ.FirstOrDefault();
            if (prikaz != null)
            {
                numberDoc = prikaz.NUMDOC;
                int numberDocInt = 0;
                Int32.TryParse(numberDoc, out numberDocInt);
                numberDocInt++;
                numberDoc = numberDocInt.ToString();
            }
            textBox11.Text = org.NAME;
            textBox12.Text = "0301001";
            textBox13.Text = org.OKPO;
            textBox14.Text = numberDoc;

            var selectedMen = model.PERSONCARD.FirstOrDefault(men => men.TABEL_NUM == tabNumber);
            textBox1.Text = selectedMen.SURNAME;
            textBox2.Text = selectedMen.NAME;
            textBox3.Text = selectedMen.MIDDLENAME;
            
            textBox4.Text = selectedMen.TABEL_NUM.ToString();
            
            textBox5.Text = selectedMen.TABEL.PODRAZDELORG.NAME;
            textBox6.Text = selectedMen.JOB_POSITION.NAME;
            textBox7.Text = selectedMen.PROFESSION.NAME;
            textBox8.Text = selectedMen.CHARACTER_WORK.NAME;

            var strStat  = model.STR_SHTAT_RASP.FirstOrDefault(stat => stat.PK_JOB_POS == selectedMen.JOB_POSITION.PK_JOB_POS);
            numericUrerpDown1.Value = (decimal) strStat.TARIFF;
            numericUrerpDown1.Value = (decimal) strStat.NADBAVKA1;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                dateTimePicker2.Enabled = true;
            }
            else
            {
                dateTimePicker2.Enabled = false;
            } 
        }
    }
}