using ClinicAPI.Models.Request;
using iTextSharp.text;
using iTextSharp.text.pdf;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using Image = iTextSharp.text.Image;

namespace ClinicAPI.HelperCode
{
    public class Export
    {
    //    public static MemoryStream DataTableToExcelXlsx(DataTable datatable, string[] ColumnNames, MasterReportDetailsPOCO obj)
    //    {
    //        MemoryStream Result = new MemoryStream();
    //        try
    //        {
    //            DataTable table = datatable;

    //            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
    //            ExcelPackage pack = new ExcelPackage();
    //            ExcelWorksheet ws = pack.Workbook.Worksheets.Add("IE Task Report");




    //            ws.Row(1).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

    //            ws.Cells[1, 2].Value = "IE Task Report";
    //            //ws.Cells[2, 1].Value = "Caution: Please Do Not alter the Values of Column A to Column E. Enter Remarks if there is no mapping otherwise leave it blank.";

    //            ws.Protection.IsProtected = true;
    //            //ws.Row(3).Style.Locked = false;
    //            //ws.Column(2).Style.Locked = false;
    //            //ws.Column(3).Style.Locked = false;
    //            //ws.Column(4).Style.Locked = false;
    //            //ws.Column(5).Style.Locked = false;
    //            //ws.Column(6).Style.Locked = false;
    //            //ws.Column(7).Style.Locked = false;
    //            //ws.Column(8).Style.Locked = false;
    //            //ws.Column(9).Style.Locked = false;
    //            //ws.Column(10).Style.Locked = false;
    //            //ws.Column(11).Style.Locked = false;
    //            //ws.Column(12).Style.Locked = false;
    //            //ws.Column(13).Style.Locked = false;
    //            //ws.Column(14).Style.Locked = false;
    //            //ws.Column(15).Style.Locked = false;
    //            //ws.Column(16).Style.Locked = false;
    //            //ws.Column(17).Style.Locked = false;
    //            //ws.Column(18).Style.Locked = false;
    //            //ws.Column(19).Style.Locked = false;

    //            ws.Row(1).Style.Font.Bold = true;
    //            ws.Row(10).Style.Font.Bold = true;
    //            //ws.Row(3).Style.Font.Bold = true;

    //            ws.Row(1).Style.Font.Size = 18;
    //            ws.Cells[3, 1].Style.Font.Bold = true;
    //            ws.Cells[4, 1].Style.Font.Bold = true;
    //            ws.Cells[5, 1].Style.Font.Bold = true;
    //            ws.Cells[6, 1].Style.Font.Bold = true;
    //            ws.Cells[7, 1].Style.Font.Bold = true;
    //            ws.Cells[8, 1].Style.Font.Bold = true;
    //            ws.Row(10).Style.Font.Size = 12;



    //            ws.Cells[3, 1].Value = "IE Name";
    //            ws.Cells[3, 2].Value = obj.IEName;
    //            ws.Cells[4, 1].Value = "Task Name";
    //            ws.Cells[4, 2].Value = obj.TaskName;
    //            ws.Cells[5, 1].Value = "Survey Name";
    //            ws.Cells[5, 2].Value = obj.SurveyName;

    //            ws.Cells[6, 1].Value = "Start Date";
    //            ws.Cells[6, 2].Value = obj.StartDate.Date.ToString("dddd, dd MMMM yyyy");
    //            ws.Cells[7, 1].Value = "End Date";
    //            ws.Cells[7, 2].Value = obj.EndDate.Date.ToString("dddd, dd MMMM yyyy");
    //            ws.Cells[8, 1].Value = "Completed Date";
    //            ws.Cells[8, 2].Value = obj.StatusModifyDate.Date.ToString("dddd, dd MMMM yyyy");


    //            int columnNameRow = 10;
    //            int columnNameColumn = 1;
    //            for (int i = 0; i < ColumnNames.Length; i++)
    //            {
    //                ws.Cells[columnNameRow, columnNameColumn].Value = ColumnNames[i];
    //                columnNameColumn++;
    //            }

    //            //ws.Cells[3, 1].Value = "RoleID";
    //            //ws.Cells[3, 2].Value = "RoleName";
    //            //ws.Cells[3, 3].Value = "Create Date";
    //            //ws.Cells[3, 4].Value = "Is Active";


