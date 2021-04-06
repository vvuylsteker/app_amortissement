using System;
using ConsoleTables;
namespace Application_amortissement
{
    public class Amortisize
    {
        private decimal base_amortisize {get; set;}
        private decimal residual_value {get; set;}
        private decimal tx_linear {get; set;}
        private decimal annuities {get; set;}
        private decimal volume_annuities {get; set;}
        private decimal compatable_value {get; set;}
        private DateTime start{get; set;}
        private int years_duration {get; set;}
        private decimal totaldays {get; set;}
        private decimal daysfirstyear {get; set;}
        private int days_in_year = 365;
        public DateTime getInputDate(){
            try{
                Console.Write("Saisir l'année : ");
                int y = int.Parse(Console.ReadLine());
                Console.Write("Saisir le mois : ");
                int m = int.Parse(Console.ReadLine());
                Console.Write("Saisir le jour : ");
                int d = int.Parse(Console.ReadLine());
                return new DateTime(y,m,d);
            }catch(Exception e){
                Design.WriterColor(e.ToString(), ConsoleColor.Red);
                return new DateTime(0,0,0);
            }
        }
        public decimal rounding(decimal d){
            return Math.Round(d,1,MidpointRounding.ToEven);
        }
        public void Amortisize_linear(){
            Console.Clear();
            Design.printTag();
            try{
                Design.WriterColor("Saisir date de commencement:", ConsoleColor.White);
                start = getInputDate();
                Design.WriterColor("Saisir la durée d'amortissement (input type int) :", ConsoleColor.Green);
                years_duration = int.Parse(Console.ReadLine());
                Design.WriterColor("Saisir la base d'amortissement (input type decimal) :", ConsoleColor.Green);
                base_amortisize = decimal.Parse(Console.ReadLine());
                Design.WriterColor("Renseigner la valeur résiduelle (input type decimal) :", ConsoleColor.Red);
                residual_value = decimal.Parse(Console.ReadLine());
                
                var table = new ConsoleTable("Années", "Base d’amortissement ", "Annuités","Cumul des annuités", "Valeur comptable");

                DateTime endyear = new DateTime(start.Year, 12, 31);
                daysfirstyear = (endyear - start).Days;
                totaldays = daysfirstyear + years_duration * days_in_year;
                tx_linear = 1/(decimal)(totaldays/days_in_year);

                int i = 0;
                Design.WriterColor("Start :" + start.ToString() +" Duration :"+ years_duration+" years  Base amortisize :" + base_amortisize +" Residual value :"+ residual_value +" Tx linear :"+ tx_linear, ConsoleColor.DarkGreen);
                while(totaldays/days_in_year - i >0){
                    int year = start.Year + i;
                    annuities = (base_amortisize-residual_value) * tx_linear;
                    if(i==0) annuities = annuities*(daysfirstyear/days_in_year);
                    volume_annuities += annuities;
                    compatable_value = base_amortisize - volume_annuities;
                    table.AddRow(year, base_amortisize, rounding(annuities), rounding(volume_annuities), rounding(compatable_value));
                    i++;
                }
                table.Write(Format.Alternative);
            }
            catch (Exception e){
                Design.WriterColor(e.ToString(), ConsoleColor.Red);
            }
            Design.WriterColor("End of the Amortisize linear", ConsoleColor.Red);
        }
    }
}