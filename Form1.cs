using System;
using System.Collections.Generic;
using System.Windows.Forms;


namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {


        string typeColumn;
        decimal sColumn;
        
        private decimal k = 0.785M;


        public Form1()
        {
            InitializeComponent();
            
        }

        
        
        Calculator Calculator = new Calculator();

        public void Attached()
        {
        Calculator.Hard = Convert.ToDecimal(hardnessTextBox.Text);
        Calculator.NO3 = Convert.ToDecimal(NO3TextBox.Text);
        Calculator.SO4 = Convert.ToDecimal(SO4TextBox.Text);
        //MessageBox.Show(Calculator.CationCapasityL.ToString()+"  "+Calculator.NO3.ToString() + "  " + Calculator.SO4.ToString());

        }

        public decimal SColumnMethod()
        {
            if (typeColumn == "8x44 или кабинет")
            {
                sColumn = k * (0.205M * 0.205M);
                return 20M;
            }
            else if (typeColumn == "10х44")
            {
                sColumn = k * (0.257M * 0.257M);
                return 25M;
            }
            else if (typeColumn == "10х54")
            {
                sColumn = k * (0.257M * 0.257M);
                return 37.5M;
            }

            else if (typeColumn == "12х52")
            {
                sColumn = k * (0.307M * 0.307M);
                return 50M;
            }

            else if (typeColumn == "13х54")
            {
                sColumn = k * (0.335M * 0.335M);
                return 50M;
            }
            else if (typeColumn == "14х65")
            {
                sColumn = k * (0.366M * 0.366M);
                return  85M;
            }
            else if (typeColumn == "16х65")
            {
                sColumn = k * (0.411M * 0.411M);
                return 113M;
            }
            else if (typeColumn == "18х65")
            {
                sColumn = k * (0.491M * 0.491M);
                return 150M;
            }
            else if (typeColumn == "21х62")
            {
                sColumn = k * (0.555M * 0.555M);
                return 188M;
            }
            else if (typeColumn == "24х72")
            {
                sColumn = k * (0.611M * 0.611M);
                return 275M;
            }
            else if (typeColumn == "30х72")
            {
                sColumn = k * (0.781M * 0.781M);
                return 425M;
            }
            else if (typeColumn == "36х72")
            {
                sColumn = k * (0.934M * 0.934M);
                return 625M;
            }
            else if (typeColumn == "42х72")
            {
                sColumn = k * (1.09M * 1.09M);
                return 850M;
            }
            else if (typeColumn == "48х72")
            {
                sColumn = k * (1.235M * 1.235M);
                return 1050M;
            }
            else
            {
                sColumn = k * (1.6M * 1.6M);
                return 1200M;
            }

        }



       

        private void Form1_Load(object sender, EventArgs e)
        {
            V250.Checked = true;
            percentMix.Text = trackBarMix.Value.ToString();
            Calculator.valuePercentMix = Convert.ToDecimal(trackBarMix.Value);
            //Коллекция типа словарь для типоразмеров колонн, привязанная к кварцу
            Dictionary<string, string> mapTwo = new Dictionary<string, string>();

            mapTwo.Add("8x44 или кабинет", "6");
            mapTwo.Add("10х44", "10");
            mapTwo.Add("10х54", "10");
            mapTwo.Add("12х52", "15");
            mapTwo.Add("13х54", "15");
            mapTwo.Add("14х65", "20");
            mapTwo.Add("16х65", "30");
            mapTwo.Add("18х65", "40");
            mapTwo.Add("21х62", "60");
            mapTwo.Add("24х72", "80");
            mapTwo.Add("30х72", "120");
            mapTwo.Add("36х72", "132");
            mapTwo.Add("42х72", "280");
            mapTwo.Add("48х72", "360");
            mapTwo.Add("63х67", "600");

            // нижние строки позволяют использовать коллекцию ключ- значение в связке с выпадающим списком  :
            columnComboBox.DataSource = new BindingSource(mapTwo, null);
            columnComboBox.DisplayMember = "Key";


            
        }

        private void Point(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsControl(e.KeyChar) && !Char.IsDigit(e.KeyChar) && !(e.KeyChar == 44))
            {
                e.Handled = true;
            }
        }

        private void ColumnComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string value = ((KeyValuePair<string, string>)columnComboBox.SelectedItem).Value;// значение значения словаря кладем в value
            textBox4.Text = value;
            typeColumn = ((KeyValuePair<string, string>)columnComboBox.SelectedItem).Key;// значение ключа словаря кладем в typeColumn
            
        }


        private void CalcNO3Button_Click(object sender, EventArgs e)
        {

            if ((NO3TextBox.Text != "" && NO3TextBox.Text != "0") && (SO4TextBox.Text != "" && SO4TextBox.Text != "0") && (hardnessTextBox.Text != "" && hardnessTextBox.Text != "0"))

            {
                labelLog.Text = "";


                Attached();

                tC007ResinL.Text = Calculator.CationCapasityL().ToString("0.0");

                Calculator.FullVolume = SColumnMethod();
                

                if (V125.Checked == true)
                {
                    textBoxNO3Bypass.Text = Calculator.Bypass125.ToString("0.0");
                    Calculator.VolumeA202= Calculator.AnionTotalPerL(Calculator.AnionCapasity125, Calculator.Bypass125);
                    a202ResinL.Text = Calculator.AnionTotalPerL(Calculator.AnionCapasity125, Calculator.Bypass125).ToString("0.00");
                    
                    textBox2.Text=Calculator.VolumeA202.ToString("0");
              
                  
                    saltTextBox.Text = Calculator.SumSalt(0.125M).ToString("0.0");
                }
                else if (V250.Checked == true)
                {
                    textBoxNO3Bypass.Text = Calculator.Bypass250.ToString("0.0");
                   
                    Calculator.VolumeA202 = Calculator.AnionTotalPerL(Calculator.AnionCapasity250, Calculator.Bypass250);
                    a202ResinL.Text = Calculator.AnionTotalPerL(Calculator.AnionCapasity250, Calculator.Bypass250).ToString("0.00");
                    
                    textBox2.Text = Calculator.VolumeA202.ToString("0");
                    saltTextBox.Text = Calculator.SumSalt(0.25M).ToString("0.0");

                    
                }
                else
                {
                    MessageBox.Show("количество соли для регенерации не выбрано");
                }

                textBox1.Text = Calculator.VolumeTC007.ToString("0");

                textBox5.Text = Calculator.Capacity().ToString("0.0");




                averageFlowTextBox.Text = (Calculator.VReal() * sColumn).ToString("0.00");//добавляем в форму производительность для выбранной колонны

                               
               
                           }

            else

            {

                labelLog.Text="Нужно заполнить все исходные данные, они не должны быть нулями";
            }



        }

        private void TrackBarMix_Scroll(object sender, EventArgs e)
        {
            percentMix.Text = trackBarMix.Value.ToString();
            Calculator.valuePercentMix = Convert.ToDecimal(trackBarMix.Value);
            if (trackBarMix.Value < 30)
            {
                labelLog.Text = "Крайне не рекомендуется использовать значение ниже 30% любой из смол в составе смеси";
            }
            else
            {
                labelLog.Text = "           ";
            }

        }

        
private void HandMix_CheckedChanged(object sender, EventArgs e)
        {
            if (handMix.Checked == true)
            {
                trackBarMix.Enabled = true;
            }
            else
            {
                trackBarMix.Enabled = false;
                percentMix.Text = "30";
                trackBarMix.Value = 30;
                Calculator.valuePercentMix = 30M;
                labelLog.Text = "           ";
            }
        }

    }
}