    //            //ws.Cells[3, 5].Value = "Author Name2";
    //            //ws.Cells[3, 6].Value = "ISBN";
    //            //ws.Cells[3, 7].Value = "Publication";
    //            //ws.Cells[3, 8].Value = "Publication Year";
    //            //ws.Cells[3, 9].Value = "Language";
    //            //ws.Cells[3, 10].Value = "Section";
    //            //ws.Cells[3, 11].Value = "Volume";
    //            //ws.Cells[3, 12].Value = "Row";
    //            //ws.Cells[3, 13].Value = "Shelf";
    //            //ws.Cells[3, 14].Value = "Purchase Date";
    //            //ws.Cells[3, 15].Value = "Price";
    //            //ws.Cells[3, 16].Value = "Pages";
    //            //ws.Cells[3, 17].Value = "Status";
    //            //ws.Cells[3, 18].Value = "Flag";

    //            ws.Protection.SetPassword("!@#ExcelPassword");
    //            ws.Protection.AllowSort = true;

    //            int col = 1;
    //            int row = 11;

    //            if (table != null)
    //            {

    //                foreach (DataRow rw in table.Rows)
    //                {
    //                    foreach (DataColumn cl in table.Columns)
    //                    {
    //                        if (rw[cl.ColumnName] != DBNull.Value)
    //                            ws.Cells[row, col].Value = rw[cl.ColumnName].ToString();
    //                        col++;
    //                    }

    //                    row++;
    //                    col = 1;
    //                }
    //            }

    //            //var valdidateSectionMaster = ws.Cells[4, 10, (table.Rows.Count + 1500), 10].DataValidation.AddListDataValidation();
    //            //valdidateSectionMaster.ShowErrorMessage = true;
    //            //valdidateSectionMaster.Error = "Please Select Section Name from Supplied Dropdown";

    //            //for (int i = 0; i < DTSectionMaster.Rows.Count; i++)
    //            //{
    //            //    valdidateSectionMaster.Formula.Values.Add(DTSectionMaster.Rows[i][1].ToString());
    //            //}




    //            ws.Protection.AllowAutoFilter = true;

    //            //using (ExcelRange Rng = ws.Cells[1, 6])
    //            //{
    //            //    Rng.Merge = true;
    //            //}
    //            //using (ExcelRange Rng = ws.Cells[2, 1, 2, 18])
    //            //{
    //            //    Rng.Merge = true;
    //            //}
    //            //using (ExcelRange Rng = ws.Cells[3, 1, 3, 18])
    //            //{
    //            //    Rng.AutoFilter = true;
    //            //    Rng.Worksheet.Protection.AllowSort = true;
    //            //}
    //            ws.Cells.AutoFitColumns();
    //            pack.SaveAs(Result);
    //            return Result;
    //        }
    //        catch (Exception ex)
    //        {
    //            return Result;
    //        }
    //    }

