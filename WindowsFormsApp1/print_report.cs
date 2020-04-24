﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using WindowsFormsApp1;

namespace WindowsFormsApp2
{
	public partial class print_report : Form
	{
		public print_report()
		{
			InitializeComponent();
			initCatalogs();
		}

		private void initCatalogs()
		{
			Model1 model = new Model1();
			String[] sorted_surname = model.PERSONCARD.OrderBy(p => p.SURNAME).Select(p => p.SURNAME).Distinct().ToArray();
			String[] sorted_name = model.PERSONCARD.OrderBy(p => p.NAME).Select(p => p.NAME).Distinct().ToArray();
			String[] sorted_middlename = model.PERSONCARD.OrderBy(p => p.MIDDLENAME).Select(p => p.MIDDLENAME).Distinct().ToArray();
			comboBox_surnameValue.Items.AddRange(sorted_surname);
			comboBox_nameValue.Items.AddRange(sorted_name);
			comboBox_middlenameValue.Items.AddRange(sorted_middlename);
		}

		private void button_run_Click(object sender, EventArgs e)
		{
			string path = @"C:\Program Files (x86)\MinGWStudio\Programs\Solutions\WindowsFormsApp2\WindowsFormsApp2\report.txt";

			using (StreamWriter writer = new StreamWriter(path, false))
			{
				Model1 model = new Model1();
				writer.WriteLine("Общество с ограниченной ответственностью");
				writer.WriteLine(model.OUR_ORG.First().ADDRESS);
				writer.WriteLine("тел. 8(822)13-80-22");
				writer.WriteLine("ИНН {0}, ОГРН {1}\n", model.OUR_ORG.First().INN, model.OUR_ORG.First().OGRN);
				writer.WriteLine("от {0}г.", DateTime.Now.ToString().Substring(0, 10)); //todo дату в текущий момент времени
				writer.WriteLine("СПРАВКА");
				PERSONCARD finded = model.PERSONCARD.SingleOrDefault(p => (p.NAME == comboBox_nameValue.Text &&
					p.MIDDLENAME == comboBox_middlenameValue.Text &&
						p.MIDDLENAME == comboBox_middlenameValue.Text));
				if (finded == null)
					MessageBox.Show("Сотрудник с указанными ФИО не найден!");
				else
				{
					writer.Write("{0} {1} {2}, {3} г.р., ", finded.SURNAME, finded.NAME, finded.MIDDLENAME, finded.BIRTHDATE.ToString().Substring(0, 10));
					writer.WriteLine("работает в ООО в должности ");
					writer.WriteLine("{0} с {1} г. ", finded.PROFESSION.NAME, finded.DATECREATE.ToString().Substring(0, 10));
					writer.WriteLine("по настоящее время, имеет оклад {0} рублей в месяц.", finded.PK_GRAZD * 4000);
					//writer.WriteLine("{0} с {1} г. ", finded.PROFESSION.NAME, finded.PEREVOD.LastOrDefault().STARTWORKDATE);
					//writer.Write("по настоящее время, имеет оклад {0} рублей в месяц.", finded.PEREVOD.LastOrDefault().JOB_POSITION1.STR_SHTAT_RASP.First().TARIFF);
					MessageBox.Show("Справка создана!");
				}
			}
		}
	}
}