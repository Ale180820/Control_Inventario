using Amazon.S3;
using Amazon.S3.Transfer;
using Amazon.S3.Model;
using System.Configuration;
using Amazon;
using ControlInventarioModel;
using Microsoft.AspNetCore.Mvc;

namespace Control_Inventario.Controllers.Registro_de_inventario
{
    public class ProductoController : Controller
    {
        // GET: ProductoController
        public async Task<IActionResult> Index()
        {
            var path = "Producto/GetList";
            IEnumerable<Producto> productos = await Functions.APIServices<IEnumerable<Producto>>.Get(path);
            var rol = HttpContext.Session.GetString("Rol");
            ViewBag.Rol = rol;
            return View(productos);
        }

        // GET: ProductoController/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Producto producto, FormFile fileImage)
        {
            if (fileImage != null)
            {
                string fileName = Path.GetFileName(fileImage.Name);
            }
            var path = "Producto/Set";
            UploadFile(producto.Foto);
            Producto movies = await Functions.APIServices<Producto>.Post(producto, path);
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Edit(int? id)
        {
            var path = "Producto/Get/" + id;
            Producto producto = await Functions.APIServices<Producto>.Get(path);
            return View(producto);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Producto producto)
        {
            if(id != producto.Id)
            {
                return NotFound();
            }

            var path = "Producto/Put/" + id;
            Producto result = await Functions.APIServices<Producto>.Post(producto, path);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            var path = "Producto/Get/" + id;
            Producto producto = await Functions.APIServices<Producto>.Get(path);
            return View(producto);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var path = "Producto/Delete/" + id;
            var result = await Functions.APIServices<GeneralResult>.Delete(path);
            return RedirectToAction(nameof(Index));
        }
        
        public void UploadFile(string path)
        {
            var accesskey = "AKIATIXWESGR4Q4KB74C";//provide your details
            var secretkey = "tNAv8aojfHYI/2QQaYkdDLuTx8nsjvx3xBIHjI2J";
            RegionEndpoint bucketRegion = RegionEndpoint.USEast1;
            var bucketName = "inventarioprogramacionweb";

            var s3Client = new AmazonS3Client(accesskey, secretkey, bucketRegion);

            var fileTransferUtility = new TransferUtility(s3Client);//create an object for TransferUtility

            string filePath = path;
            var id = Guid.NewGuid().ToString();
            //FileStream fs = File.Open(filePath, FileMode.Open);

            try
            {
                //creating object for    TransferUtilityUploadRequest
                var fileTransferUtilityRequest = new TransferUtilityUploadRequest
                {
                    BucketName = bucketName,
                    FilePath = filePath,
                    StorageClass = S3StorageClass.StandardInfrequentAccess,
                    PartSize = 6291456, // 6 MB.  
                    Key = id,//filename which u want to save in bucket
                    CannedACL = S3CannedACL.PublicRead,
                    //InputStream = fs,
                };
                fileTransferUtility.UploadAsync(fileTransferUtilityRequest).GetAwaiter().GetResult();

                //To upload without asynchronous
                //fileTransferUtility.Upload(filePath, bucketName, "SampleAudio.wav");
                //fileTransferUtility.Dispose();

                Console.WriteLine("File Uploaded Successfully!!");
            }
            catch (AmazonS3Exception amazonS3Exception)
            {
                if (amazonS3Exception.ErrorCode != null &&
                    (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId")
                    ||
                    amazonS3Exception.ErrorCode.Equals("InvalidSecurity")))
                {
                    Console.WriteLine("Check the AWS Credentials.");
                }
                else
                {
                    Console.WriteLine("Error occurred: " + amazonS3Exception.Message);
                }
            }
        }
    }
}