        public static MemoryStream DataTableToExcelXlsxForHorizontalPrint(string[] questions, string[] answers)
        {

            MemoryStream Result = new MemoryStream();
            try
            {


                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                ExcelPackage pack = new ExcelPackage();
              
                ExcelWorksheet ws = pack.Workbook.Worksheets.Add("IE Task Report");




                ws.Row(1).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                ws.Cells[1, 2].Value = "IE Task Report";
                //ws.Cells[2, 1].Value = "Caution: Please Do Not alter the Values of Column A to Column E. Enter Remarks if there is no mapping otherwise leave it blank.";

                ws.Protection.IsProtected = true;
                //ws.Row(3).Style.Locked = false;
                //ws.Column(2).Style.Locked = false;
                //ws.Column(3).Style.Locked = false;
                //ws.Column(4).Style.Locked = false;
                //ws.Column(5).Style.Locked = false;
                //ws.Column(6).Style.Locked = false;
                //ws.Column(7).Style.Locked = false;
                //ws.Column(8).Style.Locked = false;
                //ws.Column(9).Style.Locked = false;
                //ws.Column(10).Style.Locked = false;
                //ws.Column(11).Style.Locked = false;
                //ws.Column(12).Style.Locked = false;
                //ws.Column(13).Style.Locked = false;
                //ws.Column(14).Style.Locked = false;
                //ws.Column(15).Style.Locked = false;
                //ws.Column(16).Style.Locked = false;
                //ws.Column(17).Style.Locked = false;
                //ws.Column(18).Style.Locked = false;
                //ws.Column(19).Style.Locked = false;

                //ws.Row(1).Style.Font.Bold = true;
                //ws.Row(10).Style.Font.Bold = true;
                //ws.Row(3).Style.Font.Bold = true;

                ws.Row(1).Style.Font.Size = 18;
                //ws.Cells[3, 1].Style.Font.Bold = true;
                //ws.Cells[4, 1].Style.Font.Bold = true;
                //ws.Cells[5, 1].Style.Font.Bold = true;
                //ws.Cells[6, 1].Style.Font.Bold = true;
                //ws.Cells[7, 1].Style.Font.Bold = true;
                //ws.Cells[8, 1].Style.Font.Bold = true;
                ws.Row(3).Style.Font.Bold = true;



                //ws.Cells[3, 1].Value = "IE Name";
                //ws.Cells[3, 2].Value = obj.IEName;
                //ws.Cells[4, 1].Value = "Task Name";
                //ws.Cells[4, 2].Value = obj.TaskName;
                //ws.Cells[5, 1].Value = "Survey Name";
                //ws.Cells[5, 2].Value = obj.SurveyName;

                //ws.Cells[6, 1].Value = "Start Date";
                //ws.Cells[6, 2].Value = obj.StartDate.Date.ToString("dddd, dd MMMM yyyy");
                //ws.Cells[7, 1].Value = "End Date";
                //ws.Cells[7, 2].Value = obj.EndDate.Date.ToString("dddd, dd MMMM yyyy");
                //ws.Cells[8, 1].Value = "Completed Date";
                //ws.Cells[8, 2].Value = obj.StatusModifyDate.Date.ToString("dddd, dd MMMM yyyy");


                int columnNameRow = 3;
                int columnNameColumn = 1;



                //ws.Cells[columnNameRow, columnNameColumn].Value = "RFP";
                //columnNameColumn++;

                //ws.Cells[columnNameRow, columnNameColumn].Value = "EMO";

                //columnNameColumn++;


                //ws.Cells[columnNameRow, columnNameColumn].Value = "EMO Contact No";
                //columnNameColumn++;

                //ws.Cells[columnNameRow, columnNameColumn].Value = "EMO Email";
                //columnNameColumn++;
                //ws.Cells[columnNameRow, columnNameColumn].Value = "EMO Designation";
                //columnNameColumn++;

                for (int i = 0; i < questions.Length; i++)
                {

                    ws.Cells[columnNameRow, columnNameColumn].Value = questions[i];
                    columnNameColumn++;
                }

                columnNameRow = 4;
                columnNameColumn = 1;

                //if( emoDetails.Count>0)
                // {


                //     ws.Cells[columnNameRow, columnNameColumn].Value = emoDetails[0].Number.ToString();
                //     columnNameColumn++;
                //     ws.Cells[columnNameRow, columnNameColumn].Value = emoDetails[0].FirstName;
                //     columnNameColumn++;
                //     ws.Cells[columnNameRow, columnNameColumn].Value = emoDetails[0].ContactNo;
                //     columnNameColumn++;

                //     ws.Cells[columnNameRow, columnNameColumn].Value = emoDetails[0].EmailAddress;
                //     columnNameColumn++;
                //     ws.Cells[columnNameRow, columnNameColumn].Value = emoDetails[0].Designation;
                //     columnNameColumn++;

                // }

                // for (int i = 0; i < answers.Length; i++)
                // {
                //     ws.Cells[columnNameRow, columnNameColumn].Value = answers[i];
                //     columnNameColumn++;
                // }

                //


                for (int i = 0; i < questions.Length; i++)
                {
                    ws.Cells[columnNameRow, columnNameColumn].Value = questions[i];
                    columnNameColumn++;
                }
                columnNameRow = 4;
                columnNameColumn = 1;
                for (int i = 0; i < answers.Length; i++)
                {
                    ws.Cells[columnNameRow, columnNameColumn].Value = answers[i];
                    columnNameColumn++;
                }

                //ws.Cells[3, 1].Value = "RoleID";
                //ws.Cells[3, 2].Value = "RoleName";
                //ws.Cells[3, 3].Value = "Create Date";
                //ws.Cells[3, 4].Value = "Is Active";


                //ws.Cells[3, 5].Value = "Author Name2";
                //ws.Cells[3, 6].Value = "ISBN";
                //ws.Cells[3, 7].Value = "Publication";
                //ws.Cells[3, 8].Value = "Publication Year";
                //ws.Cells[3, 9].Value = "Language";
                //ws.Cells[3, 10].Value = "Section";
                //ws.Cells[3, 11].Value = "Volume";
                //ws.Cells[3, 12].Value = "Row";
                //ws.Cells[3, 13].Value = "Shelf";
                //ws.Cells[3, 14].Value = "Purchase Date";
                //ws.Cells[3, 15].Value = "Price";
                //ws.Cells[3, 16].Value = "Pages";
                //ws.Cells[3, 17].Value = "Status";
                //ws.Cells[3, 18].Value = "Flag";

                ws.Protection.SetPassword("!@#ExcelPassword");
                ws.Protection.AllowSort = true;

                //int col = 1;
                //int row = 11;

                //if (table != null)
                //{

                //    foreach (DataRow rw in table.Rows)
                //    {
                //        foreach (DataColumn cl in table.Columns)
                //        {
                //            if (rw[cl.ColumnName] != DBNull.Value)
                //                ws.Cells[row, col].Value = rw[cl.ColumnName].ToString();
                //            col++;
                //        }

                //        row++;
                //        col = 1;
                //    }
                //}

                //var valdidateSectionMaster = ws.Cells[4, 10, (table.Rows.Count + 1500), 10].DataValidation.AddListDataValidation();
                //valdidateSectionMaster.ShowErrorMessage = true;
                //valdidateSectionMaster.Error = "Please Select Section Name from Supplied Dropdown";

                //for (int i = 0; i < DTSectionMaster.Rows.Count; i++)
                //{
                //    valdidateSectionMaster.Formula.Values.Add(DTSectionMaster.Rows[i][1].ToString());
                //}




                ws.Protection.AllowAutoFilter = true;

                //using (ExcelRange Rng = ws.Cells[1, 6])
                //{
                //    Rng.Merge = true;
                //}
                //using (ExcelRange Rng = ws.Cells[2, 1, 2, 18])
                //{
                //    Rng.Merge = true;
                //}
                //using (ExcelRange Rng = ws.Cells[3, 1, 3, 18])
                //{
                //    Rng.AutoFilter = true;
                //    Rng.Worksheet.Protection.AllowSort = true;
                //}
                ws.Cells.AutoFitColumns();
                pack.SaveAs(Result);
                return Result;
            }
            catch (Exception ex)
            {
                return Result;
            }
        }
        public static MemoryStream DataTableToExcelXlsxForMultipleHorizontalPrint(List<ClinciReportsPOCO> obj)
        {
            MemoryStream Result = new MemoryStream();
            try
            {
             

                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                ExcelPackage pack = new ExcelPackage();
                ExcelWorksheet ws = pack.Workbook.Worksheets.Add("compassioncouch");




                ws.Row(1).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                ws.Cells[1, 2].Value = "compassioncouch Reports_" + DateTime.Now.ToString("MM-dd-yyyy_HHmmss");
                //ws.Cells[2, 1].Value = "Caution: Please Do Not alter the Values of Column A to Column E. Enter Remarks if there is no mapping otherwise leave it blank.";

                ws.Protection.IsProtected = true;


                ws.Row(1).Style.Font.Size = 18;

                ws.Row(3).Style.Font.Bold = true;
                int Row = 3;
                int Column = 1;
                bool printed = true;
                if (obj.Count > 0)
                {

                    int rowforHeading = 3;
                    int columnforHeading = 1;

                    ws.Cells[rowforHeading, columnforHeading].Value = "Patient_Name";
                    columnforHeading++;


                    ws.Cells[rowforHeading, columnforHeading].Value = "Doctor_Name";
                    columnforHeading++;


                    ws.Cells[rowforHeading, columnforHeading].Value = "Patient Date of Birth";
                    columnforHeading++;


                    ws.Cells[rowforHeading, columnforHeading].Value = "Appointment_Date";
                    columnforHeading++;


                    ws.Cells[rowforHeading, columnforHeading].Value = "Time";
                    columnforHeading++;

                    ws.Cells[rowforHeading, columnforHeading].Value = "Appointment Type";
                    columnforHeading++;

                    ws.Cells[rowforHeading, columnforHeading].Value = "Appointment Status";
                    columnforHeading++;
                    //

                    for (int i = 0; i < obj.Count; i++)
                    {
                            
                        Row++;
                        Column = 1;




                        
                        ws.Cells[Row, Column].Value = obj[i].PatientName.ToString();
                        Column++;


                        ws.Cells[Row, Column].Value = obj[i].DoctorName.ToString();
                        Column++;





                        ws.Cells[Row, Column].Value = obj[i].Age.ToString();
                        Column++;


                        ws.Cells[Row, Column].Value = obj[i].DateofAppointment.ToString();
                        Column++;



                        ws.Cells[Row, Column].Value = obj[i].Time.ToString();
                        Column++;



                        ws.Cells[Row, Column].Value = obj[i].AppointmentType;
                        Column++;


                        ws.Cells[Row, Column].Value = obj[i].AppointmentSatus;
                        Column++;
                        Row = Row + 2;
                        Column = 1;

                    }
                }
                ws.Protection.SetPassword("!@#ExcelPassword");
                ws.Protection.AllowSort = true;


                ws.Protection.AllowAutoFilter = true;

                ws.Cells.AutoFitColumns();
                pack.SaveAs(Result);
                return Result;
            }
            catch (Exception ex)
            {
                return Result;
            }
        }


