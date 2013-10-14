using System.Collections.Generic;
using System.Web;

namespace K94Warriors.ViewModels
{
    public class DogImageUploadViewModel
    {
        public int DogProfileId { get; set; }

        public IList<HttpPostedFileBase> Files { get; set; }
    }
}