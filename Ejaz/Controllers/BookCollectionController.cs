using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.BookCollections;
using Application.BookCollections.Core;
using Application.Core;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using System.Diagnostics;
using Application.Books.Core;

namespace Ejaz.Controllers
{

    
    public class BookCollectionController : BaseApiController
    {
        [AllowAnonymous]
        [HttpGet("getBookCollections")]
        public async Task<IActionResult> GetBookCollections([FromQuery] BookCollectionParams param)
        {
            return HandlePagedResult(await mdtr.Send(new List.Query { Params = param }));
        }

        [AllowAnonymous]
        [HttpGet("getBookCollectionsByAuthor/{id}")]
        public async Task<IActionResult> GetBookCollectionsByAuthor(Guid id, [FromQuery] BookCollectionParams param)
        {
            return HandlePagedResult(await mdtr.Send(new ListByAuthor.Query { Id = id, Params = param }));
        }

        [AllowAnonymous]
        [HttpGet("getBookCollection/{id}")]
        public async Task<IActionResult> GetBookCollection(Guid id)
        {
            return HandleResult(await mdtr.Send(new Get.Query { Id = id }));
        }

        [AllowAnonymous]
        [HttpGet("getCmdBookCollection/{id}")]
        public async Task<IActionResult> GetCmdBookCollection(Guid id)
        {
            return HandleResult(await mdtr.Send(new GetCmd.Query { Id = id }));
        }

        [AllowAnonymous]
        [HttpPost("createBookCollection")]
        public async Task<IActionResult> CreateBookCollection(BookCollectionCmdDto bookCollection)
        {
            return HandleResult(await mdtr.Send(new Create.Command { BookCollection = bookCollection }));
        }

        [AllowAnonymous]
        [HttpPut("updateBookCollection/{id}")]
        public async Task<IActionResult> UpdateBookCollection(Guid id, BookCollectionCmdDto bookCollection)
        {
            bookCollection.Bc_ID = id;
            return HandleResult(await mdtr.Send(new Update.Command { BookCollection = bookCollection }));
        }

        [AllowAnonymous]
        [HttpPut("activateBookCollection/{id}")]
        public async Task<IActionResult> ActivateBookCollection(Guid id)
        {
            
            return HandleResult(await mdtr.Send(new Activate.Command { Id = id }));
        }

        [AllowAnonymous]
        [HttpPut("activateBookCollections")]
        public async Task<IActionResult> ActivateBookCollections(ListGuidDto ids)
        {

            return HandleResult(await mdtr.Send(new ActivateBatch.Command { Ids = ids }));
        }

        [AllowAnonymous]
        [HttpPut("deactivateBookCollection/{id}")]
        public async Task<IActionResult> DeactivateBookCollection(Guid id)
        {

            return HandleResult(await mdtr.Send(new Deactivate.Command { Id = id }));
        }

        [AllowAnonymous]
        [HttpPut("deactivateBookCollections")]
        public async Task<IActionResult> DeactivateBatchBookCollection(ListGuidDto ids)
        {

            return HandleResult(await mdtr.Send(new DeactivateBatch.Command { Ids = ids }));
        }



    }
}

