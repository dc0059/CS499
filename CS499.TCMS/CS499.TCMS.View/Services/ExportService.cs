using ClosedXML.Excel;
using CS499.TCMS.View.Resources;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace CS499.TCMS.View.Services
{
    /// <summary>
    /// This class will export data to excel
    /// </summary>
    public static class ExportService
    {

        #region Methods

        /// <summary>
        /// Export DataTable to Excel
        /// </summary>
        /// <param name="filePath">file path with filename</param>
        /// <param name="data">DataTable to export</param>
        /// <param name="totalFooter">flag indicating to add a total row</param>
        public static void Export(string filePath, DataTable data, bool totalFooter)
        {

            try
            {

                // create workbook
                var workbook = new XLWorkbook();

                // create worksheet
                var worksheet = workbook.Worksheets.Add("Report");

                // Create columns and rows
                CreateColumnsAndRows(data, worksheet);

                // format worksheet
                FormatWorkSheet(totalFooter, worksheet);

                // save file
                workbook.SaveAs(filePath);

            }
            catch (Exception ex)
            {

                throw new UnauthorizedAccessException(Messages.ReportExportFailedError, ex);

            }

        }

        /// <summary>
        /// Export list of type T to Excel
        /// </summary>
        /// <param name="filePath">file path with filename</param>
        /// <param name="data">list to export</param>
        /// <param name="totalFooter">flag indicating to add a total row</param>
        public static void Export<T>(string filePath, List<T> data, bool totalFooter) where T : class
        {

            try
            {

                // create workbook
                var workbook = new XLWorkbook();

                // create worksheet
                var worksheet = workbook.Worksheets.Add("Report");

                // Create columns and rows
                CreateColumnsAndRows(data, worksheet);

                // format worksheet
                FormatWorkSheet(totalFooter, worksheet);

                // save file
                workbook.SaveAs(filePath);

            }
            catch (Exception ex)
            {

                throw new UnauthorizedAccessException(Messages.ReportExportFailedError, ex);

            }

        }

        /// <summary>
        /// Create columns and rows for worksheet based on data table
        /// </summary>
        /// <param name="data">data table</param>
        /// <param name="worksheet">worksheet</param>
        private static void CreateColumnsAndRows(DataTable data, IXLWorksheet worksheet)
        {

            // create excel columns
            for (int i = 0; i < data.Columns.Count; i++)
            {

                worksheet.Cell(1, i + 1).Value = data.Columns[i].ColumnName;

            }

            // create excel rows
            for (int col = 0; col < data.Columns.Count; col++)
            {

                for (int row = 0; row < data.Rows.Count; row++)
                {

                    // get value
                    object value = data.Rows[row][col];

                    // set value
                    worksheet.Cell(row + 2, col + 1).SetValue<string>(value.ToString());

                    // format cell based on the type of the object
                    if (value != null)
                    {
                        worksheet.Cell(row + 2, col + 1).DataType = value.GetNumberFormat();
                    }

                }

            }

        }

        /// <summary>
        /// Create columns and rows for worksheet based on list of type T
        /// </summary>
        /// <typeparam name="T">type</typeparam>
        /// <param name="data">data table</param>
        /// <param name="worksheet">worksheet</param>
        private static void CreateColumnsAndRows<T>(List<T> data, IXLWorksheet worksheet) where T : class
        {

            // get the type of the class
            Type type = typeof(T);

            // get list of properties with the description attribute
            List<PropertyInfo> properties = type.GetProperties().Where(
                p => Attribute.IsDefined(p, typeof(DescriptionNameAttribute))).ToList();

            // sort properties list
            properties.Sort((x, y) =>
            {
                DescriptionNameAttribute xAttribute = (x.GetCustomAttributes(false)[0] as DescriptionNameAttribute);
                DescriptionNameAttribute yAttribute = (y.GetCustomAttributes(false)[0] as DescriptionNameAttribute);
                return xAttribute.SortOrder.CompareTo(yAttribute.SortOrder);
            });

            // create excel columns
            for (int i = 0; i < properties.Count; i++)
            {

                object[] attributes = properties[i].GetCustomAttributes(false);

                worksheet.Cell(1, i + 1).Value = (attributes[0] as DescriptionNameAttribute).Description;

            }

            // create excel rows
            for (int col = 0; col < properties.Count; col++)
            {

                for (int row = 0; row < data.Count; row++)
                {

                    // get value of property
                    object value = data[row].GetType().GetProperty(properties[col].Name).GetValue(data[row], null);

                    // set value
                    worksheet.Cell(row + 2, col + 1).SetValue<string>(value.ToString());

                    // format cell based on the type of the object
                    if (value != null)
                    {
                        worksheet.Cell(row + 2, col + 1).DataType = value.GetNumberFormat();
                    }

                }

            }

        }

        /// <summary>
        /// Get the cell type based on the System.Type
        /// </summary>
        /// <param name="value">object</param>
        /// <returns>cell type</returns>
        private static XLCellValues GetNumberFormat(this object value)
        {

            XLCellValues format = XLCellValues.Text;

            // get the string value of the type
            string type = value.GetType().ToString();

            switch (type)
            {
                case "System.Boolean": format = XLCellValues.Boolean; break;
                case "System.Byte": format = XLCellValues.Text; break;
                case "System.SByte": format = XLCellValues.Text; break;
                case "System.Char": format = XLCellValues.Text; break;
                case "System.Decimal": format = XLCellValues.Number; break;
                case "System.Double": format = XLCellValues.Number; break;
                case "System.Single": format = XLCellValues.Number; break;
                case "System.Int32": format = XLCellValues.Number; break;
                case "System.UInt32": format = XLCellValues.Number; break;
                case "System.Int64": format = XLCellValues.Number; break;
                case "System.UInt64": format = XLCellValues.Number; break;
                case "System.Object": format = XLCellValues.Text; break;
                case "System.Int16": format = XLCellValues.Number; break;
                case "System.UInt16": format = XLCellValues.Number; break;
                case "System.String": format = XLCellValues.Text; break;
                case "System.DateTime": format = XLCellValues.DateTime; break;
                case "System.TimeSpan": format = XLCellValues.TimeSpan; break;
                default: format = XLCellValues.Text; break;
            }

            return format;
        }

        /// <summary>
        /// Format the worksheet
        /// </summary>
        /// <param name="totalFooter">flag indicating to add a total row</param>
        /// <param name="worksheet"></param>
        private static void FormatWorkSheet(bool totalFooter, IXLWorksheet worksheet)
        {
            // get accent color
            var color = XLColor.FromColor(CoreAssembly.GetAccentColor());

            // format header
            FormatHeader(worksheet, color);

            // format footer
            if (totalFooter)
            {
                FormatFooter(worksheet, color);
            }

            // format page
            FormatPage(worksheet);
        }

        /// <summary>
        /// Format worksheet page
        /// </summary>
        /// <param name="worksheet">Worksheet to format</param>
        private static void FormatPage(IXLWorksheet worksheet)
        {

            // freeze header row
            worksheet.SheetView.FreezeRows(1);

            // remove grid lines
            worksheet.ShowGridLines = false;

            // set page setup
            worksheet.PageSetup.PageOrientation = XLPageOrientation.Landscape;
            worksheet.PageSetup.PagesWide = 1;

            // set used range styles
            var usedRange = worksheet.RangeUsed();
            usedRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            usedRange.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
            usedRange.Style.Border.RightBorder = XLBorderStyleValues.Thin;
            usedRange.Style.Border.TopBorder = XLBorderStyleValues.Thin;
            usedRange.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            usedRange.Style.Border.InsideBorder = XLBorderStyleValues.Thin;

            // auto fit columns
            worksheet.Columns().AdjustToContents();

        }

        /// <summary>
        /// Format header row
        /// </summary>
        /// <param name="worksheet">work sheet</param>
        /// <param name="color">background color</param>
        private static void FormatHeader(IXLWorksheet worksheet, XLColor color)
        {
            // set title row style
            var header = worksheet.Range(1, 1, 1, worksheet.RangeUsed().ColumnCount());
            header.Style.Font.Bold = true;
            header.Style.Alignment.SetTextRotation(45);
            header.Style.Alignment.SetVertical(XLAlignmentVerticalValues.Bottom);
            header.Style.Fill.BackgroundColor = color;

        }

        /// <summary>
        /// Format footer row
        /// </summary>
        /// <param name="worksheet">work sheet</param>
        /// <param name="color">background color</param>
        private static void FormatFooter(IXLWorksheet worksheet, XLColor color)
        {

            // set total row
            int rowTotal = worksheet.RangeUsed().RowCount() + 1;
            int cols = worksheet.RangeUsed().ColumnCount();
            for (int col = 0; col < cols; col++)
            {

                if (col == 0)
                {
                    worksheet.Cell(rowTotal, col + 1).Value = "Total:";
                }
                else
                {
                    worksheet.Cell(rowTotal, col + 1).FormulaA1 = string.Format("=IF(SUM({0}{1}:{0}{2})=0, COUNTA({0}{1}:{0}{2}), SUM({0}{1}:{0}{2}))",
                        worksheet.Column(col + 1).ColumnLetter(),
                        2,
                        rowTotal - 1);
                }
            }

            // set total row style
            var footer = worksheet.Range(rowTotal, 1, rowTotal, cols);
            footer.Style.Font.Bold = true;
            footer.Style.Fill.BackgroundColor = color;

        }

        #endregion

    }
}
