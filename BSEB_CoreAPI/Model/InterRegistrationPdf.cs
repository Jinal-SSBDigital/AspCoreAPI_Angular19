using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using BSEB_CoreAPI.Model;

public class InterRegistrationPdf : IDocument
{
    private readonly List<StudentWithSubjectsDto> _data;

    public InterRegistrationPdf(List<StudentWithSubjectsDto> data)
    {
        _data = data;
    }

    public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

    public void Compose(IDocumentContainer container)
    {
        container.Page(page =>
        {
            page.Size(PageSizes.A4);
            page.Margin(20);
            page.DefaultTextStyle(x => x.FontSize(11));

            page.Header()
                .Text("INTER REGISTRATION FORM")
                .SemiBold().FontSize(16).AlignCenter();

            page.Content().Column(col =>
            {
                foreach (var item in _data)
                {
                    col.Item().Border(1).Padding(10).Column(c =>
                    {
                        // -------- STUDENT INFO --------
                        c.Item().Text($"Student Name : {item.Student.StudentName}");
                        c.Item().Text($"Father Name  : {item.Student.FatherName}");
                        c.Item().Text($"Mother Name  : {item.Student.MotherName}");
                        c.Item().Text($"DOB          : {item.Student.DOB}");
                        c.Item().Text($"Faculty      : {item.Student.FacultyName}");
                        c.Item().Text($"College      : {item.Student.CollegeName}");
                        c.Item().Text($"Mobile       : {item.Student.MobileNo}");

                        c.Item().PaddingVertical(5).Text("Subjects")
                            .SemiBold();

                        // -------- SUBJECT TABLE --------
                        c.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.ConstantColumn(40);
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                            });

                            table.Header(header =>
                            {
                                header.Cell().Text("Code").SemiBold();
                                header.Cell().Text("Subject").SemiBold();
                                header.Cell().Text("Group").SemiBold();
                            });

                            foreach (var sub in item.Subjects)
                            {
                                table.Cell().Text(sub.SubjectPaperCode?.ToString());
                                table.Cell().Text(sub.SubjectPaperName);
                                table.Cell().Text(sub.GroupName);
                            }
                        });
                    });

                    col.Item().PaddingBottom(10);
                }
            });

            page.Footer()
                .AlignCenter()
                .Text(x =>
                {
                    x.Span("Generated on ");
                    x.Span(DateTime.Now.ToString("dd-MM-yyyy"));
                });
        });
    }
}


//using BSEB_CoreAPI.Model;
//using QuestPDF.Fluent;
//using QuestPDF.Helpers;
//using QuestPDF.Infrastructure;

//public class InterRegistrationPdf : IDocument
//{
//    private readonly List<StudentInterRegiSpDto> _data;

//    public InterRegistrationPdf(List<StudentInterRegiSpDto> data)
//    {
//        _data = data;
//    }

//    public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

//    public void Compose(IDocumentContainer container)
//    {
//        container.Page(page =>
//        {
//            page.Size(PageSizes.A4);
//            page.Margin(20);
//            page.DefaultTextStyle(x => x.FontSize(11).FontFamily("Arial"));

//            page.Content().Column(col =>
//            {
//                foreach (var student in _data)
//                {
//                    // Header
//                    col.Item().Row(row =>
//                    {
//                        row.ConstantItem(100).Image("wwwroot/assets/img/bsebimage.jpg"); // logo
//                        row.RelativeItem().Column(c =>
//                        {
//                            c.Item().Text("बिहार विद्यालय परीक्षा समिति").SemiBold().AlignCenter();
//                            c.Item().Text("BIHAR SCHOOL EXAMINATION BOARD").AlignCenter();
//                            c.Item().Text("प्रायोगिक परीक्षा का प्रवेश-पत्र / Admit Card For Practical Examination").AlignCenter();
//                        });
//                        row.ConstantItem(100).Text($"FACULTY: {student.Student.FacultyName}");
//                    });

//                    col.Item().PaddingVertical(10);

//                    // Student Info Table
//                    col.Item().Table(table =>
//                    {
//                        table.ColumnsDefinition(columns =>
//                        {
//                            columns.RelativeColumn();
//                            columns.RelativeColumn();
//                        });

//                        // Left: Details
//                        table.Cell().Element(cell => cell.Border(1).Padding(5)).Column(c =>
//                        {
//                            c.Item().Text($"Unique Id: {student.Student.UniqueId}");
//                            c.Item().Text($"Student Name: {student.Student.StudentName}");
//                            c.Item().Text($"Father Name: {student.Student.FatherName}");
//                            c.Item().Text($"Mother Name: {student.Student.MotherName}");
//                        });

//                        // Right: Photo & Signature
//                        table.Cell().Element(cell => cell.Border(1).Padding(5)).Column(c =>
//                        {
//                            if (!string.IsNullOrEmpty(student.Student.StudentPhotoPath))
//                                c.Item().Image(student.Student.StudentPhotoPath, ImageScaling.FitWidth);
//                            if (!string.IsNullOrEmpty(student.Student.StudentSignaturePath))
//                                c.Item().PaddingTop(5).Image(student.Student.StudentSignaturePath, ImageScaling.FitWidth);
//                        });
//                    });

//                    col.Item().PaddingVertical(10);

//                    // Subjects Table
//                    col.Item().Text("प्रायोगिक परीक्षा के विषय").SemiBold();
//                    col.Item().Table(subTable =>
//                    {
//                        subTable.ColumnsDefinition(columns =>
//                        {
//                            columns.RelativeColumn();
//                            columns.RelativeColumn();
//                            columns.RelativeColumn();
//                        });

//                        subTable.Header(header =>
//                        {
//                            header.Cell().Text("Code").SemiBold();
//                            header.Cell().Text("Subject").SemiBold();
//                            header.Cell().Text("Group").SemiBold();
//                        });

//                        foreach (var sub in student.Subjects)
//                        {
//                            subTable.Cell().Element(cell => cell.Border(1).Padding(5))
//                                .Text(sub.SubjectPaperCode);
//                            subTable.Cell().Element(cell => cell.Border(1).Padding(5))
//                                .Text(sub.SubjectPaperName);
//                            subTable.Cell().Element(cell => cell.Border(1).Padding(5))
//                                .Text(sub.GroupName);
//                        }
//                    });

//                    col.Item().PaddingVertical(10);
//                }
//            });

//            page.Footer()
//                .AlignCenter()
//                .Text(x => x.Span($"Generated on {DateTime.Now:dd-MM-yyyy}"));
//        });
//    }
//}
