using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDataTable
{
    class Program
    {
        static void Main(string[] args)
        {
            MainContext.MainDataContext mDC = new MainContext.MainDataContext();

            MainContext.Test1 test1 = new MainContext.Test1();

            test1 = mDC.Test1s.ToList().FirstOrDefault();

            Console.WriteLine(test1.Name +test1.Age +test1.Address);

            //DataTable dt = new DataTable();
            //DataColumn[] arrayColumn = {
            //    new DataColumn("id", typeof(int)),
            //    new DataColumn("name", typeof(string)),
            //    new DataColumn("age", typeof(int)),
            //    new DataColumn("sex", typeof(string))
            //};
            //dt.Columns.AddRange(arrayColumn);
            //Console.WriteLine("---------------------------新增---------------------------");
            //DataRow dr0 = null;
            //for (int i = 0; i < 5; i++)
            //{
            //    dr0 = dt.NewRow();
            //    dr0[0] = i;
            //    dr0[1] = "xiaoming" + i;
            //    dr0[2] = 20 + i + new Random().Next(1, 10);
            //    dr0[3] = new Random().Next(i, 200) % 3 == 0 ? "男" : "女";

            //    dt.Rows.Add(dr0);
            //}
            //Console.WriteLine("---------------------------筛选之前---------------------------");
            //Print(dt);
            //DataRow[] rowArr = dt.Select("age >25", " age desc");
            //Console.WriteLine("---------------------------筛选之后按年龄排序---------------------------");
            //PrintRow(rowArr);
            
            Console.Read();
        }

        private static void PrintRow(DataRow[] rowArr)
        {
            foreach (DataRow item in rowArr)
            {
                string msg = "";
                try
                {
                    msg = $"id={ item[0]},name={item[1]},age={item[2]},sex={item[3]}";
                }
                catch (Exception e)
                {
                    msg = e.Message;

                }

                Console.WriteLine(msg);
            }
        }
        private static void Print(DataTable dt)
        {
            foreach (DataRow item in dt.Rows)
            {
                string msg = "";
                try
                {
                    msg = $"id={ item[0]},name={item[1]},age={item[2]},sex={item[3]}"; //string interpolation
                }
                catch (Exception e)
                {
                    msg = e.Message;

                }

                Console.WriteLine(msg);
            }
        }
    }
}
