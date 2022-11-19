using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using KnowledgeSpace.ViewModels.Contents;
using KnowledgeSpace.WebPortal.Extensions;
using KnowledgeSpace.WebPortal.Helpers;
using KnowledgeSpace.WebPortal.Models;
using KnowledgeSpace.WebPortal.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace KnowledgeSpace.WebPortal.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserApiClient _userApiClient;
        private readonly IKnowledgeBaseApiClient _knowledgeBaseApiClient;
        private readonly ICategoryApiClient _categoryApiClient;

        public AccountController(IUserApiClient userApiClient,
            IKnowledgeBaseApiClient knowledgeBaseApiClient,
            ICategoryApiClient categoryApiClient)
        {
            _userApiClient = userApiClient;
            _categoryApiClient = categoryApiClient;
            _knowledgeBaseApiClient = knowledgeBaseApiClient;
        }

        public IActionResult SignIn()
        {
            return Challenge(new AuthenticationProperties { RedirectUri = "/" }, "oidc");
        }

        public IActionResult SignOut()
        {
            return SignOut(new AuthenticationProperties { RedirectUri = "/" }, "Cookies", "oidc");
        }

        [Authorize]
        public async Task<ActionResult> MyProfile()
        {
            var user = await _userApiClient.GetById(User.GetUserId());
            return View(user);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> MyKnowledgeBases(int page = 1, int pageSize = 10)
        {
            var kbs = await _userApiClient.GetKnowledgeBasesByUserId(User.GetUserId(), page, pageSize);
            return View(kbs);
        }

        [HttpGet]
        public async Task<IActionResult> CreateNewKnowledgeBase()
        {
            await SetCategoriesViewBag();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewKnowledgeBase([FromForm]KnowledgeBaseCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (request.Title == null)
            {
                ModelState.AddModelError("", "Tiêu đề không được bỏ trống");
                return BadRequest(ModelState);
            }
            if (request.Description == null)
            {
                ModelState.AddModelError("", "Mô tả không được bỏ trống");
                return BadRequest(ModelState);
            }
           
            if (request.Problem == null)
            {
                ModelState.AddModelError("", "Vấn đề không được bỏ trống");
                return BadRequest(ModelState);
            }
           
            if (!Captcha.ValidateCaptchaCode(request.CaptchaCode, HttpContext))
            {
                ModelState.AddModelError("", "Mã xác nhận không đúng");
                return BadRequest(ModelState);
            }

            var result = await _knowledgeBaseApiClient.PostKnowlegdeBase(request);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> EditKnowledgeBase(int id)
        {
            var knowledgeBase = await _knowledgeBaseApiClient.GetKnowledgeBaseDetail(id);
            await SetCategoriesViewBag();
            var kb = new KnowledgeBaseEditModel()
            {
                Detail = knowledgeBase
            };
        
            return View(kb);

        }
        [HttpGet]
        public async Task<IActionResult> DeleteAttachment (int knowledgeBaseId , int attachmentId)
        {
            var result = await _knowledgeBaseApiClient.deleteAttachment(knowledgeBaseId, attachmentId);
            if (result)
            {
                var knowledgeBase = await _knowledgeBaseApiClient.GetKnowledgeBaseDetail(knowledgeBaseId);
                await SetCategoriesViewBag();
                var kb = new KnowledgeBaseEditModel()
                {
                    Detail = knowledgeBase
                };

                return View("EditKnowledgeBase", kb);
            }
            return BadRequest();

        }

        [HttpPost]
        public async Task<IActionResult> EditKnowledgeBase([FromForm] KnowledgeBaseEditModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!Captcha.ValidateCaptchaCode(request.CaptchaCode, HttpContext))
            {
                ModelState.AddModelError("", "Mã xác nhận không đúng");
                return BadRequest(ModelState);
            }
            if (request.Detail.Title == null)
            {
                ModelState.AddModelError("", "Tiêu đề không được bỏ trống");
                return BadRequest(ModelState);
            }
            if (request.Detail.Description == null)
            {
                ModelState.AddModelError("", "Mô tả không được bỏ trống");
                return BadRequest(ModelState);
            }

            if (request.Detail.Problem == null)
            {
                ModelState.AddModelError("", "Vấn đề không được bỏ trống");
                return BadRequest(ModelState);
            }
            var KB= new KnowledgeBaseCreateRequest()
            {
                CategoryId = request.Detail.CategoryId,
                Description = request.Detail.Description,
                Environment = request.Detail.Environment,
                ErrorMessage = request.Detail.ErrorMessage,
                Labels = request.Detail.Labels,
                Note = request.Detail.Note,
                Problem = request.Detail.Problem,
                StepToReproduce = request.Detail.StepToReproduce,
                Title = request.Detail.Title,
                Workaround = request.Detail.Workaround,
                Id = request.Detail.Id,
                Attachments = request.Attachments

            };

            var result = await _knowledgeBaseApiClient.PutKnowlegdeBase(request.Detail.Id, KB);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }

        private async Task SetCategoriesViewBag(int? selectedValue = null)
        {
            var categories = await _categoryApiClient.GetCategories();

            var items = categories.Select(i => new SelectListItem()
            {
                Text = i.Name,
                Value = i.Id.ToString(),
            }).ToList();

            items.Insert(0, new SelectListItem()
            {
                Value = null,
                Text = "--Chọn danh mục--"
            });
            ViewBag.Categories = new SelectList(items, "Value", "Text", selectedValue);
        }
    }
}