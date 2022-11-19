using KnowledgeSpace.ViewModels.Contents;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KnowledgeSpace.WebPortal.Models
{
    public class KnowledgeBaseEditModel
    {
        public KnowledgeBaseVm Detail { get; set; }
        public string CaptchaCode { get; set; }
        public List<IFormFile> Attachments { get; set; }
        public List<int> AttachmentID { get; set; }
}
}
