using AutoMapper;
using Microsoft.AspNetCore.StaticFiles;
using Newtonsoft.Json.Linq;
using OfficeOpenXml;
using System.Net;
using Tapfin.Api.Helpers.Result;
using Tapfin.Api.Models.DTO;
using Tapfin.Api.Models.Entities;
using Tapfin.Api.Persistence.Contracts;
using Tapfin.Api.Services.Contracts;
using Tapfin.Api.Helpers;
using System.Data;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Tapfin.Api.Services.ServiceLogic
{
    public class WorkerPayrollServices : IWorkerPayrollServices
    {
        Result _result = new Result();
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IAccountsServices _accountsServices;
        User _currentUser;

        public WorkerPayrollServices(IUnitOfWork uow, IMapper mapper, IAccountsServices accountsServices)
        {
            _uow = uow;
            _mapper = mapper;
            _accountsServices = accountsServices;

            _currentUser = _accountsServices.GetLoggedInUser();
        }


        public async Task<dynamic> getWorkerListByClientIDToDownloadFormat(GetPayrollWorkerListRequest request)
        {
            string fullFilePath = string.Empty;
            string contentType = string.Empty;
            var countryId = _currentUser.CountryId;

            /*----------- Get Worker List By ClientID -----------*/
            var workerList = await _uow.workerRepository.getAllByClientId(request.ClientId, countryId);


            /*----------- Create Payroll File -----------*/
            CreateWokerPayrollFormatDownloadableFile(workerList, request, out fullFilePath);


            /*----------- Generate Payroll File as Base64 -----------*/
            var fileBase64String = generateFileBinary(fullFilePath, out contentType);


            /*-------- Delete Generated File ----------*/
            deleteFile(fullFilePath);

            /*-------- Return as Response --------*/
            JObject payrollFileObj = new JObject();
            payrollFileObj["contentType"] = contentType;
            payrollFileObj["src"] = fileBase64String;


            return _result.AddReturnData(HttpStatusCode.OK, payrollFileObj, message: "All details has been fetched successfully.");
        }

        public async Task<dynamic> UploadWorkerPayrollDetails(UploadPayrollWorkerDetailsRequest request)
        {
            List<UploadPayrollWorkerExcelData> errorLogList = new List<UploadPayrollWorkerExcelData>();


            /*------ Crate Datatable to store Error Log ------*/
            DataTable dtErrorLog = new DataTable();
            dtErrorLog.Columns.Add(new DataColumn("WorkerId", typeof(string)));
            dtErrorLog.Columns.Add(new DataColumn("WorkerCode", typeof(string)));
            dtErrorLog.Columns.Add(new DataColumn("WorkerName", typeof(string)));
            dtErrorLog.Columns.Add(new DataColumn("PayrollMonth", typeof(string)));
            dtErrorLog.Columns.Add(new DataColumn("PayrollYear", typeof(string)));
            dtErrorLog.Columns.Add(new DataColumn("ServiceType", typeof(string)));
            dtErrorLog.Columns.Add(new DataColumn("Amount", typeof(double)));
            dtErrorLog.Columns.Add(new DataColumn("Comment", typeof(string)));
            dtErrorLog.Columns.Add(new DataColumn("ErrorMessage", typeof(string)));



            /*---------- Validate file type ------*/
            if (request.File == null || request.File.Length == 0 || !Path.GetExtension(request.File.FileName).Contains(".xlsx"))
            {
                throw new InvalidDataException("Invalid file type.");
            }

            /*-------- Get File Data into Datatable ---------*/
            var dtWorkerPayrollData = getPayrollExcelDataTable(request.File);


            /*-------- Valiadte Rows and Insert into DB ----------*/
            foreach (DataRow dr in dtWorkerPayrollData.Rows)
            {
                var workerId =   dr["WorkerId"].ToString();
                var workerCode = dr["WorkerCode"].ToString();
                var workerName = dr["WorkerName"].ToString();
                var payrollMonth = dr["PayrollMonth"].ToString();
                var payrollYear = dr["PayrollYear"].ToString();
                var amount = dr["Amount"].ToString().Trim();
                var comment = dr["Comment"];


                /*----------- Payroll Month nad Year Null Check ----------*/
                if(string.IsNullOrEmpty(payrollMonth) || string.IsNullOrEmpty(payrollYear))
                {
                    /*--- Add Invlaid Row Data To Error Log Dt ---*/
                    DataRow drErrorLog = dtErrorLog.NewRow();
                    drErrorLog.ItemArray = dr.ItemArray;
                    drErrorLog["ErrorMessage"] = "Payroll year or month in invalid.";
                    dtErrorLog.Rows.Add(drErrorLog);

                    continue;
                }



                /*----------- Worker Id, Code, Name Null Check ----------*/
                if (string.IsNullOrEmpty(workerId) || string.IsNullOrEmpty(workerName) || string.IsNullOrEmpty(workerCode))
                {
                    /*--- Add Invlaid Row Data To Error Log Dt ---*/
                    DataRow drErrorLog = dtErrorLog.NewRow();
                    drErrorLog.ItemArray = dr.ItemArray;
                    drErrorLog["ErrorMessage"] = "Worker data is blank.";
                    dtErrorLog.Rows.Add(drErrorLog);

                    continue;
                }



                /*----------- Check if Payroll Month and Payroll Year is not future date,year ------------*/
                var givenPayrollMonth = DateTime.ParseExact(payrollMonth, "MMMM", System.Globalization.CultureInfo.InvariantCulture).Month;
                var givenPayrollYear = Convert.ToInt32(payrollYear);
                var currentYear = DateTime.Now.Year;
                var currentMonth = DateTime.Now.Month;
                if (givenPayrollYear > currentYear || (givenPayrollYear == currentYear && givenPayrollMonth > currentMonth))
                {
                    /*--- Add Invlaid Row Data To Error Log Dt ---*/
                    DataRow drErrorLog = dtErrorLog.NewRow();
                    drErrorLog.ItemArray = dr.ItemArray;
                    drErrorLog["ErrorMessage"] = "Payroll year and month exceeds current timestamp.";
                    dtErrorLog.Rows.Add(drErrorLog);

                    continue;
                }



                /*-------- Validate Worker Details --------*/
                var workerData = await _uow.workerRepository.getWorkerDetails(Convert.ToInt32(workerId), workerCode, workerName);
                if (workerData == null)
                {
                    /*--- Add Invlaid Row Data To Error Log Dt ---*/
                    DataRow drErrorLog = dtErrorLog.NewRow();
                    drErrorLog.ItemArray = dr.ItemArray;
                    drErrorLog["ErrorMessage"] = "Worker data invalid.";
                    dtErrorLog.Rows.Add(drErrorLog);

                    continue;
                }


                /*-------- Validate Amount NULL Check --------*/
                if (string.IsNullOrEmpty(amount))
                {
                    /*--- Add Invlaid Row Data To Error Log Dt ---*/
                    DataRow drErrorLog = dtErrorLog.NewRow();
                    drErrorLog.ItemArray = dr.ItemArray;
                    drErrorLog["ErrorMessage"] = "Amount value is null.";
                    dtErrorLog.Rows.Add(drErrorLog);

                    continue;
                }


                /*---------- 1. Get Payroll Data -----------*/
                /*---------- 2. If not exists => Insert -----------*/
                /*---------- 3. If exists => Update -----------*/
                var payrollData = await _uow.workerPayrollRepository.getPayrollData(Convert.ToInt32(workerId), payrollMonth, payrollYear);
                if (payrollData == null) 
                {
                    WorkerPayroll workerPayroll = new WorkerPayroll();
                    workerPayroll.WorkerId = Convert.ToInt32(workerId);
                    workerPayroll.PayrollMonth = payrollMonth;
                    workerPayroll.PayrollYear = payrollYear;
                    workerPayroll.Amount = double.Parse(amount.ToString());
                    workerPayroll.Comment = comment == null ? null : comment.ToString();
                    workerPayroll.CreatedBy = _currentUser.UserCustomId;

                    await _uow.workerPayrollRepository.addWorkerPayrollDetails(workerPayroll); /*---- Insert -----*/
                    await _uow.SaveChangesAsync();
                }
                else
                {
                    payrollData.Amount = double.Parse(amount.ToString());
                    payrollData.Comment = comment == null ? null : comment.ToString();
                    payrollData.UpdatedBy = _currentUser.UserCustomId;

                    await _uow.workerPayrollRepository.updateWorkerPayrollDetails(payrollData); /*---- Update -----*/
                    await _uow.SaveChangesAsync();
                }
            }

            var uploadErrorLogList = PayrollUploadErrorLogList(dtErrorLog);

            return _result.AddReturnData(HttpStatusCode.OK, uploadErrorLogList, message: "Data imported successfully.");
        }


        public DataTable getPayrollExcelDataTable(IFormFile file)
        {
            /*---- 2. Create DataTable ----*/
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("WorkerId", typeof(string)));
            dt.Columns.Add(new DataColumn("WorkerCode", typeof(string)));
            dt.Columns.Add(new DataColumn("WorkerName", typeof(string)));
            dt.Columns.Add(new DataColumn("PayrollMonth", typeof(string)));
            dt.Columns.Add(new DataColumn("PayrollYear", typeof(string)));
            dt.Columns.Add(new DataColumn("ServiceType", typeof(string)));
            dt.Columns.Add(new DataColumn("Amount", typeof(string)));
            dt.Columns.Add(new DataColumn("Comment", typeof(string)));

            /*--- 3. Read Data From Excel ---*/
            /*--- 4. Insert Values In DataTable After Reading Excel Data ---*/
            using (var stream = new MemoryStream())
            {
                file.CopyTo(stream);
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    var rowcount = worksheet.Dimension.Rows;
                    for (int row = 2; row <= rowcount; row++)
                    {
                        DataRow dr = dt.NewRow();
                        dr["WorkerId"] = worksheet.Cells[row, 1].Value;
                        dr["WorkerCode"] = worksheet.Cells[row, 2].Value;
                        dr["WorkerName"] = worksheet.Cells[row, 3].Value;
                        dr["PayrollMonth"] = worksheet.Cells[row, 4].Value;
                        dr["PayrollYear"] = worksheet.Cells[row, 5].Value;
                        dr["ServiceType"] = worksheet.Cells[row, 6].Value;
                        dr["Amount"] = worksheet.Cells[row, 7].Value;
                        dr["Comment"] = worksheet.Cells[row, 8].Value;

                        dt.Rows.Add(dr);
                    }
                }
            }

            return dt;
        }

        private void CreateWokerPayrollFormatDownloadableFile(List<WorkerDetail> workerList, GetPayrollWorkerListRequest request, out string fullFilePath)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var guid = Guid.NewGuid();

            string rootPath = AppDomain.CurrentDomain.BaseDirectory;
            string currentDirectory = Directory.GetCurrentDirectory();
            string fileName = $"WorkerPayrollFormat_{guid}.xlsx";
            string filePath = "Files";
            fullFilePath = Path.Combine(currentDirectory, $"{filePath}\\{fileName}");

            using (ExcelPackage package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Payroll");

                /*--------- Add header row -------*/
                worksheet.Cells[1, 1].Value = "Worker Id";
                worksheet.Cells[1, 2].Value = "Worker Code";
                worksheet.Cells[1, 3].Value = "Worker Name";
                worksheet.Cells[1, 4].Value = "Payroll Month";
                worksheet.Cells[1, 5].Value = "Payroll Year";
                worksheet.Cells[1, 6].Value = "Service Type";
                worksheet.Cells[1, 7].Value = "Amount";
                worksheet.Cells[1, 8].Value = "Comment";


                /*------- Populate Worker Data in Excel Sheet --------*/
                for (int i = 0; i < workerList.Count; i++)
                {
                    worksheet.Cells[i + 2, 1].Value = workerList[i].Id;
                    worksheet.Cells[i + 2, 2].Value = workerList[i].WorkerCode;
                    worksheet.Cells[i + 2, 3].Value = workerList[i].WorkerName;
                    worksheet.Cells[i + 2, 4].Value = request.PayrollMonth.Trim();
                    worksheet.Cells[i + 2, 5].Value = request.PayrollYear;
                    worksheet.Cells[i + 2, 6].Value = workerList[i].ServiceType.ServiceTypeDesc;
                }

                /*------- Column Header Styling --------*/
                using (var headerRange = worksheet.Cells[1, 1, 1, 8])
                {
                    headerRange.Style.Font.Bold = true;
                    headerRange.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    headerRange.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    headerRange.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.CadetBlue);
                }

                /*------- Auto-fit columns --------*/
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
                worksheet.Cells[worksheet.Dimension.Address].Style.Locked = false;


                /*---------- Lock all cells in a specific column ---------*/
                worksheet.Cells["A:A"].Style.Locked = true;
                worksheet.Cells["B:B"].Style.Locked = true;
                worksheet.Cells["C:C"].Style.Locked = true;
                worksheet.Cells["D:D"].Style.Locked = true;
                worksheet.Cells["E:E"].Style.Locked = true;


                /*---------- Protect the worksheet to enforce the locks ---------*/
                //worksheet.Protection.IsProtected = true;
                //worksheet.Protection.SetPassword("1234");

                /*-------- Save the file--------*/
                FileInfo file = new FileInfo(fullFilePath);
                package.SaveAs(file);
            }
        }

        private List<UploadPayrollWorkerExcelErrorLog> PayrollUploadErrorLogList(DataTable dtErrorLog)
        {
            var errorLogList = dtErrorLog.AsEnumerable().Select(row => new UploadPayrollWorkerExcelErrorLog
            {
                WorkerId = row.Field<string?>("WorkerId"),
                WorkerCode = row.Field<string>("WorkerCode"),
                WorkerName = row.Field<string>("WorkerName"),
                PayrollMonth = row.Field<string>("PayrollMonth"),   
                PayrollYear = row.Field<string>("PayrollYear"),
                Amount = row.Field<double?>("Amount"),
                Comment = row.Field<string>("Comment"),
                ErrorMessage = row.Field<string>("ErrorMessage")
            }).ToList();

            return errorLogList;
        }

        private string generateFileBinary(string fullFilePath, out string contentType)
        {
            contentType = string.Empty;
            string fileBase64String = string.Empty;

            var fileNamee = fullFilePath.Split('/').Last();
            var fileName = fullFilePath.Split('\\').Last();

            if (File.Exists(fullFilePath))
            {
                /*------- Get File Content/Mime type--------*/
                new FileExtensionContentTypeProvider().TryGetContentType(fileName, out contentType);


                /*------- Genearte File Content as Binary --------*/
                byte[] bytes = File.ReadAllBytes(fullFilePath);
                fileBase64String = Convert.ToBase64String(bytes);
            }

            return fileBase64String;
        }

        private void deleteFile(string fullFilePath)
        {
            if (File.Exists(fullFilePath))
            {
                File.Delete(fullFilePath);
            }
        }
    }
}
