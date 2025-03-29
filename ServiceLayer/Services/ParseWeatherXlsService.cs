using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using ServiceLayer.Abstractions;
using ServiceLayer.Models;

namespace ServiceLayer.Services
{
    public class ParseWeatherXlsService : IParseWeatherXlsService
    {
        private const int _headerRowsCount = 4;
        private const string _dateFormat = "dd.MM.yyyy";
        private const string _timeFormat = "HH:mm";
        public IEnumerable<WeatherRecordDto> ParseXls(Stream stream)
        {
            var workbook = new XSSFWorkbook(stream);
            var sheets = Enumerable.Range(0, workbook.NumberOfSheets)
                .Select(i => workbook.GetSheetAt(i));

            int firstDataRowIndex = _headerRowsCount;
            var rows = sheets.SelectMany(sheet =>
                Enumerable.Range(firstDataRowIndex, sheet.LastRowNum + 1 - firstDataRowIndex)
                    .Select(rowNum => sheet.GetRow(rowNum)));

            var result = rows.Select(row => ParseRow(row));
            var l = result.ToList();
            return result;
        }

        private WeatherRecordDto ParseRow(IRow row)
        {
            return new WeatherRecordDto()
            {
                DateTime = new DateTime(
                    date: DateOnly.ParseExact(row.GetCell(0).StringCellValue, _dateFormat),
                    time: TimeOnly.ParseExact(row.GetCell(1).StringCellValue, _timeFormat),
                    kind: DateTimeKind.Utc
                ),
                Temperature = ReadDecimal(row.GetCell(2)),
                RelativeHumidity = ReadUshort(row.GetCell(3)),
                Td = ReadDecimal(row.GetCell(4)),
                AtmosphericPressure = ReadUshort(row.GetCell(5)),
                WindDirection = ReadString(row.GetCell(6)),
                WindSpeed = ReadUshort(row.GetCell(7)),
                CloudCover = ReadUshort(row.GetCell(8)),
                H = ReadUshort(row.GetCell(9)),
                VV = ReadString(row.GetCell(10)),
                WeatherPhenomena = ReadString(row.GetCell(11))
            };
        }

        private bool CheckForNullOrEmpty(ICell cell)
        {
            return (cell == null) || (cell.CellType == CellType.Blank) ||
                (cell.CellType == CellType.String && String.IsNullOrEmpty(cell.StringCellValue.Replace(" ", "")));
        }
        private ushort? ReadUshort(ICell cell)
        {
            if (CheckForNullOrEmpty(cell)) return null;
            return Convert.ToUInt16(cell.NumericCellValue);
        }

        private decimal? ReadDecimal(ICell cell)
        {
            if (CheckForNullOrEmpty(cell)) return null;
            return Convert.ToDecimal(cell.NumericCellValue);
        }

        private string? ReadString(ICell cell)
        {
            if (CheckForNullOrEmpty(cell)) return null;
            if (cell.CellType == CellType.Numeric) return cell.NumericCellValue.ToString();
            return cell.StringCellValue;
        }
    }
}
