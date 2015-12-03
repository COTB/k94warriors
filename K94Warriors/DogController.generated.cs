// <auto-generated />
// This file was generated by a T4 template.
// Don't change it directly as your change would get overwritten.  Instead, make changes
// to the .tt file (i.e. the T4 template) and save it to regenerate this file.

// Make sure the compiler doesn't complain about missing Xml comments and CLS compliance
// 0108: suppress "Foo hides inherited member Foo. Use the new keyword if hiding was intended." when a controller and its abstract parent are both processed
// 0114: suppress "Foo.BarController.Baz()' hides inherited member 'Qux.BarController.Baz()'. To make the current member override that implementation, add the override keyword. Otherwise add the new keyword." when an action (with an argument) overrides an action in a parent controller
#pragma warning disable 1591, 3008, 3009, 0108, 0114
#region T4MVC

using System;
using System.Diagnostics;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Mvc.Html;
using System.Web.Routing;
using T4MVC;
namespace K94Warriors.Controllers
{
    public partial class DogController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected DogController(Dummy d) { }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToAction(ActionResult result)
        {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoute(callInfo.RouteValueDictionary);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToAction(Task<ActionResult> taskResult)
        {
            return RedirectToAction(taskResult.Result);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToActionPermanent(ActionResult result)
        {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoutePermanent(callInfo.RouteValueDictionary);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToActionPermanent(Task<ActionResult> taskResult)
        {
            return RedirectToActionPermanent(taskResult.Result);
        }

        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult DogProfile()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.DogProfile);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult Edit()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Edit);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult Delete()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Delete);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Threading.Tasks.Task<System.Web.Mvc.ActionResult> DogThumbnail()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.DogThumbnail);
            return System.Threading.Tasks.Task.FromResult(callInfo as ActionResult);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult DogImagesPartial()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.DogImagesPartial);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Threading.Tasks.Task<System.Web.Mvc.ActionResult> ImageForBlobKey()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ImageForBlobKey);
            return System.Threading.Tasks.Task.FromResult(callInfo as ActionResult);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Threading.Tasks.Task<System.Web.Mvc.ActionResult> GetImage()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.GetImage);
            return System.Threading.Tasks.Task.FromResult(callInfo as ActionResult);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult GetDogSection()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.GetDogSection);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult ImageKeysForDogProfile()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ImageKeysForDogProfile);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Threading.Tasks.Task<System.Web.Mvc.ActionResult> UploadDogImages()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.UploadDogImages);
            return System.Threading.Tasks.Task.FromResult(callInfo as ActionResult);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Threading.Tasks.Task<System.Web.Mvc.ActionResult> DeleteImage()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.DeleteImage);
            return System.Threading.Tasks.Task.FromResult(callInfo as ActionResult);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public DogController Actions { get { return MVC.Dog; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "Dog";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "Dog";

        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string Index = "Index";
            public readonly string DogProfile = "DogProfile";
            public readonly string Create = "Create";
            public readonly string Edit = "Edit";
            public readonly string Delete = "Delete";
            public readonly string DogThumbnail = "DogThumbnail";
            public readonly string DogImagesPartial = "DogImagesPartial";
            public readonly string ImageForBlobKey = "ImageForBlobKey";
            public readonly string GetImage = "GetImage";
            public readonly string GetDogSection = "GetDogSection";
            public readonly string ImageKeysForDogProfile = "ImageKeysForDogProfile";
            public readonly string UploadDogImages = "UploadDogImages";
            public readonly string DeleteImage = "DeleteImage";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string Index = "Index";
            public const string DogProfile = "DogProfile";
            public const string Create = "Create";
            public const string Edit = "Edit";
            public const string Delete = "Delete";
            public const string DogThumbnail = "DogThumbnail";
            public const string DogImagesPartial = "DogImagesPartial";
            public const string ImageForBlobKey = "ImageForBlobKey";
            public const string GetImage = "GetImage";
            public const string GetDogSection = "GetDogSection";
            public const string ImageKeysForDogProfile = "ImageKeysForDogProfile";
            public const string UploadDogImages = "UploadDogImages";
            public const string DeleteImage = "DeleteImage";
        }


        static readonly ActionParamsClass_DogProfile s_params_DogProfile = new ActionParamsClass_DogProfile();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_DogProfile DogProfileParams { get { return s_params_DogProfile; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_DogProfile
        {
            public readonly string id = "id";
        }
        static readonly ActionParamsClass_Create s_params_Create = new ActionParamsClass_Create();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Create CreateParams { get { return s_params_Create; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Create
        {
            public readonly string model = "model";
        }
        static readonly ActionParamsClass_Edit s_params_Edit = new ActionParamsClass_Edit();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Edit EditParams { get { return s_params_Edit; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Edit
        {
            public readonly string dogProfileId = "dogProfileId";
            public readonly string model = "model";
        }
        static readonly ActionParamsClass_Delete s_params_Delete = new ActionParamsClass_Delete();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Delete DeleteParams { get { return s_params_Delete; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Delete
        {
            public readonly string dogProfileId = "dogProfileId";
        }
        static readonly ActionParamsClass_DogThumbnail s_params_DogThumbnail = new ActionParamsClass_DogThumbnail();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_DogThumbnail DogThumbnailParams { get { return s_params_DogThumbnail; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_DogThumbnail
        {
            public readonly string dogId = "dogId";
            public readonly string size = "size";
        }
        static readonly ActionParamsClass_DogImagesPartial s_params_DogImagesPartial = new ActionParamsClass_DogImagesPartial();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_DogImagesPartial DogImagesPartialParams { get { return s_params_DogImagesPartial; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_DogImagesPartial
        {
            public readonly string id = "id";
        }
        static readonly ActionParamsClass_ImageForBlobKey s_params_ImageForBlobKey = new ActionParamsClass_ImageForBlobKey();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_ImageForBlobKey ImageForBlobKeyParams { get { return s_params_ImageForBlobKey; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_ImageForBlobKey
        {
            public readonly string blobKey = "blobKey";
            public readonly string mimeType = "mimeType";
            public readonly string height = "height";
            public readonly string width = "width";
        }
        static readonly ActionParamsClass_GetImage s_params_GetImage = new ActionParamsClass_GetImage();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_GetImage GetImageParams { get { return s_params_GetImage; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_GetImage
        {
            public readonly string id = "id";
        }
        static readonly ActionParamsClass_GetDogSection s_params_GetDogSection = new ActionParamsClass_GetDogSection();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_GetDogSection GetDogSectionParams { get { return s_params_GetDogSection; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_GetDogSection
        {
            public readonly string dogId = "dogId";
        }
        static readonly ActionParamsClass_ImageKeysForDogProfile s_params_ImageKeysForDogProfile = new ActionParamsClass_ImageKeysForDogProfile();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_ImageKeysForDogProfile ImageKeysForDogProfileParams { get { return s_params_ImageKeysForDogProfile; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_ImageKeysForDogProfile
        {
            public readonly string dogProfileId = "dogProfileId";
        }
        static readonly ActionParamsClass_UploadDogImages s_params_UploadDogImages = new ActionParamsClass_UploadDogImages();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_UploadDogImages UploadDogImagesParams { get { return s_params_UploadDogImages; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_UploadDogImages
        {
            public readonly string model = "model";
        }
        static readonly ActionParamsClass_DeleteImage s_params_DeleteImage = new ActionParamsClass_DeleteImage();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_DeleteImage DeleteImageParams { get { return s_params_DeleteImage; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_DeleteImage
        {
            public readonly string blobKey = "blobKey";
        }
        static readonly ViewsClass s_views = new ViewsClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ViewsClass Views { get { return s_views; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ViewsClass
        {
            static readonly _ViewNamesClass s_ViewNames = new _ViewNamesClass();
            public _ViewNamesClass ViewNames { get { return s_ViewNames; } }
            public class _ViewNamesClass
            {
                public readonly string _DogImagesPartial = "_DogImagesPartial";
                public readonly string _ImageThumbnailModal = "_ImageThumbnailModal";
                public readonly string Create = "Create";
                public readonly string DogProfile = "DogProfile";
                public readonly string Edit = "Edit";
                public readonly string Index = "Index";
            }
            public readonly string _DogImagesPartial = "~/Views/Dog/_DogImagesPartial.cshtml";
            public readonly string _ImageThumbnailModal = "~/Views/Dog/_ImageThumbnailModal.cshtml";
            public readonly string Create = "~/Views/Dog/Create.cshtml";
            public readonly string DogProfile = "~/Views/Dog/DogProfile.cshtml";
            public readonly string Edit = "~/Views/Dog/Edit.cshtml";
            public readonly string Index = "~/Views/Dog/Index.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_DogController : K94Warriors.Controllers.DogController
    {
        public T4MVC_DogController() : base(Dummy.Instance) { }

        [NonAction]
        partial void IndexOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult Index()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Index);
            IndexOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void DogProfileOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int id);

        [NonAction]
        public override System.Web.Mvc.ActionResult DogProfile(int id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.DogProfile);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            DogProfileOverride(callInfo, id);
            return callInfo;
        }

        [NonAction]
        partial void CreateOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult Create()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Create);
            CreateOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void CreateOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, K94Warriors.Models.DogProfile model);

        [NonAction]
        public override System.Web.Mvc.ActionResult Create(K94Warriors.Models.DogProfile model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Create);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            CreateOverride(callInfo, model);
            return callInfo;
        }

        [NonAction]
        partial void EditOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int dogProfileId);

        [NonAction]
        public override System.Web.Mvc.ActionResult Edit(int dogProfileId)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Edit);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "dogProfileId", dogProfileId);
            EditOverride(callInfo, dogProfileId);
            return callInfo;
        }

        [NonAction]
        partial void EditOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, K94Warriors.Models.DogProfile model);

        [NonAction]
        public override System.Web.Mvc.ActionResult Edit(K94Warriors.Models.DogProfile model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Edit);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            EditOverride(callInfo, model);
            return callInfo;
        }

        [NonAction]
        partial void DeleteOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int dogProfileId);

        [NonAction]
        public override System.Web.Mvc.ActionResult Delete(int dogProfileId)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Delete);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "dogProfileId", dogProfileId);
            DeleteOverride(callInfo, dogProfileId);
            return callInfo;
        }

        [NonAction]
        partial void DogThumbnailOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int dogId, int size);

        [NonAction]
        public override System.Threading.Tasks.Task<System.Web.Mvc.ActionResult> DogThumbnail(int dogId, int size)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.DogThumbnail);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "dogId", dogId);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "size", size);
            DogThumbnailOverride(callInfo, dogId, size);
            return System.Threading.Tasks.Task.FromResult(callInfo as ActionResult);
        }

        [NonAction]
        partial void DogImagesPartialOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int id);

        [NonAction]
        public override System.Web.Mvc.ActionResult DogImagesPartial(int id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.DogImagesPartial);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            DogImagesPartialOverride(callInfo, id);
            return callInfo;
        }

        [NonAction]
        partial void ImageForBlobKeyOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, string blobKey, string mimeType, int height, int width);

        [NonAction]
        public override System.Threading.Tasks.Task<System.Web.Mvc.ActionResult> ImageForBlobKey(string blobKey, string mimeType, int height, int width)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ImageForBlobKey);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "blobKey", blobKey);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "mimeType", mimeType);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "height", height);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "width", width);
            ImageForBlobKeyOverride(callInfo, blobKey, mimeType, height, width);
            return System.Threading.Tasks.Task.FromResult(callInfo as ActionResult);
        }

        [NonAction]
        partial void GetImageOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, string id);

        [NonAction]
        public override System.Threading.Tasks.Task<System.Web.Mvc.ActionResult> GetImage(string id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.GetImage);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            GetImageOverride(callInfo, id);
            return System.Threading.Tasks.Task.FromResult(callInfo as ActionResult);
        }

        [NonAction]
        partial void GetDogSectionOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int dogId);

        [NonAction]
        public override System.Web.Mvc.ActionResult GetDogSection(int dogId)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.GetDogSection);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "dogId", dogId);
            GetDogSectionOverride(callInfo, dogId);
            return callInfo;
        }

        [NonAction]
        partial void ImageKeysForDogProfileOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int dogProfileId);

        [NonAction]
        public override System.Web.Mvc.ActionResult ImageKeysForDogProfile(int dogProfileId)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ImageKeysForDogProfile);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "dogProfileId", dogProfileId);
            ImageKeysForDogProfileOverride(callInfo, dogProfileId);
            return callInfo;
        }

        [NonAction]
        partial void UploadDogImagesOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, K94Warriors.ViewModels.DogImageUploadViewModel model);

        [NonAction]
        public override System.Threading.Tasks.Task<System.Web.Mvc.ActionResult> UploadDogImages(K94Warriors.ViewModels.DogImageUploadViewModel model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.UploadDogImages);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            UploadDogImagesOverride(callInfo, model);
            return System.Threading.Tasks.Task.FromResult(callInfo as ActionResult);
        }

        [NonAction]
        partial void DeleteImageOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, string blobKey);

        [NonAction]
        public override System.Threading.Tasks.Task<System.Web.Mvc.ActionResult> DeleteImage(string blobKey)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.DeleteImage);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "blobKey", blobKey);
            DeleteImageOverride(callInfo, blobKey);
            return System.Threading.Tasks.Task.FromResult(callInfo as ActionResult);
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591, 3008, 3009, 0108, 0114
