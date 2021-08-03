using OutFile.Entities;
using System;
using System.Globalization;
using System.IO;

namespace OutFile
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter file full path: ");
            //string sourceFilePath = Console.ReadLine();
            string sourceFilePath = @"C:\Temp\source.txt";

            try
            {
                string[] lines = File.ReadAllLines(sourceFilePath);

                string sourceFolderPath = Path.GetDirectoryName(sourceFilePath);//pegando nome do diretorio: Temp
                string targetFolderPath = sourceFolderPath + @"\out"; // pasta de saida
                string targetFilePath = targetFolderPath + @"\summary.csv"; // arquivo que vai ser criado

                Directory.CreateDirectory(targetFolderPath);

                using (StreamWriter sw = File.AppendText(targetFilePath))
                {
                    foreach (string line in lines)
                    {

                        string[] fields = line.Split(','); //split para separar pelo ponto
                        string name = fields[0];
                        double price = double.Parse(fields[1], CultureInfo.InvariantCulture);
                        int quantity = int.Parse(fields[2]);

                        Product prod = new Product(name, price, quantity);

                        //escrevendo no novo arquivo uma linha
                        sw.WriteLine(prod.Name + ", " + prod.Total().ToString("F2", CultureInfo.InvariantCulture));
                    }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("An error occurred");
                Console.WriteLine(e.Message);
            }
        }
    }
}
