using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace Realize_Silent_Program
{

    class Program
    {
        static void Main(string[] args)
        {
            //configからprocessキー値をprocessListに格納する
            string[] processList = ConfigurationManager.AppSettings["process"].Split(',');


            //processキー値を基準に現在実行中のプロセスで探す
            //WorkingSet64 : 指定プロセスのメモリ使用量(Byte単位)
            try
            {

                foreach (string p in processList)
                {
                    Process[] thisProcess = Process.GetProcessesByName(p); // ???
                    foreach (Process thisProc in thisProcess)
                    {
                        //logを作成
                        string msg = thisProc.ProcessName + "," + thisProc.WorkingSet64;
                        Log(msg);

                      
                        //Console.WriteLine("NAME : " + thisProc.ProcessName);
                        //Console.WriteLine("WorkingSet64 : " + thisProc.WorkingSet64);
                    }
                }
                string FilePath = Directory.GetCurrentDirectory() + @"\Logs\" + DateTime.Today.ToString("yyyyMMdd") + ".log";
               // ListCompare(FilePath);
                Text1(FilePath);

            }
            catch { }


           
       
        }

        //今日の日付を設定する
        public static string GetDateTime(){
            DateTime NowDate = DateTime.Now;
            return NowDate.ToString("yyyy-MM-dd HH:mm:ss");
        }


        //Logファイル作成
        public static void Log(string msg)
        {
            //GetCurentDirectory() : 現在実行中のアプリがあるディレクトリのパス
            string FilePath = Directory.GetCurrentDirectory() + @"\Logs\" + DateTime.Today.ToString("yyyyMMdd")+".log";
            string DirPath = Directory.GetCurrentDirectory() + @"\Logs";
            string temp;

            DirectoryInfo di = new DirectoryInfo(DirPath);
            FileInfo fi = new FileInfo(FilePath);

            try
            {
                //指定パスにLogフォルダがなければ作成
                if (di.Exists != true) Directory.CreateDirectory(DirPath);

                //ファイルがなければ
                if (fi.Exists != true)
                {
                    //Logファイルを作成し、記録
                    using (StreamWriter sw = new StreamWriter(FilePath))
                    {
                        temp = string.Format("[{0}] {1}", GetDateTime(), msg);
                        sw.WriteLine(temp);
                        sw.Close();
                       
                    }
                   
                }
                //ファイルがあれば
                else
                {
                    //Logファイルを開き、記録
                    using (StreamWriter sw = File.AppendText(FilePath))
                    {
                        temp = string.Format("[{0}] {1}", GetDateTime(), msg);
                        sw.WriteLine(temp);
                        sw.Close();
                    }
                }
                
            }
            catch{　}
        }

        //public static void ListCompare(string FilePath)
        //{
        //    StreamReader SR = new StreamReader(FilePath);

        //    string line;
        //    while((line = SR.ReadLine()) != null)
        //    {

        //    }
        //    Console.WriteLine(line);
        //    SR.Close();
        //}


        public static void Text1(string FilePath)
        {
            string[] str_OutputValue = System.IO.File.ReadAllLines(FilePath);
            if (str_OutputValue.Length > 0)
            {
                for (int i = 0; i < str_OutputValue.Length; i++)
                {
                    Console.WriteLine("TextFile" + (i + 1).ToString() + "番目行");
                    Console.WriteLine(str_OutputValue);
                }
            }

        }


    }
}