        public  string CreateStudentPdf(Prescription obj)
        {
            string logo = Path.Combine(HttpContext.Current.Server.MapPath("~/Content"), "Synapse-Logo.png");

            string path = Path.Combine(HttpContext.Current.Server.MapPath("~/prescriptions"));

           
           BaseFont bf = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);

            iTextSharp.text.Font font = new iTextSharp.text.Font(bf, 8, iTextSharp.text.Font.NORMAL);


           Image logoImage = Image.GetInstance(logo);
            logoImage.ScaleAbsolute(120, 60);
            var pgsize = new iTextSharp.text.Rectangle(612, 792);
            Document doc = new Document(pgsize, 8, 8, 10, 10);
            MemoryStream PDFData = new MemoryStream();
            Document document = new Document(pgsize, 60, 60, 80, 50);
            PdfWriter PDFWriter = PdfWriter.GetInstance(document, PDFData);
            PDFWriter.ViewerPreferences = PdfWriter.PageModeUseOutlines;
            // Our custom Header and Footer is done using Event Handler
            TwoColumnHeaderFooter PageEventHandler = new TwoColumnHeaderFooter();
            PDFWriter.PageEvent = PageEventHandler;
            PageEventHandler.HeaderLeft = "Group";
            PageEventHandler.HeaderRight = "1";
            PdfPCell logoCell = new PdfPCell(logoImage);
            logoCell.FixedHeight = 100;
            logoCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            logoCell.HorizontalAlignment = 2;
            logoCell.PaddingRight = -150;



