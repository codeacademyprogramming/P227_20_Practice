using System;
using System.Collections.Generic;
using System.Threading;

namespace Homework
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("az-AZ");
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            string countStr;
            int count;
            do
            {
                Console.WriteLine("İmtahanların sayını daxil et:");
                countStr = Console.ReadLine();

            } while (!int.TryParse(countStr,out count) || count<0);

            List<Exam> exams = new List<Exam>(count);

            for (int i = 0; i < count; i++)
            {

                var exam = GetExam(i + 1);
                exams.Add(exam);
               
            }

            Console.WriteLine("\n=======Ugurlu imtahanlarin siyahisi===========\n");
            foreach (var item in exams.FindAll(x=>x.Point>50))
            {
                Console.WriteLine($"Telebe: {item.Student} - Fenn: {item.Subject} - Qiymet: {item.Point} - Muddet: {(item.EndDate-item.StartDate).Hours}:{(item.EndDate - item.StartDate).Minutes}");
            }

            Console.WriteLine("\n=======Son heftedeki imtahanlarin siyahisi===========\n");
            foreach (var item in exams.FindAll(x => x.StartDate<DateTime.Now && x.StartDate>DateTime.Now.AddDays(-7)))
            {
                Console.WriteLine($"Telebe: {item.Student} - Fenn: {item.Subject} - Qiymet: {item.Point} - Muddet: {(item.EndDate - item.StartDate).Hours}:{(item.EndDate - item.StartDate).Minutes}");
            }

            Console.WriteLine("\n=======! saatdan uzun ceken imtahanlarin siyahisi===========\n");
         
            foreach (var item in exams.FindAll(x => (x.EndDate-x.StartDate).TotalMinutes>60))
            {
                Console.WriteLine($"Telebe: {item.Student} - Fenn: {item.Subject} - Qiymet: {item.Point} - Muddet: {(item.EndDate - item.StartDate).Hours}:{(item.EndDate - item.StartDate).Minutes}");
            }
        }

        static Exam GetExam(int order)
        {
            Console.WriteLine($"{order}. imtahanin telebe adini daxil edin:");
            string student = Console.ReadLine();

            Console.WriteLine($"{order}. imtahanin fenn adini daxil edin:");
            string subject = Console.ReadLine();


            string pointStr;
            byte point;
            do
            {
                Console.WriteLine($"{order}. imtahanin qiymetini daxil edin:");
                pointStr = Console.ReadLine();

            } while (!byte.TryParse(pointStr, out point) || point > 100);

            string startDateStr;
            DateTime startDate;

            do
            {
                Console.WriteLine("Baslama tarixini qeyd edin:");
                startDateStr  = Console.ReadLine();

            } while (!DateTime.TryParse(startDateStr,out startDate));

            string endDateStr;
            DateTime endDate;
            do
            {
                Console.WriteLine("Bitme tarixini qeyd edin:");
                endDateStr = Console.ReadLine();

            } while (!DateTime.TryParse(endDateStr,out endDate) || endDate<=startDate || (endDate-startDate).TotalMinutes<45);

            Exam exam = new Exam
            {
                Point = point,
                Student = student,
                Subject = subject,
                StartDate = startDate,
                EndDate = endDate
            };

            return exam;
        }
    }
}
