using System.Collections.Generic;
using System.IO;
using WOM_EYE.Models.Projects;
using ClosedXML.Excel;

namespace WOM_EYE.Helpers
{
    public class ExportExcelAllProjects
    {
        public static byte[] CreateWorkBook(IEnumerable<ProjectModel> dataProject)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheetProject = workbook.Worksheets.Add("All Project");
                var currentRow = 1;

                worksheetProject.Column(1).Width = 20;
                worksheetProject.Column(2).Width = 20;
                worksheetProject.Column(3).Width = 20;
                worksheetProject.Column(4).Width = 20;
                worksheetProject.Column(5).Width = 20;
                worksheetProject.Column(6).Width = 20;
                worksheetProject.Column(7).Width = 20;
                worksheetProject.Column(8).Width = 20;
                worksheetProject.Column(9).Width = 20;
                worksheetProject.Column(10).Width = 20;
                worksheetProject.Column(11).Width = 20;

                worksheetProject.Column(1).SetDataType(XLDataType.Text);
                worksheetProject.Column(2).SetDataType(XLDataType.Text);
                worksheetProject.Column(3).SetDataType(XLDataType.Text);
                worksheetProject.Column(4).SetDataType(XLDataType.Text);
                worksheetProject.Column(5).SetDataType(XLDataType.Text);
                worksheetProject.Column(6).SetDataType(XLDataType.Text);
                worksheetProject.Column(7).SetDataType(XLDataType.Text);
                worksheetProject.Column(8).SetDataType(XLDataType.Text);
                worksheetProject.Column(9).SetDataType(XLDataType.Text);
                worksheetProject.Column(10).SetDataType(XLDataType.Text);
                worksheetProject.Column(11).SetDataType(XLDataType.Text);

                #region Title Column

                worksheetProject.Cell(currentRow, 1).Value = "Solution Leader";
                worksheetProject.Cell(currentRow, 1).Style.Border.OutsideBorder = XLBorderStyleValues.Medium;
                worksheetProject.Cell(currentRow, 1).Style.Border.InsideBorder = XLBorderStyleValues.Medium;
                worksheetProject.Cell(currentRow, 2).Value = "Project Owner";
                worksheetProject.Cell(currentRow, 2).Style.Border.OutsideBorder = XLBorderStyleValues.Medium;
                worksheetProject.Cell(currentRow, 2).Style.Border.InsideBorder = XLBorderStyleValues.Medium;
                worksheetProject.Cell(currentRow, 3).Value = "Project Type";
                worksheetProject.Cell(currentRow, 3).Style.Border.OutsideBorder = XLBorderStyleValues.Medium;
                worksheetProject.Cell(currentRow, 3).Style.Border.InsideBorder = XLBorderStyleValues.Medium;
                worksheetProject.Cell(currentRow, 4).Value = "Number of Project";
                worksheetProject.Cell(currentRow, 4).Style.Border.OutsideBorder = XLBorderStyleValues.Medium;
                worksheetProject.Cell(currentRow, 4).Style.Border.InsideBorder = XLBorderStyleValues.Medium;
                worksheetProject.Cell(currentRow, 5).Value = "Description";
                worksheetProject.Cell(currentRow, 5).Style.Border.OutsideBorder = XLBorderStyleValues.Medium;
                worksheetProject.Cell(currentRow, 5).Style.Border.InsideBorder = XLBorderStyleValues.Medium;
                worksheetProject.Cell(currentRow, 6).Value = "Programmers";
                worksheetProject.Cell(currentRow, 6).Style.Border.OutsideBorder = XLBorderStyleValues.Medium;
                worksheetProject.Cell(currentRow, 6).Style.Border.InsideBorder = XLBorderStyleValues.Medium;
                worksheetProject.Cell(currentRow, 7).Value = "Function Analysis";
                worksheetProject.Cell(currentRow, 7).Style.Border.OutsideBorder = XLBorderStyleValues.Medium;
                worksheetProject.Cell(currentRow, 7).Style.Border.InsideBorder = XLBorderStyleValues.Medium;
                worksheetProject.Cell(currentRow, 8).Value = "STATUS";
                worksheetProject.Cell(currentRow, 8).Style.Border.OutsideBorder = XLBorderStyleValues.Medium;
                worksheetProject.Cell(currentRow, 8).Style.Border.InsideBorder = XLBorderStyleValues.Medium;
                worksheetProject.Cell(currentRow, 9).Value = "CATATAN";
                worksheetProject.Cell(currentRow, 9).Style.Border.OutsideBorder = XLBorderStyleValues.Medium;
                worksheetProject.Cell(currentRow, 9).Style.Border.InsideBorder = XLBorderStyleValues.Medium;
                worksheetProject.Cell(currentRow, 10).Value = "DTM_CRT";
                worksheetProject.Cell(currentRow, 10).Style.Border.OutsideBorder = XLBorderStyleValues.Medium;
                worksheetProject.Cell(currentRow, 10).Style.Border.InsideBorder = XLBorderStyleValues.Medium;

