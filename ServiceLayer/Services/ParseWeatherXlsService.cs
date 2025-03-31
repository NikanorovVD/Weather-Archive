using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using ServiceLayer.Abstractions;
using ServiceLayer.Models;
using ServiceLayer.Models.Errors;


namespace ServiceLayer.Services
{
    public class ParseWeatherXlsService : IParseWeatherXlsService
    {
        private const int _headerRowsCount = 4;
        private const string _dateFormat = "dd.MM.yyyy";
        private const string _timeFormat = "HH:mm";

        private const int _dateRow = 0;
        private const int _timeRow = 1;
        private const int _temperatureRow = 2;
        private const int _humidityRow = 3;
        private const int _tdRow = 4;
        private const int _pressureRow = 5;
        private const int _windDirectionRow = 6;
        private const int _windSpeedRow = 7;
        private const int _cloudRow = 8;
        private const int _hRow = 9;
        private const int _vvRow = 10;
        private const int _phenomenaRow = 11;

        public IEnumerable<WeatherRecordDto> ParseXls(Stream stream, string filename, ExcelFileType fileType)
        {
            try
            {
                IWorkbook workbook;
                if(fileType == ExcelFileType.XLS)
                {
                    workbook = new HSSFWorkbook(stream);
                }
                else if (fileType == ExcelFileType.XLSX)
                {
                    workbook = new XSSFWorkbook(stream);
                }
                else
                {
                    throw new NotImplementedException();
                }

                var sheets = Enumerable.Range(0, workbook.NumberOfSheets)
                    .Select(i => workbook.GetSheetAt(i));

                int firstDataRowIndex = _headerRowsCount;
                var rows = sheets.SelectMany(sheet =>
                    Enumerable.Range(firstDataRowIndex, sheet.LastRowNum + 1 - firstDataRowIndex)
                        .Select(rowNum => sheet.GetRow(rowNum)));

                return rows.Select(row => ParseRow(row)).ToList();
            }
            catch (Exception)
            {
                var error = new ValidationError(new() { { $"File {filename}", ["Invalid file format"] } });
                throw new ValidationException(error);
            }
        }

        public IEnumerable<WeatherRecordDto> ParseXls(Stream stream, string fileName)
        {
            string file_extension = Path.GetExtension(fileName);
            ExcelFileType fileType = file_extension switch
            {
                ".xls" => ExcelFileType.XLS,
                ".xlsx" => ExcelFileType.XLSX,
                _ => throw new ArgumentException("Invalid file extension", nameof(fileName))
            };
            return ParseXls(stream, fileName, fileType);
        }

        private WeatherRecordDto ParseRow(IRow row)
        {
            return new WeatherRecordDto()
            {
                DateTime = new DateTime(
                    date: DateOnly.ParseExact(row.GetCell(_dateRow).StringCellValue, _dateFormat),
                    time: TimeOnly.ParseExact(row.GetCell(_timeRow).StringCellValue, _timeFormat),
                    kind: DateTimeKind.Utc
                ),
                Temperature = ReadDecimal(row.GetCell(_temperatureRow)),
                RelativeHumidity = ReadUshort(row.GetCell(_humidityRow)),
                Td = ReadDecimal(row.GetCell(_tdRow)),
                AtmosphericPressure = ReadUshort(row.GetCell(_pressureRow)),
                WindDirection = ReadString(row.GetCell(_windDirectionRow)),
                WindSpeed = ReadUshort(row.GetCell(_windSpeedRow)),
                CloudCover = ReadUshort(row.GetCell(_cloudRow)),
                H = ReadUshort(row.GetCell(_hRow)),
                VV = ReadString(row.GetCell(_vvRow)),
                WeatherPhenomena = ReadString(row.GetCell(_phenomenaRow))
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
