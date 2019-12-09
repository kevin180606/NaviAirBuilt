using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaviAirBuilt.Utilities
{
    class ExcelHelper
    {
        /// <summary>  
        /// 将excel导入到datatable  
        /// </summary>  
        /// <param name="filePath">excel路径</param>  
        /// <param name="isColumnName">第一行是否是列名</param>  
        /// <returns>返回datatable</returns>  
        public static DataTable ExcelToDataTable(bool isColumnName, string filePath = "")
        {
            DataTable dataTable = null;
            FileStream fs = null;
            DataColumn column = null;
            DataRow dataRow = null;
            if (filePath == "")
            {
                System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog();
                ofd.Filter = "Excel 2007-13文件(*.xlsx)|*.xlsx|Excel 2003文件(*.xls)|*.xls";
                if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    filePath = ofd.FileName;
                }
                else
                {
                    return null;
                }
            }
            IWorkbook workbook = null;
            ISheet sheet = null;
            IRow row = null;
            int startRow = 0;
            try
            {
                using (fs = File.OpenRead(filePath))
                {
                    // 2007版本  
                    if (filePath.IndexOf(".xlsx") > 0)
                        workbook = new XSSFWorkbook(fs);
                    // 2003版本  
                    else if (filePath.IndexOf(".xls") > 0)
                        workbook = new HSSFWorkbook(fs);

                    if (workbook != null)
                    {
                        int sheetnum = workbook.NumberOfSheets;
                        for (int s = 0; s < sheetnum; s++)
                        {
                            sheet = workbook.GetSheetAt(s);
                            if (s == 0)
                            {
                                dataTable = new DataTable();
                                if (sheet != null)
                                {
                                    int rowCount = sheet.LastRowNum;//总行数  
                                    if (rowCount > 0)
                                    {
                                        IRow firstRow = sheet.GetRow(0);//第一行  
                                        IRow header = sheet.GetRow(sheet.FirstRowNum);
                                        int cellCount = firstRow.LastCellNum;//列数  
                                        List<int> columns = new List<int>();
                                        //构建datatable的列  
                                        if (isColumnName)
                                        {
                                            startRow = 1;//如果第一行是列名，则从第二行开始读取  
                                            for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                                            {
                                                object obj = GetValueType(header.GetCell(i));
                                                if (obj == null || obj.ToString() == string.Empty)
                                                {
                                                    dataTable.Columns.Add(new DataColumn("Columns" + i.ToString()));
                                                }
                                                else
                                                {
                                                    dataTable.Columns.Add(new DataColumn(obj.ToString()));
                                                }
                                                columns.Add(i);
                                            }
                                        }
                                        else
                                        {
                                            for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                                            {
                                                column = new DataColumn("column" + (i + 1));
                                                dataTable.Columns.Add(column);
                                            }
                                        }

                                        //填充行  
                                        for (int i = startRow; i <= rowCount; ++i)
                                        {
                                            row = sheet.GetRow(i);
                                            if (row == null) continue;

                                            dataRow = dataTable.NewRow();
                                            bool hasValue = false;
                                            foreach (int j in columns)
                                            {
                                                dataRow[j] = GetValueType(sheet.GetRow(i).GetCell(j));
                                                if (dataRow[j] != null && dataRow[j].ToString() != string.Empty)
                                                {
                                                    hasValue = true;
                                                }
                                            }
                                            if (hasValue)
                                            {
                                                dataTable.Rows.Add(dataRow);
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                isColumnName = false;
                                if (sheet != null)
                                {
                                    int rowCount = sheet.LastRowNum;//总行数  
                                    if (rowCount > 0)
                                    {
                                        IRow firstRow = sheet.GetRow(0);//第一行  
                                        IRow header = sheet.GetRow(sheet.FirstRowNum);
                                        int cellCount = firstRow.LastCellNum;//列数

                                        //填充行  
                                        for (int i = 1; i <= rowCount; ++i)
                                        {
                                            row = sheet.GetRow(i);
                                            if (row == null) continue;

                                            dataRow = dataTable.NewRow();
                                            bool hasValue = false;
                                            for (int j = 0; j < dataTable.Columns.Count; j++)
                                            {
                                                dataRow[j] = GetValueType(sheet.GetRow(i).GetCell(j));
                                                if (dataRow[j] != null && dataRow[j].ToString() != string.Empty)
                                                {
                                                    hasValue = true;
                                                }
                                            }
                                            if (hasValue)
                                            {
                                                dataTable.Rows.Add(dataRow);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                return dataTable;
            }
            catch (Exception)
            {
                if (fs != null)
                {
                    fs.Close();
                }
                return null;
            }
        }

        /// <summary>
        /// 获取单元格类型
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        private static object GetValueType(ICell cell)


        {
            if (cell == null)
                return null;
            switch (cell.CellType)
            {
                case CellType.Blank: //BLANK:  
                    return null;
                case CellType.Boolean: //BOOLEAN:  
                    return cell.BooleanCellValue;
                case CellType.Numeric: //NUMERIC: 
                    if (DateUtil.IsCellDateFormatted(cell))
                    {
                        return cell.DateCellValue;
                    }
                    else
                    {
                        return cell.NumericCellValue;
                    }
                    short format = cell.CellStyle.DataFormat;
                    if (format != 0) { return cell.DateCellValue; } else { return cell.NumericCellValue; }
                case CellType.String: //STRING:  
                    return cell.StringCellValue;
                case CellType.Error: //ERROR:  
                    return cell.ErrorCellValue;
                case CellType.Formula: //FORMULA:  
                default:
                    return "=" + cell.CellFormula;
            }
        }

        /// <summary>
        /// Datable导出成Excel
        /// </summary>
        /// <param name="dt">需要导出的datatable</param>
        /// <param name="file">导出路径(包括文件名与扩展名)</param>
        public static void DataTableToExcel(DataTable dt, string file = "", bool isShowResult = true)
        {
            IWorkbook workbook;
            if (file == "")
            {
                System.Windows.Forms.SaveFileDialog sfd = new System.Windows.Forms.SaveFileDialog();
                sfd.Filter = "Excel 2007-13文件(*.xlsx)|*.xlsx|Excel 2003文件(*.xls)|*.xls";
                sfd.DefaultExt = "xlsx";
                sfd.AddExtension = true;
                if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    file = sfd.FileName;
                }
                else
                {
                    return;
                }
            }

            string fileExt = Path.GetExtension(file).ToLower();
            if (fileExt == ".xlsx")
            {
                workbook = new XSSFWorkbook();
            }
            else if (fileExt == ".xls")
            {
                workbook = new HSSFWorkbook();
            }
            else
            {
                workbook = null;
            }
            if (workbook == null)
            {
                return;
            }
            ISheet sheet = string.IsNullOrEmpty(dt.TableName) ? workbook.CreateSheet("Sheet1") : workbook.CreateSheet(dt.TableName);

            //表头  
            IRow row = sheet.CreateRow(0);
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                ICell cell = row.CreateCell(i);
                cell.SetCellValue(dt.Columns[i].ColumnName);
            }

            //数据  
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                IRow row1 = sheet.CreateRow(i + 1);
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    ICell cell = row1.CreateCell(j);
                    cell.SetCellValue(dt.Rows[i][j].ToString());
                }
            }

            //转为字节数组  
            MemoryStream stream = new MemoryStream();
            workbook.Write(stream);
            var buf = stream.ToArray();

            //保存为Excel文件  
            using (FileStream fs = new FileStream(file, FileMode.Create, FileAccess.Write))
            {
                fs.Write(buf, 0, buf.Length);
                fs.Flush();
                if (isShowResult)
                {
                    System.Windows.MessageBox.Show("保存成功!");
                }
            }
        }
    }

}