            PdfPCell schoolNameCell = new PdfPCell(new Phrase("", font));
            //schoolNameCell.Colspan = 2;
            //schoolNameCell.FixedHeight = 30;

            schoolNameCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            schoolNameCell.HorizontalAlignment = 0;
            schoolNameCell.PaddingLeft = -28;

            PdfPCell schoolAddCell = new PdfPCell(new Phrase("", font));
            schoolAddCell.Colspan = 3;
            schoolAddCell.FixedHeight = 15;
            schoolAddCell.PaddingTop = -7;
            schoolAddCell.HorizontalAlignment = 1;

            PdfPCell emptyCell = new PdfPCell(new Phrase("", font));
            schoolAddCell.Colspan = 3;
            schoolAddCell.FixedHeight = 15;
            schoolAddCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            schoolAddCell.HorizontalAlignment = 1;


            // Define the page header
            PageEventHandler.Title = "";
            PageEventHandler.HeaderFont = FontFactory.GetFont(BaseFont.COURIER_BOLD, 10, iTextSharp.text.Font.BOLD);
            PageEventHandler.HeaderCenterLogo = logoCell;
            PageEventHandler.HeaderCenterSchoolName = schoolNameCell;
            PageEventHandler.HeaderCenterSchoolAddress = schoolAddCell;
            PageEventHandler.HeaderCenterSchoolAddress = schoolAddCell;
            PageEventHandler.HeaderCenterEmptyCell = schoolAddCell;
            document.Open();


            PdfPTable tableFirstRowData = new PdfPTable(2);
            tableFirstRowData.SetWidthPercentage(new float[] { 400, 300 }, pgsize);

            tableFirstRowData.AddCell(new PdfPCell(new Phrase("Name : " + Convert.ToString(obj.Patient_Name), font)) { VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT, FixedHeight = 15, Border = iTextSharp.text.Rectangle.NO_BORDER });
            tableFirstRowData.AddCell(new PdfPCell(new Phrase("Midical Record (MR) Number :" + Convert.ToString(obj), font)) { VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_RIGHT, FixedHeight = 15, Border = iTextSharp.text.Rectangle.NO_BORDER });

            //tableFirstRowData.AddCell(new PdfPCell(new Phrase("Age : " + Convert.ToString(obj.DateOfBirth), font)) { VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT, FixedHeight = 15, Border = iTextSharp.text.Rectangle.NO_BORDER });
            //tableFirstRowData.AddCell(new PdfPCell(new Phrase("Date:" + Convert.ToString(DateTime.Now), font)) { VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_RIGHT, FixedHeight = 15, Border = iTextSharp.text.Rectangle.NO_BORDER });

