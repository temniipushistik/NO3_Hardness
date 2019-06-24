using System;
using System.Collections.Generic;
using System.Windows.Forms;


namespace WindowsFormsApp3
{
    public class Calculator
    {


        

        private decimal anionCapasityL;
        
        private decimal cationCapasityL;






        //}
        /// <summary>
        /// свойство, которое принимает и отдает значение процентов с ползунка
        /// кстати value как раз значение проперти, которое передаем в свойство
        /// </summary>
        public decimal valuePercentMix { get; set; }


        /// <summary>
        /// Свойство, работающее с жесткостью
        /// </summary>
        public decimal Hard { get; set; }
        /// <summary>
        /// Емкость одного литра катионита
        /// </summary>
        /// <returns></returns>
        public decimal CationCapasityL()
        { 
             
                return cationCapasityL = 1.2M / Hard;
            
        }

        /// <summary>
        /// Автосвойство для чтения/записи нитратов в исходной воде
        /// </summary>
        public decimal NO3 { get; set; }

        /// <summary>
        /// Автосвойство для чтения/записи сульфатов в исходной воде
        /// </summary>

        public decimal SO4 { get; set; }


        /// <summary>
        /// Автосвойство полного объема смолы в выбранной в выпающем списке колонны
        /// </summary>
        public decimal FullVolume { get; set; }

        /// <summary>
        /// переменная, учавствующая в подборе соотношения смол
        /// </summary>

        decimal temporary;


        /// <summary>
        /// Суммируем анионы, введенные пользователем
        /// </summary>
        public decimal SumAnion
        {
            get
            {
                return NO3 + SO4;
            }
 
        }


        
        /// <summary>
        /// Счиаем линейную скорость на анионе
        /// </summary>
        /// <returns>скорость на анионе </returns>
        /// 
        public int VAnion
        {
            get
            {
                if (SumAnion <= 50M)
                {
                    return 25;
                }
                else if (SumAnion > 50M && SumAnion <= 100M)
                {
                    return 20;
                }
                else if (SumAnion > 100M && SumAnion <= 150M)
                {
                    return 15;
                }
                else if (SumAnion > 150M && SumAnion <= 200M)
                {
                    return 12;
                }
                else
                {
                    return 10;
                }
            }

        }

        /// <summary>
        /// Счиаем линейную скорость на катионе
        /// </summary>
        /// <returns>скорость на катионе </returns>
        /// 
        public int VCation
        {
            //катионы по жесткости
            get
            {
                if (this.Hard <= 5)
                {

                    return 25; //назначаем линейные скорости
                }
                else if (this.Hard > 5 & this.Hard <= 10)
                {
                    return 20;
                }
                else if (this.Hard > 10 & this.Hard <= 14)
                {
                    return 15;
                }
                else if (this.Hard > 14 & this.Hard <= 18)
                {
                    return 12;
                }
                else
                {
                    return 10;
                }
            }
        }

        /// <summary>
        /// выбираем реальную линейную скорость по наименьшей 
        /// </summary>
        /// <returns>получаем реальную линейную скорость </returns>
        public int VReal()
        {
            return Math.Min(VCation, VAnion);

        }


        /// <summary>
        /// считаем отношение нитратов к сумме сульфатом и нитратов в мг-экв/л
        /// </summary>
        /// <returns>отношение в мг-экв/л</returns>
        /// 
        public decimal DevideNO3SO4
        {
            get
            {
                return (NO3 / 62) / (NO3 / 62 + this.SO4 / 48);
            }
        }