                #endregion

                worksheetProject.Column(11).SetDataType(XLDataType.Text);


                foreach (var row in dataProject)
                {
                    if (row.NO_PROJECT == null) continue;
                    currentRow++;

                    #region List of Data

                    worksheetProject.Cell(currentRow, 1).Value = row.SOL_LEADER;
                    worksheetProject.Cell(currentRow, 1).Style.Border.OutsideBorder = XLBorderStyleValues.Medium;
                    worksheetProject.Cell(currentRow, 1).Style.Border.InsideBorder = XLBorderStyleValues.Medium;
                    worksheetProject.Cell(currentRow, 2).Value = row.PROJECT_LEADER;
                    worksheetProject.Cell(currentRow, 2).Style.Border.OutsideBorder = XLBorderStyleValues.Medium;
                    worksheetProject.Cell(currentRow, 2).Style.Border.InsideBorder = XLBorderStyleValues.Medium;
                    worksheetProject.Cell(currentRow, 3).Value = row.JENIS_PROJECT;
                    worksheetProject.Cell(currentRow, 3).Style.Border.OutsideBorder = XLBorderStyleValues.Medium;
                    worksheetProject.Cell(currentRow, 3).Style.Border.InsideBorder = XLBorderStyleValues.Medium;
                    worksheetProject.Cell(currentRow, 4).Value = row.NO_PROJECT;
                    worksheetProject.Cell(currentRow, 4).Style.Border.OutsideBorder = XLBorderStyleValues.Medium;
                    worksheetProject.Cell(currentRow, 4).Style.Border.InsideBorder = XLBorderStyleValues.Medium;
                    worksheetProject.Cell(currentRow, 5).Value = row.DESKRIPSI;
                    worksheetProject.Cell(currentRow, 5).Style.Border.OutsideBorder = XLBorderStyleValues.Medium;
                    worksheetProject.Cell(currentRow, 5).Style.Border.InsideBorder = XLBorderStyleValues.Medium;
                    worksheetProject.Cell(currentRow, 6).Value = row.PROGRAMMER;
                    worksheetProject.Cell(currentRow, 6).Style.Border.OutsideBorder = XLBorderStyleValues.Medium;
                    worksheetProject.Cell(currentRow, 6).Style.Border.InsideBorder = XLBorderStyleValues.Medium;
                    worksheetProject.Cell(currentRow, 7).Value = row.FUNCTION_ANALYST;
                    worksheetProject.Cell(currentRow, 7).Style.Border.OutsideBorder = XLBorderStyleValues.Medium;
                    worksheetProject.Cell(currentRow, 7).Style.Border.InsideBorder = XLBorderStyleValues.Medium;
                    worksheetProject.Cell(currentRow, 8).Value = row.STATUS;
                    worksheetProject.Cell(currentRow, 8).Style.Border.OutsideBorder = XLBorderStyleValues.Medium;
                    worksheetProject.Cell(currentRow, 8).Style.Border.InsideBorder = XLBorderStyleValues.Medium;
                    worksheetProject.Cell(currentRow, 9).Value = row.CATATAN;
                    worksheetProject.Cell(currentRow, 9).Style.Border.OutsideBorder = XLBorderStyleValues.Medium;
                    worksheetProject.Cell(currentRow, 9).Style.Border.InsideBorder = XLBorderStyleValues.Medium;
                    worksheetProject.Cell(currentRow, 10).Value = "'" + row.DTM_CRT;
                    worksheetProject.Cell(currentRow, 10).Style.Border.OutsideBorder = XLBorderStyleValues.Medium;
                    worksheetProject.Cell(currentRow, 10).Style.Border.InsideBorder = XLBorderStyleValues.Medium;

                    #endregion


                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return content;
                }

            }
        }
    }
}