            document.Add(tableFirstRowData);


            //PdfPTable tableHeader = new PdfPTable(12);
            //tableHeader.SetWidthPercentage(new float[] { 22, 200, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50 }, pgsize);

            //tableHeader.AddCell(new PdfPCell(new Phrase("Medication", font)) { VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT, FixedHeight = 15 });
            //tableHeader.AddCell(new PdfPCell(new Phrase("Item Name", font)) { VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT, FixedHeight = 15 });
            //tableHeader.AddCell(new PdfPCell(new Phrase("Unit Price", font)) { VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT, FixedHeight = 15 });
            //tableHeader.AddCell(new PdfPCell(new Phrase("Quantity", font)) { VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT, FixedHeight = 15 });
            //tableHeader.AddCell(new PdfPCell(new Phrase("SubTotal", font)) { VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT, FixedHeight = 15 });
            //tableHeader.AddCell(new PdfPCell(new Phrase("Disc %", font)) { VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT, FixedHeight = 15 });
            //tableHeader.AddCell(new PdfPCell(new Phrase("Disc Price", font)) { VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT, FixedHeight = 15 });
            //tableHeader.AddCell(new PdfPCell(new Phrase("PerPallet", font)) { VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT, FixedHeight = 15 });
            //tableHeader.AddCell(new PdfPCell(new Phrase("PalletsQty", font)) { VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT, FixedHeight = 15 });
            //tableHeader.AddCell(new PdfPCell(new Phrase("PalletPrice", font)) { VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT, FixedHeight = 15 });
            //tableHeader.AddCell(new PdfPCell(new Phrase("TotalPalletPrice", font)) { VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT, FixedHeight = 15 });


            //tableHeader.AddCell(new PdfPCell(new Phrase("Net Amount", font)) { VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT, FixedHeight = 15 });

            //document.Add(tableHeader);

            //for (int j = 0; j < obj.Count; j++)
            //{
            //    var currentData =  obj;
            //    PdfPTable table = new PdfPTable(12);
            //    table.SetWidthPercentage(new float[] { 22, 200, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50 }, pgsize);

            //    table.AddCell(new PdfPCell(new Phrase((1 + 1).ToString(), font)) { VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT, FixedHeight = 15 });
            //    table.AddCell(new PdfPCell(new Phrase(currentData..ToString(), font)) { VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT, FixedHeight = 15 });
            //    table.AddCell(new PdfPCell(new Phrase(Convert.ToString(currentData.UnitPrice), font)) { VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT, FixedHeight = 15 });
            //    table.AddCell(new PdfPCell(new Phrase(Convert.ToString(currentData.Quantity), font)) { VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT, FixedHeight = 15 });
            //    table.AddCell(new PdfPCell(new Phrase(Convert.ToString(currentData.SubTotal), font)) { VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT, FixedHeight = 15 });
            //    table.AddCell(new PdfPCell(new Phrase(Convert.ToString(currentData.ProductPerUnitDiscountPercentage), font)) { VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT, FixedHeight = 15 });
            //    table.AddCell(new PdfPCell(new Phrase(Convert.ToString(currentData.ProductPerUnitDiscountPrice), font)) { VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT, FixedHeight = 15 });
            //    table.AddCell(new PdfPCell(new Phrase(Convert.ToString(currentData.ItemPerPallet), font)) { VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT, FixedHeight = 15 });
            //    table.AddCell(new PdfPCell(new Phrase(Convert.ToString(currentData.ItemPalletsQuantity), font)) { VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT, FixedHeight = 15 });
            //    table.AddCell(new PdfPCell(new Phrase(Convert.ToString(currentData.ItemPalletPrice), font)) { VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT, FixedHeight = 15 });
            //    table.AddCell(new PdfPCell(new Phrase(Convert.ToString(currentData.ItemTotalPalletPrice), font)) { VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT, FixedHeight = 15 });









            //    table.AddCell(new PdfPCell(new Phrase(Convert.ToString(currentData.NetSubTotal), font)) { VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT, FixedHeight = 15 });

            //    document.Add(table);
            

            //PdfPTable tableFooter = new PdfPTable(2);
            //tableFooter.SetWidthPercentage(new float[] { 620, 100 }, pgsize);