        /// <summary>
        /// определяем по графику проскок по нитратам для 125г NaCl на л (формулы с графика)
        /// </summary>
        /// <returns>процент по проскоку </returns>
        public decimal Bypass125
        {
            get
            {

                if (DevideNO3SO4 > 0.8M && DevideNO3SO4 <= 1.0M)
                {
                    return (-5M * DevideNO3SO4 + 17M) * NO3 * 0.01M;
                }

                else if (DevideNO3SO4 > 0.6M && DevideNO3SO4 <= 0.8M)
                {
                    return (-22.5M * DevideNO3SO4 + 31M) * NO3 * 0.01M;
                }


                else if (DevideNO3SO4 > 0.5M && DevideNO3SO4 <= 0.6M)
                {
                    return (-35M * DevideNO3SO4 + 38.5M) * NO3 * 0.01M;
                }


                else if (DevideNO3SO4 > 0.4M && DevideNO3SO4 <= 0.5M)
                {
                    return (-39M * DevideNO3SO4 + 40M) * NO3 * 0.01M;
                }



                else if (DevideNO3SO4 > 0.3M && DevideNO3SO4 <= 0.4M)
                {
                    return (-47M * DevideNO3SO4 + 43.6M) * NO3 * 0.01M;
                }


                else if (DevideNO3SO4 > 0.2M && DevideNO3SO4 <= 0.3M)
                {
                    return (-65M * DevideNO3SO4 + 49M) * NO3 * 0.01M;
                }


                else if (DevideNO3SO4 > 0M && DevideNO3SO4 <= 0.2M)
                {
                    return (-100M * DevideNO3SO4 + 56M) * NO3 * 0.01M;
                }
                else
                {
                    return NO3;
                }
            }
        }


        /// <summary>
        /// определяем по графику проскок по нитратам для 250г NaCl на л (формулы с графика)
        /// </summary>
        /// <returns>процент по проскоку </returns>

        public decimal Bypass250
        {
            get
            {

                if (DevideNO3SO4 > 0.8M && DevideNO3SO4 <= 1.0M)
                {
                    return (-2.5M * DevideNO3SO4 + 11.5M) * NO3 * 0.01M;
                }
                else if (DevideNO3SO4 > 0.6M && DevideNO3SO4 <= 0.8M)
                {
                    return (-7.5M * DevideNO3SO4 + 15.5M) * NO3 * 0.01M;
                }
                else if (DevideNO3SO4 > 0.4M && DevideNO3SO4 <= 0.6M)
                {
                    return (-25M * DevideNO3SO4 + 26M) * NO3 * 0.01M;
                }
                else if (DevideNO3SO4 > 0.2M && DevideNO3SO4 <= 0.4M)
                {
                    return (-45M * DevideNO3SO4 + 34M) * NO3 * 0.01M;
                }
                else if (DevideNO3SO4 > 0M && DevideNO3SO4 <= 0.2M)
                {
                    return (-75M * DevideNO3SO4 + 40M) * NO3 * 0.01M;
                }
                else
                {
                    return NO3;
                }

            }
        }

        /// <summary>
        /// Считаем емкость смолы в мг-экв/л при 125г/л (формулы с графика)
        /// </summary>
        /// <returns>емкость при 125г/л</returns>

        public decimal AnionCapasity125 {
            get
            {
              
                if (DevideNO3SO4 > 0.8M && DevideNO3SO4 <= 1.0M)
                {
                    return (0.6M * DevideNO3SO4 + 0.01M);
                }
                else if (DevideNO3SO4 > 0.6M && DevideNO3SO4 <= 0.8M)
                {
                    return (0.5M * DevideNO3SO4 + 0.1M);
                }
                else if (DevideNO3SO4 > 0.4M && DevideNO3SO4 <= 0.6M)
                {
                    return (0.3M * DevideNO3SO4 + 0.23M);
                }


                else if (DevideNO3SO4 > 0.2M && DevideNO3SO4 <= 0.4M)
                {
                    return (0.2M * DevideNO3SO4 + 0.26M);
                }

                else if (DevideNO3SO4 > 0.1M && DevideNO3SO4 <= 0.2M)
                {
                    return (0.1M * DevideNO3SO4 + 0.28M);
                }

                else if (DevideNO3SO4 > 0M && DevideNO3SO4 <= 0.1M)
                {
                    return 0.29M;
                }


                else
                {
                    return 0;
                }
            }

        }

