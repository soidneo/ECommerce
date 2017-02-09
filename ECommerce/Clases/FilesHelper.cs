using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ECommerce.Clases
{
    public class FilesHelper
    {
        public static string SubirImagen(HttpPostedFileBase file, string folder)
        {
            string path = string.Empty;
            string pic = string.Empty;

            if (file!=null)
            {
                pic = Path.GetFileName(file.FileName);
                path = Path.Combine(HttpContext.Current.Server.MapPath(folder), pic);
                file.SaveAs(path);
                using (MemoryStream ms = new MemoryStream())
                {
                    file.InputStream.CopyTo(ms);
                    byte[] array = ms.GetBuffer();
                }
            }
            return pic;
        }
    }
}
