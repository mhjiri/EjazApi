﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Books;
using Application.Books.Core;
using Application.Core;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using System.Diagnostics;

namespace Ejaz.Controllers
{

    
    public class BookController : BaseApiController
    {
        [AllowAnonymous]
        [HttpGet("getBooks")]
        public async Task<IActionResult> GetBooks([FromQuery] BookParams param)
        {
            return HandlePagedResult(await mdtr.Send(new List.Query { Params = param }));
        }

        [AllowAnonymous]
        [HttpGet("getBookList")]
        public async Task<IActionResult> GetBookList([FromQuery] BookParams param)
        {
            return HandlePagedResult(await mdtr.Send(new ListBooks.Query { Params = param }));
        }

        [AllowAnonymous]
        [HttpGet("getBook/{id}")]
        public async Task<IActionResult> GetBook(Guid id)
        {
            return HandleResult(await mdtr.Send(new Get.Query { Id = id }));
        }

        [AllowAnonymous]
        [HttpGet("getBookByAuthor/{id}")]
        public async Task<IActionResult> GetBookByAuthor(Guid id, [FromQuery] BookParams param)
        {
            return HandlePagedResult(await mdtr.Send(new ListByAuthor.Query { Id = id, Params = param }));
        }

        [AllowAnonymous]
        [HttpGet("getBookByCategory/{id}")]
        public async Task<IActionResult> GetBookByCategory(Guid id, [FromQuery] BookParams param)
        {
            return HandlePagedResult(await mdtr.Send(new ListByCategory.Query { Id = id, Params = param }));
        }

        [AllowAnonymous]
        [HttpGet("getBookByGenre/{id}")]
        public async Task<IActionResult> GetBookByGenre(Guid id, [FromQuery] BookParams param)
        {
            return HandlePagedResult(await mdtr.Send(new ListByGenre.Query { Id = id, Params = param }));
        }

        [AllowAnonymous]
        [HttpGet("getBookByPublisher/{id}")]
        public async Task<IActionResult> GetBookByPublisher(Guid id, [FromQuery] BookParams param)
        {
            return HandlePagedResult(await mdtr.Send(new ListByPublisher.Query { Id = id, Params = param }));
        }

        [AllowAnonymous]
        [HttpGet("getBookByTag/{id}")]
        public async Task<IActionResult> GetBookByTag(Guid id, [FromQuery] BookParams param)
        {
            return HandlePagedResult(await mdtr.Send(new ListByTag.Query { Id = id, Params = param }));
        }

        [AllowAnonymous]
        [HttpGet("getBookByThematicArea/{id}")]
        public async Task<IActionResult> GetBookByThematicArea(Guid id, [FromQuery] BookParams param)
        {
            return HandlePagedResult(await mdtr.Send(new ListByThematicArea.Query { Id = id, Params = param }));
        }

        [AllowAnonymous]
        [HttpGet("getCmdBook/{id}")]
        public async Task<IActionResult> GetCmdBook(Guid id)
        {
            return HandleResult(await mdtr.Send(new GetCmd.Query { Id = id }));
        }

        [AllowAnonymous]
        [HttpPost("createBook")]
        public async Task<IActionResult> CreateBook(BookCmdDto book)
        {
            return HandleResult(await mdtr.Send(new Create.Command { Book = book }));
        }

        [AllowAnonymous]
        [HttpPut("updateBook/{id}")]
        public async Task<IActionResult> UpdateBook(Guid id, BookCmdDto book)
        {
            book.Bk_ID = id;
            return HandleResult(await mdtr.Send(new Update.Command { Book = book }));
        }

        [AllowAnonymous]
        [HttpPut("activateBook/{id}")]
        public async Task<IActionResult> ActivateBook(Guid id)
        {
            
            return HandleResult(await mdtr.Send(new Activate.Command { Id = id }));
        }

        [AllowAnonymous]
        [HttpPut("activateBooks")]
        public async Task<IActionResult> ActivateBatchBook(ListGuidDto ids)
        {

            return HandleResult(await mdtr.Send(new ActivateBatch.Command { Ids = ids }));
        }

        [AllowAnonymous]
        [HttpPut("deactivateBook/{id}")]
        public async Task<IActionResult> DeactivateBook(Guid id)
        {

            return HandleResult(await mdtr.Send(new Deactivate.Command { Id = id }));
        }

        [AllowAnonymous]
        [HttpPut("deactivateBooks")]
        public async Task<IActionResult> DeactivateBatchBook(ListGuidDto ids)
        {

            return HandleResult(await mdtr.Send(new DeactivateBatch.Command { Ids = ids }));
        }



    }
}

