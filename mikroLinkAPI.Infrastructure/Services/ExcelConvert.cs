using mikroLinkAPI.Application.Services;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.ComponentModel.DataAnnotations;

namespace mikroLinkAPI.Infrastructure.Services
{
    public class ExcelConvert : IExcelConvert
    {
        public byte[] ModelToExcel<TModel>(List<TModel> collection)
        {
            try
            {

                Type type = typeof(TModel);
                var properties = type.GetRuntimeProperties();
                ExcelPackage Ep = new ExcelPackage();
                ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("Report");
                char[] alph = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'Y', 'Z' };
                int i = 0;
                foreach (var item in properties)
                {
                    bool attr = item.CustomAttributes.Any(p => p.AttributeType.UnderlyingSystemType.Name == "KeyAttribute");
                    if (!attr && item.PropertyType.IsSealed)
                    {
                        string DisplayName = GetPropertyDisplayName<TModel>(item);
                        Sheet.Cells[string.Format("{0}1", alph[i])].Value = DisplayName;
                        i++;
                    }
                }
                i -= 1;
                Sheet.Cells["A1:" + string.Format("{0}1", alph[i]) + ""].Style.Fill.PatternType = ExcelFillStyle.Solid;
                Sheet.Row(1).Height = 30;
                Sheet.Cells["A1:" + string.Format("{0}1", alph[i]) + ""].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                Sheet.Cells["A1:" + string.Format("{0}1", alph[i]) + ""].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                Sheet.Cells["A1:" + string.Format("{0}1", alph[i]) + ""].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#DDD9C4"));
                int row = 2;
                foreach (var field in collection)
                {
                    int count = 0;
                    foreach (var item in properties)
                    {
                        bool attr = item.CustomAttributes.Any(p => p.AttributeType.UnderlyingSystemType.Name == "NotMappedAttribute");
                        bool notaccept = item.CustomAttributes.Any(p => p.AttributeType.UnderlyingSystemType.Name == "KeyAttribute");
                        if (!notaccept && item.PropertyType.IsSealed)
                        {

                            PropertyInfo prop = type.GetProperty(item.Name);

                            object list = prop?.GetValue(field);
                            string c = alph[count].ToString();
                            if (prop?.PropertyType == typeof(DateTime))
                            {
                                Sheet.Cells[string.Format("{1}{0}", row, c)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                            }
                            Sheet.Cells[string.Format("{1}{0}", row, c)].Value = list;

                            count++;
                        }
                    }
                    row++;
                }
                Sheet.Cells["A1:" + string.Format("{1}{0}", row, alph[i]) + ""].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                Sheet.Cells["A1:" + string.Format("{1}{0}", row, alph[i]) + ""].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                Sheet.Cells["A1:" + string.Format("{1}{0}", row, alph[i]) + ""].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                Sheet.Cells["A1:" + string.Format("{1}{0}", row, alph[i]) + ""].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                Sheet.Cells["A1:" + string.Format("{1}{0}", row, alph[i]) + ""].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                Sheet.Cells["A1:" + string.Format("{1}{0}", row, alph[i]) + ""].AutoFitColumns();
                Sheet.Cells["A1:" + string.Format("{1}{0}", row, alph[i]) + ""].Style.Font.Name = "Calibri";
                Sheet.Cells["A1:" + string.Format("{1}{0}", row, alph[i]) + ""].Style.Font.Size = 9;
                Sheet.Cells["A1:" + string.Format("{0}1", alph[i]) + ""].Style.Font.Bold = true;
                Sheet.Cells["A1:" + string.Format("{1}{0}", row, alph[i]) + ""].AutoFitColumns();
                //System.IO.File.WriteAllBytes("server.xlsx", Ep.GetAsByteArray());

                return Ep.GetAsByteArray();

            }
            catch (Exception ex)
            {

                var x = ex;
                return null;
            }
        }
        private string GetPropertyDisplayName<T>(MemberInfo memberInfo)
        {
            //var memberInfo = GetPropertyInformation(propertyExpression.Body);
            if (memberInfo == null)
            {
                throw new ArgumentException(
                    "No property reference expression was found.",
                    "propertyExpression");
            }

            var attr = memberInfo.GetCustomAttribute<DisplayAttribute>(false);
            if (attr == null)
            {
                return memberInfo.Name;
            }

            return attr.Name;
        }
    }
}