            //tableFooter.AddCell(new PdfPCell(new Phrase("Tax : ", font)) { VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_RIGHT, FixedHeight = 15, Border = iTextSharp.text.Rectangle.NO_BORDER });
            //tableFooter.AddCell(new PdfPCell(new Phrase(Convert.ToString(obj.TaxAmount), font)) { VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_RIGHT, FixedHeight = 15, Border = iTextSharp.text.Rectangle.NO_BORDER });
            //tableFooter.AddCell(new PdfPCell(new Phrase("Total : ", font)) { VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_RIGHT, FixedHeight = 15, Border = iTextSharp.text.Rectangle.NO_BORDER });
            //tableFooter.AddCell(new PdfPCell(new Phrase(Convert.ToString(obj.TotalAmount), font)) { VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_RIGHT, FixedHeight = 15, Border = iTextSharp.text.Rectangle.NO_BORDER });

            //tableFooter.AddCell(new PdfPCell(new Phrase("Total Disc : ", font)) { VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_RIGHT, FixedHeight = 15, Border = iTextSharp.text.Rectangle.NO_BORDER });
            //tableFooter.AddCell(new PdfPCell(new Phrase(Convert.ToString(obj.DiscontedTotalAmount), font)) { VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_RIGHT, FixedHeight = 15, Border = iTextSharp.text.Rectangle.NO_BORDER });
            //tableFooter.AddCell(new PdfPCell(new Phrase("", font)) { VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_RIGHT, FixedHeight = 15, Border = iTextSharp.text.Rectangle.NO_BORDER });
            //tableFooter.AddCell(new PdfPCell(new Phrase("____________", font)) { VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_RIGHT, FixedHeight = 15, Border = iTextSharp.text.Rectangle.NO_BORDER });

            //tableFooter.AddCell(new PdfPCell(new Phrase("Net Amount : ", font)) { VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_RIGHT, FixedHeight = 15, Border = iTextSharp.text.Rectangle.NO_BORDER });
            //tableFooter.AddCell(new PdfPCell(new Phrase(Convert.ToString(obj.NetAmount), font)) { VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_RIGHT, FixedHeight = 15, Border = iTextSharp.text.Rectangle.NO_BORDER });

            //document.Add(tableFooter);
            //}
            document.Close();
            PDFData.Flush(); //Always catches me out
          //  PDFData.Position = 0; //Not sure if this is required
            string fileName = obj.Patient_Name + DateTime.Now.ToString("yyyy-MM-dd_HHmmss") + ".pdf";
            SaveStreamAsFile(path, PDFData, fileName);
            return fileName;


        }

