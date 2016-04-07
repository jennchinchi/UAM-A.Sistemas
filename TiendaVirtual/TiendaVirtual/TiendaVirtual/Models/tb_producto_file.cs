using System;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

namespace TiendaVirtual.Models
{
    public class tb_producto_file : tb_producto
    {
        public byte[] InternalImage { get; set; }
        [Display(Name = "Local file")]
        [NotMapped]
        public HttpPostedFileBase File
        {
            get
            {
                return null;
            }

            set
            {
                try
                {
                    MemoryStream target = new MemoryStream();

                    if (value.InputStream == null)
                        return;

                    value.InputStream.CopyTo(target);
                    imagen = target.ToArray();
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    Console.Write(ex.StackTrace);
                }
            }
        }

        [DisplayName("Item Picture URL")]
        [StringLength(1024)]
        public string ItemPictureUrl { get; set; }
    }
}