        /// <summary>
        /// Считаем емкость смолы в мг-экв/л при 250г/л (формулы с графика)
        /// </summary>
        /// <returns>емкость при 250г/л</returns>

        public decimal AnionCapasity250
        {
            get
            {


                if (DevideNO3SO4 > 0.8M && DevideNO3SO4 <= 1.0M)
                {
                    return (0.65M * DevideNO3SO4 + 0.03M);
                }
                else if (DevideNO3SO4 > 0.6M && DevideNO3SO4 <= 0.8M)
                {
                    return (0.3M * DevideNO3SO4 + 0.32M);
                }
                else if (DevideNO3SO4 > 0.4M && DevideNO3SO4 <= 0.6M)
                {
                    return (0.25M * DevideNO3SO4 + 0.35M);
                }

                else if (DevideNO3SO4 > 0.1M && DevideNO3SO4 <= 0.4M)
                {
                    return (0.1M * DevideNO3SO4 + 0.4M);
                }

                else if (DevideNO3SO4 > 0M && DevideNO3SO4 <= 0.1M)
                {
                    return (0.05M * DevideNO3SO4 + 0.415M);
                }


                else
                {
                    return 0;
                }
            }

        }



      
        /// <summary>
        /// Метод считающий емкость одного литра анионита с учетом погрешности
        /// </summary>
        /// <param name="anionCapasity"> взависимости от того, что в классе Form1 в агумент попадает либо
        /// расчет из метода AnionCapasity125, либо расчет из AnionCapasity250 </param>
        /// <param name="bypass">опираясь на значение байпаса в аргументе мы используем его для расчета ресурса </param>
        /// <returns></returns>

        public decimal AnionTotalPerL(decimal anionCapasity, decimal bypass)
        {

            return anionCapasityL = (anionCapasity * 0.9M) / ((NO3 - bypass) / 62);

        }


        
        /// <summary>
        /// Автосвойство для работы с объемом катионнообменной смолы
        /// </summary>
        public decimal VolumeTC007 { set; get; }

        /// <summary>
        /// Свойсиво для работы с объемом анионнообменной смолы
        /// </summary>
        public decimal volumeA202;
        public decimal VolumeA202
        {
            get
             {
                temporary = Convert.ToDecimal(Math.Floor((anionCapasityL * FullVolume) / (anionCapasityL + cationCapasityL)));

                if (temporary > ((1M - (valuePercentMix / 100M))) * FullVolume)
                {
                    VolumeTC007 = Convert.ToDecimal(Math.Floor((100M - valuePercentMix) * FullVolume) / 100M);
                    //MessageBox.Show("процент подмеса " + valuePercentMix.ToString() + temporary + "временное значение ");
                    return FullVolume - VolumeTC007;

                }
                else if (temporary < ((valuePercentMix / 100M) * FullVolume))
                {
                    VolumeTC007 = Convert.ToDecimal(Math.Floor((valuePercentMix / 100M) * FullVolume));
                    //MessageBox.Show(valuePercentMix.ToString());
                    return FullVolume - VolumeTC007;
                }
                else /*MessageBox.Show(valuePercentMix.ToString());*/
                    VolumeTC007 = temporary;
                    return  FullVolume - temporary;


            }
            set
            {
               anionCapasityL = value;

            }
        }


        /// <summary>
        /// возвращаем минимальное значение из 2х ресурсов для определения ресурса до регенерации
        /// </summary>
        /// <returns></returns>


        public decimal Capacity()
        {

            return Math.Min(cationCapasityL * VolumeTC007, VolumeA202 * anionCapasityL);
        }



        /// sumSalt определяет сколько кг соли нужно на регенерацию смеси смол



          //ебучий гит сосет хуй и как всегда нихуя не работает 

        public decimal SumSalt(decimal salt)
        {

            return  (salt * VolumeA202) + (0.12M * VolumeTC007);
        }

  
        
        

    }
}
