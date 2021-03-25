using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace Autravel.Extentions
{
    public static class DataTableExtentions
    {

        public static DateTime ToDate(this DataRow input, string col)
        {
            var val = input[col].ToString();
            DateTime.TryParse(val, out DateTime Result);
            return Result;
        }
        public static string FirstOrDefault(this DataTable input, string col)
        {
            string Result = "";
            if (input.Rows.Count>0)
            {
                Result = input.Rows[0][col].ToString();
            }
              return Result;
        }
        public static DataRow FirstOrDefault(this DataTable input)
        {
            return input.Rows[0];
        }
        public static string ToDateString(this DataRow input, string col)
        {
            var val = input[col].ToString();
            var date = input.ToDate(col);
            if (date.Year<2000)
            {
                return string.Empty;
            }
            return date.ToString("dd/MM/yyyy");
        }
        public static string ToIntCurrency(this DataRow input, string col)
        {
            var val = input[col].ToString();
            int.TryParse(val, out int result);

            var ouput = result.ToString("#,####");
            if (string.IsNullOrEmpty(ouput))
            {
                ouput = "0";
            }
            return ouput;
        }  
        public static int ToInt(this DataRow input, string col)
        {
            var val = input[col].ToString();
            int.TryParse(val, out int result);
             
            return result ;
        }  
        public static float ToFloat(this DataRow input, string col)
        {
            var val = input[col].ToString();
            float.TryParse(val, out float result);
             
            return result;
        }
        public static void WriteToExcelFile(this DataTable dataTable, string filePath)
        {
            using (ExcelPackage excelPackage = new ExcelPackage())
            {
                //create a WorkSheet
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Sheet 1");

                //add all the content from the DataTable, starting at cell A1
                worksheet.Cells["A1"].LoadFromDataTable(dataTable, true);
                int colNumber = 1;

                foreach (DataColumn col in dataTable.Columns)
                {
                    if (col.DataType == typeof(DateTime))
                    {
                        worksheet.Column(colNumber).Style.Numberformat.Format = "yyyy-MM-dd hh:mm:ss";
                    }
                    colNumber++;
                }

                byte[] data = excelPackage.GetAsByteArray();

                File.WriteAllBytes(filePath, data);
            }
        }
        public static void WriteToCsvFile(this DataTable dataTable, string filePath)
        {
            StringBuilder fileContent = new StringBuilder();

            foreach (var col in dataTable.Columns)
            {
                fileContent.Append(col.ToString() + ",");
            }

            fileContent.Replace(",", System.Environment.NewLine, fileContent.Length - 1, 1);

            foreach (DataRow dr in dataTable.Rows)
            {
                foreach (var column in dr.ItemArray)
                {
                    fileContent.Append("\"" + column.ToString() + "\",");
                }

                fileContent.Replace(",", System.Environment.NewLine, fileContent.Length - 1, 1);
            }

            System.IO.File.WriteAllText(filePath, fileContent.ToString());
        }

    }
}
