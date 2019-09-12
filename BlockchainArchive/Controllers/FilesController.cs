using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BlockchainArchive.Data;
using BlockchainArchive.Models;
using Microsoft.AspNetCore.Http;
using BlockchainArchive.Logic;
using Microsoft.AspNetCore.Authorization;

namespace BlockchainArchive.Controllers
{
    [Authorize]
    public class FilesController : Controller
    {
        private readonly IFilesManagementLogic _filesManagementLogic;
        private readonly IOwnersManagementLogic _ownersManagementLogic;

        public FilesController(IFilesManagementLogic filesManagementLogic, IOwnersManagementLogic ownersManagementLogic)
        {
            _filesManagementLogic = filesManagementLogic;
            _ownersManagementLogic = ownersManagementLogic;
        }

        // GET: Files
        public async Task<IActionResult> Index()
        {
            var files = await _filesManagementLogic.GetFilesWithHistoryAsync();
            var model = new List<SavedFileViewModel>();

            foreach(var file in files)
            {
                model.Add(new SavedFileViewModel(file));
            };

            return View(model);
        }

        // GET: Files/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || !id.HasValue)
            {
                return BadRequest();
            }

            var file = await _filesManagementLogic.GetFileWithHistoryAsync(id.Value);
            if (file == null)
            {
                return NotFound();
            }

            return View(file);
        }

        // GET: Files/Upload
        public IActionResult Upload()
        {
            var owners = _ownersManagementLogic.GetOwnersAsync().GetAwaiter().GetResult();

            var selectItems = new List<SelectListItem>();

            foreach(var owner in owners)
            {
                selectItems.Add(new SelectListItem($"{owner.FirstName} {owner.LastName}", owner.Id.ToString()));
            }

            ViewBag.Owners = selectItems;
            return View();
        }

        // POST: Files/Upload
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(UploadFileViewModel uploadedFile)
        {
            if (uploadedFile == null || uploadedFile.FormFile == null || uploadedFile.FormFile.Length == 0)
                return BadRequest();

            var isSuccess = await _filesManagementLogic.SaveUploadedFile(uploadedFile);

            if (isSuccess)
                return RedirectToAction(nameof(Index));
            else
                return new StatusCodeResult(StatusCodes.Status417ExpectationFailed);
        }

        public async Task<IActionResult> Verify(Guid? id)
        {
            if (!id.HasValue)
                return BadRequest();

            var isSuccess = await _filesManagementLogic.VerifyUploadedFile(id.Value);

            if (isSuccess)
                return RedirectToAction(nameof(Index));
            else
                return new StatusCodeResult(StatusCodes.Status417ExpectationFailed);
        }

        // GET: Files/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || !id.HasValue)
            {
                return BadRequest();
            }

            var file = await _filesManagementLogic.GetFileWithHistoryAsync(id.Value);
            if (file == null)
            {
                return NotFound();
            }

            return View(new SavedFileViewModel(file));
        }

        // POST: Files/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _filesManagementLogic.DeleteFileAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
