
using AppStore.Repositories.Abstract;

namespace AppStore.Repositories.Implementation
{
    public class FileService : IFileService
    {
        //para poder acceder a la carpeta Uploads
        private readonly IWebHostEnvironment webHostEnvironment;

        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
        }

        public bool DeleteImage(string imageFileName)
        {
            try
            {
                var wwwPath = this.webHostEnvironment.WebRootPath;
                var path = Path.Combine(wwwPath,"Upload\\",imageFileName);
                if(System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                    return true;
                }
                else{
                    return false;
                }
            }
            catch (Exception)
            {
                
                return false;
            }
        }

        public Tuple<int, string> SaveImage(IFormFile imageFile)
        {
            try{
                var wwwPath=this.webHostEnvironment.WebRootPath;
                var path=Path.Combine(wwwPath,"Upload");
                if(!Directory.Exists(path)){
                    Directory.CreateDirectory(path);
                }
                var ext = Path.GetExtension(imageFile.FileName);
                var allowedExtensions=new String[]{".jpg",".png","jpeg"};
                if(!allowedExtensions.Contains(ext))
                {
                    var message =$"Solo estan permitidas las extenciones {allowedExtensions}";
                    return new Tuple<int, string>(0,message);
                }
                string uniqueString = Guid.NewGuid().ToString();
                var newFilename = uniqueString+ext;
                var fileWhitPath=Path.Combine(path,newFilename);

                var stream = new FileStream(fileWhitPath,FileMode.Create);
                imageFile.CopyTo(stream);
                stream.Close();
                return new Tuple<int, string>(1,newFilename);

            }catch(Exception ){
                return new Tuple<int,string> (0, "Error guardando la imagen");
            }
        }
    }
}