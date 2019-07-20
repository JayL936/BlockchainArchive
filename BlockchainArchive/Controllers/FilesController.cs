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

        public FilesController(IFilesManagementLogic filesManagementLogic)
        {
            _filesManagementLogic = filesManagementLogic;
        }

        // GET: Files
        public async Task<IActionResult> Index()
        {
            return View(await _filesManagementLogic.GetFilesAsync());
        }

        // GET: Files/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || !id.HasValue)
            {
                return BadRequest();
            }

            var file = await _filesManagementLogic.GetFileAsync(id.Value);
            if (file == null)
            {
                return NotFound();
            }

            return View(file);
        }

        // GET: Files/Upload
        public IActionResult Upload()
        {
            return View();
        }

        // POST: Files/Upload
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest();

            await _filesManagementLogic.SaveUploadedFile(file);

            return RedirectToAction(nameof(Index));
        }

        // GET: Files/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || !id.HasValue)
            {
                return BadRequest();
            }

            var file = await _filesManagementLogic.GetFileAsync(id.Value);
            if (file == null)
            {
                return NotFound();
            }
            return View(file);
        }

        // POST: Files/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Guid,Name,StorageUrl,BlockReference")] File file)
        {
            if (id != file.Guid)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var result = await _filesManagementLogic.UpdateFileAsync(file);

                if (result == StatusCodes.Status200OK)
                    return RedirectToAction(nameof(Index));
                else
                    return new StatusCodeResult(result);
            }

            return View(file);
        }

        // GET: Files/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || !id.HasValue)
            {
                return BadRequest();
            }

            var file = await _filesManagementLogic.GetFileAsync(id.Value);
            if (file == null)
            {
                return NotFound();
            }

            return View(file);
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
