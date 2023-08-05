namespace HotelAdmin.Helpers
{
    static public class FileUploadHelper
    {
        static public async Task<string> Upload(IFormFile file)
        {
            if (file != null)
            {
                //var filename = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                //using var fs = new FileStream(@$"D:/Study-11.02.2023/Study/Web курсовой проект/Проекты/CourseProjectWeb/HotelWebsiteBooking/HotelWebsiteBooking/wwwroot/uploads/{filename}", FileMode.Create);
                using var fs = new FileStream(@$"../HotelWebsiteBooking/wwwroot/img/room/{file.FileName}", FileMode.Create);
                await file.CopyToAsync(fs);
                return @$"img/room/{file.FileName}";
            }

            throw new Exception("File was not uploaded!");
        }
    }
}