        public static void SaveStreamAsFile(string filePath, Stream inputStream, string fileName)
        {
            DirectoryInfo info = new DirectoryInfo(filePath);
            if (!info.Exists)
            {
                info.Create();
            }

            string path = Path.Combine(filePath, fileName);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            MemoryStream ms = new MemoryStream();

            FileStream file = new FileStream(path, FileMode.Create, FileAccess.Write);
            ms.WriteTo(file);
            file.Close();
            //using (FileStream outputFileStream = new FileStream(path, FileMode.Create, FileAccess.Write))
            //{


            //    inputStream.CopyTo(outputFileStream);
            //}
        }

    }

    public class TwoColumnHeaderFooter : PdfPageEventHelper
    {
        // This is the contentbyte object of the writer
        PdfContentByte cb;
        // we will put the final number of pages in a template
        PdfTemplate template;
        // this is the BaseFont we are going to use for the header / footer
        BaseFont bf = null;
        // This keeps track of the creation time
        DateTime PrintTime = DateTime.Now;
        #region Properties
        private string _Title;
        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }
        private PdfPCell _HeaderCenterLogo;
        public PdfPCell HeaderCenterLogo
        {
            get { return _HeaderCenterLogo; }
            set { _HeaderCenterLogo = value; }
        }
        private PdfPCell _HeaderCenterSchoolName;
        public PdfPCell HeaderCenterSchoolName
        {
            get { return _HeaderCenterSchoolName; }
            set { _HeaderCenterSchoolName = value; }
        }
        private PdfPCell _HeaderCenterEmptyCell;
        public PdfPCell HeaderCenterEmptyCell
        {
            get { return _HeaderCenterEmptyCell; }
            set { _HeaderCenterEmptyCell = value; }
        }
        private PdfPCell _HeaderCenterSchoolAddress;
        public PdfPCell HeaderCenterSchoolAddress
        {
            get { return _HeaderCenterSchoolAddress; }
            set { _HeaderCenterSchoolAddress = value; }
        }
        private string _HeaderLeft;
        public string HeaderLeft
        {
            get { return _HeaderLeft; }
            set { _HeaderLeft = value; }
        }
        private string _HeaderRight;
        public string HeaderRight
        {
            get { return _HeaderRight; }
            set { _HeaderRight = value; }
        }
        private iTextSharp.text.Font _HeaderFont;
        public iTextSharp.text.Font HeaderFont
        {
            get { return _HeaderFont; }
            set { _HeaderFont = value; }
        }
        private iTextSharp.text.Font _FooterFont;
        public iTextSharp.text.Font FooterFont
        {
            get { return _FooterFont; }
            set { _FooterFont = value; }
        }
        #endregion
        // we override the onOpenDocument method
        public override void OnOpenDocument(PdfWriter writer, Document document)
        {
            try
            {
                PrintTime = DateTime.Now;
                bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                cb = writer.DirectContent;
                template = cb.CreateTemplate(50, 50);
            }
            catch (DocumentException de)
            {
            }
            catch (System.IO.IOException ioe)
            {
            }
        }

        public override void OnStartPage(PdfWriter writer, Document document)
        {
            base.OnStartPage(writer, document);
            iTextSharp.text.Rectangle pageSize = document.PageSize;
            if (Title != string.Empty)
            {
                cb.BeginText();
                cb.SetFontAndSize(bf, 15);
                //cb.SetRGBColorFill(50, 50, 200);
                cb.SetTextMatrix(pageSize.GetLeft(40), pageSize.GetTop(20));
                cb.ShowText(Title);
                cb.EndText();
            }
            if (HeaderLeft + HeaderRight != string.Empty)
            {
                PdfPTable HeaderTable = new PdfPTable(3);
                HeaderTable.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                HeaderTable.TotalWidth = pageSize.Width - 80;
                HeaderTable.SetWidthPercentage(new float[] { 45, 45, 45 }, pageSize);

                PdfPCell headerCenterLogo = HeaderCenterLogo;
                PdfPCell headerCenterSchoolName = HeaderCenterSchoolName;
                PdfPCell headerCenterSchoolAddress = HeaderCenterSchoolAddress;
                //new PdfPCell(new Phrase(8, HeaderLeft, HeaderFont));
                //HeaderLeftCell.Padding = 5;
                //He
                //HeaderLeftCell.PaddingBottom = 8;
                //HeaderLeftCell.BorderWidthRight = 0;
                HeaderTable.AddCell(headerCenterLogo);
                HeaderTable.AddCell(headerCenterSchoolName);
                HeaderTable.AddCell(headerCenterSchoolAddress);
                //PdfPCell HeaderRightCell = new PdfPCell(new Phrase(8, HeaderRight, HeaderFont));
                //HeaderRightCell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                //HeaderRightCell.Padding = 5;
                //HeaderRightCell.PaddingBottom = 8;
                //HeaderRightCell.BorderWidthLeft = 0;
                //HeaderTable.AddCell(HeaderRightCell);
                //cb.SetRGBColorFill(0, 0, 0);
                HeaderTable.WriteSelectedRows(0, -1, pageSize.GetLeft(40), pageSize.GetTop(20), cb);
            }
        }
        public override void OnEndPage(PdfWriter writer, Document document)
        {
            base.OnEndPage(writer, document);
            int pageN = writer.PageNumber;
            String text = "Page " + pageN + " of ";
            float len = bf.GetWidthPoint(text, 8);

            iTextSharp.text.Rectangle pageSize = document.PageSize;
            //cb.SetRGBColorFill(100, 100, 100);
            cb.BeginText();
            cb.SetFontAndSize(bf, 8);
            cb.SetTextMatrix(pageSize.GetLeft(40), pageSize.GetBottom(30));
            cb.ShowText(text);
            cb.EndText();
            cb.AddTemplate(template, pageSize.GetLeft(40) + len, pageSize.GetBottom(30));

            cb.BeginText();
            cb.SetFontAndSize(bf, 8);
            cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT,
                "Printed On " + PrintTime.ToString(),
                pageSize.GetRight(40),
                pageSize.GetBottom(30), 0);
            cb.EndText();
        }
        public override void OnCloseDocument(PdfWriter writer, Document document)
        {
            base.OnCloseDocument(writer, document);
            template.BeginText();
            template.SetFontAndSize(bf, 8);
            template.SetTextMatrix(0, 0);
            template.ShowText("" + (writer.PageNumber - 1));
            template.EndText();
        }
    }
}