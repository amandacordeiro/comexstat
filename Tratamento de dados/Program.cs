using System;
using System.IO;
using System.Text;
using System.Linq;

namespace Comex
{
    class Program
    {
        static StreamWriter sqlExportacoes2019;
        static StreamWriter sqlExportacoes2020;
        static StreamWriter sqlNCM;
        static StreamWriter sqlPais;
        static StreamWriter sqlMunicipio;
        static StreamReader csvExportacoes2019;
        static StreamReader csvExportacoes2020;
        static StreamReader csvNCM;
        static StreamReader csvPais;
        static StreamReader csvMunicipio;
        static void AbreArquivoExportacoes()
        {
            csvExportacoes2019 = new StreamReader("C:/Users/amand/Desktop/COMEXSTAT/arquivos_csv/EXP_2019_MUN.csv");
            csvExportacoes2020 = new StreamReader("C:/Users/amand/Desktop/COMEXSTAT/arquivos_csv/EXP_2020_MUN.csv");
            sqlExportacoes2019 = new StreamWriter("C:/Users/amand/Desktop/COMEXSTAT/scripts/arq_inExportacao2019.sql");
            sqlExportacoes2020 = new StreamWriter("C:/Users/amand/Desktop/COMEXSTAT/scripts/arq_inExportacao2020.sql");
        }
        static void AbreArquivoNCM()
        {
            csvNCM = new StreamReader("C:/Users/amand/Desktop/COMEXSTAT/arquivos_csv/NCM_SH.csv");
            sqlNCM = new StreamWriter("C:/Users/amand/Desktop/COMEXSTAT/scripts/arq_inNCM.sql");
        }
        static void AbreArquivoPais()
        {
            csvPais = new StreamReader("C:/Users/amand/Desktop/COMEXSTAT/arquivos_csv/PAIS.csv");
            sqlPais = new StreamWriter("C:/Users/amand/Desktop/COMEXSTAT/scripts/arq_inPais.sql");
        }
        static void AbreArquivoMunicipio()
        {
            csvMunicipio = new StreamReader("C:/Users/amand/Desktop/COMEXSTAT/arquivos_csv/UF_MUN.csv");
            sqlMunicipio = new StreamWriter("C:/Users/amand/Desktop/COMEXSTAT/scripts/arq_inMunicipio.sql");
        }
        static void FechaArquivoExportacoes()
        {
            csvExportacoes2019.Close();
            csvExportacoes2020.Close();
            sqlExportacoes2019.Close();
            sqlExportacoes2020.Close();
        }
        static void FechaArquivoNCM()
        {
            csvNCM.Close();
            sqlNCM.Close();
        }
        static void FechaArquivoPais()
        {
            csvPais.Close();
            sqlPais.Close();
        }
        static void FechaArquivoMunicipio()
        {
            csvMunicipio.Close();
            sqlMunicipio.Close();
        }
        static void Main(string[] args)
        {
            String linha;
            String[] valores;

            AbreArquivoExportacoes();

            Console.WriteLine("Lendo valores do arquivo CSV e gravando script SQL...");

            csvExportacoes2019.ReadLine(); // Pula o cabeçalho

            while (!csvExportacoes2019.EndOfStream)
            {
                linha = csvExportacoes2019.ReadLine();

                valores = linha.Split(';');

                sqlExportacoes2019.WriteLine("INSERT INTO exportacoes VALUES (" + valores[0].Replace("\"", "")
                    + "," + valores[1].Replace("\"", "") + "," + valores[2].Replace("\"", "") + "," + valores[3].Replace("\"", "")
                    + "," + valores[4].Replace("\"", "'") + "," + valores[5].Replace("\"", "") + "," + valores[6].Replace("\"", "") + ");");
            }

            csvExportacoes2020.ReadLine();

            while (!csvExportacoes2020.EndOfStream)
            {
                linha = csvExportacoes2020.ReadLine();

                valores = linha.Split(';');

                sqlExportacoes2020.WriteLine("INSERT INTO exportacoes VALUES (" + valores[0].Replace("\"", "")
                    + "," + valores[1].Replace("\"", "") + "," + valores[2].Replace("\"", "") + "," + valores[3].Replace("\"", "")
                    + "," + valores[4].Replace("\"", "'") + "," + valores[5].Replace("\"", "") + "," + valores[6].Replace("\"", "") + ");");
            }


            AbreArquivoNCM();

            Console.WriteLine("Lendo valores do arquivo CSV e gravando script SQL...");

            csvNCM.ReadLine();

            while (!csvNCM.EndOfStream)
            {
                linha = csvNCM.ReadLine();
                linha = linha.Replace("\"", "");

                valores = linha.Split(';');

                sqlNCM.WriteLine("INSERT INTO codncm VALUES (" + valores[4] + ",\"" + valores[5] + "\"" + ");");
            }

            Console.WriteLine("Script SQL gerado com sucesso!");

            FechaArquivoNCM();


            AbreArquivoMunicipio();

            Console.WriteLine("Lendo valores do arquivo CSV e gravando script SQL...");

            csvMunicipio.ReadLine();

            while (!csvMunicipio.EndOfStream)
            {
                linha = csvMunicipio.ReadLine();

                valores = linha.Split(';');

                sqlMunicipio.WriteLine("INSERT INTO codmunicipio VALUES (" + valores[0].Replace("\"", "") + "," + valores[1] + ");");
            }

            Console.WriteLine("Script SQL gerado com sucesso!");

            FechaArquivoMunicipio();


            AbreArquivoPais(); // Abre o arquivo original e o destino final

            Console.WriteLine("Lendo valores do arquivo CSV e gravando script SQL..."); // Imprime a informação

            csvPais.ReadLine(); // Pula a primeira linha

            string armario = "";

            while (!csvPais.EndOfStream) // Enquanto não chega no final do arquivo
            {
                linha = csvPais.ReadLine(); // Armazena as informações das linhas individualmente do arquivo na variavél linha

                int gaveta = linha.Split(';').Length;

                if (gaveta < 6)
                {
                    for (int i = 0; i <= 4; i++)
                    {
                        armario = armario + linha;
                        gaveta = armario.Split(';').Length;
                        if (gaveta == 6)
                        {
                            linha = armario;
                            break;
                        }
                        else
                        {
                            linha = csvPais.ReadLine();
                        }
                    }

                    valores = linha.Split(';'); // Armazena os valores separados por ponto e vírgula da variavél linha na string []valores
                    sqlPais.WriteLine("INSERT INTO codpais VALUES (" + valores[0].Replace("\"", "") + "," + valores[3] + ");");
                }
                else
                {
                    valores = linha.Split(';'); // Armazena os valores separados por ponto e vírgula da variavél linha na string []valores
                    sqlPais.WriteLine("INSERT INTO codpais VALUES (" + valores[0].Replace("\"", "") + "," + valores[3] + ");");
                }
            }

            Console.WriteLine("Script SQL gerado com sucesso!");

            FechaArquivoPais();
        }
    }
}